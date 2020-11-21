using DoAn2.Models;
using DoAn2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class AdminSachController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();
        // GET: AdminSach
        public ActionResult Index()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                var saches = db.Saches.Where(x => x.HetHang == false);
                return View(saches.ToList());
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        #region Chi tiết
        // GET: AdminSach/Details/5
        public ActionResult Details(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sach sach = db.Saches.Find(id);
                if (sach == null)
                {
                    return HttpNotFound();
                }
                return View(sach);
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }
        #endregion

        #region Thêm
        // GET: AdminSach/Create
        public ActionResult Create()
        {
            //var item = db.Saches.ToList().Count();
            //Console.log(typeof item);
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe");
                ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNhaXuatBan");
                return View();
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        // POST: AdminSach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenSach,GiaBan,MaChuDe,NhaXuatBanID,MoTa,HinhBia,SoTrang,TrongLuong,NgayCapNhat,SoLanXem,SoLuongBan,HetHang")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                //var str = db.Saches.ToList().Count() + 1;
                //sach.SachID = str;
                sach.NgayCapNhat = DateTime.Now;
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNhaXuatBan", sach.NhaXuatBanID);
            return View(sach);
        }
        #endregion

        #region Sửa
        // GET: AdminSach/Edit/5
        public ActionResult Edit(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sach sach = db.Saches.Find(id);
                if (sach == null)
                {
                    return HttpNotFound();
                }
                ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe", sach.MaChuDe);
                ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNhaXuatBan", sach.NhaXuatBanID);
                return View(sach);
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        // POST: AdminSach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SachID,TenSach,GiaBan,MaChuDe,NhaXuatBanID,MoTa,HinhBia,SoTrang,TrongLuong,NgayCapNhat,SoLanXem,SoLuongBan,HetHang")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNhaXuatBan", sach.NhaXuatBanID);
            return View(sach);
        }
        #endregion

        #region Xóa

        // GET: AdminSach/Delete/5
        public ActionResult Delete(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (nguoiDung.UserRole.RoleID == 1 || nguoiDung.UserRole.RoleID == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Sach sach = db.Saches.Find(id);
                    if (sach == null)
                    {
                        return HttpNotFound();
                    }
                    return View(sach);
                }
                else
                    return RedirectToAction("Login", "AdminUser");
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        // POST: AdminSach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            sach.HetHang = true;
            db.Entry(sach).State = EntityState.Modified;
            db.SaveChanges();


            var nguoiDung = Session["NguoiDung"] as User;
            var listLog = Session["Log"] as LogModel;
            if (listLog == null)
            {
                listLog = new LogModel();
                Session["Log"] = listLog;
            }
            LogItem logItem = new LogItem();
            logItem.ThongBao = $"Người dùng {nguoiDung.TaiKhoan} đã xóa sản phẩm có ID= {id}";
            logItem.ThoiGian = DateTime.Now;
            listLog.Add(logItem);


            return RedirectToAction("Index");
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


        #region Xử lý Upload hình
        // GET: AdminSanPham/Upload/5
        public ActionResult Upload(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                try
                {
                    Sach sach = db.Saches.Find(id);
                    if (sach == null) throw new Exception($"Sản phẩm ID = " + id + " không tồn tại!!!");
                    return View(sach);
                }
                catch (Exception ex)
                {
                    object cauThongBao = $"Lỗi truy cập dữ liệu.<br/>Lý do: {ex.Message}";
                    return View("Error2", cauThongBao);
                }
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(int? id, HttpPostedFileBase tapTin)
        {
            try
            {
                Sach sach = db.Saches.Find(id);
                if (tapTin != null && tapTin.ContentLength > 0)
                {
                    string duongDan = Server.MapPath("~/Images/");
                    string tenHinh = string.Format("{0}{1}", sach.SachID, Path.GetExtension(tapTin.FileName));
                    tapTin.SaveAs(duongDan + tenHinh);
                    if (sach.HinhBia != null && sach.HinhBia != tenHinh)
                    {
                        System.IO.File.Delete(duongDan + sach.HinhBia);
                    }
                    sach.HinhBia = tenHinh;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ThongBao = "Bạn chưa chọn file hoặc file bạn chọn không có nội dung";
                return View(sach);
            }
            catch (Exception ex)
            {
                object cauThongBao = $"Lỗi truy cập dữ liệu.<br/>Lý do: {ex.Message}";
                return View("Error2", cauThongBao);
            }
        }
        #endregion
    }
}