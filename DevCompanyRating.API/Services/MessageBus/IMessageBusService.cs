using System.Threading.Tasks;

namespace DevCompanyRating.API.Services.MessageBus
{
    public interface IMessageBusService
    {
        Task Publish(string queue, byte[] content);
    }
}
