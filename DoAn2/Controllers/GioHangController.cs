using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DoAn2.ViewModels;

namespace DoAn2.Controllers
{
    public class GioHangController : Controller
    {
        BOOKSTOREEntities db = new BOOKSTOREEntities();

        // GET: GioHang
        public ActionResult Index()
        {
            // Tham chiếu đến giỏ hàng lưu trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            if (gioHang == null || gioHang.TongSanPham == 0)
            {// Điều hướng về trang chủ
                return RedirectToAction("Index", "Home");
            }
            return View(gioHang);
        }

        #region Thêm
        [HttpPost]
        public ActionResult Them(int sanPhamID, int soLuong = 1)
        {
            UserKH khachhang = Session["NguoiDung"] as UserKH;
            if (khachhang != null)
            {
                // Tham chiếu đến giỏ hàng lưu trong Session
                var gioHang = Session["GioHang"] as GioHangModel;
                if (gioHang == null)
                {
                    gioHang = new GioHangModel();
                    Session["GioHang"] = gioHang;
                }
                var sanPhamChonMua = db.Saches
                    .SingleOrDefault(sp => sp.SachID == sanPhamID);
                var item = new GioHangItem(sanPhamChonMua, soLuong);
                gioHang.Add(item);

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("DangNhap", "KhachHangUser");
        }
        #endregion

        #region Sửa
        [HttpPost]
        public ActionResult HieuChinh(int sanPhamID, int soLuong)
        {
            //Tham chiếu đến giỏ hàng trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            gioHang.Update(sanPhamID, soLuong);
            return RedirectToAction("Index");
        }

        #endregion

        #region Xóa
        [HttpPost]
        public ActionResult Xoa(int sanPhamID)
        {
            //Tham chiếu đến giỏ hàng trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            gioHang.Remove(sanPhamID);
            return RedirectToAction("Index");
        }
        #endregion

        #region Xử lý phát sinh đơn đặt hàng

        [HttpGet]
        public ActionResult DatHang()
        {
            //Tham chiếu đến giỏ hàng trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            if (gioHang.TongSanPham == 0) return RedirectToAction("Index", "Home");
            ViewBag.GioHang = gioHang;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatHang(DatHang hoaDon)
        {
            var gioHang = Session["GioHang"] as GioHangModel;
            if (gioHang == null || gioHang.TongSanPham == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            // Xử lý phát sinh HoaDon và HoaDonChiTiet
            try
            {
                //1. Thêm HoaDon
                hoaDon.NgayDatHang = DateTime.Now;
                hoaDon.TriGia = gioHang.TongTriGia;
                db.DatHangs.Add(hoaDon);
                //2. Thêm DatHangCT              
                foreach (var item in gioHang.Items)
                {
                    DatHangCT ct = new DatHangCT();
                    ct.DatHangID = hoaDon.DatHangID;
                    ct.SachID = item.SanPham.SachID;
                    ct.SoLuong = item.SoLuong;
                    ct.DonGia = item.SanPham.GiaBan;
                    ct.ThanhTien = item.SanPham.GiaBan * item.SoLuong;
                    db.DatHangCTs.Add(ct);
                }
               
                db.SaveChanges();
                gioHang.Clear();

                return View("DatHangThanhCong", hoaDon);
            }
            catch (Exception ex)
            {
                ViewData["LoiDatHang"] = "Đặt hàng không thành công.<br>" + ex.Message;
                return View(hoaDon);
            }
        }
        #endregion

        [ChildActionOnly]
        public PartialViewResult _DropDownCart()
        {
            var dsSanPham = Session["GioHang"] as GioHangModel;
            ViewBag.Cart = dsSanPham;
            return PartialView();
        }

    }
}