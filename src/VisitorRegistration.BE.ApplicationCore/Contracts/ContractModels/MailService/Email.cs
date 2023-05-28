// (c) Visitor Registration

namespace VisitorRegistration.BE.ApplicationCore.Contracts.ContractModels.MailService;

public class Email
{
    public string? To { get; set; }

    public string Subject { get; set; } = "Your visitor";

    public string Body { get; set; } = "You visitor has just checked.";
}