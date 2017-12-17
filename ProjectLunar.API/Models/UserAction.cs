using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Models
{
    public class UserAction : ModelBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PrayerPlaceId { get; set; }
        public bool? isLike { get; set; }
        public bool? isDislike { get; set; }

        public virtual User User { get; set; }
        public virtual PrayerPlace PrayerPlace { get; set; }
    }
}
