using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Request
{
    public class UserDislikeRequestContract
    {
        public Guid PrayerPlaceId { get; set; }
        public Guid UserId { get; set; }
        public bool? isDislike { get; set; }
    }
}
