using AuthService.Domain.Models;

namespace AuthService.Repositories.Interface
{
    public interface IUserRepository : IReadWriteRepository<UserModel>
    {
        IEnumerable<UserModel> GetByEmail(string email);
    }
}
