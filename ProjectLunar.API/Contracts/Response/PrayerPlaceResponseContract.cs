using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Response
{
    public class PrayerPlaceResponseContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
    }
}
