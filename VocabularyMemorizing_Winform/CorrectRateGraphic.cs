using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VocabularyMemorizing.Common;

namespace VocabularyMemorizing_Winform
{
    public partial class CorrectRateGraphic : Form
    {
        const string correctCountPath = @"CorrectCount.txt";
        const string correctRatePath = @"CorrectRate.txt";

        List<CorrectRate> crl = new List<CorrectRate>();

        public CorrectRateGraphic()
        {
            InitializeComponent();
        }

        private void CorrectRateGraphic_Load(object sender, EventArgs e)
        {
            try
            {
                OpenFile();
                CorrectRate[] chartData = crl.ToArray();
                chart1.DataSource = chartData;
                
                chart1.Series["CorrectRate"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["CorrectRate"].XValueMember = "PassTime";
                chart1.Series["CorrectRate"].YValueMembers = "CorrectRateValue";
                
            }
            catch(IOException ex)
            {
                MessageBox.Show("文件正在被占用！");
            }
            catch(Exception ex)
            {
                MessageBox.Show("{0}", ex.Message);
            }

        }

        private void OpenFile()
        {
            openFileDialog1.ShowDialog();
            Stream fs = openFileDialog1.OpenFile();
            string filename = openFileDialog1.FileName;
            
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] ss = s.Split(',');
                    CorrectRate cr = new CorrectRate(int.Parse(ss[0]), double.Parse(ss[1].Remove(ss[1].Length-1)));
                    crl.Add(cr);
                }
            }

        }
    }
}
