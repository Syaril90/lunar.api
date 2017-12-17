using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Request
{
    public class ChangeStatusRequestContract
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
