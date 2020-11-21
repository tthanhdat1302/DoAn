using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAn2.Models;

namespace DoAn2.ViewModels
{
    public class LogItem
    {
        public string ThongBao { get; set; }
        public DateTime ThoiGian { get; set; }

        public LogItem() { }

        public LogItem(string thongBao,DateTime thoiGian)
        {
            this.ThongBao = thongBao;
            this.ThoiGian = thoiGian;
        }
    }
    public class LogModel
    {
        private List<LogItem> _items = new List<LogItem>();
        public List<LogItem> Items
        {
            get { return _items; }
        }
        public void Add(LogItem item)
        {
            _items.Add(item);
        }
    }
}