using AuthService.Domain.Models;
using AuthService.Providers.Cryptography;
using AuthService.Repositories.Interface;
using System.ComponentModel;

namespace AuthService.UseCases
{
    public class SignUpUsecase : BaseUsecase<SignUpDto>
    {
        private readonly IUserRepository _rep;
        public SignUpUsecase(IUserRepository rep)
        {
            _rep = rep;
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
            var encryp = new CryptographyProvider();
            var encrypPass = encryp.Encryp(dto.Password);

            var model = new UserModel() {
                Email = dto.Email,
                Password = encrypPass,
                CreatedById = new Guid(actor.Id),
            };

            _rep.Create(model);

            model = _rep.Get(model.Id);

            return Task.FromResult<dynamic>( new {
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
