using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IEmailAccountService _emailAccountService;

        public MailService(IEmailAccountService emailAccountService)
        {
            _emailAccountService = emailAccountService;
        }

        public async Task<Tuple<bool, string>> SendResetPasswordMailAsync(string Email)
        {
            EmailAccount emailAccount = await _emailAccountService.GetBySystemName("ResetPassword");
            if (emailAccount != null)
            {
                Tuple<bool, string> result = await SendMailAsync(
                    emailAccount.FromAddress,
                    emailAccount.DisplayName,
                    emailAccount.SmtpUsername,
                    emailAccount.SmtpAddress,
                    emailAccount.SmtpPort,
                    emailAccount.SmtpPassword,
                    Email,
                    emailAccount.Subject,
                    emailAccount.MessageFormat,
                    emailAccount.SSLTLS);

                return result;
            }

            return Tuple.Create(false, "");
        }

        private async Task<Tuple<bool, string>> SendMailAsync(string FromEmailAddress, string DisplayName, string SmtpUsername, string SmtpAddress, int SmtpPort, string SmtpPassword, string ToEmailAddress, string Subject, string Message, bool EnableSslTls)
        {

            return Tuple.Create(false, "");
        }

    }
}
