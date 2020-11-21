using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class AdminNhaXuatBanController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();
        // GET: AdminNhaXuatBan
        public ActionResult Index()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                return View(db.NhaXuatBans.ToList());
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        #region Chi tiết
        // GET: AdminNhaXuatBan/Details/5
        public ActionResult Details(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (id == null || id < 1) return RedirectToAction("Index");
                //Nếu id = null hoặc < 1 thì quay về trang Index              
                try
                {
                    NhaXuatBan nxb = db.NhaXuatBans
                                  .SingleOrDefault(p => p.NhaXuatBanID == id);
                    if (nxb == null) throw new Exception($"NXB ID = " + id + " không tồn tại!!!");
                    return View(nxb);
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
        // GET: AdminNhaXuatBan/Create
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

        // POST: AdminNhaXuatBan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NhaXuatBanID,TenNhaXuatBan,DiaChi,DienThoai")] NhaXuatBan nhaXuatBan)
        {
            if (ModelState.IsValid)
            {
                db.NhaXuatBans.Add(nhaXuatBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhaXuatBan);
        }
        #endregion

        #region Sửa 
        // GET: AdminNhaXuatBan/Edit/5
        public ActionResult Edit(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (id == null || id < 1) return RedirectToAction("Index");
                //Nếu id = null hoặc < 1 thì quay về trang Index              
                try
                {
                    NhaXuatBan nxb = db.NhaXuatBans
                                  .Find(id);
                    if (nxb == null) throw new Exception($"NXB ID = " + id + " không tồn tại!!!");
                    return View(nxb);
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

        // POST: AdminNhaXuatBan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NhaXuatBanID,TenNhaXuatBan,DiaChi,DienThoai")] NhaXuatBan nhaXuatBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaXuatBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaXuatBan);
        }
        #endregion

        #region Xóa

        // GET: AdminNhaXuatBan/Delete/5
        public ActionResult Delete(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (nguoiDung.UserRole.RoleID == 1)
                {
                    if (id == null || id < 1) return RedirectToAction("Index");
                    //Nếu id = null hoặc < 1 thì quay về trang Index              
                    try
                    {
                        NhaXuatBan nxb = db.NhaXuatBans
                                      .SingleOrDefault(p => p.NhaXuatBanID == id);
                        if (nxb == null) throw new Exception($"NXB ID = " + id + " không tồn tại!!!");
                        return View(nxb);
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

        // POST: AdminNhaXuatBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //m ngồi xóa s với es đi là đc 
            try
            {
                int d = db.Saches.Count(sp => sp.NhaXuatBanID == id);
                if (d > 0) throw new Exception("NXB đã có sản phẩm rồi !!!!!!");
                NhaXuatBan nxb = db.NhaXuatBans
                              .Find(id);
                if (nxb == null) throw new Exception("NXB đã bị xóa rồi !!!!!!");
                db.NhaXuatBans.Remove(nxb);
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