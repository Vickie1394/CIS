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

namespace CIS
{
    public partial class Form1 : Form
    {
        int PatientId = 0;
        public Form1()
        {
            InitializeComponent();
        }

        //Connection String
        string cs = @"Data Source=DESKTOP-A03Q2T3\SQLEXPRESS;
Initial Catalog=CIS;Integrated Security=True;";
        //btn_Login Click event
        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (textUsername.Text == "" || textPassword.Text == "")
            {
                MessageBox.Show("Please provide UserName and Password");
                return;
            }
            try
            {
                //Create SqlConnection
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("Select * from Admin where Username=@username and Password=@password", con);
                cmd.Parameters.AddWithValue("@username", textUsername.Text);
                cmd.Parameters.AddWithValue("@password", textPassword.Text);
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();
                int count = ds.Tables[0].Rows.Count;
                //If count is equal to 1, than show mainform
                if (count == 1)
                {
                    MessageBox.Show("Login Successful!");
                    this.Hide();
                    tabControl1.SelectTab(1);
                    tabControl1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                    tabControl1.SelectTab(0);
                    tabControl1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Insertbtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP - A03Q2T3\SQLEXPRESS;
            Initial Catalog = CIS; Integrated Security = True; ");
            SqlCommand cmd;
            SqlDataAdapter adapt;

            if (textName.Text != "" && textPhoneNo.Text != "" && textTreatment.Text != "" && comboBoxGender.SelectedItem != "" && textEmail.Text != "" && comboBoxBlood.SelectedItem != "")
            {
                cmd = new SqlCommand("insert into Patient (Name,PhoneNo,TreatmentDetails,Gender,Email, BloodGroup) values(@name,@phoneNo, @treatmentDetails,@sex,@Email, @bloodGroup)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", textName.Text);
                cmd.Parameters.AddWithValue("@PhoneNo", textPhoneNo.Text);
                cmd.Parameters.AddWithValue("treatment", textTreatment.Text);
                cmd.Parameters.AddWithValue("@gender", comboBoxGender.SelectedItem);
                cmd.Parameters.AddWithValue("@email", textEmail.Text);
                cmd.Parameters.AddWithValue("@blood", comboBoxBlood.SelectedItem);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void DisplayData()
        {
            throw new NotImplementedException();
        }

        private void ClearData()
        {
            throw new NotImplementedException();
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP - A03Q2T3\SQLEXPRESS;
            Initial Catalog = CIS; Integrated Security = True; ");
            SqlCommand cmd;
            SqlDataAdapter adapt;
            {
                if (textName.Text != "" && textPhoneNo.Text != "" && textTreatment.Text != "" && comboBoxGender.SelectedItem != "" && textEmail.Text != "" && comboBoxBlood.SelectedItem != "")
                {
                    cmd = new SqlCommand("update Patient set Name=@name,PhoneNo=@phoneNo,Treatment= @treatment,Gender=@gender, Email=@email,Bloodgroup=@bloodGroup where PatiendId=@patientId", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@patientId", PatientId);
                    cmd.Parameters.AddWithValue("@name", textName.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo", textPhoneNo.Text);
                    cmd.Parameters.AddWithValue("treatment", textTreatment.Text);
                    cmd.Parameters.AddWithValue("@gender", comboBoxGender.SelectedItem);
                    cmd.Parameters.AddWithValue("@email", textEmail.Text);
                    cmd.Parameters.AddWithValue("@blood", comboBoxBlood.SelectedItem);
                    cmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated Successfully");
                    con.Close();
                    DisplayData();
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Please Select Record to Update");
                }
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP - A03Q2T3\SQLEXPRESS;
            Initial Catalog = CIS; Integrated Security = True; ");
            SqlCommand cmd;
            SqlDataAdapter adapt;
            if (PatientId != 0)
            {
                cmd = new SqlCommand("delete tbl_Record where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@patientId", PatientId);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}