using AuthService.Domain.Models;
using AuthService.Repositories.Interface;

namespace AuthService.Repositories
{
    public class LazyLoadingRepository
    {
        private readonly IUserRepository _userRep;
        public LazyLoadingRepository(IUserRepository userRep)
        {
            _userRep = userRep;
        }

        public void LoadCreateModified<T>(ref T model) where T : BaseModel
        {
            var createUser = _userRep.Get(model.CreatedById);
            createUser.Password = "";
            model.CreatedBy = createUser;

            if (model.ModifiedById != null)
            {
                var modifyUser = _userRep.Get(model.ModifiedById.Value);
                modifyUser.Password = "";
                model.ModifiedBy = modifyUser;
            }

        }
    }
}
