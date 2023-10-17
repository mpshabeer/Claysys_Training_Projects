using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using System.Text.RegularExpressions;

namespace Registration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

     

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-U0M528H\SQLEXPRESS;Initial Catalog=myproject;Integrated Security=True");

            //SqlCommand cmd = new SqlCommand("SELECT * FROM registration",con);

           // DataTable dt=new DataTable();
             

           // con.Open();

           // SqlDataReader sdr = cmd.ExecuteReader();
           // dt.Load(sdr);
          //  con.Close();
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            password.PasswordChar = '*';
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void States_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Kerala")
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "Choose City";
                comboBox2.Items.Add("Kochi");
                comboBox2.Items.Add("Calicut");
                comboBox2.Items.Add("Malappuram");
            }
           else if (comboBox1.SelectedItem == "Tamilnadu")
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "Choose City";
                comboBox2.Items.Add("Chennai");
                comboBox2.Items.Add("Madurai");
                comboBox2.Items.Add("selam");
            }
            else if (comboBox1.SelectedItem == "Karnataka")
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "Choose City";
                comboBox2.Items.Add("Bengaluru");
                comboBox2.Items.Add("Mysuru");
                comboBox2.Items.Add("Mangaluru");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Isvalid())
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-U0M528H\SQLEXPRESS;Initial Catalog=myproject;Integrated Security=True");

                SqlCommand cmd = new SqlCommand("INSERT INTO registration1(fname,lname,email,phone,age,address,state,city,gender,username,password) VALUES(@fname,@lname,@email,@phone,@age,@address,@state,@city,@gender,@ussername,@password)", con);
                

              //  DataTable dt = new DataTable();

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@fname", fname.Text);
                cmd.Parameters.AddWithValue("@lname", lname.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@phone", phone.Text);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt32(age.Text));
                cmd.Parameters.AddWithValue("@address", adr.Text);
                cmd.Parameters.AddWithValue("@state", comboBox1.Text);
                cmd.Parameters.AddWithValue("@city", comboBox2.Text);
                cmd.Parameters.AddWithValue("@gender", gender.Text);
                cmd.Parameters.AddWithValue("@ussername", username.Text);
                cmd.Parameters.AddWithValue("@password", password.Text);



                con.Open();
                cmd.ExecuteNonQuery(); 

               // SqlDataReader sdr = cmd.ExecuteReader();
               // dt.Load(sdr);
                con.Close();
                MessageBox.Show("Registration Success");

                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();


                phone.Clear();
                fname.Clear();
                lname.Clear ();
                email.Clear ();
                password.Clear();
                cpassword.Clear();
                age.Clear();
                adr.Clear();
                comboBox2.Items.Clear();
                username.Clear();
            }
        }

        private bool Isvalid()
        {
            if (fname.Text == String.Empty)
            {
                MessageBox.Show("First Name is Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fname.Focus();
                return false;
            }
            else if (!Regex.IsMatch(fname.Text, @"^[a-zA-Z ]+$"))
            {
                MessageBox.Show("First Name Mustbe Letters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fname.Focus();
                return false;
            }
            else if (lname.Text == String.Empty)
            {
                MessageBox.Show("Last Name is Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                lname.Focus();
            }
            else if (!Regex.IsMatch(lname.Text, @"^[a-zA-Z ]+$"))
            {
                MessageBox.Show("Last Name Mustbe Letters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lname.Focus();
                return false;
            }


            else if (email.Text == String.Empty)
            {
                MessageBox.Show("E-Mail  is Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                email.Focus();
            }

            else if (!Regex.IsMatch(email.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                MessageBox.Show("Enter a valid E-mail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lname.Focus();
                return false;
            }

           
            else if (phone.Text == String.Empty)
            {
                MessageBox.Show("Phone number is Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                phone.Focus();
            }
            else if (!Regex.IsMatch(phone.Text, @"^[6-9]\d{9}$"))
            {
                MessageBox.Show("Enter a valid Phone Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lname.Focus();
                return false;
            }

            else if (age.Text == String.Empty)
            {
                MessageBox.Show("Age  is Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                age.Focus();
            }
          
            else if (adr.Text == String.Empty)
            {
                MessageBox.Show(" Address is Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                adr.Focus();
            }
            else if (gender.Text == String.Empty)
            {
                MessageBox.Show("Gender is Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                gender.Focus();
            }
            else if (comboBox1.Text == String.Empty)
            {
                MessageBox.Show("Choose your state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                comboBox1.Focus();
            }
            else if (comboBox2.Text == String.Empty)
            {
                MessageBox.Show("Choose your City", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                comboBox2.Focus();
            }
            else if (username.Text == String.Empty)
            {
                MessageBox.Show("Username is Required\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                username.Focus();
            }
            else if (password.Text == String.Empty)
            {
                MessageBox.Show("Password is Required\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                password.Focus();
            }
            else if (cpassword.Text == String.Empty)
            {
                MessageBox.Show("Password is Required\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                cpassword.Focus();
            }
            else if (!Regex.IsMatch(password.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))
            {
                MessageBox.Show("Password must contain at least one digit, one lowercase letter, one uppercase letter, and be at least 8 characters long", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                password.Focus();
                return false;
            }
            else if (password.Text != cpassword.Text)
            {
                MessageBox.Show("Passwords Doesn't Mach\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                password.Focus();
            }
            else
            {
                return true;
            }
        }

        private void gender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 frm=new Form3();
            this.Hide();
            frm.Show();
            
            
        }

        private void cpassword_TextChanged(object sender, EventArgs e)
        {
            cpassword.PasswordChar = '*';
        }

        private void fname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
