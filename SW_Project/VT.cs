using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SW_Project
{
    public partial class VT : Form
    { 
        ViewTransactions transactions;
        public int ReceivedID { get; set; }
        public VT()
        {

            InitializeComponent();
        }

        private void VT_Load(object sender, EventArgs e)
        {
            textBox1.Text = ReceivedID.ToString();
            transactions = new ViewTransactions();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            transactions.SetParameterValue(0, textBox1.Text);
            crystalReportViewer1.ReportSource = transactions;
        }
    }
}
