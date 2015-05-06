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
    public partial class MainForm : Form
    {
        WordList wl = new WordList();
        Random rd = new Random();
        int rdn = 0;
        bool moveToNext = false;
        int count = 1;

        public MainForm()
        {
            InitializeComponent();
            btnNext.Enabled = false;
            btnFinish.Enabled = false;
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Stream st = openFileDialog1.OpenFile();
            using(StreamReader sr = new StreamReader(st))
            {
                while(!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] ss = s.Split(':');
                    wl.AddWord(ss);
                }
            }

            GetWord();

        }

        private void GetWord()
        {
            rdn = rd.Next(wl.Count());
            if (!wl.IsPassed(rdn))
            {
                txtMeaning.Text = wl.GetMeaning(rdn);
                lblHint.Text = wl.GetHint(rdn);
                btnNext.Enabled = false;
                btnFinish.Enabled = false;
                lblNumber.Text = count.ToString();
                count++;
            }
            else
            {
                GetWord();
            }
        }

        private void txtAnswer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r' && !moveToNext)
            {
                if(lblHint.Text + txtAnswer.Text == wl.GetContent(rdn))
                {
                    txtResult.Text = "Correct!";
                    wl.SetCorrect(rdn, true);
                }
                else
                {
                    txtResult.Text = "Incorrect! The answer is " + wl.GetContent(rdn);
                    wl.SetCorrect(rdn, false);
                }

                wl.SetPassed(rdn, true);
                
                if(wl.IsAllFinished())
                {
                    btnNext.Enabled = false;
                    btnFinish.Enabled = true;
                    moveToNext = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnFinish.Enabled = false;
                    moveToNext = true;
                }
            }
            else if (e.KeyChar == '\r' && moveToNext)
            {
                GetWord();
                txtAnswer.Text = "";
                txtResult.Text = "";
                moveToNext = false;
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetWord();
            txtAnswer.Text = "";
            txtResult.Text = "";
            moveToNext = false;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("correct ratio = {0:P}", wl.CorrectRatio()), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
