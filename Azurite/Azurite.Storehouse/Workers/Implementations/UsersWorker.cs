using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Models.Helpers;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class UsersWorker : IUsersWorker
    {
        private readonly IRepository<User> _rep;

        public UsersWorker(IRepository<User> rep)
        {
            this._rep = rep;
        }

        public UserW Get(Guid Id)
        {
            var user = _rep.Get(Id);
            var userW = Mapper.Map<UserW>(user);

            return userW;
        }

        public IQueryable<UserW> GetAll()
        {
            return _rep.GetAll()
                .Where(u => u.Username != "master") //ignore master!
                .ProjectTo<UserW>();
        }

        public ITicket Add(UserW userW)
        {
            try
            {
                var user = Mapper.Map<User>(userW);
                user.Id = Guid.NewGuid();
                _rep.Add(user);

                _rep.Save();

                return new Ticket(true);
            }
            catch (Exception exc)
            {
                ElmahHelper.Handle(exc);
                return new Ticket(false, "Възникна проблем при добавянето на потребител!");
            }
        }

        public ITicket Edit(UserW userW)
        {
            try
            {
                var user = _rep.Get(userW.Id);

                user.Username = userW.Username;
                user.Password = userW.Password;
                user.FirstName = userW.FirstName;
                user.LastName = userW.LastName;

                _rep.Save();

                return new Ticket(true);
            }
            catch (Exception exc)
            {
                ElmahHelper.Handle(exc);
                return new Ticket(false, "Възникна проблем при промяната на потребител!");
            }
        }

        public ITicket Delete(Guid Id)
        {
            try
            {
                _rep.Remove(Id);
                _rep.Save();
            }
            catch (DbUpdateException exc)
            {
                ElmahHelper.Handle(exc);
                return new Ticket(false, "Опитвате се да изтриете запис, който се използва във връзка с други обекти!");
            }
            catch (SqlException exc)
            {
                ElmahHelper.Handle(exc);
                return new Ticket(false, "Възникна проблем при работа с базата!");
            }
            catch (Exception exc)
            {
                ElmahHelper.Handle(exc);
                string excName = exc.GetType().Name;
                return new Ticket(false, "Възникна неочаквана грешка!");
            }

            return new Ticket(true);
        }
    }
}