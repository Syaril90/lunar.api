using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Request
{
    public class UserLikeRequestContract
    {
        //public Guid Id { get; set; }
        public Guid PrayerPlaceId { get; set; }
        public Guid UserId { get; set; }
        public bool? isLike { get; set; }
    }
}
