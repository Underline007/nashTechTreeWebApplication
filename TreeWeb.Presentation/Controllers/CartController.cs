using Microsoft.AspNetCore.Mvc;
using TreeWeb.Application.Interfaces;

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _context;
        public CartController(ICartRepository context)
        {
            _context = context;
        }
        public async Task<IActionResult> AddItem(int productId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _context.AddItem(productId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int productId)
        {
            var cartCount = await _context.RemoveItem(productId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _context.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _context.GetCartItemCount();
            return Ok(cartItem);
        }

        public async Task<IActionResult> Checkout()
        {
            bool isCheckedOut = await _context.DoCheckout();
            if (!isCheckedOut)
                throw new Exception("Something happen in server side");
            return RedirectToAction("Index", "Home");
        }
    }
}
