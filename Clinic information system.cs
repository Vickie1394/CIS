using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CIS
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=DESKTOP-A03Q2T3\SQLEXPRESS;Initial Catalog=CIS;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
            textPassword.UseSystemPasswordChar = true; // Use system password character
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textUsername.Text) || string.IsNullOrWhiteSpace(textPassword.Text))
            {
                MessageBox.Show("Please provide username and password.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM Admin WHERE Username=@username AND Password=@password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", textUsername.Text);
                    cmd.Parameters.AddWithValue("@password", textPassword.Text);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    con.Close();

                    if (count == 1)
                    {
                        MessageBox.Show("Login successful!");
                        tabControl1.Visible = true;
                        tabControl1.SelectTab(1);
                        tabControl1.Enabled = true;
                        panel1.Visible = false;


                        // Clear login details
                        textUsername.Text = "";
                        textPassword.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Login failed!");
                        tabControl1.Visible = false;
                        tabControl1.SelectTab(0);
                       tabControl1.Enabled = false;
                       

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Insertbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textName.Text) || string.IsNullOrWhiteSpace(textPhoneNo.Text) ||
                string.IsNullOrWhiteSpace(textTreatment.Text) || comboBoxGender.SelectedItem == null ||
                string.IsNullOrWhiteSpace(textEmail.Text) || comboBoxBlood.SelectedItem == null)
            {
                MessageBox.Show("Please provide all details.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Patient (PatientId, Name, PhoneNo, TreatmentDetails, Gender, Email, BloodGroup) " +
                        "VALUES (@patientId, @name, @phoneNo, @treatmentDetails, @gender, @email, @bloodGroup)";

                    Guid patientId = Guid.NewGuid();

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@patientId", patientId);
                    cmd.Parameters.AddWithValue("@name", textName.Text);
                    cmd.Parameters.AddWithValue("@phoneNo", textPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@treatmentDetails", textTreatment.Text);
                    cmd.Parameters.AddWithValue("@gender", comboBoxGender.SelectedItem);
                    cmd.Parameters.AddWithValue("@email", textEmail.Text);
                    cmd.Parameters.AddWithValue("@bloodGroup", comboBoxBlood.SelectedItem);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted. Patient ID: " + patientId);
                        DisplayPatientData();
                        ClearPatientData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert data.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected rows?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            string query = "DELETE FROM Patient WHERE PatientId=@patientId";

                            SqlCommand cmd = new SqlCommand(query, con);
                            con.Open();

                            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                            {
                                Guid patientId = (Guid)row.Cells["PatientId"].Value;
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@patientId", patientId);
                                cmd.ExecuteNonQuery();
                            }

                            con.Close();
                            MessageBox.Show("Selected rows deleted.");
                            DisplayPatientData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select at least one row to delete.");
            }
        }

        private void buttonInsert_Click_1(object sender, EventArgs e)

        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-A03Q2T3\SQLEXPRESS;Initial Catalog=CIS;Integrated Security=True;");
            try
            {
                con.Open();

                SqlCommand cmd;

                if (radioButtonNurse.Checked == true)
                {
                    cmd = new SqlCommand("INSERT INTO Nurse (NurseId, Name, Email, PhoneNo, Address, Speciality) VALUES (@nurseId, @name, @email, @phoneNo, @address, @speciality)", con);
                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO Doctor (DoctorId, Name, Email, PhoneNo, Speciality) VALUES (@doctorId, @name, @email, @phoneNo, @speciality)", con);
                }

                Guid id = Guid.NewGuid();
                cmd.Parameters.AddWithValue("@nurseId", id); // Replace with appropriate column name
                cmd.Parameters.AddWithValue("@doctorId", id); // Replace with appropriate column name
                cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                cmd.Parameters.AddWithValue("@phoneNo", textBoxPhoneNo.Text);
                cmd.Parameters.AddWithValue("@address", textBoxAddress.Text);
                cmd.Parameters.AddWithValue("@speciality", textBoxSpeciality.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data inserted successfully. Nurse/Doctor Id: " + id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        private void DisplayPatientData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Patient";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapt.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void ClearPatientData()
        {
            textName.Text = "";
            textPhoneNo.Text = "";
            textTreatment.Text = "";
            comboBoxGender.Text = "";
            textEmail.Text = "";
            comboBoxBlood.Text = "";
        }



        private void ClearNurseDoctorData()
        {
            textBoxName.Text = "";
            textBoxPhoneNo.Text = "";
            textBoxEmail.Text = "";
            textBoxSpeciality.Text = "";
        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {
            textPassword.UseSystemPasswordChar = true; // Use system password character
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            Appointment appointmentForm = new Appointment();
            appointmentForm.Show();
        }

       
    }
    }

