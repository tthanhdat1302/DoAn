using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn2.Models;

namespace DoAn2.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult ColumnChart()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null && (int)nguoiDung.UserRole.RoleID == 1)
            {
                using (var db = new BOOKSTOREEntities())
                {
                    //Pie Chart
                    var temp = db.DatHangs.ToList();
                    ArrayList header = new ArrayList { "Task Name", "Hours" };

                    ArrayList data = new ArrayList { header };
                    for (int i = 0; i < temp.Count; i++)
                    {
                        data.Add(new ArrayList { temp[i].DatHangID.ToString(), temp[i].TriGia });
                    }


                    string datastr = JsonConvert.SerializeObject(data, Formatting.None);
                    ViewBag.Data = new HtmlString(datastr);

                    //Area Chart
                    var query = db.DatHangs.Where(m => m.DaGiao == true )
                        .GroupBy(m => m.NgayDatHang.Year)
                        .Select(m => new { tong = m.Sum(a => a.TriGia), Year = m.Key.ToString() })
                        .ToList();
                    ArrayList headerAreaChart = new ArrayList { "Year", "Tổng Tiền" };
                    ArrayList dataAreaChart = new ArrayList { headerAreaChart };

                    for (int i = 0; i < query.Count; i++)
                    {
                        dataAreaChart.Add(new ArrayList { query[i].Year, query[i].tong });
                    }
                    string datastrArea = JsonConvert.SerializeObject(dataAreaChart, Formatting.None);
                    ViewBag.DataArea = new HtmlString(datastrArea);

                    //Column Chart

                    var query1 = db.DatHangCTs
                        .GroupBy(m => m.Sach.TenSach)
                        .Select(m => new
                        {
                            tong1 = m.Sum(a => a.SoLuong).ToString(),
                            Name = m.Key.ToString(),
                            tong2 =
                        m.Sum(a => a.ThanhTien)
                        })
                        .ToList();
                    ArrayList headerColumnChart = new ArrayList { "Tên Sách", "Số Lượng", "Tổng Tiền Bán" };
                    ArrayList dataColumnChart = new ArrayList { headerColumnChart };
                    for (int i = 0; i < query1.Count; i++)
                    {
                        dataColumnChart.Add(new ArrayList { query1[i].Name, float.Parse(query1[i].tong1), query1[i].tong2 });
                    }
                    string datastrColumn = JsonConvert.SerializeObject(dataColumnChart, Formatting.None);
                    ViewBag.DataColumn = new HtmlString(datastrColumn);

                    //Donut Chart
                    var query2 = db.Saches.GroupBy(m => m.TenSach)
                        .Select(m => new { solan = m.Count(a => a.SachID == a.SachID).ToString(), name1 = m.Key.ToString() })
                        .ToList();
                    ArrayList headerDonutChart = new ArrayList { "Tên Sách", "Số lượt xem" };
                    ArrayList dataDonutChart = new ArrayList { headerDonutChart };
                    for (int i = 0; i < query2.Count; i++)
                    {
                        dataDonutChart.Add(new ArrayList { query2[i].name1, int.Parse(query2[i].solan) });
                    }
                    string datastrDonut = JsonConvert.SerializeObject(dataDonutChart, Formatting.None);
                    ViewBag.DataDonut = new HtmlString(datastrDonut);
                    return View();
                }
            }
            return RedirectToAction("Login", "AdminUser");
        }

        public ActionResult VisualizeBillsResults()
        {
            return Json(Result(),JsonRequestBehavior.AllowGet);
        }

        public List<DatHangCT> Result()
        {
            List<DatHangCT> list = new List<DatHangCT>();
            using (var db = new BOOKSTOREEntities())
            {
                list = db.DatHangCTs.ToList();
            }
            return list;
        }



    }
}