
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
    public partial class Register : Form
    {
        string source = "data source =orcl;user id =scott;password =tiger";
        OracleConnection connect;

        public Register()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                int maxid, newid;
                connect = new OracleConnection(source);
                connect.Open();
                OracleCommand command = new OracleCommand();
                OracleCommand getID = new OracleCommand();

                command.Connection = connect;
                getID.Connection = connect;

                getID.CommandText = "getuserid";
                getID.CommandType = CommandType.StoredProcedure;
                getID.Parameters.Add("id",OracleDbType.Int32,ParameterDirection.Output);
                getID.ExecuteNonQuery();
                maxid = Convert.ToInt32(getID.Parameters["id"].Value.ToString());
                newid = maxid + 1;

                command.CommandText = "insert into userinfo values (:idx,:username,:password)";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("idx", newid);
                command.Parameters.Add("username", textBox1.Text);
                command.Parameters.Add("password", textBox2.Text);
                command.ExecuteNonQuery(); 

            }
            else if(radioButton2.Checked)
            {
                int maxid, newid;
                connect = new OracleConnection(source);
                connect.Open();
                OracleCommand command = new OracleCommand();
                OracleCommand getID = new OracleCommand();

                command.Connection = connect;
                getID.Connection = connect;

                getID.CommandText = "getemployeeid";
                getID.CommandType = CommandType.StoredProcedure;
                getID.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
                getID.ExecuteNonQuery();
                maxid = Convert.ToInt32(getID.Parameters["id"].Value.ToString());
                newid = maxid + 1;

                command.CommandText = "insert into employees values (:idx,:username,:password)";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("idx", newid);
                command.Parameters.Add("username", textBox1.Text);
                command.Parameters.Add("password", textBox2.Text);
                command.ExecuteNonQuery();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                int id = 0;
                connect = new OracleConnection(source);
                connect.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connect;
                cmd.CommandText = "select username,password,userid from userinfo " +
                                   "where username =:name and password =:pass";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("name", textBox1.Text);
                cmd.Parameters.Add("pass", textBox2.Text);
            
                OracleDataReader rd = cmd.ExecuteReader();
                
                if (rd.Read())
                {
                    id = Convert.ToInt32(rd[2].ToString());
                    customer cust = new customer();
                    cust.ReceivedID = id;
                    cust.Show();
                }
                else
                {
                    MessageBox.Show("invalid username or password");
                }
            }
            else if (radioButton2.Checked)
            {
                connect = new OracleConnection(source);
                connect.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connect;
                cmd.CommandText = "select empname,password from employees " +
                                   "where empname =:name and password =:pass";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("name", textBox1.Text);
                cmd.Parameters.Add("pass", textBox2.Text);
                OracleDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Employee emp = new Employee();
                    emp.Show();
                }
                else
                {
                    MessageBox.Show("invalid username or password");
                }

            }
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            connect.Dispose();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
