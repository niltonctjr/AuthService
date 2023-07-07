using System.Security;

namespace AuthService.Providers.Mail.MailTrap
{
    public class MailTrapSetting
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? User { get; set; }
        public string? Pass { get; set; }
    }
}