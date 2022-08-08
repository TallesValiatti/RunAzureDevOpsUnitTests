using App.Worker.Messages;

namespace App.Worker.Services
{
    public class ServiceBus : IServiceBus
    {
        public async Task<EmailChangedMessage> GetLastEmailChangedMessageAsync()
        {
            // We are mocking the last message from the service bus
            // Could be an Azure Service Bus, a RabbitMQ, etc ...
            return await Task.FromResult(new EmailChangedMessage
            {
                UserId = Guid.Parse("b0d5eaf1-b457-44f7-ac85-d3913c5c5a55"),
                newEmail = "myNewEmail@outlook.com"
            });
        }
    }
}