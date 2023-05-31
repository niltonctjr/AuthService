using Microsoft.AspNetCore.Authentication;
using System.Data;

namespace AuthService.Repositories.Contexts
{
    public interface IBaseContext
    {        
        IDbConnection Open();
        void Dispose();
    }

    public abstract class BaseContext<T> : IBaseContext
        where T : IDbConnection
    {
        private readonly IDbConnection _conn;

        public BaseContext(string? connectionString)
        {
            if (connectionString == null) throw new Exception("String de conexão não informada");

            _conn = Activator.CreateInstance<T>();
            _conn.ConnectionString = connectionString;            
        }
        public IDbConnection Open() 
        {
            if (_conn.State != ConnectionState.Open) 
                _conn.Open();
            return _conn;
        }
        public void Dispose()
        {
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
            }
        }
    }


}