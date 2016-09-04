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

        public IQueryable<UserW> GetAll()
        {
            return rep.GetAll()
                .ProjectTo<UserW>();
        }
    }
}