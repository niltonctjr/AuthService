
namespace AuthService.Domain.Models
{
    public class UserModel : BaseModel
    {
        public UserModel(Guid id) : base(id)
        {

        }

        public UserModel() : base()
        {

        }

        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsValid { get; set; }
    }
}

