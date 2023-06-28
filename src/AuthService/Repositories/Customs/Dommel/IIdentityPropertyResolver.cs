using Dommel;

namespace AuthService.Repositories.Customs.Dommel
{
    internal interface IIdentityPropertyResolver
    {
        ColumnPropertyInfo[] ResolveIdentityProperties(Type type);
    }
}