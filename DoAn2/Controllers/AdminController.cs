using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoAn2.Controllers
{
    public class AdminController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();
        // GET: Admin
        public ActionResult Index()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                List<string> ds = new List<string>();
                foreach (var item in db.ChuDes.ToList())
                {
                    string soLuongSachTheoNXB = db.Saches.Count(s => s.MaChuDe == item.MaChuDe).ToString();
                    ds.Add(soLuongSachTheoNXB);
                    var d = db.DatHangs.Where(v => v.NgayDatHang.Day == DateTime.Now.Day).ToList();
                    ViewBag.DTNgay = d.Sum(hd => hd.TriGia).ToString("#,##0VNĐ");
                    ViewBag.DonHangMoi = db.DatHangs.Where(v => v.NgayGiao.Value.Day == DateTime.Now.Day).Count();
                    ViewBag.ds = ds;
                    var items = db.Saches.ToList();
                    var listDT = db.DatHangs.Where(x => x.NgayDatHang.Month == DateTime.Now.Month).ToList();
                    ViewBag.DTThang = listDT.Sum(x => x.TriGia).ToString("#,##0VNĐ");
                    ViewBag.SanPhamMoi = db.Saches.Count(x => x.NgayCapNhat.Month == DateTime.Now.Month);
                    return View(items);
                }
            }
            else
                return RedirectToAction("Login", "AdminUser");
            return View();
        }

        public ActionResult ExportToExcel()
        {
            var items = Session["Log"] as ViewModels.LogModel;

            var products = new System.Data.DataTable("teste");
            products.Columns.Add("Thông báo", typeof(string));
            products.Columns.Add("Thời gian", typeof(DateTime));


            foreach (var item in items.Items)
            {
                products.Rows.Add($"{item.ThongBao}", $"{item.ThoiGian}");
            }

            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachHoatDong.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }
    }
}