using AuthService.Domain.Models.Enums;
using System.ComponentModel;

namespace AuthService.Providers.Mail
{
    public abstract class BaseMailProvider : IMailProvider
    {
        public MailProviderOutput Emit(MailProviderModel model) 
        {
            var fexception = (StatusDto status, Exception e) => 
            new MailProviderOutput() {
                status = status,
                Exception = e
            };

            try
            {
                if (!ValidForSend(model))
                    throw new WarningException("Model de email inválido");

                Send(model);
                return new MailProviderOutput() { status = StatusDto.Success };
            }
            catch (WarningException we)
            {
                return fexception(StatusDto.Invalid, we);
            }
            catch (Exception e)
            {
                return fexception(StatusDto.Error, e);
            } 
        }

        public bool ValidForSend(MailProviderModel model)
        {
            if (model == null)
                throw new WarningException("Não foi informado modelo de e-mail provedor");

            if (model?.From == null || !model.From.Any(m=> !string.IsNullOrEmpty(m)))
                throw new WarningException("Não foi informado e-mail de");

            if (model?.To == null || !model.To.Any(m => !string.IsNullOrEmpty(m)))
                throw new WarningException("Não foi informado e-mail para");

            if (string.IsNullOrEmpty(model.Subject))
                throw new WarningException("Não foi informado o assunto do e-mail");

            if (string.IsNullOrEmpty(model.Body))
                throw new WarningException("Não foi informado o corpo do e-mail");

            return true;
        }

        public abstract void Send(MailProviderModel model);
    }
}
