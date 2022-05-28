using DAISY.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAISY.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        DaisyContext context = new DaisyContext();

        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
            if(user!= null)
            {
                Session["Name"] = user.Name;
            }
            var listsp = context.TB_SANPHAM.OrderBy(p => p.TENSP).ToList();
            return View(listsp);

        }
    }
}