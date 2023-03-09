using ETrade_BootCamp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade_BootCamp.Components
{
    public class EmployeeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int? employeeId,int? reportsId) 
        {
            NorthwindContext context = new NorthwindContext();
            IQueryable<Employee> employees = context.Employees;

            //Filtreli(where) SelectList
            if (employeeId.HasValue) //Eğer ID geliyorsa Güncellleme ekranıdır.Where'li SelectList yapılcak
            {
                employees = employees.Where(a => a.EmployeeId != employeeId.Value);
            }

            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in employees.Select(a => new { a.FirstName, a.LastName, a.EmployeeId }))
            {
                selectList.Add(new SelectListItem()
                {
                    Text = item.FirstName + " " + item.LastName,
                    Value = item.EmployeeId.ToString(),
                    Selected = reportsId.HasValue ? item.EmployeeId == reportsId.Value : false
                    //Selected = item.EmployeeId == reportsId
                });
            }

            return View(selectList);
        }  
    }
}
