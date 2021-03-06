﻿using ClosedXML.Excel;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuicContentLoader;
using Excel = Microsoft.Office.Interop.Excel;
using Helper = TuicContentLoader.Helpers.Helper;

namespace WindowsFormsApplication1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        int time = 10;
        List<Cabin> cabins;
        IWebDriver driver;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startLoad = DateTime.Now;
            string json = GET("http://derwolters.de/json.json");
            DateTime endLoad = DateTime.Now;
            List<Trips> trips = JsonConvert.DeserializeObject<List<Trips>>(json);


            if (txtTripsCode.Text.Length > 0)
            {
                List<Trips> searchList = new List<Trips>();
                foreach (Trips trip in trips)
                {
                    if (trip.Code.Contains(txtTripsCode.Text))
                    {
                        searchList.Add(trip);
                    }
                }

                dataGridView1.DataSource = searchList;
            }
            else
            {
                dataGridView1.DataSource = trips;
            }

            dataGridView1.AutoResizeColumns();

            Console.WriteLine(startLoad.Ticks + " " + endLoad.Ticks);
            float sum = endLoad.Ticks - startLoad.Ticks;
            lblTripsTimerOld.Text = lblTripsTimer.Text;
            lblTripsTimer.Text = (sum / 1000000).ToString();

            Console.WriteLine(lblTripsTimerOld.Text);
            float sumOld = float.Parse(lblTripsTimerOld.Text);

            if ((sum / 1000000) < float.Parse(lblTripsTimerOld.Text))
            {
                lblTripsArrow.ForeColor = Color.Green;
                lblTripsArrow.Text = "k";
            }
            else
            {
                lblTripsArrow.ForeColor = Color.Red;
                lblTripsArrow.Text = "m";
            }
            Console.WriteLine("SUM: " + sum / 10000000);
        }

        // Routes laden
        private void button2_Click(object sender, EventArgs e)
        {
            DateTime startLoad = DateTime.Now;
            string json = GET("http://derwolters.de/03_Response_Routes.txt");
            DateTime endLoad = DateTime.Now;
            List<Routes> routes = JsonConvert.DeserializeObject<List<Routes>>(json);

            dataGridRoutes.DataSource = routes;
            dataGridRoutes.AutoResizeColumns();

            float sum = endLoad.Ticks - startLoad.Ticks;
            lblRoutesTimerOld.Text = lblRoutesTimer.Text;
            lblRoutesTimer.Text = (sum / 10000000).ToString();

            float sumOld = float.Parse(lblTripsTimerOld.Text);

            if ((sum / 1000000) < float.Parse(lblTripsTimerOld.Text))
            {
                lblRoutesArrow.ForeColor = Color.Green;
                lblRoutesArrow.Text = "k";
            }
            else
            {
                lblRoutesArrow.ForeColor = Color.Red;
                lblRoutesArrow.Text = "m";
            }
        }

        // Cabin laden
        private void button3_Click(object sender, EventArgs e)
        {
            DateTime startLoad = DateTime.Now;
            string json = GET("http://derwolters.de/09_Response_GetCabins.txt");
            DateTime endLoad = DateTime.Now;
            cabins = JsonConvert.DeserializeObject<List<Cabin>>(json);

            dataGridCabins.DataSource = cabins;
            dataGridCabins.AutoResizeColumns();

            float sum = endLoad.Ticks - startLoad.Ticks;
            lblCabinsTimerOld.Text = lblCabinsTimer.Text;
            lblCabinsTimer.Text = (sum / 10000000).ToString();

            float sumOld = float.Parse(lblCabinsTimerOld.Text);

            if ((sum / 1000000) < float.Parse(lblCabinsTimerOld.Text))
            {
                lblCabinsArrow.ForeColor = Color.Green;
                lblCabinsArrow.Text = "k";
            }
            else
            {
                lblCabinsArrow.ForeColor = Color.Red;
                lblCabinsArrow.Text = "m";
            }

        }

        // Ports laden
        private void button4_Click(object sender, EventArgs e)
        {
            DateTime startLoad = DateTime.Now;
            string json = GET("http://derwolters.de/12_Response_GetPorts.txt");
            DateTime endLoad = DateTime.Now;
            List<Port> ports = JsonConvert.DeserializeObject<List<Port>>(json);

            dataGridPorts.DataSource = ports;
            dataGridPorts.AutoResizeColumns();

            float sum = endLoad.Ticks - startLoad.Ticks;
            lblPortsTimerOld.Text = lblPortsTimer.Text;
            lblPortsTimer.Text = (sum / 10000000).ToString();

            float sumOld = float.Parse(lblPortsTimerOld.Text);

            if ((sum / 1000000) < float.Parse(lblPortsTimerOld.Text))
            {
                lblPortsArrow.ForeColor = Color.Green;
                lblPortsArrow.Text = "k";
            }
            else
            {
                lblPortsArrow.ForeColor = Color.Red;
                lblPortsArrow.Text = "m";
            }
        }

        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = Convert.ToString(time);
            progressBar1.Value = time;
            if (time == 0)
            {
                button1_Click(sender, e);
                button2_Click(sender, e);
                button3_Click(sender, e);
                button4_Click(sender, e);
                button5_Click_1(sender, e);
                time = int.Parse(txtTimerValue.Text);
            }
        }

        private void dataGridCabins_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                foreach (Cabin kabine in cabins)
                {
                    if (kabine.CabinNo.Equals(dataGridCabins.Rows[e.RowIndex].Cells[1].Value))
                    {
                        dataGridEquipment.DataSource = kabine.Equipment;
                        dataGridEquipment.AutoResizeColumns();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                btnTimerEnabled.BackColor = Color.Red;
                btnTimerEnabled.Text = "Autoload inaktiv";
                txtTimerValue.Enabled = true;
            }
            else
            {
                try
                {
                    time = int.Parse(txtTimerValue.Text);
                    timer1.Enabled = true;
                    progressBar1.Maximum = time;
                    btnTimerEnabled.BackColor = Color.Green;
                    btnTimerEnabled.Text = "Autoload aktiv";
                    txtTimerValue.Enabled = false;
                }
                catch (Exception eec)
                {
                    MessageBox.Show(eec.Message, "Fehler");
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("ROWCOUNT: " + dataGridView1.RowCount);

            ExcelLoader loader = new ExcelLoader();
            Excel.Workbook wb = loader.getWorkbook("C:/Users/fleet/Documents/myWorkbook.xlsx");

            Excel.Worksheet sheet = (Excel.Worksheet)wb.Worksheets.get_Item(1);
            Excel.Range range = sheet.UsedRange;
            dataGridView1.SuspendLayout();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[3].Value = (range.Cells[i + 1, 1] as Excel.Range).Value2;
                if (dataGridView1.Rows[i].Cells[2].Value.Equals(dataGridView1.Rows[i].Cells[3].Value))
                {
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Green;
                    dataGridView1.Rows[i].Cells[4].Value = "OK";
                }
                else
                {
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Red;
                    dataGridView1.Rows[i].Cells[4].Value = "FEHLER";
                }
            }
            dataGridView1.AutoResizeColumns();
            dataGridView1.ResumeLayout();
            loader.quit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TuicContentLoader.Settings1 settings = new Settings1();
            txtTimerValue.Text = settings.timerValue;
            lblTimer.Text = settings.timerValue;
            progressBar1.Maximum = Int16.Parse(settings.timerValue);

            lblTripsArrow.Text = "";
            lblRoutesArrow.Text = "";
            lblCabinsArrow.Text = "";
            lblPortsArrow.Text = "";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            TuicContentLoader.Settings1 settings = new Settings1();
            settings.timerValue = txtTimerValue.Text;
            settings.Save();

            if (driver != null)
            {
                driver.Quit();
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (metroComboBox1.Text)
            {
                case "Default":
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Default;
                    break;

                case "Dark":
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
                    break;
                case "Light":
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
                    break;
            }
        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (metroComboBox2.Text)
            {
                case "Default":
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Default;
                    break;

                case "Blue":
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Blue;
                    break;
                case "Black":
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Black;
                    break;

                case "Silver":
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Silver;
                    break;
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver(Directory.GetCurrentDirectory());
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Url = "https://tuicruises.com/kreuzfahrt-buchen";
            driver.FindElement(By.CssSelector("input[type='submit'][value='Reise finden']")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("filter-options-toggle")));
            driver.FindElement(By.Id("filter-options-toggle")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.noUiSlider.noUi-target.noUi-ltr.noUi-horizontal.noUi-background > div > div:nth-child(2)")));

            /*
            Actions action = new Actions(driver);
            action.ClickAndHold(driver.FindElement(By.CssSelector("div.noUiSlider.noUi-target.noUi-ltr.noUi-horizontal.noUi-background > div > div:nth-child(2)")));
            action.Perform();
            
            for (int i = 0; i < 120; i++)
            {
                action.MoveByOffset(-5, 0);
                action.Perform();
                Console.WriteLine("Offset: {0}  Tage: {1}", i, driver.FindElement(By.CssSelector("[class='max']")).Text);
                Thread.Sleep(100);
            }

            //action.ClickAndHold(driver.FindElement(By.CssSelector("div.noUiSlider.noUi-target.noUi-ltr.noUi-horizontal.noUi-background > div > div:nth-child(2)"))).MoveByOffset(-100, 0).Release();
            //action.Perform();
            */


            try
            {
                selectMaxDays(driver, Int32.Parse(txtCruiseDays.Text));
            }
            catch (Exception exception)
            {
                selectMaxDays(driver, 42);
            }

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("searchoptionblocker")));

            driver.FindElement(By.Id("startingPort-6")).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("searchoptionblocker")));

            IList<IWebElement> reisen = driver.FindElements(By.CssSelector("div[class='route-item']"));
            Console.WriteLine("Anzahl der Reisen: {0}", reisen.Count);

            IList<IWebElement> clearedlist = getRealReisen(reisen);
            Console.WriteLine("Cleared: {0}", clearedlist.Count);

            IList<CruiseData> rawData = getCruiseData(clearedlist);
            gridFoundCruises.DataSource = rawData;

            IList<CruiseData> compareList = ExcelLoader.getCruisedataFromExcel();
            Console.WriteLine("SIZE COMPARELIST: {0}", compareList.Count);

            var enumList = compareList.Except(rawData).ToList();
            var enumList2 = rawData.Except(compareList).ToList();
            Console.WriteLine("ENUMLIST COUNT: " + enumList.Count);
            Console.WriteLine("ENUMLIST2 COUNT: " + enumList2.Count);

            //var diffList = getDiffList(compareList, rawData);

            if (compareList.Equals(ExcelLoader.getCruisedataFromExcel()))
            {
                Console.WriteLine("GLEICH");
            }
            else
            {
                Console.WriteLine("NEIN");
            }

            // driver.Quit();
        }

        public IList<CruiseData> getDiffList(IList<CruiseData> one, IList<CruiseData> two)
        {
            List<CruiseData> diffList = new List<CruiseData>();
            for (int i = 0; i < one.Count; i++)
            {
                if (!one.ElementAt(i).Equals(two.ElementAt(i)))
                {
                    diffList.Add(two.ElementAt(i));
                }
            }

            return diffList;
        }

        public IList<CruiseData> getCruiseData(IList<IWebElement> clearedList)
        {
            IList<CruiseData> cruiseDataList = new List<CruiseData>();
            foreach (IWebElement element in clearedList)
            {
                CruiseData cd = new CruiseData();
                cd.cruise = element.FindElement(By.CssSelector("a > span > span.details-body > h4")).Text;

                try
                {
                    cd.price = element.FindElement(By.CssSelector("[class='cruiseOnly price']")).Text;
                }
                catch
                {
                    cd.price = element.FindElement(By.CssSelector("[class='fully-booked']")).Text;
                }

                cd.ship = element.FindElement(By.CssSelector("[class='ship']")).Text;

                cruiseDataList.Add(cd);
            }

            return cruiseDataList;
        }

        public class CruiseData
        {
            public string cruise { get; set; }
            public string ship { get; set; }
            public string price { get; set; }
        }

        public IList<IWebElement> getRealReisen(IList<IWebElement> liste)
        {
            List<IWebElement> realList = new List<IWebElement>();
            foreach (IWebElement e in liste)
            {
                if (e.GetAttribute("style").Equals(""))
                {
                    realList.Add(e);
                }
            }
            return realList;
        }


        public void selectMaxDays(IWebDriver driver, int day)
        {
            try
            {
                Actions action = new Actions(driver);
                int offset = Helper.getOffset(day);
                action.ClickAndHold(driver.FindElement(By.CssSelector("div.noUiSlider.noUi-target.noUi-ltr.noUi-horizontal.noUi-background > div > div:nth-child(2)"))).MoveByOffset(offset, 0).Release();
                action.Perform();
            }
            catch (Exception exception) { Console.WriteLine(exception); }


        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                selectMaxDays(driver, Int32.Parse(metroTextBox1.Text));
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("Bitte nur eine Zahl in das Feld eingeben", "ArgumentNullException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Bitte nur eine Zahl in das Feld eingeben", "FormatException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException eo)
            {
                MessageBox.Show("Bitte nur eine Zahl in das Feld eingeben", "OverflowException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            var cruisedataList = ExcelLoader.getCruisedataFromExcel();
        }
    }
}
