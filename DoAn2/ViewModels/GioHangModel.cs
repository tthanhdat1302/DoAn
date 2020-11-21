using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2.ViewModels
{
    public class GioHangModel
    {
        // Field
        private List<GioHangItem> _items = new List<GioHangItem>();
        // Read Only Property
        public List<GioHangItem> Items
        {
            get { return _items; }
        }
        // Methods
        public void Add(GioHangItem item)
        {
            var gioHangItem = _items.Find(p => p.SanPham.SachID == item.SanPham.SachID);
            if (gioHangItem == null)
                _items.Add(item);
            else
                gioHangItem.SoLuong += item.SoLuong;
        }
        public void Update(int id, int soLuong)
        {
            var itemHieuChinh = _items.Find(p => p.SanPham.SachID == id);
            itemHieuChinh.SoLuong = soLuong;
        }
        public void Remove(int id)
        {
            var itemXoa = _items.Find(p => p.SanPham.SachID == id);
            _items.Remove(itemXoa);
        }
        public void Clear()
        {
            _items.Clear();
        }
        // Read Only Properties
        public int TongSanPham
        {
            get { return _items.Count; }
        }
        public int TongSoLuong
        {
            get { return _items.Sum(p => p.SoLuong); }
        }
        public int TongTriGia
        {
            get
            {
                int kq = 0;
                kq = _items.Sum(p => p.SoLuong * Convert.ToInt32(p.SanPham.GiaBan));
                return kq;
            }
        }
    }
}