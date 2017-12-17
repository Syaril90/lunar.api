using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Models
{
    public class ModelBase
    {
        public DateTime? InsAt { get; set; }
        public string InsBy { get; set; }
        public DateTime? UpdAt { get; set; }
        public string UpdBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
