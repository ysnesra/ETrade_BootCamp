using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade_BootCamp.Controllers
{
    public class EmployeeController : EtradeBaseController
    {
       //NorthwindContext context = new NorthwindContext();

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
            //List<SelectListItem> selectList= new List<SelectListItem>();
            
            //foreach(var item in context.Employees.Select (a => new {a.EmployeeId, a.FirstName, a.LastName }))
            //{
            //    selectList.Add(new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.EmployeeId.ToString() });
            //}

            //ViewBag.Employees = selectList;

            //SelectList kodlarımızı EmployeeViewComponente taşıdık

            return View("EmployeeFormFirst", new EmployeeCreateViewModel() );
        }

        //Yeni Çalışan FormEkranından Ekleme
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel employeeModel)
        {
            //Sesiondaki veri varsa birinci ekrandan geliyor,
            //Veri yoksa birinci ekrana ilk defa geliyor

            if (IsExistInSession("NewEmp"))  //Session doluysa ikinci ekran birinci ekrandan geliyordur
            {
                Employee employee = GetValueFromSession<Employee>("NewEmp");  //Sessiondaki NewEmp içindeki datayı aldık
                IEnumerable<string> selectedIDs = employeeModel.Territories.Where(a => a.IsSelected).Select(a => a.TerritoryID);  //IsSelected true olanların TerritoryID lerini listele 

                foreach (Territory item in context.Territories.Where(a => selectedIDs.Contains(a.TerritoryId)))
                {
                    employee.Territories.Add(item);
                }

                context.Employees.Add(employee);
                context.SaveChanges();

                ClearSession("NewEmp");

                return RedirectToAction(nameof(List));
            }
            else   //birinci ekran ilk defa geliyor
            {
                Employee employee = new Employee();             
                employee.FirstName = employeeModel.FirstName;
                employee.LastName = employeeModel.LastName;
                employee.Title = employeeModel.Title;
                employee.ReportsTo = employeeModel.Reports;

                SetValueToSession<Employee>("NewEmp", employee);

                //Modelden gelen Territories listesini; veritabanındaki değerlerle dolduralım
                employeeModel.Territories = context.Territories.Select(a => new EmployeeTerritoryViewModel()
                {
                    IsSelected = false,
                    Name = a.TerritoryDescription,
                    TerritoryID = a.TerritoryId
                }).ToList();

                return View("EmployeeFormSecond", employeeModel /*new EmployeeCreateViewModel()*/);
            }

            //İlk yaptığım
            //if (ModelState.IsValid)
            //{
            //    context.Employees.Add(new Employee
            //    {
            //        FirstName = employeeModel.FirstName,
            //        LastName = employeeModel.LastName,
            //        Title = employeeModel.Title,
            //        ReportsTo=employeeModel.Reports
            //    });
            //    context.SaveChanges();
            //    return RedirectToAction(nameof(List));

            //}
            //return View("EmployeeForm", new EmployeeListViewModel());
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
