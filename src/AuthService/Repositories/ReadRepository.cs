using AuthService.Models;
using AuthService.Repositories.Contexts;
using System.Data;
using Dommel;
using System.Linq.Expressions;
using Dapper;
using Microsoft.VisualBasic;
using System;

namespace AuthService.Repositories
{
    public interface IReadRepository<T> where T : BaseModel
    {
        T? Get(Guid id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null);
    }

    public abstract class ReadRepository<T> : IReadRepository<T>
        where T : BaseModel
    {
        protected readonly IBaseContext _ctx;
        public ReadRepository(IBaseContext ctx)
        {
            _ctx = ctx;
        }
        protected Tout OpenConnection<Tout>(Func<IDbConnection, Tout> func)
        {
            using (var conn = _ctx.Open())
            {
                return func(conn);
            }
        }
        
        public virtual T? Get(Guid id) => OpenConnection(conn => {
            return conn.Get<T>(id);
            //return conn.Select<T>(x=> x.Id == id).First();
        });

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null) => OpenConnection(conn => {

            if (predicate == null)
                return conn.GetAll<T>();

            return conn.Select<T>(predicate);

        });
    }
}