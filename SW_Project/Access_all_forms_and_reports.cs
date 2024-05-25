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
    public partial class Access_all_forms_and_reports : Form
    {
        public Access_all_forms_and_reports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            customer cust = new customer();
            cust.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VT v = new VT();
            v.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VD vD = new VD();
            vD.Show();
        }
    }
}
