using ETrade_BootCamp.Extentions;
using ETrade_BootCamp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETrade_BootCamp.Controllers
{
    public abstract class EtradeBaseController : Controller
    {
        protected NorthwindContext context;  //protected çocuklarda erişmemizi sağlar

        public EtradeBaseController()
        {
            context = new NorthwindContext();
        }

        protected void SetValueToSession<T>(string key, T instance)
        {
            HttpContext.Session.SetObject<T>(key, instance);
        }

        protected T GetValueFromSession<T>(string key)
        {
            return HttpContext.Session.GetObject<T>(key);
        }

        //Sessionda böyle bir değer var mı
        protected bool IsExistInSession(string key)
        {
            return HttpContext.Session.Keys.Contains(key);
        }

        //Sessionı temizle
        protected void ClearSession(string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                HttpContext.Session.Clear();  //Session boşsa hepsini temizle
            }
            else
            {
                HttpContext.Session.Remove(key);  //ilgili key değerindeki değeri temizle
            }
        }
    }
}
