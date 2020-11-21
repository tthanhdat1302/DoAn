using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn2.ViewModels
{
    public class HoaDon
    {
        [Key]
        public int ID { get; set; }
        public string TenKH { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public DateTime NgayDatHang { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien { get; set; }
    }


}