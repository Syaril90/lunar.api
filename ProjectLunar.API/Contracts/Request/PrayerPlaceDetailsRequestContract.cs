using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Request
{
    public class PrayerPlaceDetailsRequestContract
    {
        public Guid PrayerPlaceId { get; set; }
        public Guid UserId { get; set; }
    }
}
