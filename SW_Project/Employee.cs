using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;


namespace SW_Project
{
    public partial class Employee : Form
    {

        OracleDataAdapter adpter;
        OracleCommandBuilder commandBuilder;
        DataSet ds;

        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            string database = "Data Source =orcl;user id =scott;password =tiger;";
            string command = "select productid from products";


            adpter = new OracleDataAdapter(command, database);
            ds = new DataSet();
            adpter.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                comboBox1.Items.Add(row["productid"]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string database = "Data Source =orcl;user id =scott;password =tiger;";
            string command = "select * from products";
            adpter = new OracleDataAdapter(command, database);
            ds = new DataSet();
            adpter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            commandBuilder = new OracleCommandBuilder(adpter);
            adpter.Update(ds.Tables[0]);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string database = "Data Source =orcl;user id =scott;password =tiger;";
            string v = comboBox1.SelectedItem.ToString();
            string command = "select * from products where productid ="+v;
            adpter = new OracleDataAdapter(command, database);
            ds = new DataSet();
            adpter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

    }
}
