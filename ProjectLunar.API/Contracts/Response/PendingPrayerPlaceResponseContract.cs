using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Response
{
    public class PendingPrayerPlaceResponseContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public string Category { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime? InsAt { get; set; }
        public string InsBy { get; set; }
    }
}
