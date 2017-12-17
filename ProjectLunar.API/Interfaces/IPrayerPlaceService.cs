using ProjectLunar.API.Contracts.Response;
using ProjectLunar.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Interfaces
{
    public interface IPrayerPlaceService
    {
        PrayerPlace Get(Guid id);
        IQueryable<PrayerPlace> GetApprovedPrayerPlace();
        IQueryable<PrayerPlace> GetPendingPlace();
        PrayerPlace Create(PrayerPlace request);
        PrayerPlace Update(PrayerPlace request);
        PrayerPlaceResponseDetailsContract GetPrayerPlaceDetails(Guid Id, Guid UserId);
        List<PrayerPlaceResponseContract> SearchNearbyPrayerPlace(double currentLatitude, double currentLongitude);
        void Delete(Guid id);
        void CommitChanges();

    }
}
