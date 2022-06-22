using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DAISY
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "Danh muc",
                url: "danh-muc/{METATITLE}-{id}" ,
                defaults: new { controller = "Home", action = "SanPhamByDanhMuc", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "San pham",
                url: "san-pham-chung/{METATITLE}-{id}",
                defaults: new { controller = "Home", action = "SanPhamBySanPham", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Cua hang",
                url: "cua-hang/{METATITLE}-{id}",
                defaults: new { controller = "CuaHang", action = "Giaodien", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Gio hang",
                url: "gio-hang",
                defaults: new { controller = "GioHang", action = "GioHang"}
            );

            routes.MapRoute(
                name: "Dat hang",
                url: "dat-hang",
                defaults: new { controller = "GioHang", action = "DatHang" }
            );

            routes.MapRoute(
                name: "Quan ly",
                url: "quan-ly-tai-khoan",
                defaults: new { controller = "Manage", action = "Index" }
            );

            routes.MapRoute(
                name: "Danh muc ngoai",
                url: "danh-muc",
                defaults: new { controller = "LoaiSanPham", action = "Index" }
            );

            routes.MapRoute(
                name: "San pham danh muc",
                url: "san-pham/{METATITLE}-{id}",
                defaults: new { controller = "Home", action = "SanPhamByDanhMuc", id = UrlParameter.Optional }
            );*/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
