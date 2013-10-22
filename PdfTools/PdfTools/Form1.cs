using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ClassTemplate;
using ClassTemplate.Student;

namespace DownloadPdf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var th = new Thread(StudentStart);
            th.Start();
        }

        private void StudentStart()
        {
            var ct = new StudentIndex();
            var name = ct.CreateReport();
            richTextBox1.AppendText("已生成："+name);
        }
        
   
        private void button2_Click(object sender, EventArgs e)
        {
            var ct = new StudentIndex();
            ct.Test();
        }

    }
}
