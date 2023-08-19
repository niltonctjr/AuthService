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
        public ValidateEmailUsecase(IUserRepository rep)
        {
            _rep = rep;
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
            var user = _rep.GetByEmail(dto.Email).First();
            var token = Token.Generate(_authSetting, user);

            return Task.FromResult<dynamic>(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Email,
                    user.CreatedAt,
                    user.ModifiedAt,
                }
            });
        }
    }

    public class ValidateEmailDto
    {
        public string Token { get; set; } = "";
    }
}
