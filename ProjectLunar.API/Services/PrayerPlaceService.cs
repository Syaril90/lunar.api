using ProjectLunar.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectLunar.API.Models;
using Microsoft.Extensions.Configuration;
using ProjectLunar.API.Contracts.Response;
using Microsoft.EntityFrameworkCore;

namespace ProjectLunar.API.Services
{
    public class PrayerPlaceService : IPrayerPlaceService
    {
        private IEntityRepository<PrayerPlace> _PrayerPlace;
        private IUserActionService _UserActionService;

        public PrayerPlaceService(IEntityRepository<PrayerPlace> PrayerPlace, IUserActionService UserActionService)
        {
            _PrayerPlace = PrayerPlace;
            _UserActionService = UserActionService;
        }

        public PrayerPlace Get(Guid id)
        {
            var item = _PrayerPlace.Get(x => x.Id == id).Include("Photos").FirstOrDefault();

            return item;
        }

        public IQueryable<PrayerPlace> GetApprovedPrayerPlace()
        {
            var item = _PrayerPlace.GetAll().Where(x => x.IsDeleted == false && x.Status == Enums.Status.Approved.ToString());

            return item;
        }

        public IQueryable<PrayerPlace> GetPendingPlace()
        {
            var item = _PrayerPlace.GetAll().Where(x => x.IsDeleted == false && x.Status == Enums.Status.Pending.ToString());

            return item;
        }

        public PrayerPlace Create(PrayerPlace request)
        {
            _PrayerPlace.InsertOnCommit(request);
            return request;
        }

        public PrayerPlace Update(PrayerPlace request)
        {
            var item = _PrayerPlace.Get(x => x.Id == request.Id).FirstOrDefault();
            _PrayerPlace.UpdateOnCommit(request);
            return request;
        }

        public void Delete(Guid id)
        {
            var item = _PrayerPlace.Get(x => x.Id == id).FirstOrDefault();

            if (item != null)
            {
                _PrayerPlace.DeleteOnCommit(item);
            }
        }

        public void CommitChanges()
        {
            _PrayerPlace.CommitChanges();
        }

        public PrayerPlaceResponseDetailsContract GetPrayerPlaceDetails(Guid Id, Guid UserId)
        {
            List<PrayerPlaceResponseDetailsContract> PrayerPlaceResponseDetailsContract = new List<PrayerPlaceResponseDetailsContract>();
            var query = _PrayerPlace.GetAll().Include("Photos").Include("UserActions").Where(x => x.IsDeleted == false && x.Status == Enums.Status.Approved.ToString() && x.Id == Id);
            var PrayerPlaceResponse = new PrayerPlaceResponseDetailsContract();
            foreach (var item in query)
            {
                PrayerPlaceResponse.UserAction = new UserActionContract();
                PrayerPlaceResponse.Photos = new List<PhotoResponseContract>();
                PrayerPlaceResponse.Id = item.Id;
                PrayerPlaceResponse.Name = item.Name;
                PrayerPlaceResponse.Address = item.Address;
                PrayerPlaceResponse.Latitud = item.Latitud;
                PrayerPlaceResponse.Longitud = item.Longitud;
                PrayerPlaceResponse.Remarks = item.Remarks;
                PrayerPlaceResponse.LikeCount = _UserActionService.LikeCount(item.Id);
                PrayerPlaceResponse.DislikeCount = _UserActionService.DislikeCount(item.Id);
                PrayerPlaceResponse.InsAt = item.InsAt;
                PrayerPlaceResponse.InsBy = item.InsBy;
                PrayerPlaceResponse.UpdAt = item.UpdAt;
                PrayerPlaceResponse.UpdBy = item.UpdBy;

                foreach (var photo in item.Photos.Where(x => x.IsDeleted == false))
                {
                    var PhotoResponseContract = new PhotoResponseContract();
                    PhotoResponseContract.Id = photo.Id;
                    PhotoResponseContract.Name = photo.Name;
                    PhotoResponseContract.Extension = photo.Extension;
                    PhotoResponseContract.File = photo.File;
                    PhotoResponseContract.InsBy = photo.InsBy;
                    PhotoResponseContract.InsAt = photo.InsAt;
                    PhotoResponseContract.UpdBy = photo.UpdBy;
                    PhotoResponseContract.UpdAt = photo.UpdAt;

                    PrayerPlaceResponse.Photos.Add(PhotoResponseContract);
                }

                foreach (var UserAction in item.UserActions.Where(x => x.UserId == UserId && x.IsDeleted == false))
                {
                    var UserActionContract = new UserActionContract();
                    UserActionContract.Id = UserAction.Id;
                    UserActionContract.isLike = UserAction.isLike;
                    UserActionContract.isDislike = UserAction.isDislike;
                    UserActionContract.InsBy = UserAction.InsBy;
                    UserActionContract.InsAt = UserAction.InsAt;
                    UserActionContract.UpdBy = UserAction.UpdBy;
                    UserActionContract.UpdAt = UserAction.UpdAt;

                    PrayerPlaceResponse.UserAction = UserActionContract;
                }
            }

            return PrayerPlaceResponse;
        }

        public List<PrayerPlaceResponseContract> SearchNearbyPrayerPlace(double currentLatitude, double currentLongitude)
        {
            List<PrayerPlaceResponseContract> NearbyPrayerPlace = new List<PrayerPlaceResponseContract>();
            var query = _PrayerPlace.GetAll().Include("Photos").Where(x => x.IsDeleted == false && x.Status == Enums.Status.Approved.ToString());
            foreach (var item in query)
            {
                double distance = Distance(currentLatitude, currentLongitude, item.Latitud, item.Longitud);
                if (distance < 25)          //nearbyplaces which are within 25 kms 
                {
                    var PrayerPlaceResponse = new PrayerPlaceResponseContract();
                    PrayerPlaceResponse.Id = item.Id;
                    PrayerPlaceResponse.Name = item.Name;
                    PrayerPlaceResponse.Category = item.Category;
                    PrayerPlaceResponse.Latitud = item.Latitud;
                    PrayerPlaceResponse.Longitud = item.Longitud;

                    NearbyPrayerPlace.Add(PrayerPlaceResponse);
                }
            }
            return NearbyPrayerPlace;
        }

        private double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = (dist * 60 * 1.1515) / 0.6213711922;          //miles to kms
            return (dist);
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad * 180.0 / Math.PI);
        }

    }
}
