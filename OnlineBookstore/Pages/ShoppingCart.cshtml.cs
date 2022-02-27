using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookstore.Infrastructure;
using OnlineBookstore.Models;

namespace OnlineBookstore.Pages
{
    public class ShoppingCartModel : PageModel
    {
        public IBookstoreRepository repo { get; set; }
        public ShoppingCartModel (IBookstoreRepository temp)
        {
            repo = temp;
        }
        public ShoppingCart shoppingcart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            shoppingcart = HttpContext.Session.GetJson<ShoppingCart>("shoppingcart") ?? new ShoppingCart();
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Books b = repo.Books.FirstOrDefault(b => b.BookId == bookId);
            shoppingcart = HttpContext.Session.GetJson<ShoppingCart>("shoppingcart") ?? new ShoppingCart();
            shoppingcart.AddItem(b, 1);

            HttpContext.Session.SetJson("shoppingcart", shoppingcart);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
