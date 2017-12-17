using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Models
{
    public class User : ModelBase
    {
        public User()
        {
            PrayerPlaces = new HashSet<PrayerPlace>();
            UserActions = new HashSet<UserAction>();
        }
        public Guid Id { get; set; }
        public string AuthId { get; set; }
        public string AuthFrom { get; set; }

        public ICollection<PrayerPlace> PrayerPlaces { get; set; }
        public ICollection<UserAction> UserActions { get; set; }
    }
}
