using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class UsersWorker : IUsersWorker
    {
        private readonly IRepository<User> rep;

        public UsersWorker(IRepository<User> rep)
        {
            this.rep = rep;
        }

        public UserW Get(Guid Id)
        {
            var user = rep.Get(Id);
            var userW = Mapper.Map<UserW>(user);

            return userW;
        }

        public IQueryable<UserW> GetAll()
        {
            return rep.GetAll()
                .Where(u => u.Username != "master") //ignore master!
                .ProjectTo<UserW>();
        }

        public void Add(UserW userW)
        {
            var user = Mapper.Map<User>(userW);
            user.Id = Guid.NewGuid();
            rep.Add(user);

            rep.Save();
        }

        public void Edit(UserW userW)
        {
            var user = rep.Get(userW.Id);

            user.Username = userW.Username;
            user.Password = userW.Password;
            user.FirstName = userW.FirstName;
            user.LastName = userW.LastName;

            rep.Save();
        }

        public void Delete(Guid Id)
        {
            rep.Remove(Id);
            rep.Save();
        }
    }
}