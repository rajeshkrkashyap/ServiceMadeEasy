using System.Web.Mvc;

namespace ServiceMadeEasy.Com.Areas.GST
{
    public class GSTAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GST";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GST_default",
                "GST/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}