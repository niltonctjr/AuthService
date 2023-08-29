using AuthService.Domain.Models;
using AuthService.Domain.Utils.Cryptography;
using AuthService.Extensions;
using AuthService.Repositories.Interface;
using AuthService.Settings;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace AuthService.UseCases
{
    public class ValidateEmailUsecase : BaseUsecase<ValidateEmailDto>
    {
        private readonly IUserRepository _rep;
        private readonly AuthSetting _authSetting;
        public ValidateEmailUsecase(IUserRepository rep, IOptions<AuthSetting> authSetting)
        {
            _rep = rep;
            _authSetting = authSetting.Value;
        }

        public override bool IsValid(Actor actor, ValidateEmailDto dto)
        {
            if (actor == null)
                throw new WarningException("Ator não informado");

            if (string.IsNullOrEmpty(dto.Token))
                throw new WarningException("Token não informado");

            return true;
        }

        public override Task<dynamic> Run(Actor actor, ValidateEmailDto dto)
        {
            var user = Token.DecodeTokenMail(_authSetting, dto.Token);
            user = _rep.Get(user.Id);
            if (user == null)
                throw new WarningException("User não encontrado");

            user.IsValid = true;
            user.ModifiedById = new Guid(actor.Id);
            _rep.Alter(user);

            return Task.FromResult<dynamic>(new
            {
                Ok = true
            });
        }
    }

    public class ValidateEmailDto
    {
        public string Token { get; set; } = "";
    }
}
