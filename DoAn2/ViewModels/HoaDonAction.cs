using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2.ViewModels
{
    public class HoaDonAction
    {
        BOOKSTOREEntities db = new BOOKSTOREEntities();

        #region ViewModel Hóa Đơn
        //HoaDon hoaDon = new HoaDon();
        //public List<HoaDon> HoaDon(HoaDon item)
        //{
        //    var dathang = new DatHang();
        //    dathang.HoTen = item.TenKH;
        //    dathang.DiaChi = item.DiaChi;
        //    dathang.DienThoai = item.DienThoai;
        //    dathang.Email = item.Email;
        //    dathang.NgayDatHang = item.NgayDatHang;
        //    var dathangCT = new DatHangCT();
        //    dathangCT.Sach.TenSach = item.TenSach;
        //    dathangCT.SoLuong = item.SoLuong;
        //    dathangCT.DonGia = item.DonGia;
        //    dathangCT.ThanhTien = item.ThanhTien;
        //    //Session["HoaDon"] = item;

        //    return HoaDon(item).ToList();
        //}
        #endregion

        public DatHang DatHangChiTiet(int id)
        {
            return db.DatHangs.SingleOrDefault(x=>x.DatHangID==id);
        }

        //public List<DatHang> DatHangChiTiet()
        //{
        //    return db.DatHangs.ToList();
        //}

        



    }
}