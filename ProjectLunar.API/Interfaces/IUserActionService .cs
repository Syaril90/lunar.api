using ProjectLunar.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Interfaces
{
    public interface IUserActionService
    {
        UserAction Get(Guid UserId, Guid PrayerPlaceId);
        UserAction Get(Guid PrayerPlaceId);
        IQueryable<UserAction> GetAll();
        UserAction Create(UserAction request);
        UserAction Update(UserAction request);
        void Delete(Guid id);
        void CommitChanges();
        bool isNewAction(Guid UserId, Guid PrayerPlaceId);
        int? LikeCount(Guid PrayerPlaceId);
        int? DislikeCount(Guid PrayerPlaceId);
    }
}
