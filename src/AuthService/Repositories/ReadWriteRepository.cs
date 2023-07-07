using Dommel;
using AuthService.Domain.Models;
using AuthService.Domain.Models.Enums;
using AuthService.Repositories.Contexts;
using AuthService.Repositories.Customs.Dommel;
using AuthService.Domain.Models;
using System.ComponentModel;

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
            if (model.ModifiedBy != null)
            {
                model.ModifiedById = model.ModifiedBy.Id;
            }


            if (model.ModifiedById == null || model.ModifiedById == Guid.Empty)
                throw new WarningException("Não informado usuario de modificação");

            model.ModifiedAt = DateTime.Now;
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