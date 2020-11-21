using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class AdminChuDeController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();
        // GET: AdminChuDe
        public ActionResult Index()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                return View(db.ChuDes.ToList());
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        #region Chi tiết
        // GET: AdminChuDe/Details/5
        public ActionResult Details(string id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (id == null) return RedirectToAction("Index");
                //Nếu id = null hoặc < 1 thì quay về trang Index              
                try
                {
                    ChuDe chuDe = db.ChuDes
                                  .Find(id);
                    if (chuDe == null) throw new Exception($"Mã chủ đề = " + id + " không tồn tại!!!");
                    return View(chuDe);
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
        #endregion

        #region Thêm
        // GET: AdminChuDe/Create
        public ActionResult Create()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        // POST: AdminChuDe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChuDe,TenChuDe")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                db.ChuDes.Add(chuDe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chuDe);
        }
        #endregion

        #region Sửa
        // GET: AdminChuDe/Edit/5
        public ActionResult Edit(string id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ChuDe chuDe = db.ChuDes.Find(id);
                if (chuDe == null)
                {
                    return HttpNotFound();
                }
                return View(chuDe);
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        // POST: AdminChuDe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChuDe,TenChuDe")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuDe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chuDe);
        }
        #endregion

        #region Xóa

        // GET: AdminChuDe/Delete/5
        public ActionResult Delete(string id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (nguoiDung.UserRole.RoleID == 1)
                {
                    if (id == null) return RedirectToAction("Index");
                    //Nếu id = null  thì quay về trang Index              
                    try
                    {
                        ChuDe chuDe = db.ChuDes
                                      .Find(id);
                        if (chuDe == null) throw new Exception($"Mã chủ đề = " + id + " không tồn tại!!!");
                        return View(chuDe);
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
            else
                return RedirectToAction("Login", "AdminUser");
        }

        // POST: AdminChuDe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                int d = db.Saches.Count(sp => sp.MaChuDe == id);
                if (d > 0) throw new Exception("Chủ đề đã có sản phẩm rồi !!!!!!");
                ChuDe chuDe = db.ChuDes
                              .Find(id);
                if (chuDe == null) throw new Exception("Chủ đề đã bị xóa rồi !!!!!!");
                db.ChuDes.Remove(chuDe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                object cauThongBao = $"Lỗi không hủy được dữ liệu.<br/>Lý do: {ex.Message}";
                return View("Error2", cauThongBao);
            }
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