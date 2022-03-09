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
        public ShoppingCartModel (IBookstoreRepository temp, ShoppingCart sc)
        {
            repo = temp;
            shoppingcart = sc;
        }
        public ShoppingCart shoppingcart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Books b = repo.Books.FirstOrDefault(b => b.BookId == bookId);

            shoppingcart.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            shoppingcart.RemoveItem(shoppingcart.Items.First(x => x.Books.BookId == bookId).Books);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
