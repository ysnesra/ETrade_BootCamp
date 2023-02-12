using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETrade_BootCamp.Controllers
{
    public class ProductController : Controller
    {
        NorthwindContext context = new NorthwindContext();

        //ProductsList
        public IActionResult Index(int orderId)
        {
            var query = (from o in context.Orders
                         join od in context.OrderDetails on o.OrderId equals od.OrderId
                         join p in context.Products on od.ProductId equals p.ProductId
                         where o.OrderId == orderId
                         select new ProductDTO
                         {
                             ProductId = od.ProductId,
                             ProductName=p.ProductName,
                             QuantityPerUnit=p.QuantityPerUnit,
                         }).ToList();
            return View(query);
        }
    }
}
