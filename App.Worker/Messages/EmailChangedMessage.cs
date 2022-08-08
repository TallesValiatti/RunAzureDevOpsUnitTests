#nullable disable
namespace App.Worker.Messages
{
    public class EmailChangedMessage
    {
        public Guid UserId { get; set; }
        public string newEmail { get; set; }
    }
}