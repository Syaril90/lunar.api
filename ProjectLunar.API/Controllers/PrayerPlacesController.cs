using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectLunar.API.Interfaces;
using ProjectLunar.API.Contracts.Request;
using ProjectLunar.API.Models;
using ProjectLunar.API.Contracts.Response;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ProjectLunar.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class PrayerPlaces : Controller
    {
        private readonly IPrayerPlaceService _PrayerPlaceService;
        private readonly IUserActionService _UserActionService;

        public PrayerPlaces(IPrayerPlaceService PrayerPlaceService, IUserActionService UserActionService)
        {
            _PrayerPlaceService = PrayerPlaceService;
            _UserActionService = UserActionService;
        }

        [HttpPost("SavePrayerPlace")]
        public IActionResult Save([FromBody]PrayerPlaceRequestContract Request)
        {
            var PrayerPlaceResponseContract = new PrayerPlaceResponseDetailsContract();
            var MessageContract = new MessageResponseContract();
            var PhotoResponseContract = new PhotoResponseContract();

            if (Request == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Cannot stringify the json";

                return new BadRequestObjectResult(MessageContract);
            }

            if (Request.Latitud == 0 || Request.Longitud == 0)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Cannot stringify the jsonLatitud or longitud cannot be null";
                return new BadRequestObjectResult(MessageContract);
            }

            if (Request.UserId == null || Request.UserId == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Unauthorized user";

                return new BadRequestObjectResult(MessageContract);
            }

            if (Request.Photos == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Must upload at least 1 photo";
            }

            try
            {
                var PrayerPlace = new PrayerPlace();
                PrayerPlace.Id = Guid.NewGuid();
                PrayerPlace.Name = Request.Name;
                PrayerPlace.Address = Request.Address;
                PrayerPlace.Latitud = Request.Latitud;
                PrayerPlace.Longitud = Request.Longitud;
                PrayerPlace.Remarks = Request.Remarks;
                PrayerPlace.Status = Enums.Status.Pending.ToString();
                PrayerPlace.InsAt = DateTime.Now;
                PrayerPlace.InsBy = "LunarAdmin";
                PrayerPlace.IsDeleted = false;
                PrayerPlace.UserId = Request.UserId;

                var CallbackData = _PrayerPlaceService.Create(PrayerPlace);
                _PrayerPlaceService.CommitChanges();

                foreach (var item in Request.Photos)
                {
                    var Photo = new Photo();

                    Photo.PrayerPlaceId = CallbackData.Id;
                    Photo.File = Convert.FromBase64String(item.File);
                    Photo.InsAt = DateTime.Now;
                    Photo.InsBy = "LunarAdmin";

                    CallbackData.Photos.Add(Photo);
                }

                _PrayerPlaceService.Update(CallbackData);
                _PrayerPlaceService.CommitChanges();
            }

            catch (Exception ex)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Failed to save - " + ex.Message;
                return new BadRequestObjectResult(MessageContract);
            }
            MessageContract.Code = HttpStatusCode.OK.ToString();
            MessageContract.Message = "Save succeed";
            return Ok(MessageContract);
        }

        [HttpPost("EditPrayerPlace")]
        public IActionResult EditPrayerPlace([FromBody]EditPrayerPlaceResponseContract Request)
        {
            var PrayerPlaceResponseContract = new PrayerPlaceResponseDetailsContract();
            var MessageContract = new MessageResponseContract();
            var PhotoResponseContract = new PhotoResponseContract();

            if (Request == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Cannot stringify the json";

                return new BadRequestObjectResult(MessageContract);
            }

            if (Request.Latitud == 0 || Request.Longitud == 0)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Cannot stringify the jsonLatitud or longitud cannot be null";
                return new BadRequestObjectResult(MessageContract);
            }

            if (Request.UserId == null || Request.UserId == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Unauthorized user";

                return new BadRequestObjectResult(MessageContract);
            }

            if (Request.Photos == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Must upload at least 1 photo";
            }

            try
            {
                var PrayerPlaceToEdit = _PrayerPlaceService.Get(Request.PrayerPlaceId);
                PrayerPlaceToEdit.Name = Request.Name;
                PrayerPlaceToEdit.Address = Request.Address;
                PrayerPlaceToEdit.Latitud = Request.Latitud;
                PrayerPlaceToEdit.Longitud = Request.Longitud;
                PrayerPlaceToEdit.Remarks = Request.Remarks;
                PrayerPlaceToEdit.Status = Enums.Status.Pending.ToString();
                PrayerPlaceToEdit.UpdAt = DateTime.Now;
                PrayerPlaceToEdit.UpdBy = "LunarAdmin";
                PrayerPlaceToEdit.IsDeleted = false;
                PrayerPlaceToEdit.UserId = Request.UserId;

                foreach (var item in PrayerPlaceToEdit.Photos)
                {
                    item.IsDeleted = true;
                    PrayerPlaceToEdit.UpdAt = DateTime.Now;
                    PrayerPlaceToEdit.UpdBy = "LunarAdmin";
                }

                var CallbackData = _PrayerPlaceService.Update(PrayerPlaceToEdit);
                _PrayerPlaceService.CommitChanges();

                foreach (var item in Request.Photos)
                {
                    var Photo = new Photo();

                    Photo.PrayerPlaceId = CallbackData.Id;
                    Photo.Name = item.Name;
                    Photo.Extension = item.Extension;
                    Photo.File = Convert.FromBase64String(item.File);
                    Photo.UpdAt = DateTime.Now;
                    Photo.UpdBy = "LunarAdmin";

                    CallbackData.Photos.Add(Photo);
                }

                _PrayerPlaceService.Update(CallbackData);
                _PrayerPlaceService.CommitChanges();
            }

            catch (Exception ex)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Failed to update - " + ex.Message;
                return new BadRequestObjectResult(MessageContract);
            }
            MessageContract.Code = HttpStatusCode.OK.ToString();
            MessageContract.Message = "Save succeed";
            return Ok(MessageContract);
        }

        [HttpGet("DeletePrayerPlace")]
        public IActionResult Delete([FromQuery] Guid Id)
        {
            var MessageContract = new MessageResponseContract();

            if (Id == null || Id == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "All data are compulsory";

                return new BadRequestObjectResult(MessageContract);
            }

            try
            {
                var PrayerPlaceToDelete = _PrayerPlaceService.Get(Id);
                PrayerPlaceToDelete.IsDeleted = true;
                PrayerPlaceToDelete.UpdAt = DateTime.Now;
                PrayerPlaceToDelete.UpdBy = "LunarAdmin";

                _PrayerPlaceService.Update(PrayerPlaceToDelete);
                _PrayerPlaceService.CommitChanges();

                MessageContract.Code = HttpStatusCode.OK.ToString();
                MessageContract.Message = "Successfully delete data";

                return Ok(MessageContract);
            }
            catch (Exception ex)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Failed to delete data - " + ex.Message;

                return new BadRequestObjectResult(MessageContract);
            }
        }

        [HttpPost("GetNearbyPrayerPlaces")]
        public IActionResult GetNearbyPrayerPlaces([FromBody]SearchNearbyRequestContract Request)
        {
            var MessageContract = new MessageResponseContract();

            if (Request == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Cannot stringify the json";

                return new BadRequestObjectResult(MessageContract);
            }

            if (Request.Latitud == 0 || Request.Longitud == 0)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Latitud or longitud cannot be null";
                return new BadRequestObjectResult(MessageContract);
            }

            try
            {
                var PrayerPlaceList = _PrayerPlaceService.SearchNearbyPrayerPlace(Request.Latitud, Request.Longitud);
                return Ok(PrayerPlaceList);
            }
            catch (Exception ex)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Failed to retrieve data - " + ex.Message;
                return new BadRequestObjectResult(MessageContract);
            }
        }

        [HttpPost("GetPendingPrayerPlace")]
        public IActionResult GetPendingPrayerPlace()
        {
            var PrayerPlaceResponseContractList = new List<PendingPrayerPlaceResponseContract>();
            var MessageContract = new MessageResponseContract();

            try
            {
                var PendingPrayerPlace = _PrayerPlaceService.GetPendingPlace();

                foreach (var item in PendingPrayerPlace)
                {
                    var PrayerPlaceResponseContract = new PendingPrayerPlaceResponseContract();
                    PrayerPlaceResponseContract.Id = item.Id;
                    PrayerPlaceResponseContract.Name = item.Name;
                    PrayerPlaceResponseContract.Address = item.Address;
                    PrayerPlaceResponseContract.Category = item.Category;
                    PrayerPlaceResponseContract.Latitud = item.Latitud;
                    PrayerPlaceResponseContract.Longitud = item.Longitud;
                    PrayerPlaceResponseContract.Remarks = item.Remarks;
                    PrayerPlaceResponseContract.InsAt = item.InsAt;
                    PrayerPlaceResponseContract.InsBy = item.InsBy;

                    PrayerPlaceResponseContractList.Add(PrayerPlaceResponseContract);
                }

                return Ok(PrayerPlaceResponseContractList);
            }
            catch (Exception ex)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Failed to get data - " + ex.Message;
                return new BadRequestObjectResult(MessageContract);
            }
        }

        [HttpPost("GetPrayerPlaceDetails")]
        public IActionResult GetPrayerPlaceDetails([FromBody]PrayerPlaceDetailsRequestContract PrayerPlaceDetailsRequestContract)
        {
           var Details = _PrayerPlaceService.GetPrayerPlaceDetails(PrayerPlaceDetailsRequestContract.PrayerPlaceId, PrayerPlaceDetailsRequestContract.UserId);

            return Ok(Details);
        }

        [HttpPost("ChangeStatus")]
        public IActionResult ChangeStatus([FromBody] ChangeStatusRequestContract Request)
        {
            var PrayerPlaceResponse = new PrayerPlaceResponseDetailsContract();
            var MessageContract = new MessageResponseContract();

            if (Request == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Cannot stringify the json";

                return new BadRequestObjectResult(MessageContract);
            }

            if (Request.Id == null || Request.Status == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "All the data are compulsory";

                return new BadRequestObjectResult(MessageContract);
            }

            try
            {
                var PrayerPlaceToUpdate = _PrayerPlaceService.Get(Request.Id);

                if (Request.Status.Equals(Enums.Status.Approved.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    PrayerPlaceToUpdate.Status = Enums.Status.Approved.ToString();
                }
                else if (Request.Status.Equals(Enums.Status.Rejected.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    PrayerPlaceToUpdate.Status = Enums.Status.Rejected.ToString();
                }

                PrayerPlaceToUpdate.UpdAt = DateTime.Now;
                PrayerPlaceToUpdate.UpdBy = "LunarAdmin";

                var CallbackData = _PrayerPlaceService.Update(PrayerPlaceToUpdate);
                _PrayerPlaceService.CommitChanges();

                MessageContract.Code = HttpStatusCode.OK.ToString();
                MessageContract.Message = "Save succeed";
                return Ok(MessageContract);
            }
            catch (Exception ex)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Failed to change the status - " + ex.Message;
                return new BadRequestObjectResult(MessageContract);
            }
        }
    }
}
