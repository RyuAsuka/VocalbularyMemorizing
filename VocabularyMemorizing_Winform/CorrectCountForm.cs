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
    public partial class CorrectCountForm : Form
    {
        List<CorrectCount> ccl = new List<CorrectCount>();
        const string correctCountPath = @"CorrectCount.txt";

        public CorrectCountForm()
        {
            InitializeComponent();
        }

        private void CorrectCountForm_Load(object sender, EventArgs e)
        {
            try
            {
                OpenFile();
                CorrectCount[] data = ccl.ToArray();
                chart1.DataSource = data;
                chart1.Series["Count"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                chart1.Series["Count"].XValueMember = "Word";
                chart1.Series["Count"].YValueMembers = "Count";
            }
            catch (IOException ex)
            {
                MessageBox.Show("文件正在被占用！");
            }
            catch (Exception ex)
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
                    string[] ss = s.Split(' ');
                    CorrectCount cc = new CorrectCount(ss[0], int.Parse(ss[1]));
                    ccl.Add(cc);
                }
            }
        }
    }
}
