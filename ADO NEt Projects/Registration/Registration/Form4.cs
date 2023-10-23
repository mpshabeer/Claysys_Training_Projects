
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace Registration
{

    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }


        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            String username, command, phone, mail;
            username = txt_username.Text;
            command = txt_command.Text;
            phone=txt_phone.Text;
            mail=txt_mail.Text; 

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-U0M528H\SQLEXPRESS;Initial Catalog=myproject;Integrated Security=True");

           
               

                SqlCommand cmd = new SqlCommand("INSERT INTO contact(name,mail,command,phone) VALUES(@name,@mail,@command,@phone)", con);


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name",username);
                cmd.Parameters.AddWithValue("@mail",mail);
                cmd.Parameters.AddWithValue("@command",command);
                cmd.Parameters.AddWithValue("@phone",phone);
              


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Your request was recived");
                Form1 form1 = new Form1();
                this.Close();
                form1.Show();

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            txt_phone.PasswordChar = '*';

        }

        private void txt_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Close();
            form.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

