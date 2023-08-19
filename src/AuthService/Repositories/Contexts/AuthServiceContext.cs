using Microsoft.Extensions.Options;
using Npgsql;
using System.Data.SqlClient;

namespace AuthService.Repositories.Contexts
{
    //public class AuthServiceContext : BaseContext<SqlConnection>
    public class AuthServiceContext : BaseContext<NpgsqlConnection>
    {
        public AuthServiceContext(IConfiguration config) : base(config.GetConnectionString("AuthService"))
        {
        }
    }
}
