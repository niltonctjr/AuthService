using AuthService.Domain.Models;
using AuthService.Domain.Utils.Cryptography;
using AuthService.Extensions;
using AuthService.Providers.Mail;
using AuthService.Repositories.Interface;
using AuthService.Settings;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace AuthService.UseCases
{
    public class SignUpUsecase : BaseUsecase<SignUpDto>
    {
        private readonly IUserRepository _rep;
        private readonly IMailProvider _mailProvider;
        private readonly AuthSetting _authSetting;

        public SignUpUsecase(IUserRepository rep, IMailProvider mailProvider, IOptions<AuthSetting> authSetting)
        {
            _rep = rep;
            _mailProvider = mailProvider;
            _authSetting = authSetting.Value;
        }

        public override bool IsValid(Actor actor, SignUpDto dto)
        {
            if (actor == null)
                throw new WarningException("Ator não informado");

            if (string.IsNullOrEmpty(dto.Email))
                throw new WarningException("Email não informado");

            if (string.IsNullOrEmpty(dto.Password))
                throw new WarningException("Password não informado");


            var isExists = _rep.GetByEmail(dto.Email);
            if (isExists != null && isExists.Any())
                throw new WarningException("Email informado já é utilizado");

            return true;
        }

        public override Task<dynamic> Run(Actor actor, SignUpDto dto)
        {
            var encrypPass = dto.Password.Encryp();

            var model = new UserModel()
            {
                Email = dto.Email,
                Password = encrypPass,
                CreatedById = new Guid(actor.Id),
            };

            _rep.Create(model);

            model = _rep.Get(model.Id);

            var token = Token.EncodeTokenMail(_authSetting, model);
            _mailProvider.Emit(new MailProviderModel()
            {
                From = new string[] { "no-reply@authservice.com" },
                To = new string[] { model.Email },
                Subject = "Sua inscrição foi realizada com sucesso",
                Body = $"Sua inscrição foi realizada com sucesso, <a href='{_authSetting.UrlValidateEmail}\\{token}'>Clique aqui</a> para validar seu e-mail."
            });

            return Task.FromResult<dynamic>(new
            {
                model.Id,
                model.Email,
                model.CreatedAt,
                model.CreatedById,
            });
        }
    }

    public class SignUpDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
