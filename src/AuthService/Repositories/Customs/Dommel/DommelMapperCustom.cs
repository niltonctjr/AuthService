using Dommel;
using System.Data;
using Dapper;
using System.Collections.Concurrent;

namespace AuthService.Repositories.Customs.Dommel
{
    public static partial class DommelMapperCustom
    {
        internal static ConcurrentDictionary<QueryCacheKey, string> QueryCache { get; } = new ConcurrentDictionary<QueryCacheKey, string>();
        internal static IIdentityPropertyResolver IdentityPropertyResolver = new DefaultIdentityPropertyResolver();

        public static bool KeyNotIdentity = false;
    }
}
