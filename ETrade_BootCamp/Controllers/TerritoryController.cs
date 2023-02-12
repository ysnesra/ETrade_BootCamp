using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ETrade_BootCamp.Controllers
{
    public class TerritoryController : Controller
    {
        NorthwindContext context = new NorthwindContext();

        //TerritoriesLists
        public IActionResult Index(int employeeId)
        {
            //var query = (from e in context.Employees
            //             join t in context.Territories on new { e.EmployeeId, } equals new { t.TerritoryId }
            //             join r in context.Regions on t.RegionId equals r.RegionId
            //             where o.EmployeeId == employeeId
            //             select new TerritoryDTO
            //             {
            //                 EmployeeId = e.EmployeeId,
            //                 TerritoryId = t.TerritoryId,
            //                 RegionId = r.RegionId,
            //                 FirstName = r.FirstName,
            //                 LastName = r.LastName,
            //                 RegionDescription = r.RegionDescription
            //             }).ToList();
            //return View(query);

            //var query = context.Employees.Include(t => t.Territories).Where(x=>x.EmployeeId==2).ToList();

            var region = context.Employees.Include(t => t.Territories).ThenInclude(x => x.Region).Where(x=>x.EmployeeId== employeeId).FirstOrDefault().Territories
                .Select(x => new TerritoryDTO
                {
                     EmployeeId=x.Employees.FirstOrDefault().EmployeeId,
                     FirstName=x.Employees.FirstOrDefault().FirstName,
                     LastName=x.Employees.FirstOrDefault().LastName,
                     RegionDescription=x.Region.RegionDescription,
                     RegionId=x.Region.RegionId,
                     TerritoryId=x.TerritoryId
                }).ToList();

            return View(region);


        }
    }
}
