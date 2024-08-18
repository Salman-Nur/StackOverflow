
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using StackOverflow.Application.Utilities;
using Microsoft.Extensions.Logging;


namespace StackOverflow.Application.Features.Training.Services
{
    public class QueueService:IQueueService
    {
        private readonly ILogger<QueueService> _logger;

        public QueueService(ILogger<QueueService> logger)
        {
            _logger = logger;
        }

        public async Task SendMessageToQueue(string receiverName, string receiverEmail,
            string subject, string body, string QueueURL)
        {
            var queueUrl = QueueURL;
            try
            {
                var config = new AmazonSQSConfig
                {
                    RegionEndpoint = Amazon.RegionEndpoint.USEast1
                };

                using (var client = new AmazonSQSClient(config))
                {
                    var messageBody = new
                    {
                        ReceiverName = receiverName,
                        ReceiverEmail = receiverEmail,
                        Subject = subject,
                        Body = body
                    };

                    var request = new SendMessageRequest
                    {
                        QueueUrl = queueUrl,
                        MessageBody = JsonConvert.SerializeObject(messageBody)
                    };

                    await client.SendMessageAsync(request);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message to SQS");
            }
        }


        public async Task<List<MessageInfo>> ReadMessagesFromQueue(string QueueURL, int numMessagesToRead)
        {
            var queueUrl = QueueURL;
            try
            {
                var config = new AmazonSQSConfig
                {
                    RegionEndpoint = Amazon.RegionEndpoint.USEast1
                };

                using (var client = new AmazonSQSClient(config))
                {
                    var request = new ReceiveMessageRequest
                    {
                        QueueUrl = queueUrl,
                        MaxNumberOfMessages = numMessagesToRead
                    };

                    var response = await client.ReceiveMessageAsync(request);

                    if (response.Messages.Count > 0)
                    {
                        List<MessageInfo> messages = new List<MessageInfo>();
                        foreach (var message in response.Messages)
                        {
                            var messageBody = JsonConvert.DeserializeObject<MessageInfo>(message.Body);

                            var messageInfo = new MessageInfo
                            {
                                ReceiverName = messageBody.ReceiverName,
                                ReceiverEmail = messageBody.ReceiverEmail,
                                Subject = messageBody.Subject,
                                Body = messageBody.Body
                            };

                            messages.Add(messageInfo);
                            await client.DeleteMessageAsync(queueUrl, message.ReceiptHandle);
                        }
                        return messages;
                    }
                    else
                    {
                        return new List<MessageInfo>();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading messages from SQS");
                return new List<MessageInfo>();
            }
        }
    }
}
