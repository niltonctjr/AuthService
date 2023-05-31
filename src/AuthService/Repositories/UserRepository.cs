using Dapper;
using AuthService.Models;
using AuthService.Repositories.Contexts;
using AuthService.Models.Enums;

namespace AuthService.Repositories
{
    public class UserRepository : ReadWriteRepository<UserModel>
    {        
        public UserRepository(AuthServiceContext ctx) : base(ctx)
        {            
        }

        public override string SqlConsult() =>
            "select " +
                "[id], " +
                "[state], " +
                "[created_at], " +
                "[modified_at], " +
                "[created_by_id], " +
                "[modified_by_id], " +
                "[email], " +
                "[password] " +
            "from [dbo].[users] ";
        public override string SqlInsert() => 
            "insert into [dbo].[users] (" +
                "[id], " +
                "[state], " +
                "[created_at], " +
                "[modified_at], " +
                "[created_by_id], " +
                "[modified_by_id], " +
                "[email], " +
                "[password]) " +
            "values (" +
                "@id, " +
                "@state, " +
                "@created_at, " +
                "@modified_at, " +
                "@created_by_id, " +
                "@modified_by_id, " +
                "@email, " +
                "@password)";
        public override string SqlUpdate() => 
            "update [dbo].[users] set " +
                "[state] = @state, " +
                "[modified_at] = @modified_at, " +
                "[modified_by_id] = @modified_by_id, " +
                "[email] = @email, " +
                "[password] = @password " +
            "where [id] = @id";
        public override string SqlDelete() => 
            "delete from [dbo].[users] " +
            "where [id] = @id";
        public override string SqlChangeState() => 
            "update [dbo].[users] set " +
                "[state] = @state " +
            "where [id] = @id";
    }
}
