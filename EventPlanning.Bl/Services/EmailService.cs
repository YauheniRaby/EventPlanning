using EventPlanning.Bl.Configuration;
using EventPlanning.Bl.Services.Abstract;
using EventPlanning.DA.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System;

namespace EventPlanning.Bl.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptionsMonitor<MessageTemplates> _messageTemplates;
        private readonly IOptionsMonitor<MailSettings> _mailSettings;
        private readonly IOptionsMonitor<AppConfiguration> _appConfiguration;

        public EmailService(IOptionsMonitor<MessageTemplates> messageTemplates, IOptionsMonitor<MailSettings> mailSettings, IOptionsMonitor<AppConfiguration> appConfiguration)
        {
            _messageTemplates = messageTemplates;
            _mailSettings = mailSettings;
            _appConfiguration = appConfiguration;
        }
        
        public async Task SendCodeAsync(User user)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_mailSettings.CurrentValue.DisplayName, _mailSettings.CurrentValue.Mail));
            emailMessage.To.Add(new MailboxAddress("", user.Login));
            emailMessage.Subject = _appConfiguration.CurrentValue.VerifiedCodeSubject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(_messageTemplates.CurrentValue.VerifiedCode, user.Id, user.VerifiedCode)
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_mailSettings.CurrentValue.Host, Convert.ToInt32(_mailSettings.CurrentValue.Port));
                await client.AuthenticateAsync(_mailSettings.CurrentValue.Mail, _mailSettings.CurrentValue.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
