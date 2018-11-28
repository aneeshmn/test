using System.Web;
using System.Web.Mvc;

namespace FSE_19_ADODotNet_2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
