using AuthService.Domain.Models.Enums;
using AuthService.UseCases;

namespace AuthService.Providers.Mail
{
    public class MailProviderModel
    {
        public string[]? From { get; set; }
        public string[]? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public IEnumerable<AttachmentMailProvider> Attachments { get; set; }
    }

    public class AttachmentMailProvider
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }

    public class MailProviderOutput
    {
        public StatusDto status { get; set; }
        public Exception? Exception { get; set; }
    }
}