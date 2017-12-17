using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Request
{
    public class PhotoRequestContract
    {
        public string File { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
    }
}
