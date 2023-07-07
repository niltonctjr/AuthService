using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace AuthService.Providers.Mail.MailTrap
{
    public class MailTrapProvider : BaseMailProvider
    {
        private readonly MailTrapSetting _setting;
        public MailTrapProvider(IOptions<MailTrapSetting> setting)
        {
            _setting = setting.Value;
        }
        public override void Send(MailProviderModel model)
        {
            var from = string.Join(";", model.From);
            var to = string.Join(";", model.To);

            var mail = new MailMessage(from, to);
            mail.Subject = model.Subject;
            mail.Body = model.Body;
            mail.IsBodyHtml = true;

            if (model.Attachments!=null && model.Attachments.Any(a=> a.Content.Any()))
            {
                foreach (var a in model.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(a.Content);
                        mail.Attachments.Add(new Attachment(ms, a.Name));
                    }
                }
            }

            using (var client = new SmtpClient(_setting.Host, _setting.Port))
            {
                client.Credentials = new NetworkCredential(_setting.User, _setting.Pass);
                client.EnableSsl = true;               
                client.Send(mail);
            };
        }
    }
}
