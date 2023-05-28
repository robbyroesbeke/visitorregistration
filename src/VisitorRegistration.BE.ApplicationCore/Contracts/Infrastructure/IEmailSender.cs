// (c) Visitor Registration

using VisitorRegistration.BE.ApplicationCore.Contracts.ContractModels.MailService;

namespace VisitorRegistration.BE.ApplicationCore.Contracts.Infrastructure;

public interface IEmailSender
{
    public ValueTask<bool> SendEmailAsync (Email email);
}