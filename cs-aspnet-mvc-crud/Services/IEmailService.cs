using System.Collections.Generic;

namespace Infra.EmailServices
{
    interface IEmailService
    {
        void Send(string recipient, string subject, string body);
        void Send(List<string> recipients, string subject, string body);
    }
}
