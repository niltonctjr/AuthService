using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace AuthService.Repositories.Contexts
{
    public class AuthServiceContext : BaseContext<SqlConnection>
    {
        public AuthServiceContext(IConfiguration config) : base(config.GetConnectionString("AuthService"))
        {
        }
    }
}
