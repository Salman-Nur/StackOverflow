using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Utilities
{
    public interface IEmailService
    {
        Task SendSingleEmail(string receiverName, string receiverEmail,
            string subject, string body);
    }
}