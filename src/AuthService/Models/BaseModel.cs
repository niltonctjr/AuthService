using AuthService.Models.Enums;
using AuthService.Providers.UniqueIdentify;
using System;

namespace AuthService.Models
{
    public abstract class BaseModel
    {
        private readonly UniqueIdentifyProvider _uid;
        private readonly Guid _id;
        public Guid Id => _id;
        public StateGeneric State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
        public UserModel? CreatedBy { get; set; }
        public UserModel? ModifiedBy { get; set; }

        public BaseModel(Guid id)
        {
            _uid = new UniqueIdentifyProvider();
            _id = id;
            CreatedAt = _uid.GetDate(Id);            
        }
        public BaseModel()
        {
            _uid = new UniqueIdentifyProvider();
            _id = _uid.getUID();
            CreatedAt = _uid.GetDate(Id);
            State = Models.Enums.StateGeneric.Active;
        }

        public abstract void LazyLoading();
    }
}
