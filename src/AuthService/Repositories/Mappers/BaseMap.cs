using AuthService.Models;
using Dapper.FluentMap.Mapping;

namespace AuthService.Repositories.Mappers
{
    public abstract class BaseMap<T> : EntityMap<T>
        where T : BaseModel
    {
        public BaseMap()
        {            
            Map(e => e.Id).ToColumn("id");
            Map(e => e.State).ToColumn("state");
            Map(e => e.CreatedAt).ToColumn("created_at");
            Map(e => e.ModifiedAt).ToColumn("modified_at");
            Map(e => e.CreatedById).ToColumn("created_by_id");
            Map(e => e.ModifiedById).ToColumn("modified_by_id");
            Map(e => e.CreatedBy).Ignore();
            Map(e => e.ModifiedBy).Ignore();
        }
    }
}
