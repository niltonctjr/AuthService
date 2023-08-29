using AuthService.Domain.Utils.Cryptography;
using AuthService.Extensions;
using AuthService.Repositories.Interface;
using AuthService.Settings;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace AuthService.UseCases
{
    public class SignInUsecase : BaseUsecase<SignInDto>
    {
        private readonly IUserRepository _rep;
        private readonly AuthSetting _authSetting;
        public SignInUsecase(IUserRepository rep, IOptions<AuthSetting> authSetting)
        {
            _rep = rep;
            _authSetting = authSetting.Value;
        }

        public override bool IsValid(Actor actor, SignInDto dto)
        {
            if (actor == null)
                throw new WarningException("Ator não informado");

            if (string.IsNullOrEmpty(dto.Email))
                throw new WarningException("Email não informado");

            if (string.IsNullOrEmpty(dto.Password))
                throw new WarningException("Password não informado");

            var user = _rep.GetByEmail(dto.Email).FirstOrDefault();
            if (user == null || (user.Password != dto.Password.Encryp()))
            {
                throw new WarningException("Falha ao entrar, usuario ou senha não encontrado");
            }

            return true;
        }

        public override Task<dynamic> Run(Actor actor, SignInDto dto)
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

    public class SignInDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
