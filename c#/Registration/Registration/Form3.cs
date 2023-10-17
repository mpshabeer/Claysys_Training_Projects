
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registration
{

    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }


        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            String username, password;
            username = txt_username.Text;
            password = txt_password.Text;
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-U0M528H\SQLEXPRESS;Initial Catalog=myproject;Integrated Security=True");

            try
            {
                // string query= "SELECT * FROM login WHERE username = '"+txt_username.Text +'"AND password="'+ txt_password.Text+"'";
                string query = "SELECT * FROM registration1 WHERE username = '" + txt_username.Text + "' AND password = '" + txt_password.Text + "'";

                //  string query = "SELECT * FROM login WHERE username = @username AND password = @password";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    username = txt_username.Text;
                    password = txt_password.Text;

                    MessageBox.Show("Login sucessful");
                    Form4 frm =new Form4();
                    
                    this.Close();
                    frm.Show();

                }
                else
                {
                    MessageBox.Show("Invalid Login Details", "Error", MessageBoxButtons.OK);
                    txt_username.Clear();
                    txt_password.Clear();

                }
;
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            txt_password.PasswordChar = '*';
           
        }

        private void txt_username_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
