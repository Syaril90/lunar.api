using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Request
{
    public class UserRequestContract
    {
        public string AuthId { get; set; }
        public string AuthFrom { get; set; }
    }
}
