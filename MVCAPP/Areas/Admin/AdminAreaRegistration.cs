using System.Web.Mvc;

namespace MVCAPP.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}/{*catchall}",
                new { action = "Index", id = UrlParameter.Optional }
                

            );

        }
    }
}