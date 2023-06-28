using Dommel;
using System.Collections.Concurrent;

namespace AuthService.Repositories.Customs.Dommel
{
    public static class ResolversCustom
    {
        private static readonly ConcurrentDictionary<Type, ColumnPropertyInfo[]> TypeKeyPropertiesCache = new();
        public static ColumnPropertyInfo[] IdentityProperties(Type type)
        {
            if (!TypeKeyPropertiesCache.TryGetValue(type, out var keyProperties))
            {
                keyProperties = DommelMapperCustom.IdentityPropertyResolver.ResolveIdentityProperties(type);
                TypeKeyPropertiesCache.TryAdd(type, keyProperties);
            }

            DommelMapper.LogReceived?.Invoke($"Resolved property '{string.Join(", ", keyProperties.Select(p => p.Property.Name))}' as identity property for '{type}'");
            return keyProperties;
        }
    }
}
