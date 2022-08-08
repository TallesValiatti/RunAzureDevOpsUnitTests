using App.Worker.Messages;

namespace App.Worker.Services
{
    public interface IServiceBus
    {
        Task<EmailChangedMessage> GetLastEmailChangedMessageAsync();
    }
}