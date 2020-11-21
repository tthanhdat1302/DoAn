using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace DoAn2.Controllers
{
    public class HomeController : Controller
    {
        BOOKSTOREEntities db = new BOOKSTOREEntities();
        public ActionResult Index()
        {
            //var x = Session["NguoiDung"] as UserKH;
            //if (x == null)
            //{
            //    ViewBag.Title1 = "<h1>sadsad</h1>";

            //}
            //else
            //{
            //    ViewBag.Title1 = "Xin chao";
            //}
            ViewBag.TcAct = "current";
            var listItems = db.Saches
                            .Take(4)
                            .ToList();

            var dsSach = db.Saches
                        .Where(s => s.SoLanXem > 1000)
                        .Take(4)
                        .ToList();
            ViewBag.dsSach = dsSach;

            return View(listItems);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.ContactAct = "current";
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        [ChildActionOnly]
        
        public ActionResult TopMenuTrangChu()
        {
            var model = new ViewModels.XuLyMenu().ListByGroupId(3);
            ViewBag.TaiKhoan = db.UserKHs.ToList();
            return PartialView(model);
        }
    }
}