using ProjectLunar.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectLunar.API.Models;

namespace ProjectLunar.API.Services
{
    public class UserActionService : IUserActionService
    {
        private IEntityRepository<PrayerPlace> _PrayerPlace;
        private IEntityRepository<User> _User;
        private IEntityRepository<UserAction> _UserAction;

        public UserActionService(IEntityRepository<PrayerPlace> PrayerPlace, IEntityRepository<User> User, IEntityRepository<UserAction> UserAction)
        {
            _PrayerPlace = PrayerPlace;
            _User = User;
            _UserAction = UserAction;
        }

        public UserAction Get(Guid UserId, Guid PrayerPlaceId)
        {
            var item = _UserAction.Get(x => x.IsDeleted == false && x.PrayerPlaceId == PrayerPlaceId && x.UserId == UserId).FirstOrDefault();

            return item;
        }

        public UserAction Get(Guid PrayerPlaceId)
        {
            var item = _UserAction.Get(x => x.PrayerPlaceId == PrayerPlaceId).FirstOrDefault();

            return item;
        }

        public IQueryable<UserAction> GetAll()
        {
            var item = _UserAction.GetAll();

            return item;
        }

        public UserAction Create(UserAction request)
        {
            _UserAction.InsertOnCommit(request);
            return request;
        }

        public UserAction Update(UserAction request)
        {
            var item = _UserAction.Get(x => x.Id == request.Id).FirstOrDefault();
            _UserAction.UpdateOnCommit(request);
            return request;
        }

        public void Delete(Guid id)
        {
            var item = _UserAction.Get(x => x.Id == id).FirstOrDefault();

            if (item != null)
            {
                _UserAction.DeleteOnCommit(item);
            }
        }

        public void CommitChanges()
        {
            _UserAction.CommitChanges();
        }

        public bool isNewAction(Guid UserId, Guid PrayerPlaceId)
        {
            return _UserAction.Get(x => x.IsDeleted == false && x.PrayerPlaceId == PrayerPlaceId && x.UserId == UserId).Count() > 0 ? false : true;
        }

        public int? LikeCount(Guid PrayerPlaceId)
        {
            var item = _UserAction.Get(x => x.IsDeleted == false && x.PrayerPlaceId == PrayerPlaceId && x.isLike == true).Count();

            return item;
        }

        public int? DislikeCount(Guid PrayerPlaceId)
        {
            var item = _UserAction.Get(x => x.IsDeleted == false && x.PrayerPlaceId == PrayerPlaceId && x.isDislike == true).Count();

            return item;
        }
    }
}
