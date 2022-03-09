using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public interface IOrderRespository
    {
        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}
