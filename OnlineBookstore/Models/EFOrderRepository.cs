using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class EFOrderRepository : IOrderRespository
    {
        private BookstoreContext context;
        public EFOrderRepository (BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Order> Orders => context.Orders.Include(x => x.Lines).ThenInclude(x => x.Books);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(x => x.Books));

            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }

            context.SaveChanges();
        }
    }
}
