using Dapper;
using AuthService.Models;
using AuthService.Repositories.Contexts;
using System.Data;

namespace AuthService.Repositories
{
    public abstract class ReadRepository<T>
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
        
        public virtual Task<T> Get(Guid id) => OpenConnection(conn => 
            conn.QueryFirstAsync<T>($"{SqlConsult()} where [id] = @id", new { id })
        );

        public virtual Task<T> GetAll(string? where = null, object? param = null) => OpenConnection(conn => {

            if (where != null && param != null)
            {
                var sql = $"{SqlConsult()} where {where}";
                return conn.QueryFirstAsync<T>(sql, param);
            }
            return conn.QueryFirstAsync<T>(SqlConsult());

        });


        public abstract string SqlConsult();
    }
}