using AuthService.Domain.Models;
using AuthService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServiceTest.Mocks.Repositories
{
    public abstract class MockReadWriteRepository<T> : MockReadRepository<T>, IReadWriteRepository<T>
        where T : BaseModel
    {
        public void Alter(T model)
        {
            var old = Get(model.Id);
            MemoryStorage.Remove(old);
            MemoryStorage.Add(model);
        }

        public void Create(T model) => MemoryStorage.Add(model);

        public void Delete(Guid id) => MemoryStorage.Remove(Get(id));

        public void Disable(Guid id) => ChangeState(AuthService.Domain.Models.Enums.StateGeneric.Inactive, id);

        public void Enable(Guid id) => ChangeState(AuthService.Domain.Models.Enums.StateGeneric.Active, id);

        private void ChangeState(AuthService.Domain.Models.Enums.StateGeneric state, Guid id)
        {
            var model = Get(id);
            model.State = state;
            MemoryStorage.Add(model);
        }
    }
}
