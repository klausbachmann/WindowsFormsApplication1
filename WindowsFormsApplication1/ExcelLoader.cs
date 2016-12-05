using System;
using System.Collections;
using System.Collections.Generic;
using static WindowsFormsApplication1.Form1;
using Excel = Microsoft.Office.Interop.Excel;
using XLS = ClosedXML.Excel;

namespace TuicContentLoader
{
    public class ExcelLoader
    {
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;

        public Excel.Workbook getWorkbook(string sFile)
        {

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(sFile, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "t", false, false, 0, true, 1, 0);

            return xlWorkBook;
        }

        public void quit()
        {
            xlWorkBook.Close(false, null, null);
            xlApp.Quit();
        }

        public static XLS.XLWorkbook getClosedXMLWorkbook()
        {
            XLS.XLWorkbook workbook = new XLS.XLWorkbook(@"C:\Users\fleet\Documents\cruises.xlsx");
            var worksheet = workbook.Worksheet(1);
            var usedRange = worksheet.RangeUsed();

            Console.WriteLine("USED: {0}", usedRange.RowCount());

            return workbook;
        }

        public static IList<CruiseData> getCruisedataFromExcel()
        {
            IList<CruiseData> cruisedataList = new List<CruiseData>();
            XLS.XLWorkbook workbook = new XLS.XLWorkbook(@"C:\Users\fleet\Documents\cruises.xlsx");
            var worksheet = workbook.Worksheet(1);
            var usedRange = worksheet.RangeUsed();

            for (int i = 1; i <= usedRange.RowCount(); i++)
            {
                CruiseData cs = new CruiseData();
                cs.cruise = worksheet.Row(i).Cell(1).GetValue<String>();
                cs.ship = worksheet.Row(i).Cell(2).GetValue<String>();
                cs.price = worksheet.Row(i).Cell(3).GetValue<String>();
                cruisedataList.Add(cs);
            }
            workbook.Dispose();
            return cruisedataList;
        }

    }

    
}
