using EmiSoft.CleanArchitecture.Application.Models.Config;

namespace EmiSoft.CleanArchitecture.Application.Interfaces.Email;

public interface IEmailSenderService
{
    Task SendAsync(string to, EmailConfig from, string[] CC, string subject, string body, string[] attachments);
}
