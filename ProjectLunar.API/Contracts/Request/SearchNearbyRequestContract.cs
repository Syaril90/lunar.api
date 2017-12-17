using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Request
{
    public class SearchNearbyRequestContract
    {
        public float Latitud { get; set; }
        public float Longitud { get; set; }
    }
}
