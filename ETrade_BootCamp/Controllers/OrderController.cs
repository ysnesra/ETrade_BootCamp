using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using X.PagedList;

namespace ETrade_BootCamp.Controllers
{
    public class OrderController : Controller
    {
        NorthwindContext context = new NorthwindContext();

        //OrdersList
        public IActionResult Index(int employeeId, int page=1)  //employeeId bu id ile
        {   
            if(employeeId == 0)  
            {
                return RedirectToAction("List","Employee",employeeId);//EmployeeList ekranına dönsün
            }

            //Method Expression- EagerLoading ********
            //EmployeeId; OrderDetails tablosunda zaten var Employee tablosunu ilişkilendirmeye gerek yok 
            //Discount => Kdv hesaplanırken 1 den çıkarılıp çarpılır.
            //veritabanındaki OrderDetails tablosundan verileri çekip hesaplayıp modelimizdeki TotalPrice a atarız

            //******SAYFALAMA____sipariş listesini 10tane 10tane göstermesini sağlayabilriz. 
            IPagedList<OrderListViewModel> orders = context.Orders.Include(a => a.OrderDetails).Where(a => a.EmployeeId == employeeId).Select(a => new OrderListViewModel
            {
                OrderNo = a.OrderId,
                OrderCountry = a.ShipCountry,
                OrderDate = a.ShippedDate.HasValue ? a.OrderDate.Value.ToShortDateString() : "Tarihi Yok",
                TotalPrice = a.OrderDetails.Sum(od => od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount))
            }).ToPagedList(page,10);

            ViewBag.employeeId=employeeId;
            //List<OrderListViewModel> orders = context.Orders.Include(a => a.OrderDetails).Where(a => a.EmployeeId == employeeId).Select(a => new OrderListViewModel
            //{
            //    OrderNo = a.OrderId,
            //    OrderCountry = a.ShipCountry,
            //    OrderDate = a.ShippedDate.HasValue ? a.OrderDate.Value.ToShortDateString() : "Tarihi Yok",
            //    TotalPrice = a.OrderDetails.Sum(od => od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount))
            //}).ToList();

            return View(orders);

            //****Eğer Sipariş listesinde OrderDetails tablosundaki UnitPrice ,Quantity,Discount kolonlarını da görmek isteseydik:
            //ManyToMany ilişkiden dolayı SelectMany() kullanmalıyız
            //List<OrderListViewModel> orders = context.Orders.Include(a => a.OrderDetails).Where(a => a.EmployeeId == employeeId).SelectMany(a => a.OrderDetails, (o, d) => new OrderListViewModel() 
            //{
            //    OrderNo = o.OrderId,
            //    OrderCountry = o.ShipCountry,
            //    OrderDate = o.ShippedDate.HasValue ? o.OrderDate.Value.ToShortDateString() : "Tarihi Yok",
            //    UnitPrice=d.UnitPrice,
            //    Quantity = d.Quantity,
            //    Discount = d.Discount,
            //    TotalPrice = o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount))
            //}).ToList();
            //return View(orders);


            //****Joinle yazmak mantıklı değil burda Include ile yazmak doğru
            //var query = (from o in context.Orders
            //             join od in context.OrderDetails on o.OrderId equals od.OrderId
            //             where o.EmployeeId == employeeId
            //             select new OrderListViewModel
            //             {
            //                 OrderNo = o.OrderId,
            //                 OrderDate = o.ShippedDate.Value.ToShortDateString(),
            //                 OrderCountry = o.ShipCountry,
            //                 UnitPrice = od.UnitPrice,
            //                 Quantity = od.Quantity,
            //                 Discount = od.Discount,
            //                 TotalPrice = (od.UnitPrice - (od.UnitPrice * (decimal)od.Discount)) * od.Quantity
            //             }).ToList();
            //return View(query);


        }
    }
}


