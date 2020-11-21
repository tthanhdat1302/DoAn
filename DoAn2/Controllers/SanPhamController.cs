using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
namespace DoAn2.Controllers
{
    public class SanPhamController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();
        #region Chi tiết sách
        //GET: SanPham
        public ActionResult ChiTiet(int? id)
        {
            if (id == null || id < 1)
            {
                object cauthongbao = $"Sản phẩm không tồn tại!!!";
                return View("Error", cauthongbao);
            }
            try
            {
                ViewBag.DmAct = "current";
                Sach item = db.Saches
                                .SingleOrDefault(s => s.SachID == id);

                var items = db.Saches
                    .Where(s => s.MaChuDe == item.MaChuDe && s.SachID != item.SachID)
                    .Take(5)
                    .ToList();
                ViewBag.dsSachCCD = items;
                ViewBag.Users = db.UserKHs.ToList();
                ViewBag.Rating = db.Comments.Where(x => x.SachID == id).ToList();
                Session["So"] = item.SachID;
                if (item == null) throw new Exception($"Sản phẩm không tồn tại!!!");
                return View(item);
            }
            catch (Exception ex)
            {
                object cauThongBao = $"Lỗi truy cập dữ liệu.<br/>Lý do: {ex.Message}";
                return View("Error", cauThongBao);
            }


        }

        #endregion

        #region Tìm Kiếm
        
		 public ViewResult TimKiem(string tuKhoa)
        {
            List<Sach> items = db.Saches
                        .Where(sp => sp.TenSach.Contains(tuKhoa) && sp.HetHang == false)
                        .ToList();
            ViewBag.SanPhams = items;
            return View("KetQuaTimKiem");
        }
        #endregion

        #region View đọc nhiều nhất
        public ViewResult XemNhieu(int? page)
        {
            ViewBag.NnAct = "current";
            if (page == null || page < 1) page = 1;
            ViewBag.TieuDeXN = "Xem nhiều";
            IPagedList<Sach> items = db.Saches
                            .Where(s => s.SoLanXem > 1000 && s.HetHang == false)
                            .OrderByDescending(s => s.SachID)
                            .ToPagedList(page.Value, 12);
            return View(items);
        }
        #endregion

        #region Sách theo NXB
        public ActionResult DanhMucTheoNXB(int? id, int? page)
        {
            if (page == null || page < 1) page = 1;
            NhaXuatBan nxb = db.NhaXuatBans.Find(id);
            if (nxb == null)
            {
                object cauBaoLoi = "Dữ liệu truy cập không hợp lệ!!!!";
                return View("Error", cauBaoLoi);//pt6
            }
            ViewBag.NccAct = "current";
            IPagedList<Sach> items = db.Saches
                .Where(p => p.NhaXuatBan.NhaXuatBanID == id && p.HetHang == false)
                .OrderByDescending(p => p.SachID)
                .ToPagedList(page.Value, 12);
            ViewBag.TieuDeXN = $"Danh mục sách của NXB : {nxb.TenNhaXuatBan}";
            ViewBag.NhaXuatBanID = id;
            return View("XemNhieu", items);
        }

        #endregion

        #region View danh mục sách
        public ViewResult DanhMuc(int? page)
        {
            ViewBag.DmAct = "current";
            if (page == null || page < 1) page = 1;
            int d = db.Saches.Count();
            ViewBag.TieuDeDM = "Danh mục sách";
            IPagedList<Sach> items = db.Saches
                                .Where(s => s.HetHang == false)
                                .OrderByDescending(s => s.SachID)
                                .ToPagedList(page.Value, 9);
            return View(items);
        }
        #endregion

        #region Sách theo chủ đề
        public ActionResult DanhMucTheoChuDe(string id, int? page)
        {
            if (page == null || page < 1) page = 1;
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                object cauBaoLoi = "Dữ liệu truy cập không hợp lệ!!!!";
                return View("Error", cauBaoLoi);//pt6
            }
            ViewBag.DmAct = "current";
            IPagedList<Sach> items = db.Saches
                .Where(s => s.ChuDe.MaChuDe == id && s.HetHang == false)
                .OrderByDescending(s => s.SachID)
                .ToPagedList(page.Value, 9);
            ViewBag.TieuDeDM = $"Danh mục sách của chủ đề : {chuDe.TenChuDe}";
            ViewBag.MaChuDe = id;

            return View("DanhMuc", items);
        }
        #endregion

        #region Partial view NXB
        [ChildActionOnly]
        public PartialViewResult _SachTheoNXBPartial()
        {
            var items = db.NhaXuatBans.ToList();
            return PartialView(items);
        }
        #endregion

        #region Partial view Chủ Đề
        [ChildActionOnly]
        public PartialViewResult _SachTheoChuDePartial()
        {
            var items = db.ChuDes.Include(s => s.Saches).ToList();
            return PartialView(items);
        }
        #endregion

        #region Comment

        [ChildActionOnly]
        public PartialViewResult _ThemDanhGiaPartial()
        {
            var arrRating = new[]
            {
                new {Rating="0",Ten="--- Chọn Sao ---"},
                new {Rating="1",Ten="1 sao"},
                new {Rating="2",Ten="2 sao"},
                new {Rating="3",Ten="3 sao"},
                new {Rating="4",Ten="4 sao"},
                new {Rating="5",Ten="5 sao"},
            };
            ViewBag.Rating = new SelectList(arrRating, "Rating", "Ten");
            return PartialView();
        }

        public ActionResult DanhGia(Comment item)
        {
            var nguoiDung = Session["NguoiDung"] as UserKH;
            if (nguoiDung != null)
            {
                item.SachID = (int)Session["So"];
                //item.UserID = nguoiDung.ID;
                item.NgayDang = DateTime.Now.Date;
                db.Comments.Add(item);
                db.SaveChanges();
                return RedirectToAction("ChiTiet", new { id = (int)Session["So"] });
            }
            else
            {
                return RedirectToAction("DangNhap", "KhachHangUser");
            }
        }



    }
    #endregion

}
