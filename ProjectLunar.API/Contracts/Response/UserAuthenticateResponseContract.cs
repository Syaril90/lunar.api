using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Response
{
    public class UserAuthenticateResponseContract
    {
        public Guid UserId { get; set; }
        public bool? isNewUser { get; set; }
    }
}
