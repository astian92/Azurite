using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Wrappers;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface ICategoriesWorker
    {
        CategoryW Get(Guid Id);

        IQueryable<CategoryW> GetAll();

        IQueryable<CategoryIndexViewModel> GetAllWithoutParents();

        Task<ITicket> Add(CategoryW categoryW, HttpPostedFileBase photo);

        Task<ITicket> Edit(CategoryW categoryW, HttpPostedFileBase photo, bool deleted);

        ITicket Delete(Guid Id);
    }
}