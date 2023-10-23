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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-U0M528H\SQLEXPRESS;Initial Catalog=myproject;Integrated Security=True");
        public int rid;


       
       

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

       

     

        private void signup_Click(object sender, EventArgs e)
        {
            if (Isvalid())
            {
               

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
                MessageBox.Show("Insertion Success");
                get_registered_user_details();
                Clearall();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            fname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            lname.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            email.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            phone.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            age.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            adr.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            gender.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            username.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
            password.Text = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
            cpassword.Text= dataGridView1.SelectedRows[0].Cells[11].Value.ToString();



        }

        private void get_registered_user_details()
        {
            
            // SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-U0M528H\SQLEXPRESS;Initial Catalog=myproject;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("SELECT * FROM registration1",con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt;
          


        }

       

       

        private void button1_Click_2(object sender, EventArgs e)
        {
            Clearall();
           
        }

        private void Clearall()
        {
            fname.Clear();
            phone.Clear();
            comboBox1.Text = "Choose State";
            comboBox2.Text = "Choose City";
            gender.Text = "Select Gender";

            lname.Clear();
            email.Clear();
            password.Clear();
            cpassword.Clear();
            age.Clear();
            adr.Clear();
            comboBox2.Items.Clear();
            username.Clear();
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            get_registered_user_details();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (rid > 0)
            {
                if (Isvalid())
                {


                    SqlCommand cmd = new SqlCommand("UPDATE registration1 SET fname = @fname,lname = @lname,email = @email,phone = @phone,age = @age,address = @address,state = @state,city = @city,gender = @gender,username = @username,password = @password WHERE rid = @rid", con);

                    //  DataTable dt = new DataTable();

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@rid", this.rid);
                    cmd.Parameters.AddWithValue("@fname", fname.Text);
                    cmd.Parameters.AddWithValue("@lname", lname.Text);
                    cmd.Parameters.AddWithValue("@email", email.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text);
                    cmd.Parameters.AddWithValue("@age", Convert.ToInt32(age.Text));
                    cmd.Parameters.AddWithValue("@address", adr.Text);
                    cmd.Parameters.AddWithValue("@state", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@city", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@gender", gender.Text);
                    cmd.Parameters.AddWithValue("@username", username.Text);
                    cmd.Parameters.AddWithValue("@password", password.Text);



                    con.Open();
                    cmd.ExecuteNonQuery();

                    // SqlDataReader sdr = cmd.ExecuteReader();
                    // dt.Load(sdr);
                    con.Close();
                    MessageBox.Show("Updation Success");
                    get_registered_user_details();
                    Clearall();

                }
            }
            else
            {
                MessageBox.Show("Please select a row to update");
            }
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (rid > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM  registration1 WHERE rid = @rid", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@rid", this.rid);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted");
                get_registered_user_details();
                Clearall();

            }
            else
            {
                MessageBox.Show("Please Select a Row","warning");
            }

        }
    }
}

