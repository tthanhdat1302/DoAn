using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn2.Models;

namespace WebBanSach.Controllers
{
   
    public class AdminDatHangController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();

        // GET: AdminDatHang
        public ActionResult Index()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                return View(db.DatHangs.ToList());
            }
            else
                return RedirectToAction("Login", "AdminUser");            
        }

        #region Chi tiết
        // GET: AdminDatHang/Details/5
        public ActionResult Details(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DatHang datHang = db.DatHangs.Find(id);
                if (datHang == null)
                {
                    return HttpNotFound();
                }
                return View(datHang);
            }
            else
                return RedirectToAction("Login", "AdminUser");           
        }
        #endregion

        #region Sửa
        // GET: AdminDatHang/Edit/5
        public ActionResult Edit(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DatHang datHang = db.DatHangs.Find(id);
                if (datHang == null)
                {
                    return HttpNotFound();
                }
                return View(datHang);
            }
            else
                return RedirectToAction("Login", "AdminUser");           
        }

        // POST: AdminDatHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DatHangID,KhachHangID,NgayDatHang,TriGia,DaGiao,NgayGiao,HoTen,DiaChi,DienThoai,Email")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(datHang);
        }
        #endregion

        #region Giải phóng biến
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
