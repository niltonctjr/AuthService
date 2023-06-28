using AuthService.Models;
using AuthService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServiceTest.Mocks.Repositories
{
    public abstract class MockReadRepository<T> : IReadRepository<T> where T : BaseModel
    {
        protected List<T> MemoryStorage { get; set; } = new List<T>();
        
        public T Get(Guid id) => MemoryStorage.First(x => x.Id == id);

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null) => predicate == null ? MemoryStorage : MemoryStorage.Where<T>(predicate.Compile());
    }
}
