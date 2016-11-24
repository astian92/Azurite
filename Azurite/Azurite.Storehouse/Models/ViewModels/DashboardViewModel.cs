﻿using Azurite.Storehouse.Wrappers.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models.ViewModels
{
    public class DashboardViewModel
    {
        public OrdersCountList OrdersCounts { get; set; }
        public List<MiniOrder> IssuedOrders { get; set; }
        public List<MiniOrder> InProgressOrders { get; set; }
        public List<MiniProduct> DecreasingQuantityProducts { get; set; }

        public DashboardViewModel()
        {
            this.OrdersCounts = new OrdersCountList();
            this.IssuedOrders = new List<MiniOrder>();
            this.InProgressOrders = new List<MiniOrder>();
            this.DecreasingQuantityProducts = new List<MiniProduct>();
        }
    }
}