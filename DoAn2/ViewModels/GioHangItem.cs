using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2.ViewModels
{
    public class GioHangItem
    {
        // Properties
        public Sach SanPham { get; set; }
        public int SoLuong { get; set; }

        // Constructors
        public GioHangItem() { }

        public GioHangItem(Sach sanPham, int soLuong)
        {
            this.SanPham = sanPham;
            this.SoLuong = soLuong;
        }
    }
}