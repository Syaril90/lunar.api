using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Models
{
    public class PrayerPlace : ModelBase
    {
        public PrayerPlace()
        {
            Photos = new HashSet<Photo>();
            UserActions = new HashSet<UserAction>();
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Status{ get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public string Remarks { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public virtual User User { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<UserAction> UserActions { get; set; }
    }
}
