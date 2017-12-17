using ProjectLunar.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Interfaces
{
    public interface IUserService
    {
        User Get(Guid id);
        IQueryable<User> GetAll();
        User Create(User request);
        User Update(User request);
        void Delete(Guid id);
        void CommitChanges();
        User GetUserbyAuthId(string AuthId);
    }
}
