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
    public partial class customer : Form
    {
        string db = "data source =orcl;user id =scott;password =tiger";
        OracleConnection conn ;
        public int ReceivedID { get; set; }
        
        
        public customer()
        {
            InitializeComponent();
        }

        private void customer_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(db);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "getProductsName";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("names", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader rd  = cmd.ExecuteReader();
            while(rd.Read())
            {
                comboBox1.Items.Add(rd[0]);

            }
            rd.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            float priceAfterDiscount = 0,discount,price;
            
            conn = new OracleConnection(db);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select price,numberinstock,discount from products " +
                              "where productname =:name";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("name", comboBox1.SelectedItem.ToString());
            OracleDataReader dr = cmd.ExecuteReader();
            

            if(dr.Read())
            {
                price = Convert.ToInt32(dr[0].ToString());
                discount = Convert.ToInt32(dr[2].ToString());
                priceAfterDiscount = price - (discount / 100) * price;
                textBox1.Text = price.ToString();
                textBox2.Text = dr[1].ToString();
                textBox3.Text = discount.ToString();
            }
            textBox4.Text = priceAfterDiscount.ToString();
            dr.Close();

        }

        private void customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int numberInStock = 0;
            conn = new OracleConnection(db);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select numberinstock from products where productname =:name";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("name", comboBox1.Text);
            OracleDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                numberInStock = Convert.ToInt32(dr[0].ToString());
                if (numberInStock == 0)
                { MessageBox.Show("this product doesnt exist in stock"); }
                else
                {
                    numberInStock -= 1;
                }
            }
            dr.Close();            
            if(numberInStock > 0)
            {
                OracleCommand comm = new OracleCommand();
                comm.Connection = conn;

                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand.Connection = conn;

                oracleCommand.CommandText = "update products set numberinstock =:num where productname = "
                                            + ":name";
                oracleCommand.CommandType = CommandType.Text;
                oracleCommand.Parameters.Add("num", numberInStock);
                oracleCommand.Parameters.Add("name", comboBox1.Text);
                oracleCommand.ExecuteNonQuery();

                comm.CommandText = "insert into transactions values(:id,:name,:price,:discount)";
                comm.CommandType = CommandType.Text;
                comm.Parameters.Add("id", ReceivedID);
                comm.Parameters.Add("name", comboBox1.Text);
                comm.Parameters.Add("price", textBox1.Text);
                comm.Parameters.Add("discount", textBox3.Text);
                comm.ExecuteNonQuery();

            }
            else
            {
                MessageBox.Show("we dont have this product");
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            VT view = new VT();
            view.ReceivedID = ReceivedID;
            view.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VD view = new VD();
            view.Show();
        }
    }
}
