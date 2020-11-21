using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class KiemTraDuLieuController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();
        // GET: KiemTraDuLieu
        public JsonResult KiemTraTrungTenChuDe(string ten, string MaChuDe)
        {
            int kq = 0;
            if(MaChuDe==null)
            {
                kq = db.ChuDes.Count(s => s.TenChuDe == ten);
            }
            else
            {
                kq = db.ChuDes.Count(s => s.MaChuDe != MaChuDe && s.TenChuDe == ten);
            }
            if (kq > 0)
                return Json("Tên chủ đề bị trùng!!!", JsonRequestBehavior.AllowGet);
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public JsonResult KiemTraTrungTenNXB(string ten, int? nhaXuatBanID)
        {
            int kq = 0;
            if (nhaXuatBanID == null)
            {
                kq = db.NhaXuatBans.Count(s => s.TenNhaXuatBan == ten);
            }
            else
            {
                kq = db.NhaXuatBans.Count(s => s.NhaXuatBanID != nhaXuatBanID && s.TenNhaXuatBan == ten);
            }
            if (kq > 0)
                return Json("Tên nhà xuất bản bị trùng!!!", JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KiemTraTrungSach(string ten, int? sachID)
        {
            int kq = 0;
            if (sachID == null)
            {
                kq = db.Saches.Count(s => s.TenSach == ten);
            }
            else
            {
                kq = db.Saches.Count(s => s.SachID != sachID && s.TenSach == ten);
            }
            if (kq > 0)
                return Json("Tên sách bị trùng!!!", JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}