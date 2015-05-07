﻿using System;
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
        StatData sd;
        int rdn = 0;
        bool moveToNext = false;
        int count = 1;
        double CorrectRate = 0.0;
        int passTime = 1;

        const string correctCountPath = @"CorrectCount.txt";
        const string correctRatePath = @"CorrectRate.txt";

        public MainForm()
        {
            InitializeComponent();
            btnNext.Enabled = false;
            btnFinish.Enabled = false;
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                Stream st = openFileDialog1.OpenFile();
                using (StreamReader sr = new StreamReader(st))
                {
                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        string[] ss = s.Split(':');
                        wl.AddWord(ss);
                    }
                }

                FileStream fs = new FileStream(correctCountPath, FileMode.OpenOrCreate);
                if(fs.Length == 0)
                {
                    sd = new StatData(wl);
                }
                else
                {
                    List<string[]> filedata = new List<string[]>();
                    using(StreamReader sr = new StreamReader(fs))
                    {
                        while(!sr.EndOfStream)
                        {
                            string s = sr.ReadLine();
                            string[] ss = s.Split(' ');
                            filedata.Add(ss);
                        }
                        sd = new StatData(filedata);
                    }
                }
                fs.Close();

                fs = new FileStream(correctRatePath, FileMode.OpenOrCreate);
                using(StreamReader sr = new StreamReader(fs))
                {
                    while(!sr.EndOfStream)
                    {
                        sr.ReadLine();
                        passTime++;
                    }
                }

                count = 1;
                GetWord();
            }
            catch(Exception ex)
            {
                // do nothing.
            }

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
                    sd.SelfAdd(wl.GetContent(rdn));
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
            txtMeaning.Text = "";
            txtAnswer.Text = "";
            txtResult.Text = "";
            lblHint.Text = "";
            moveToNext = false;
            btnNext.Enabled = false;
            btnFinish.Enabled = false;
            CorrectRate = wl.CorrectRatio();
            MessageBox.Show(string.Format("correct ratio = {0:P}", wl.CorrectRatio()), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            StatSave();
        }

        private void StatSave()
        {
            using (StreamWriter sw = new StreamWriter(correctCountPath))
            {
                for(int i = 0; i < sd.Count; i++)
                {
                    sw.WriteLine("{0} {1}", sd.GetItem(i).Key, sd.GetItem(i).Value) ;
                }
            }

            using(StreamWriter sw = new StreamWriter(correctRatePath, true))
            {
                sw.WriteLine("{0},{1:P}", passTime, CorrectRate);
            }
        }


    }
}
