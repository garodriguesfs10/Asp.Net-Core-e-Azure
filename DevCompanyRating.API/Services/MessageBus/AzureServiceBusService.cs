using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DevCompanyRating.API.Services.MessageBus
{
    public class AzureServiceBusService : IMessageBusService
    {
        private readonly string _connectionString;
        public AzureServiceBusService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ServiceBusCs");
        }

        public async Task Publish(string queue, byte[] content)
        {
            var queueClient = new QueueClient(_connectionString, queue);

            var message = new Message(content);

            await queueClient.SendAsync(message);
        }
    }
}
