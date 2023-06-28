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
        private IDbConnection _conn;
        private readonly string _stringConn;

        public BaseContext(string? connectionString)
        {
            if (connectionString == null) throw new Exception("String de conexão não informada");
            _stringConn = connectionString;
        }

        public IDbConnection Open() 
        {
            if (_conn == null || _conn.State != ConnectionState.Open) 
            {
                _conn = Activator.CreateInstance<T>();
                _conn.ConnectionString = _stringConn;
                _conn.Open();
            }
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