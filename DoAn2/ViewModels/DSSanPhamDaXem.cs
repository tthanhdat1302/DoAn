using DoAn2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2.ViewModels
{
    public class DSSanPhamDaXem
    {
        //File
        private List<Sach> _items = new List<Sach>();
        //Read Only Property
        public List<Sach> Items
        {
            get { return _items; }
        }

        public void Add(Sach items)
        {
            var sanpham = _items.Find(p => p.SachID == items.SachID);
            if(sanpham==null)
            {
                _items.Add(items);
            }
        }
    }
}