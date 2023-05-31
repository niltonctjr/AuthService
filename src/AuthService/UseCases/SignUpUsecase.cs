using System.ComponentModel;

namespace AuthService.UseCases
{
    public class SignUpUsecase : BaseUsecase<SignUpDto>
    {
        public override bool IsValid(SignUpDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email))
                throw new WarningException("Email não informado");

            if (string.IsNullOrEmpty(dto.Password))
                throw new WarningException("Password não informado");

            return true;
        }

        public override Task<dynamic> Run(SignUpDto dto)
        {
            throw new NotImplementedException();
        }
    }

    public class SignUpDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
