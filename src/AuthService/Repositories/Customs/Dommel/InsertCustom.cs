using Dommel;
using System.Data;
using Dapper;

namespace AuthService.Repositories.Customs.Dommel
{
    public static partial class DommelMapperCustom
    {
        public static object InsertCustom<TEntity>(this IDbConnection connection, TEntity entity, IDbTransaction? transaction = null)
    where TEntity : class
        {
            var sql = BuildInsertQuery(DommelMapper.GetSqlBuilder(connection), typeof(TEntity));
            DommelMapper.LogReceived?.Invoke(sql);
            return connection.ExecuteScalar(sql, entity, transaction);
        }

        internal static string BuildInsertQuery(ISqlBuilder sqlBuilder, Type type)
        {            
            var cacheKey = new QueryCacheKey(QueryCacheType.Insert, sqlBuilder, type);
            if (!QueryCache.TryGetValue(cacheKey, out var sql))
            {
                var tableName = Resolvers.Table(type, sqlBuilder);

                IEnumerable<ColumnPropertyInfo> exceptProperties;
                if (!KeyNotIdentity)
                    exceptProperties = Resolvers.KeyProperties(type);                
                else
                    exceptProperties = ResolversCustom.IdentityProperties(type);

                // Use all non-identity and non-generated properties for inserts       
                var typeProperties = Resolvers.Properties(type)
                    .Where(x => !x.IsGenerated)
                    .Select(x => x.Property)
                    .Except(exceptProperties.Where(p => p.IsGenerated).Select(p => p.Property));

                var columnNames = typeProperties.Select(p => Resolvers.Column(p, sqlBuilder, false)).ToArray();                
                var paramNames = typeProperties.Select(p => sqlBuilder.PrefixParameter(p.Name)).ToArray();

                sql = sqlBuilder.BuildInsert(type, tableName, columnNames, paramNames);

                QueryCache.TryAdd(cacheKey, sql);
            }

            return sql;
        }
    }
}
