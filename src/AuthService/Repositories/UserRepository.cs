using AuthService.Models;
using AuthService.Repositories.Contexts;
using AuthService.Repositories.Interface;

namespace AuthService.Repositories
{
    public class UserRepository : ReadWriteRepository<UserModel>, IUserRepository
    {        
        public UserRepository(AuthServiceContext ctx) : base(ctx)
        {
        }

        public IEnumerable<UserModel> GetByEmail(string email) => 
            GetAll(u=> u.Email == email);
        
    }
}