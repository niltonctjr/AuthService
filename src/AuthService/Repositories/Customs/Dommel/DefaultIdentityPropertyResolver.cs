using Dapper.FluentMap.Dommel.Mapping;
using Dapper.FluentMap.Mapping;
using Dapper.FluentMap;
using Dommel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace AuthService.Repositories.Customs.Dommel
{
    internal class DefaultIdentityPropertyResolver : IIdentityPropertyResolver
    {
        public ColumnPropertyInfo[] ResolveIdentityProperties(Type type)
        {
            IEntityMap entityMap;
            if (!FluentMapper.EntityMaps.TryGetValue(type, out entityMap))
            {
                return Resolvers.KeyProperties(type);
            }

            var mapping = entityMap as IDommelEntityMap;
            if (mapping != null)
            {
                var allPropertyMaps = entityMap.PropertyMaps.OfType<DommelPropertyMap>();
                var keyPropertyInfos = allPropertyMaps
                     .Where(e => e.Identity)
                     .Select(x => new ColumnPropertyInfo(
                        x.PropertyInfo,
                        x.GeneratedOption != null ? x.GeneratedOption : (x.Identity ? DatabaseGeneratedOption.Identity : DatabaseGeneratedOption.None)
                        ))
                     .ToArray();

                //// Now make sure there aren't any missing key properties that weren't explicitly defined in the mapping.
                //try
                //{
                //    // Make sure to exclude any keys that were defined in the dommel entity map and not marked as keys.
                //    var defaultKeyPropertyInfos = ResolveIdentityPropertiesDefault(type).Where(x => allPropertyMaps.Count(y => y.PropertyInfo.Equals(x.Property)) == 0);
                //    keyPropertyInfos = keyPropertyInfos.Union(defaultKeyPropertyInfos).ToArray();
                //}
                //catch
                //{
                //    // There could be no default Ids found. This is okay as long as we found a custom one.
                //    if (keyPropertyInfos.Length == 0)
                //    {
                //        throw new InvalidOperationException($"Could not find the key properties for type '{type.FullName}'.");
                //    }
                //}

                return keyPropertyInfos;
            }

            // Fall back to the default mapping strategy.
            return Resolvers.KeyProperties(type);
        }
    }
}