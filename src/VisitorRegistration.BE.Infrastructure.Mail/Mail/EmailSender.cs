// (c) Visitor Registration

using VisitorRegistration.BE.ApplicationCore.Contracts.ContractModels.MailService;
using VisitorRegistration.BE.ApplicationCore.Contracts.Infrastructure;

namespace VisitorRegistration.BE.Infrastructure.Mail.Mail;

public class EmailSender : IEmailSender
{
    // Return true for demo purposes
    public ValueTask<bool> SendEmailAsync (Email email)
        => ValueTask.FromResult(true);
}