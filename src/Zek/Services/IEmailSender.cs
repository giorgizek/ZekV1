using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Zek.Model.Config;
using Zek.Model.ViewModel.Email;

namespace Zek.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailAsync(EmailViewModel model);
    }

    public class EmailSenderBase : IEmailSender
    {
        protected readonly EmailSenderOptions Options;

        public EmailSenderBase(EmailSenderOptions options)
        {
            if (Options == null)
                throw new ArgumentNullException(nameof(options));
            Options = options;
        }

        public EmailSenderBase(IOptions<EmailSenderOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
                throw new ArgumentNullException(nameof(optionsAccessor));
            Options = optionsAccessor.Value;
        }

        public virtual Task SendEmailAsync(EmailViewModel model)
        {
            throw new NotImplementedException();
        }

        public virtual async Task SendEmailAsync(string email, string subject, string message)
        {
            var model = new EmailViewModel
            {
                Subject = subject,
                Body = message,
                To = new List<EmailAddressViewModel>
                {
                    new EmailAddressViewModel {Address = email}
                },
            };
            await SendEmailAsync(model);
        }
    }
}