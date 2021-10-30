using DevCompanyRating.API.Domain;
using DevCompanyRating.API.Repositories;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevCompanyRating.API.Consumers
{
    public class AddRatingConsumer : IAddRatingConsumer
    {
        private readonly QueueClient _queueClient;
        private readonly IServiceProvider _serviceProvider;
        public AddRatingConsumer(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            
            _serviceProvider = serviceProvider;

            var connectionString = configuration.GetConnectionString("ServiceBusCs");

            _queueClient = new QueueClient(connectionString, "new-rating");
        }

        public void RegisterHandler()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionHandler)
            {
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessage, messageHandlerOptions);
        }

        public async Task ProcessMessage(Message message, CancellationToken cancellationToken)
        {
            var messageString = Encoding.UTF8.GetString(message.Body);
            var companyRating = JsonConvert.DeserializeObject<CompanyRating>(messageString);

            using (var scope = _serviceProvider.CreateScope())
            {
                var companyRepository = scope.ServiceProvider.GetRequiredService<ICompanyRepository>();

                companyRepository.AddRating(companyRating);

                await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
            }
        }
        
        public Task ExceptionHandler(ExceptionReceivedEventArgs args)
        {
            return Task.CompletedTask;
        }


    }
}
