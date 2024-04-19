using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.UnitTest.Mocks
{
    public class OrderFEMocks
    {
        public static List<OrderFE> GetOrders()
        {
            List<OrderFE> orderFEs = new List<OrderFE>()
            {
                new OrderFE()
                {
                    Id = 1,
                    OrderId = 2,
                    CustomerId = 3,
                    StatusId = 1,
                    OrderDate   = DateTime.Now,
                    OrderValue = 1,
                    CustomerName = "Name",
                    OrderStatus = null,
                    ProdValue = 1,
                    ProcentRabate = 1,
                    CashBack = 1,
                    FutureCashBack = 1,
                },
                 new OrderFE()
                {
                    Id = 1,
                    OrderId = 2,
                    CustomerId = 3,
                    StatusId = 1,
                    OrderDate   = DateTime.Now,
                    OrderValue = 1,
                    CustomerName = "Name",
                    OrderStatus = null,
                    ProdValue = 1,
                    ProcentRabate = 1,
                    CashBack = 1,
                    FutureCashBack = 1,
                },
                  new OrderFE()
                {
                    Id = 1,
                    OrderId = 2,
                    CustomerId = 3,
                    StatusId = 1,
                    OrderDate   = DateTime.Now,
                    OrderValue = 1,
                    CustomerName = "Name",
                    OrderStatus = null,
                    ProdValue = 1,
                    ProcentRabate = 1,
                    CashBack = 1,
                    FutureCashBack = 1,
                },
                   new OrderFE()
                {
                    Id = 1,
                    OrderId = 2,
                    CustomerId = 3,
                    StatusId = 1,
                    OrderDate   = DateTime.Now,
                    OrderValue = 1,
                    CustomerName = "Name",
                    OrderStatus = null,
                    ProdValue = 1,
                    ProcentRabate = 1,
                    CashBack = 1,
                    FutureCashBack = 1,
                }
            };
          return orderFEs;
        }
    }
}
