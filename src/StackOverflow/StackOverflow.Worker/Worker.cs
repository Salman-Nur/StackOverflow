
using Serilog;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Application.Utilities;

namespace StackOverflow.EmailWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailService _emailService;
        private readonly IQueueService _queueService;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IEmailService emailService, 
            IQueueService queueService, IConfiguration configuration)
        {
            _logger = logger;
            _emailService = emailService;
            _queueService = queueService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               try
                {
                    string queueUrl = _configuration["SQSConfig:QueueUrl"];
                    List<MessageInfo> emails = await _queueService.ReadMessagesFromQueue(queueUrl, 5);
                    if (emails.Count != 0)
                    {
                        foreach (var email in emails)
                        {
                            await _emailService.SendSingleEmail(email.ReceiverName, email.ReceiverEmail,
                                                                    email.Subject, email.Body);
                        }
                        _logger.LogInformation($"Email sent");
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}