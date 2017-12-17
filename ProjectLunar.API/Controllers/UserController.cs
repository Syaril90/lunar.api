using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectLunar.API.Interfaces;
using ProjectLunar.API.Contracts.Request;
using ProjectLunar.API.Models;
using ProjectLunar.API.Contracts.Response;
using System.Net;

namespace ProjectLunar.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _UserService;
        private readonly IUserActionService _UserActionService;

        public UserController(IUserService UserService, IUserActionService UserActionService)
        {
            _UserService = UserService;
            _UserActionService = UserActionService;
        }

        [HttpPost("SaveUser")]
        public IActionResult SaveUser([FromBody]UserRequestContract Request)
        {
            var MessageContract = new MessageResponseContract();
            var UserAuthResponseContract = new UserAuthenticateResponseContract();
            var CurrentUser = _UserService.GetUserbyAuthId(Request.AuthId);

            if (Request == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Cannot stringify the json";

                return new BadRequestObjectResult(MessageContract);
            }

            if (CurrentUser != null)
            {
                UserAuthResponseContract.UserId = CurrentUser.Id;
                UserAuthResponseContract.isNewUser = false;
                return Ok(UserAuthResponseContract);
            }

            try
            {
                var User = new User();
                User.Id = Guid.NewGuid();
                User.AuthId = Request.AuthId;
                User.AuthFrom = Request.AuthFrom;
                User.InsAt = DateTime.Now;
                User.InsBy = "LunarAdmin";
                User.IsDeleted = false;

                var CallbackData = _UserService.Create(User);
                _UserService.CommitChanges();

                UserAuthResponseContract.UserId = CallbackData.Id;
                UserAuthResponseContract.isNewUser = true;

                return Ok(UserAuthResponseContract);

            }
            catch (Exception ex)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Failed to save - " + ex.Message;
                return new BadRequestObjectResult(MessageContract);
            }
        }

        [HttpGet("DeleteUser")]
        public IActionResult DeleteUser(Guid Id)
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
                var UserToDelete = _UserService.Get(Id);
                UserToDelete.IsDeleted = true;
                UserToDelete.UpdAt = DateTime.Now;
                UserToDelete.UpdBy = "LunarDB";
                _UserService.Update(UserToDelete);
                _UserService.CommitChanges();

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

        [HttpPost("Like")]
        public IActionResult Like([FromBody]UserLikeRequestContract Request)
        {
            var MessageContract = new MessageResponseContract();
            
            if (Request == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Cannot stringify the json";

                return new BadRequestObjectResult(MessageContract);
            }
            
            if (Request.UserId == null || Request.UserId == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Unauthorized user";

                return new BadRequestObjectResult(MessageContract);
            }

            var isNewAction = _UserActionService.isNewAction(Request.UserId, Request.PrayerPlaceId);

            if (isNewAction == false)
            {
                var ExistingAction = _UserActionService.Get(Request.UserId, Request.PrayerPlaceId);

                ExistingAction.isDislike = null;
                ExistingAction.isLike = Request.isLike;
                ExistingAction.UpdAt = DateTime.Now;
                ExistingAction.UpdBy = "LunarAdmin";

                _UserActionService.Update(ExistingAction);
                _UserActionService.CommitChanges();

                MessageContract.Code = HttpStatusCode.OK.ToString();
                MessageContract.Message = "Succeed";

                return Ok(MessageContract);
            }
            
            var UserAction = new UserAction();

            UserAction.Id = Guid.NewGuid();
            UserAction.isLike = Request.isLike;
            UserAction.UserId = Request.UserId;
            UserAction.PrayerPlaceId = Request.PrayerPlaceId;
            UserAction.InsAt = DateTime.Now;
            UserAction.InsBy = "LunarAdmin";
            UserAction.IsDeleted = false;

            MessageContract.Code = HttpStatusCode.OK.ToString();
            MessageContract.Message = "Succeed";

            _UserActionService.Create(UserAction);
            _UserActionService.CommitChanges();

            return Ok(MessageContract);
        }

        [HttpPost("Dislike")]
        public IActionResult Dislike([FromBody]UserDislikeRequestContract Request)
        {
            var MessageContract = new MessageResponseContract();
           

            if (Request == null)
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Cannot stringify the json";

                return new BadRequestObjectResult(MessageContract);
            }

            if (Request.UserId == null || Request.UserId == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Unauthorized user";

                return new BadRequestObjectResult(MessageContract);
            }

            var isNewAction = _UserActionService.isNewAction(Request.UserId, Request.PrayerPlaceId);

            if (isNewAction == false)
            {
                var ExistingAction = _UserActionService.Get(Request.UserId, Request.PrayerPlaceId);

                ExistingAction.isLike = null;
                ExistingAction.isDislike = Request.isDislike;
                ExistingAction.UpdAt = DateTime.Now;
                ExistingAction.UpdBy = "LunarAdmin";

                _UserActionService.Update(ExistingAction);
                _UserActionService.CommitChanges();

                MessageContract.Code = HttpStatusCode.OK.ToString();
                MessageContract.Message = "Succeed";

                return Ok(MessageContract);
            }

            var UserAction = new UserAction();

            UserAction.Id = Guid.NewGuid();
            UserAction.isDislike = Request.isDislike;
            UserAction.UserId = Request.UserId;
            UserAction.PrayerPlaceId = Request.PrayerPlaceId;
            UserAction.InsAt = DateTime.Now;
            UserAction.InsBy = "LunarAdmin";
            UserAction.IsDeleted = false;

            MessageContract.Code = HttpStatusCode.OK.ToString();
            MessageContract.Message = "Succeed";

            _UserActionService.Create(UserAction);
            _UserActionService.CommitChanges();

            return Ok(MessageContract);
        }
        
        [HttpGet("DeleteUserAction")]
        public IActionResult DeleteUserAction(Guid UserId, Guid PrayerPlaceId)
        {
            var MessageContract = new MessageResponseContract();

            if (UserId == null || UserId == new Guid("00000000-0000-0000-0000-000000000000") || PrayerPlaceId == null || PrayerPlaceId == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "All data are compulsory";

                return new BadRequestObjectResult(MessageContract);
            }

            try
            {
                var UserToDelete = _UserActionService.Get(UserId, PrayerPlaceId);
                UserToDelete.IsDeleted = true;
                UserToDelete.UpdAt = DateTime.Now;
                UserToDelete.UpdBy = "LunarDB";
                _UserActionService.Update(UserToDelete);
                _UserActionService.CommitChanges();

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
    }
}
