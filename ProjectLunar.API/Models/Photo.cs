using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Models
{
    public class Photo : ModelBase
    {
        public Guid Id { get; set; }
        public Guid PrayerPlaceId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] File { get; set; }

        public virtual PrayerPlace PrayerPlace { get; set; }
    }
}
