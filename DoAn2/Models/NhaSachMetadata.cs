using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace DoAn2.Models
{
   
    [MetadataType(typeof(ChuDe.ChuDeMetadata))]
    public partial class ChuDe
    {
        internal class ChuDeMetadata
        {
            private ChuDeMetadata() { }

            [Display(Name = "Mã chủ đề")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string MaChuDe { get; set; }

            [Display(Name = "Tên chủ đề")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            [Remote("KiemTraTrungTenChuDe", "KiemTraDuLieu", AdditionalFields = "MaChuDe")]
            public string TenChuDe { get; set; }
        }
    }




    [MetadataType(typeof(NhaXuatBan.NhaXuatBanMetadata))]
    public partial class NhaXuatBan
    {
        internal class NhaXuatBanMetadata
        {
            private NhaXuatBanMetadata() { }

            [Display(Name = "Mã nhà xuất bản")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public int NhaXuatBanID { get; set; }

            [Display(Name = "Tên nhà xuất bản")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            [Remote("KiemTraTrungTenNXB", "KiemTraDuLieu", AdditionalFields = "NhaXuatBanID")]
            public string TenNhaXuatBan { get; set; }

            [Display(Name = "Địa chỉ")]
            public string DiaChi { get; set; }

            [Display(Name = "SĐT")]
            public string DienThoai { get; set; }
        }
    }




    [MetadataType(typeof(Sach.SachMetadata))]
    public partial class Sach
    {
        internal class SachMetadata
        {
            private SachMetadata() { }

            [Display(Name = "Mã sách")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public int SachID { get; set; }

            [Display(Name = "Tên sách")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            [Remote("KiemTraTrungTenSach", "KiemTraDuLieu", AdditionalFields = "SachID")]
            public string TenSach { get; set; }

            [Display(Name = "Giá bán")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            [Range(1, int.MaxValue, ErrorMessage = "Giá bán phải > 0")]
            public double GiaBan { get; set; }

            [Display(Name = "Mã chủ đề")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string MaChuDe { get; set; }

            [Display(Name = "Mã nhà xuất bản")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            [Range(1, int.MaxValue, ErrorMessage = "Bạn chưa chọn nhà xuất bản")]
            public int NhaXuatBanID { get; set; }

            [Display(Name = "Mô tả")]
            public string MoTa { get; set; }

            [Display(Name = "Hình bìa")]
            public string HinhBia { get; set; }

            [Display(Name = "Số trang")]
            public int SoTrang { get; set; }

            [Display(Name = "Trọng lượng")]
            public int TrongLuong { get; set; }

            [Display(Name = "Ngày cập nhật")]
            public DateTime NgayCapNhat { get; set; }

            [Display(Name = "Số lần xem")]
            public int SoLanXem { get; set; }

            [Display(Name = "Số lượng bán")]
            public int SoLuongBan { get; set; }

            [Display(Name = "Hết hàng")]
            public bool HetHang { get; set; }
        }
    }




    [MetadataType(typeof(DatHang.DatHangMetadata))]
    public partial class DatHang
    {
        internal class DatHangMetadata
        {
            private DatHangMetadata() { }

            [Display(Name = "Mã đơn hàng")]
            public int DatHangID { get; set; }

            [Display(Name = "Ngày đặt hàng")]
            public DateTime NgayDatHang { get; set; }

            [Display(Name = "Tổng trị giá")]
            public double TriGia { get; set; }

            [Display(Name = "Đã giao")]
            public bool DaGiao { get; set; }

            [Display(Name = "Ngày giao hàng")]
            public DateTime? NgayGiao { get; set; }

            [Display(Name = "Họ và tên")]
            public string HoTen { get; set; }

            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string DiaChi { get; set; }

            [Display(Name = "Điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string DienThoai { get; set; }

            [Display(Name = "Email")]
            public string Email { get; set; }
        }
    }




    [MetadataType(typeof(DatHangCT.DatHangCTMetadata))]
    public partial class DatHangCT
    {
        internal class DatHangCTMetadata
        {
            private DatHangCTMetadata() { }

            [Display(Name = "Mã đơn hàng")]
            public int DatHangID { get; set; }

            [Display(Name = "Mã sách")]
            public int SachID { get; set; }

            [Display(Name = "Số lượng")]
            public int SoLuong { get; set; }

            [Display(Name = "Đơn giá")]
            public double DonGia { get; set; }

            [Display(Name = "Thành tiền")]
            public double ThanhTien { get; set; }
        }
    }




    [MetadataType(typeof(UserRole.UserRoleMetadata))]
    public partial class UserRole
    {
        internal class UserRoleMetadata
        {
            [Display(Name = "RoleID")]
            public int RoleID { get; set; }

            [Display(Name = "Tên")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string TenRole { get; set; }
        }
    }




    [MetadataType(typeof(User.UserMetadata))]
    public partial class User
    {
        internal class UserMetadata
        {
            [Display(Name = "Mã người dùng")]
            public int UserID { get; set; }

            [Display(Name = "Tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string TaiKhoan { get; set; }

            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string MatKhau { get; set; }

            [Display(Name = "Trạng thái")]
            public bool TrangThai { get; set; }

            [Display(Name = "Quyền")]
            public int? RoleID { get; set; }

            [Display(Name ="Email")]
            [Required(ErrorMessage ="{0} không được để trống.")]
            public string Email { get; set; }
        }
    }



    [MetadataType(typeof(UserKH.UserKHMetadata))]
    public partial class UserKH
    {
        internal class UserKHMetadata
        {
            [Display(Name = "Mã người dùng")]
            public long ID { get; set; }



            [Display(Name = "Tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string TaiKhoan { get; set; }



            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string PassWord { get; set; }



            [Display(Name = "Xác nhận mật khẩu")]
            [System.ComponentModel.DataAnnotations.Compare("PassWord", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
            public string ConfirmPassword { get; set; }



            [Display(Name = "Họ tên")]
            [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
            public string Ten { get; set; }



            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string DiaChi { get; set; }



            [Display(Name ="Email")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string Email { get; set; }



            [Display(Name = "Điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống.")]
            public string DienThoai { get; set; }



            [Display(Name = "Tỉnh/thành")]
            public string TinhThanh { get; set; }



            [Display(Name = "Quận/Quyện")]
            public string QuanHuyen { get; set; }


            [Display(Name = "Ngày Tạo")]
            public DateTime? NgayTao { get; set; }


            [Display(Name = "Ngày Cập Nhật")]
            public DateTime? NgayCapNhat { get; set; }


            [Display(Name = "Trạng Thái")]
            public bool? TrangThai { get; set; }
        }
    }
}
