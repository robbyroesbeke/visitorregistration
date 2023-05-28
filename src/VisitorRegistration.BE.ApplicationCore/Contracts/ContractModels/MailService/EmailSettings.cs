// (c) Visitor Registration

namespace VisitorRegistration.BE.ApplicationCore.Contracts.ContractModels.MailService;

public class EmailSettings
{
    public string APIKey { get; set; } = string.Empty;

    public string FromAddress { get; set; } = string.Empty;

    public string FromName { get; set; } = "Business Park";
}