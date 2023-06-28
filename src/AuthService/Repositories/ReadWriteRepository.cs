using Dommel;
using AuthService.Models;
using AuthService.Models.Enums;
using AuthService.Repositories.Contexts;
using AuthService.Repositories.Customs.Dommel;

namespace AuthService.Repositories
{
    public interface IReadWriteRepository<T> : IReadRepository<T>
        where T : BaseModel
    {
        void Create(T model);
        void Alter(T model);
        void Delete(Guid id);
        void Disable(Guid id);
        void Enable(Guid id);
    }
    public abstract class ReadWriteRepository<T> : ReadRepository<T>, IReadWriteRepository<T>
        where T : BaseModel
    {
        protected ReadWriteRepository(IBaseContext ctx) : base(ctx)
        {
        }

        private void ChangeState(StateGeneric state, Guid id)
        {
            var model = Get(id);
            if (model == null)
                throw new Exception($"Não encontrado registro do tipo {typeof(T).FullName} com id {id}");

            model.State = state;
            Alter(model);
        }

        public virtual void Create(T model) => OpenConnection(conn =>
            conn.InsertCustom(model));
        public virtual void Alter(T model) => OpenConnection(conn =>
        {
            return conn.Update(model);
        });
        public virtual void Delete(Guid id)
        {
            var model = Get(id);
            if (model == null)
                throw new Exception($"Não encontrado registro do tipo {typeof(T).FullName} com id {id}");

            OpenConnection(conn => conn.Delete(model));
        }
        public virtual void Disable(Guid id) => ChangeState(StateGeneric.Inactive, id);
        public virtual void Enable(Guid id) => ChangeState(StateGeneric.Active, id);        

    }
}