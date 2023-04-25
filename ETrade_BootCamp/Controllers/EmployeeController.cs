using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;

namespace ETrade_BootCamp.Controllers
{
    public class EmployeeController : EtradeBaseController
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(IMapper mapper, NorthwindContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        //EmployeesList
        public IActionResult List()
        {
            //List<EmployeeListViewModel> employeesDto = new List<EmployeeListViewModel>();
            //employeesDto = context.Employees.Select(x => new EmployeeListViewModel
            //{
            //    EmployeeId = x.EmployeeId,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    Title = x.Title,
            //}).ToList();
            List<EmployeeListViewModel> employeesDto = _context.Employees.ToList().Select(x => _mapper.Map<EmployeeListViewModel>(x)).ToList();

            return View(employeesDto);

           
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


        //Çalışan Güncelleme Form Ekranı
        [HttpGet]
        public IActionResult Edit(int employeeId)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in _context.Employees.Select(a => new { a.EmployeeId, a.FirstName, a.LastName }))
            {
                selectList.Add(new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.EmployeeId.ToString() });
            }
            ViewBag.Employees = selectList;


            Employee employee = _context.Employees.Find(employeeId);
            EmployeeEditViewModel model = _mapper.Map<EmployeeEditViewModel>(employee);

            return View("EmployeeFormEdit", model);
        }

        //Çalışan Güncelleme 
        [HttpPost]
        public IActionResult Edit(int employeeId, EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _context.Employees.Find(employeeId); //veritabnından personeli bul
                _mapper.Map(model, employee);  //modelde yeni girilen değerleri employee'ye kopyala
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            return View("EmployeeFormEdit", model);
        }

        //raw sql kullanarak dark olarak silelim
        //ilişkili olduğu tüm tablolardan tek tek sileriz
        public IActionResult Delete(int employeeId)
        {
            var rowsExisted = _context.Database
                                .ExecuteSqlRaw($"SELECT COUNT(*) FROM Employees WHERE EmployeeId='{employeeId}'");
            if (rowsExisted == -1)
            {
                var EmployeeTerritoriesdeleted = _context.Database
               .ExecuteSqlRaw($"DELETE FROM EmployeeTerritories WHERE EmployeeId='{employeeId}'");

                var Ordersdeleted = _context.Database
              .ExecuteSqlRaw($"DELETE FROM Orders WHERE EmployeeId='{employeeId}'");

                var Employeesdeleted = _context.Database
               .ExecuteSqlRaw($"DELETE FROM Employees WHERE EmployeeId='{employeeId}'");
            }
            return RedirectToAction(nameof(List));
        }


    }
}
