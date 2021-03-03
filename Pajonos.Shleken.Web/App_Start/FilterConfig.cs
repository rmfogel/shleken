using System.Web;
using System.Web.Mvc;

namespace Pajonos.Shleken.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalsFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
