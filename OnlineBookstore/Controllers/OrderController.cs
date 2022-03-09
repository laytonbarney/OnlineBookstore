using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRespository repo { get; set; }
        private ShoppingCart shoppingcart { get; set; }
        public OrderController (IOrderRespository temp, ShoppingCart sc)
        {
            repo = temp;
            shoppingcart = sc;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (shoppingcart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your shopping cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = shoppingcart.Items.ToArray();
                repo.SaveOrder(order);
                shoppingcart.ClearBasket();

                return RedirectToPage("/Confirmation");
            }

            else
            {
                return View();
            }
        }
    }
}
