using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade_BootCamp.Controllers
{
    public class EmployeeController : Controller
    {
        NorthwindContext context = new NorthwindContext();

        //EmployeesList
        public IActionResult List()
        {
            List<EmployeeListViewModel> employeesDto = new List<EmployeeListViewModel>();
            employeesDto = context.Employees.Select(x => new EmployeeListViewModel
            {
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title = x.Title,
            }).ToList();

            return View(employeesDto);

            //Query Expression
            //var query = (from e in context.Employees
            //             select new EmployeeDTO()
            //             {
            //                FirstName = e.FirstName,
            //                LastName = e.LastName,
            //                Title = e.Title,
            //             }).ToList();
            //return View(query);
        }

        //Yeni Çalışan FormEkranı
        [HttpGet]
        public IActionResult Create()
        {
            //Seçim listesi kutucuğu oluşturma:
            //SelectListItem hazır metodumuzdan instance oluşturup içini doldurup ViewBag ile EmployeeForm view sayfasına taşırız
            //Personel listesindeki herkes yeni eklenen kişinin amiri olabilir.Kişilerin hepsini seçim kutusuna liste olrak getirelim:
            List<SelectListItem> selectList= new List<SelectListItem>();
            foreach(var item in context.Employees.Select (a => new {a.EmployeeId, a.FirstName, a.LastName }))
            {
                selectList.Add(new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.EmployeeId.ToString() });
            }

            ViewBag.Employees = selectList;

            return View("EmployeeForm", new EmployeeCreateViewModel() );
        }

        //Yeni Çalışan FormEkranından Ekleme
        [HttpPost]
        public IActionResult Create(EmployeeListViewModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                context.Employees.Add(new Employee
                {
                    FirstName = employeeModel.FirstName,
                    LastName = employeeModel.LastName,
                    Title = employeeModel.Title,
                });
                context.SaveChanges();
                return RedirectToAction(nameof(List));

            }
            return View("EmployeeForm", new EmployeeListViewModel());
        }
        //public IActionResult Create(EmployeeDTO employeeModel)
        //{
        //    Employee employee = new Employee();
        //    employee.EmployeeId = employeeModel.EmployeeId;
        //    employee.FirstName = employeeModel.FirstName;
        //    employee.LastName = employeeModel.LastName;
        //    employee.Title = employeeModel.Title;
        //    context.Employees.Add(employee);
        //    context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
