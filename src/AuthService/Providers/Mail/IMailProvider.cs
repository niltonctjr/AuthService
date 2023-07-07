namespace AuthService.Providers.Mail
{
    public interface IMailProvider
    {
        MailProviderOutput Emit(MailProviderModel model);
        bool ValidForSend(MailProviderModel model);
    }
}
