using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ETrade_BootCamp.Controllers
{
    public class OrderController : Controller
    {
        NorthwindContext context = new NorthwindContext();

        //OrdersList
        public IActionResult Index(int employeeId)
        {       
            var query = (from o in context.Orders
                        join e in context.Employees on o.EmployeeId equals e.EmployeeId
                        join od in context.OrderDetails on o.OrderId equals od.OrderId
                        where o.EmployeeId == employeeId 
                        select new OrderDTO
                        {
                            EmployeeId=e.EmployeeId,
                            OrderId = o.OrderId,
                            ShippedDate = o.ShippedDate,
                            ShipCountry = o.ShipCountry,
                            UnitPrice = od.UnitPrice,
                            Quantity = od.Quantity,
                            Discount = od.Discount,
                            TotalPrice =(od.UnitPrice-(od.UnitPrice *(decimal)od.Discount))*od.Quantity
                        }).ToList();

            return View(query);

            //Method Expression doğru olmadı
            //var model = context.Employees.Include(e => e.Orders).ThenInclude(o => o.OrderDetails)
            //    .Where(x => x.EmployeeId == employeeId).Select(x => new OrderDTO
            //    {
            //        EmployeeId = x.EmployeeId,
            //        OrderId = x.Orders.FirstOrDefault().OrderId,
            //        ShippedDate = x.OrderDetails.ShippedDate,
            //        ShipCountry = e.ShipCountry,
            //        UnitPrice = o.UnitPrice,
            //        Quantity = o.Quantity,
            //        Discount = o.Discount,
            //        TotalPrice = (od.UnitPrice - (od.UnitPrice * (decimal)od.Discount)) * od.Quantity
            //    }).ToList();
            //return View(model);

           
        }
    }
}


