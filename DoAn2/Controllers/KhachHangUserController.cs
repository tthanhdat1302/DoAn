using DoAn2.Models;
using DoAn2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace DoAn2.Controllers
{
    public class KhachHangUserController : Controller
    {
        BOOKSTOREEntities db = new BOOKSTOREEntities();

        // GET: KhachHangUser

        #region Đăng ký
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult DangKy(UserKH model)
        {
            if (ModelState.IsValid)
            {
                
                var xuly = new XuLyUser();
                if (xuly.CheckTaiKhoan(model.TaiKhoan))
                { ModelState.AddModelError("", "Tên đăng nhập đã tồn tại"); }
                else if (xuly.CheckEmail(model.Email))
                { ModelState.AddModelError("", "Email đã tồn tại"); }
                else
                {
                    var user = new UserKH();
                    user.TaiKhoan = model.TaiKhoan;
                    user.ConfirmPassword = model.ConfirmPassword;
                    user.Ten = model.Ten;
                    user.PassWord = model.PassWord;
                    user.DienThoai = model.DienThoai;
                    user.Email = model.Email;
                    user.DiaChi = model.DiaChi;
                    user.NgayTao = DateTime.Now;
                    user.TrangThai = true;
                    if (!string.IsNullOrEmpty(model.TinhThanh))
                    {
                        user.TinhThanh = model.TinhThanh;
                    }
                    if (!string.IsNullOrEmpty(model.QuanHuyen))
                    {
                        user.QuanHuyen = model.QuanHuyen;
                    }
                    var result = Insert(user);
                    if (result != null)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new UserKH();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View("DangNhap", model);
        }
        #endregion

        #region đăng nhập/xuất
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DangNhap(UserKH user)
        {
            var item = db.UserKHs.SingleOrDefault(x => x.TaiKhoan == user.TaiKhoan );
            if(item==null)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
                
            }
            else
            {
                if(item.TrangThai==false)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else
                {
                    if(item.PassWord==user.PassWord)
                    {
                        
                        Session["NguoiDung"] = item;
                        
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng.");
                    }
                }
            }
           
            return View();
        }

        public ActionResult DangXuat()
        {
            //var nguoiDung = Session["NguoiDung"] as UserKH;
            
            Session["NguoiDung"] = null;
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Load tỉnh thành quận huyện
        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Data/Provinces_Data.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadDistrict(int provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID);

            var list = new List<DistrictModel>();
            DistrictModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
                list.Add(district);

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<long> Insert(UserKH entity)
        {
            db.UserKHs.Add(entity);
            await db.SaveChangesAsync();
            return entity.ID;
        }
        #endregion
    }
}