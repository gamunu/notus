using System.Web.Mvc;

namespace Notus.Hub
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           // filters.Add(new RequireHttpsAttribute());
        }
    }
}