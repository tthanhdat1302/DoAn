using DoAn2.Models;
using DoAn2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class AdminUserController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();
        // GET: AdminUser
        public async Task<ActionResult> Index()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (nguoiDung.UserRole.RoleID == 1 || nguoiDung.UserRole.RoleID == 2)
                {
                    var users = db.Users.Include(u => u.UserRole);
                    return View(await users.ToListAsync());
                }
                else
                    return RedirectToAction("Login", "AdminUser");
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        #region Chi Tiết

        public async Task<ActionResult> Details(int? id)
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
                    User user = await db.Users.FindAsync(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
                else
                    return RedirectToAction("Login", "AdminUser");
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }
        #endregion

        #region Thêm

        // GET: AdminUser/Create
        [HttpGet]
        public ActionResult Create()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (nguoiDung.UserRole.RoleID == 1)
                {
                    var item = db.UserRoles.Where(x => x.RoleID != 1).ToList();
                    ViewBag.RoleID = new SelectList(item,"RoleID","TenRole");
                    return View();
                }
                else
                    return RedirectToAction("Login", "AdminUser");
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,TaiKhoan,MatKhau,TrangThai,Email,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "TenRole",user.RoleID);
            return View(user);
        }


        #endregion

        #region Sửa
        // GET: AdminUser/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
                    User user = await db.Users.FindAsync(id);

                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "TenRole", user.RoleID);
                    return View(user);
                }
                else
                    return RedirectToAction("Login", "AdminUser");
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,TaiKhoan,MatKhau,TrangThai,Email,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();

                var item = db.Users.SingleOrDefault(x => x.UserID == user.UserID);
                var nguoiDung = Session["NguoiDung"] as User;
                var listLog = Session["Log"] as LogModel;
                if (listLog == null)
                {
                    listLog = new LogModel();
                    Session["Log"] = listLog;
                }
                LogItem logItem = new LogItem();
                logItem.ThongBao = $"Người dùng {nguoiDung.TaiKhoan} đã cập nhật thông tin của người dùng {item.TaiKhoan}";
                logItem.ThoiGian = DateTime.Now;
                listLog.Add(logItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "TenRole", user.RoleID);
            return View(user);
        }
        #endregion

        #region Xóa
        // GET: AdminUser/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                if (nguoiDung.UserRole.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    User user = await db.Users.FindAsync(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
                else
                    return RedirectToAction("Login", "AdminUser");
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }

        // POST: AdminUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Đăng nhập và Log
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var item = db.Users.SingleOrDefault(x => x.TaiKhoan == user.TaiKhoan && x.MatKhau == user.MatKhau);
            if (item == null)
                return RedirectToAction("Login");
            else
            {
                if (item.TrangThai == false)
                {
                    Session["NguoiDung"] = item;
                    var listLog = Session["Log"] as LogModel;
                    if (listLog == null)
                    {
                        listLog = new LogModel();
                        Session["Log"] = listLog;
                    }
                    LogItem logItem = new LogItem();
                    logItem.ThongBao = $"Người dùng {item.TaiKhoan} đã đăng nhập";
                    logItem.ThoiGian = DateTime.Now;
                    listLog.Add(logItem);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = "Tài khoản của bạn đã bị khóa!!!";
                    return View("Login");
                }
            }
        }

        public ActionResult LogOff()
        {

            var nguoiDung = Session["NguoiDung"] as User;
            var listLog = Session["Log"] as LogModel;
            if (listLog == null)
            {
                listLog = new LogModel();
                Session["Log"] = listLog;
            }
            LogItem logItem = new LogItem();
            logItem.ThongBao = $"Người dùng {nguoiDung.TaiKhoan} đã đăng xuất";
            logItem.ThoiGian = DateTime.Now;
            listLog.Add(logItem);

            Session["NguoiDung"] = null;
            return RedirectToAction("Login");
        }
        #endregion
    }
}