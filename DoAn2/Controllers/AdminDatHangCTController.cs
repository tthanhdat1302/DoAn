using DoAn2.Models;
using DoAn2.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class AdminDatHangCTController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();

        #region Chi Tiết Đơn Hàng Index 
        // GET: AdminDatHangCT
        public async Task<ActionResult> Index()
        {
            User nguoiDung = Session["NguoiDung"] as User;
            if (nguoiDung != null)
            {
                var datHangCTs = db.DatHangCTs.Include(d => d.DatHang).Include(d => d.Sach);
                return View(await datHangCTs.ToListAsync());
            }
            else
                return RedirectToAction("Login", "AdminUser");
        }
        #endregion


        #region Xuất Excel
        //public ActionResult ExportToExcel(int id)
        //{
        //    // lấy dữ liệu từ database
        //    var products = db.DatHangs.ToList<DatHang>();


        //    ExcelPackage pck = new ExcelPackage();
        //    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

        //    ws.Cells["A1:D1"].Value = "FAHAHA Book Store";
        //    ws.Cells["A1:D1"].Merge = true;
        //    ws.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    //ws.Cells["A1:D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    ws.Cells["A2:D2"].Value = "Đơn hàng";
        //    ws.Cells["A2:D2"].Merge = true;
        //    ws.Cells["A2:D2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    //ws.Cells["A2:D2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);



        //    ws.Cells["A3"].Value = "Người đặt hàng";
        //    ws.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    // ws.Cells["A3"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    //ws.Cells["B3"].Value = bill.Account.UserName;
        //    //ws.Cells["B3:D3"].Merge = true;
        //    //ws.Cells["B3:D3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    //  ws.Cells["B3:D3"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    ws.Cells["A4"].Value = "Ngày đặt hàng";
        //    ws.Cells["A4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    // ws.Cells["A4"].Style.Border.BorderAround(ExcelBorderStyle.Thin);


        //    //ws.Cells["B4"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", bill.FoundedDate);
        //    //ws.Cells["B4:D4"].Merge = true;
        //    //ws.Cells["B4:D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    //  ws.Cells["B4:D4"].Style.Border.BorderAround(ExcelBorderStyle.Thin);


        //    //Tên thuộc tính cột cột A6 là stt
        //    //dòng 6 cột A
        //    ws.Cells["A6"].Value = "STT";
        //    ws.Cells["A6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells["A6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    //Tên thuộc tính cột cột B6 là ngày đặt
        //    // dòng 6 cột B
        //    ws.Cells["B6"].Value = "Ngày đặt hàng";
        //    ws.Cells["B6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells["B6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    ws.Cells["C6"].Value = "Trị giá";
        //    ws.Cells["C6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells["C6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    ws.Cells["D6"].Value = "Đã giao";
        //    ws.Cells["D6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells["D6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    ws.Cells["E6"].Value = "Ngày giao";
        //    ws.Cells["E6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells["E6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    ws.Cells["F6"].Value = "Họ tên";
        //    ws.Cells["F6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells["F6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    ws.Cells["G6"].Value = "Địa chỉ";
        //    ws.Cells["G6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells["G6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    ws.Cells["H6"].Value = "Điện thoại";
        //    ws.Cells["H6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells["H6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //    ws.Cells["I6"].Value = "Email";
        //    ws.Cells["I6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells["I6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);


        //     // dòng bắt đầu bỏ dữ liệu vô là dòng 7
        //    int rowStart = 7;

        //    // đánh số tt
        //int stt = 1;

        //    // bắt đầu duyệt dữ liệu từ db vào excel
        //    foreach (var item in products)
        //    {

        //        //if (item.Count < 10)
        //        //{
        //        //    //    ws.Cells[string.Format("A{0}:D{1}",rowStart,rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        //        //    //    ws.Cells[string.Format("A{0}:D{1}", rowStart, rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
        //        //}


        //        ws.Cells[string.Format("A{0}", rowStart)].Value = stt.ToString();
        //        ws.Cells[string.Format("A{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        ws.Cells[string.Format("A{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //        //ngày đặt hàng
        //        ws.Cells[string.Format("B{0}", rowStart)].Value = item.NgayDatHang.ToString();
        //        ws.Cells[string.Format("B{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        ws.Cells[string.Format("B{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //        ws.Cells[string.Format("C{0}", rowStart)].Value = item.Count;
        //        ws.Cells[string.Format("C{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        ws.Cells[string.Format("C{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //        ws.Cells[string.Format("D{0}", rowStart)].Value = item.Price;
        //        ws.Cells[string.Format("D{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        ws.Cells[string.Format("D{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        //        rowStart++;
        //        stt++;
        //    }

        //    ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Value = "TỔNG TIỀN THANH TOÁN:";
        //    ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Merge = true;
        //    ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //    ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Style.Font.Bold = true;

        //    ws.Cells[string.Format("D{0}", rowStart)].Value = bill.TotalCost;
        //    ws.Cells[string.Format("D{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells[string.Format("D{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //    ws.Cells[string.Format("D{0}", rowStart)].Style.Font.Bold = true;
        //    int rowEnd = rowStart + 2;

        //    ws.Cells[string.Format("A{0}:D{1}", rowEnd, rowEnd)].Value = "XIN CẢM ƠN QUÝ KHÁCH!";
        //    ws.Cells[string.Format("A{0}:D{1}", rowEnd, rowEnd)].Merge = true;
        //    ws.Cells[string.Format("A{0}:D{1}", rowEnd, rowEnd)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //    ws.Cells[string.Format("A{0}:D{1}", rowEnd, rowEnd)].Style.Border.Top.Style = ExcelBorderStyle.DashDot;


        //    ws.Cells["A:AZ"].AutoFitColumns();
        //    Response.Clear();
        //    Response.ContentType = "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attactment: filename=" + "ExcelReport.xlsx");
        //    Response.BinaryWrite(pck.GetAsByteArray());
        //    Response.End();

        //    return View();
        //}
        #endregion

        #region exprot

        public ActionResult ExportToExcel(int id)
        {
            var products = db.DatHangs.SingleOrDefault(x => x.DatHangID == id);

            //var bill = new HoaDonAction().ThamChieuDatHang(id);
            var list = db.DatHangCTs.Where(x => x.DatHangID == id).ToList();
            //HoaDon hoaDon = new HoaDon();
            
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1:D1"].Value = "CanVas Book Store";
            ws.Cells["A1:D1"].Merge = true;
            ws.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //ws.Cells["A1:D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            ws.Cells["A2:D2"].Value = "Đơn hàng";
            ws.Cells["A2:D2"].Merge = true;
            ws.Cells["A2:D2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //ws.Cells["A2:D2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);



            ws.Cells["A3"].Value = "Người đặt hàng";
            ws.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            // ws.Cells["A3"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            ws.Cells["B3"].Value = products.HoTen;
            ws.Cells["B3:D3"].Merge = true;
            ws.Cells["B3:D3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //  ws.Cells["B3:D3"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            ws.Cells["A4"].Value = "Ngày đặt hàng";
            ws.Cells["A4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            // ws.Cells["A4"].Style.Border.BorderAround(ExcelBorderStyle.Thin);


            ws.Cells["B4"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", products.NgayDatHang);
            ws.Cells["B4:D4"].Merge = true;
            ws.Cells["B4:D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //  ws.Cells["B4:D4"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            ws.Cells["A6"].Value = "STT";
            ws.Cells["A6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            ws.Cells["B6"].Value = "Tên sách";
            ws.Cells["B6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["B6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            ws.Cells["C6"].Value = "Số lượng";
            ws.Cells["C6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["C6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            ws.Cells["D6"].Value = "Giá sau giảm";
            ws.Cells["D6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["D6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            int rowStart = 7;
            int stt = 1;
            foreach (var item in list ) 
            {

                if (item.SoLuong < 10)
                {
                    //    ws.Cells[string.Format("A{0}:D{1}",rowStart,rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    //    ws.Cells[string.Format("A{0}:D{1}", rowStart, rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = stt.ToString();
                ws.Cells[string.Format("A{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Sach.TenSach;
                ws.Cells[string.Format("B{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ws.Cells[string.Format("C{0}", rowStart)].Value = item.SoLuong;
                ws.Cells[string.Format("C{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ws.Cells[string.Format("D{0}", rowStart)].Value = item.DonGia;
                ws.Cells[string.Format("D{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                rowStart++;
                stt++;
            }

            ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Value = "TỔNG TIỀN THANH TOÁN:";
            ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Merge = true;
            ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            ws.Cells[string.Format("A{0}:C{1}", rowStart, rowStart)].Style.Font.Bold = true;

            ws.Cells[string.Format("D{0}", rowStart)].Value = products.TriGia;
            ws.Cells[string.Format("D{0}", rowStart)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[string.Format("D{0}", rowStart)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            ws.Cells[string.Format("D{0}", rowStart)].Style.Font.Bold = true;
            int rowEnd = rowStart + 2;

            ws.Cells[string.Format("A{0}:D{1}", rowEnd, rowEnd)].Value = "XIN CẢM ƠN QUÝ KHÁCH!";
            ws.Cells[string.Format("A{0}:D{1}", rowEnd, rowEnd)].Merge = true;
            ws.Cells[string.Format("A{0}:D{1}", rowEnd, rowEnd)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[string.Format("A{0}:D{1}", rowEnd, rowEnd)].Style.Border.Top.Style = ExcelBorderStyle.DashDot;


            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attactment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

            return View("MyView");
        }

        #endregion

       

       
    }
}