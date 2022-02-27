using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartLineItem> Items { get; set; } = new List<ShoppingCartLineItem>();
        public void AddItem (Books book, int qty)
        {
            ShoppingCartLineItem line = Items
                .Where(b => b.Books.BookId == book.BookId)
                .FirstOrDefault();

            if (line == null)
            {
                Items.Add(new ShoppingCartLineItem
                {
                    Books = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }
        public double CalculateTotal()
        {
            decimal sum = Items.Sum(x => x.Quantity * x.Books.Price);
            return (double)sum;
        }
    }

    public class ShoppingCartLineItem
    {
        public int LineID { get; set; }
        public Books Books { get; set; }
        public int Quantity { get; set; }
    }
}
