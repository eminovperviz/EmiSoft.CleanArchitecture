using EmiSoft.CleanArchitecture.Application.Interfaces.Email;
using EmiSoft.CleanArchitecture.Application.Models.Config;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace EmiSoft.CleanArchitecture.Infrastructure.Services;

public class EmailSenderService : IEmailSenderService
{
    readonly ILogger<EmailSenderService> _logger;

    public EmailSenderService(ILogger<EmailSenderService> logger)
    {
        _logger = logger;
    }

    public async Task SendAsync(string to, EmailConfig from, string[] CC, string subject, string body, string[] attachments)
    {
        SmtpClient emailClient = new();
        emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        emailClient.UseDefaultCredentials = false;
        emailClient.EnableSsl = true;
        emailClient.Host = from.Host;
        emailClient.Port = from.Port;
        emailClient.Credentials = new NetworkCredential(from.From, from.Password);
        MailMessage message = new()
        {
            From = new MailAddress(from.From),
            Subject = subject,
            Body = body
        };

        if (CC is not null)
            foreach (string copy in CC)
            {
                message.CC.Add(new MailAddress(copy));
            }

        if (attachments is not null)
            foreach (string attachmentPath in attachments)
            {
                message.Attachments.Add(new Attachment(attachmentPath));
            }
        message.To.Add(new MailAddress(to));
        await emailClient.SendMailAsync(message);
        _logger.LogInformation($"Sending email to {to} from {from.From} with subject {subject}.");
    }
}
