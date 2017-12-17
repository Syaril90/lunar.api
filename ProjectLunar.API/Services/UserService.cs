using ProjectLunar.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectLunar.API.Models;

namespace ProjectLunar.API.Services
{
    public class UserService : IUserService
    {
        private IEntityRepository<User> _User;

        public UserService(IEntityRepository<User> User)
        {
            _User = User;
        }

        public User Get(Guid id)
        {
            var item = _User.Get(x => x.Id == id).FirstOrDefault();

            return item;
        }

        public IQueryable<User> GetAll()
        {
            var item = _User.GetAll();

            return item;
        }

        public User Create(User request)
        {
            _User.InsertOnCommit(request);
            return request;
        }

        public User Update(User request)
        {
            var item = _User.Get(x => x.Id == request.Id).FirstOrDefault();
            _User.UpdateOnCommit(request);
            return request;
        }

        public void Delete(Guid id)
        {
            var item = _User.Get(x => x.Id == id).FirstOrDefault();

            if (item != null)
            {
                _User.DeleteOnCommit(item);
            }
        }

        public void CommitChanges()
        {
            _User.CommitChanges();
        }

        public User GetUserbyAuthId(string AuthId)
        {
            return _User.GetAll().Where(x => x.AuthId == AuthId).FirstOrDefault();
        }

    }
}
