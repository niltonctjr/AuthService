using AuthService.Domain.Models;
using AuthService.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServiceTest.Mocks.Repositories
{
    public class MockUserRepository : MockReadWriteRepository<UserModel>, IUserRepository
    {
        public IEnumerable<UserModel> GetByEmail(string email) => 
            MemoryStorage.Where(x=> x.Email == email);
    }
}
