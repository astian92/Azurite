using System;
using System.Collections.Generic;
using System.Linq;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Wrappers;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface IOrdersWorker
    {
        OrderW Get(Guid id);

        IQueryable<OrderW> GetAll();

        IQueryable<OrderViewModel> GetAllVm();

        IList<OrderStatusW> GetOrderStatuses();

        ITicket Update(Guid orderid, int statusId, string notes);
    }
}