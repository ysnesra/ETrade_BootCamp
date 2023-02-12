using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace ETrade_BootCamp.Controllers
{
    public class EmployeeController : Controller
    {
        NorthwindContext context = new NorthwindContext();

        //EmployeesList
        public IActionResult Index()
        {
            List<EmployeeDTO> employeesDto = new List<EmployeeDTO>();
            employeesDto = context.Employees.Select(x => new EmployeeDTO
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
            return View("EmployeeForm", new EmployeeDTO() );
        }

        //Yeni Çalışan FormEkranından Ekleme
        [HttpPost]
        public IActionResult Create(EmployeeDTO employeeModel)
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
                return RedirectToAction("Index");

            }
            return View("EmployeeForm", new EmployeeDTO());
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
