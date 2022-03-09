using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartLineItem> Items { get; set; } = new List<ShoppingCartLineItem>();
        public virtual void AddItem (Books book, int qty)
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

        public virtual void RemoveItem (Books book)
        {
            Items.RemoveAll(x => x.Books.BookId == book.BookId);
        }

        public virtual void ClearBasket()
        {
            Items.Clear();
        }
        public double CalculateTotal()
        {
            decimal sum = Items.Sum(x => x.Quantity * x.Books.Price);
            return (double)sum;
        }
    }

    public class ShoppingCartLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Books Books { get; set; }
        public int Quantity { get; set; }
    }
}
