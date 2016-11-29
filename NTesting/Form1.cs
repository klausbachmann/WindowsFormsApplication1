using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using help = NTesting.Helpers.Helpers;

namespace NTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            String dir = Directory.GetCurrentDirectory();
            //Process p = new Process();
            //p.StartInfo.FileName = "nunit3-console.exe NTesting.dll";
            //p.Start();

            Process.Start("nunit3-console.exe", "NTesting2.dll").WaitForExit();


            if (File.Exists("TestResult.xml"))
            {
                Console.WriteLine("Datei ist vorhanden");
                XmlReader reader = XmlReader.Create("TestResult.xml");

                bool inTestCase = false;

                List<TestCaseModel> cases = new List<TestCaseModel>();
                TestCaseModel m;
                while (reader.Read())
                {

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "test-run"))
                    {
                        Console.WriteLine("ID: {0} RESULT: {1} TOTAL: {2} PASSED: {3} FAILED: {4}", reader.GetAttribute("id"), reader.GetAttribute("result"), reader.GetAttribute("total"), reader.GetAttribute("passed"), reader.GetAttribute("failed"));
                    }


                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "test-case"))
                    {
                        m = new TestCaseModel();
                        m.id = reader.GetAttribute("id");
                        m.name = reader.GetAttribute("name");
                        m.fullname = reader.GetAttribute("fullname");
                        m.methodname = reader.GetAttribute("methodname");
                        m.classname = reader.GetAttribute("classname");
                        m.runstate = reader.GetAttribute("runstate");
                        m.seed = reader.GetAttribute("seed");
                        m.result = reader.GetAttribute("result");
                        m.startTime = reader.GetAttribute("start-time");
                        m.endTime = reader.GetAttribute("end-time");
                        m.duration = reader.GetAttribute("duration");
                        m.asserts = reader.GetAttribute("asserts");
                        inTestCase = true;
                        cases.Add(m);
                    }

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "message"))
                    {
                        if (inTestCase)
                        {
                            TestCaseModel lastItem = cases.Last<TestCaseModel>();
                            lastItem.message = reader.ReadElementContentAsString();
                            inTestCase = false;
                            reader.Read();
                            lastItem.stacktrace = reader.ReadElementContentAsString();
                        }
                    }

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "test-suite"))
                    {
                        inTestCase = false;
                    }

                }
                reader.Close();
                Console.WriteLine("Anzahl cases: {0}", cases.Count);
                dataGridView1.DataSource = cases;
            }
        }

        public class TestCaseModel
        {
            public string id { get; set; }
            public string name { get; set; }
            public string fullname { get; set; }
            public string methodname { get; set; }
            public string classname { get; set; }
            public string runstate { get; set; }
            public string seed { get; set; }
            public string result { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public string duration { get; set; }
            public string asserts { get; set; }
            public string message { get; set; }
            public string stacktrace { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                
                DateTime input = DateTime.Parse(textBox1.Text);
              
                Console.WriteLine("Day: {0} DayOfWeek: {1} Month: {2}", input.Day, input.DayOfWeek, input.Month);
                textBox2.Text = string.Format("{0}, {1}.{2}.{3}", help.getDayName(input.DayOfWeek), input.Day, input.Month, input.Year);
            }
            catch (Exception ec) {
                textBox2.Text = "Ungültig!";
            }
        }

        public string getDayName2(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Mo";
                case DayOfWeek.Tuesday:
                    return "Di";
                case DayOfWeek.Wednesday:
                    return "Mi";
                case DayOfWeek.Thursday:
                    return "Do";
                case DayOfWeek.Friday:
                    return "Fr";
                case DayOfWeek.Saturday:
                    return "Sa";
                case DayOfWeek.Sunday:
                    return "So";

                default: return "Mo";
            }
        }
    }
}
