using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2.ViewModels
{
    public class XuLyUser
    {
        BOOKSTOREEntities db = new BOOKSTOREEntities();
        public bool CheckTaiKhoan(string taikhoan)
        {
            return db.UserKHs.Count(x => x.TaiKhoan == taikhoan) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.UserKHs.Count(x => x.Email == email) > 0;
        }
    }
}