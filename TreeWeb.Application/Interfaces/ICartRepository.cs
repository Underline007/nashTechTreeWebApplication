using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeWeb.Core.Entities;

namespace TreeWeb.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<int> AddItem(int productId, int qty);
        Task<int> RemoveItem(int productId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout();
    }
}
