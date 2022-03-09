using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OnlineBookstore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class SessionShoppingCart : ShoppingCart
    {
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionShoppingCart shoppingcart = session?.GetJson<SessionShoppingCart>("ShoppingCart") ?? new SessionShoppingCart();
            shoppingcart.Session = session;
            return shoppingcart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Books book, int qty)
        {
            base.AddItem(book, qty);
            Session.SetJson("ShoppingCart", this);
        }

        public override void RemoveItem(Books book)
        {
            base.RemoveItem(book);
            Session.SetJson("ShoppingCart", this);
        }
        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("ShoppingCart");
        }

    }
}
