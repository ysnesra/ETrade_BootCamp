using AutoMapper;
using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ETrade_BootCamp.Controllers
{
    public class EmployeeAjaxController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public EmployeeAjaxController(IMapper mapper, NorthwindContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public IActionResult List()
        {
            return View();
        }

        //Bu actionı tetiklendiğinde  _EmployeeListPartial.cshtml sayfasına gider.Burada modelden veriler yüklenir
        public IActionResult EmployeeListPartial()
        {
            List<EmployeeListViewModel> employeesDto = _context.Employees.ToList().Select(x => _mapper.Map<EmployeeListViewModel>(x)).ToList();

            return PartialView("_EmployeeListPartial", employeesDto);
        }

        [HttpGet]
        public IActionResult AddNewEmployeePartial()
        //Bu actionı tetiklendiğinde  _AddNewUserPartial.cshtml sayfasına gider.Burada modeli boş göndereceğiz.Yeni kullanıcı eklediğimiz için
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var item in _context.Employees.Select(a => new { a.EmployeeId, a.FirstName, a.LastName }))
            {
                selectList.Add(new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.EmployeeId.ToString() });
            }

            ViewBag.Employees = selectList;
            return PartialView("_AddNewEmployeePartial", new EmployeeCreateViewModel());
        }
        [HttpPost]
        public IActionResult AddNewEmployeePartial(EmployeeCreateViewModel employeeModel)
        //Bu action _AddNewUserPartial.cshtml sayfasındaki modal tarafından tetiklenir.
        {
            ModelState.Remove("Territories");
            if (ModelState.IsValid)
            {
                if (_context.Employees.Any(x => x.FirstName.ToLower() == employeeModel.FirstName.ToLower() && x.LastName.ToLower() == employeeModel.LastName.ToLower()))
                {
                    ModelState.AddModelError(employeeModel.FirstName, "Bu personel adı ve soyadı kullanılmaktadır");
                    return View("EmployeeForm", employeeModel);
                }

                //AutoMapper Yönt
                Employee employee = _mapper.Map<Employee>(employeeModel);
                _context.Employees.Add(employee);

                _context.SaveChanges();
                return PartialView("_AddNewEmployeePartial", new EmployeeCreateViewModel { Done = "Kullanıcı eklendi" });
                //ekledikten sonra kullanıcıya bilgi vermek için EmployeeCreateViewModel Modele "Done" propertysi ekledim.
                //ekledikten sonra modelin hepsini dönderme sadece Done kısmını doldurup bilgi ver
            }
            return PartialView("_AddNewEmployeePartial", employeeModel);
        }


        //Çalışan Güncelleme ModalPop-up Form Ekranı
        [HttpGet]
        public IActionResult EditEmployeePartial(int employeeId)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in _context.Employees.Select(a => new { a.EmployeeId, a.FirstName, a.LastName }))
            {
                selectList.Add(new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.EmployeeId.ToString() });
            }
            ViewBag.Employees = selectList;


            Employee employee = _context.Employees.Find(employeeId);
            EmployeeEditViewModel model = _mapper.Map<EmployeeEditViewModel>(employee);

            return View("_EditEmployeePartial", model);
        }

        //Çalışan Güncelleme 
        [HttpPost]
        public IActionResult EditEmployeePartial(int employeeId, EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _context.Employees.Find(employeeId); //veritabnından personeli bul
                _mapper.Map(model, employee);  //modelde yeni girilen değerleri employee'ye kopyala
                _context.SaveChanges();
                return PartialView("_EditEmployeePartial", new EmployeeEditViewModel { Done = "Kullanıcı güncellendi" });
            }
            return PartialView("_EditEmployeePartial", model);
        }

        public IActionResult DeleteEmployeePartial(int employeeId)
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
            return EmployeeListPartial();
        }


    }
}