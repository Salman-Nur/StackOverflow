
using StackOverflow.Application.Utilities;

namespace StackOverflow.Application.Features.Training.Services
{
    public interface IQueueService
    {
        Task SendMessageToQueue(string receiverName, string receiverEmail,
            string subject, string body, string QueueURL);
        Task<List<MessageInfo>> ReadMessagesFromQueue(string QueueURL, int numMessagesToRead);
    }
}
