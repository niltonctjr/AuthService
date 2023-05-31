using Dapper;
using AuthService.Models;
using AuthService.Models.Enums;
using AuthService.Repositories.Contexts;
using System.Data;

namespace AuthService.Repositories
{
    public abstract class ReadWriteRepository<T> : ReadRepository<T>
        where T : BaseModel
    {
        protected ReadWriteRepository(IBaseContext ctx) : base(ctx)
        {
        }

        private Task ChangeState(StateGeneric state, Guid id) => OpenConnection(conn =>
            conn.ExecuteAsync(SqlChangeState(), new
            {
                state,
                id
            })
        );

        public virtual Task Create(T model) => OpenConnection(conn =>
            conn.ExecuteAsync(SqlInsert(), model)
        );
        public virtual Task Alter(T model) => OpenConnection(conn =>
            conn.ExecuteAsync(SqlUpdate(), model)
        );
        public virtual Task Delete(Guid id) => OpenConnection(conn =>
            conn.ExecuteAsync(SqlDelete(), new { id })
        );     
        public virtual Task Disable(Guid id) => ChangeState(StateGeneric.Inactive, id);
        public virtual Task Enable(Guid id) => ChangeState(StateGeneric.Active, id);        

        public abstract string SqlInsert();
        public abstract string SqlUpdate();
        public abstract string SqlDelete();
        public abstract string SqlChangeState();
    }
}