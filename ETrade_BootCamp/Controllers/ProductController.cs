using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETrade_BootCamp.Controllers
{
    public class ProductController : Controller
    {
        NorthwindContext context = new NorthwindContext();

        //ProductsList
        //orderId geliyor.OrderDetails üzerinden ulaşacağız.Sadece Product ile OrderDetails Joinlemek yeterli

        public IActionResult Index(int orderId)
        {
            List<ProductListViewModel> products = context.OrderDetails.Include(a => a.Product)
                                      .Where(a => a.OrderId == orderId)
                                      .Select(a => new ProductListViewModel()
                                      {
                                          ProductName = a.Product.ProductName,
                                          Quantity = a.Quantity
                                      }).ToList();

            //Sayfanın başında şu nolu siparişin ürünleri yazsın istiyorum.
            //ViewBag ile id yi göndermiş oluruz.Bu id yi yani sipariş numarasını ekrana yazdırcaz
            ViewBag.OrderNo = orderId;   //Viewbag ile view tarafına gönderiyoruz
            return View(products);


            //var query = (from o in context.Orders
            //             join od in context.OrderDetails on o.OrderId equals od.OrderId
            //             join p in context.Products on od.ProductId equals p.ProductId
            //             where o.OrderId == orderId
            //             select new ProductDTO
            //             {
            //                 ProductId = od.ProductId,
            //                 ProductName = p.ProductName,
            //                 QuantityPerUnit = p.QuantityPerUnit,
            //             }).ToList();
            //return View(query);
        }
    }
}
