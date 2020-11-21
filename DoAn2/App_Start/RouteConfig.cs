using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAn2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

   //         routes.MapRoute(
   //    name: "Đăng Nhập",
   //    url: "dang-nhap",
   //    defaults: new { controller = "KhachHangUser", action = "DangNhap", id = UrlParameter.Optional },
   //    namespaces: new[] { "DoAn2.Controllers" }
   //);

            //    routes.MapRoute(
            //name: "Dang Ky",
            //url: "dang-ky",
            //defaults: new { controller = "KhachHangUser", action = "DangKy", id = UrlParameter.Optional },
            //namespaces: new[] {"DoAn2.Controllers"}
            //    );
        }
    }
}
