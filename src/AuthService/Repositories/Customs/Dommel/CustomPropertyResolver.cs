using Dapper.FluentMap.Dommel.Mapping;
using Dapper.FluentMap.Mapping;
using Dapper.FluentMap;
using Dommel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace AuthService.Repositories.Customs.Dommel
{
    internal class CustomPropertyResolver : DefaultPropertyResolver
    {
        public override IEnumerable<ColumnPropertyInfo> ResolveProperties(Type type)
        {
            foreach (var property in FilterComplexTypes(type.GetRuntimeProperties()))
            {
                if (!property.IsDefined(typeof(IgnoreAttribute)) && !property.IsDefined(typeof(NotMappedAttribute)))
                {
                    yield return new ColumnPropertyInfo(property);
                }
            }
        }
    }
}