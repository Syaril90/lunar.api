using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Request
{
    public class EditPrayerPlaceResponseContract
    {
        public Guid PrayerPlaceId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public string Remarks { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public Guid UserId { get; set; }
        public List<PhotoRequestContract> Photos { get; set; }
    }
}

