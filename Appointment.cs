
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIS
{
    public partial class Appointment : Form
    {
        private string patientName;
        private DateTime dateAppointment;
        private string reason;

        public Appointment()
        {
            InitializeComponent();
        }

        public Appointment(string patientName, DateTime dateAppointment, string reason)
        {
            this.patientName = patientName;
            this.dateAppointment = dateAppointment;
            this.reason = reason;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string patientName = textPatientName.Text;
            DateTime dateAppointment = dateTimePicker1.Value;
            string reason = textReason.Text;

            string connectionString = @"Data Source=DESKTOP-A03Q2T3\SQLEXPRESS;Initial Catalog=CIS;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Get the maximum existing Id value
                string getMaxIdQuery = "SELECT MAX(Id) FROM Appointment";
                using (SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, connection))
                {
                    object result = getMaxIdCommand.ExecuteScalar();
                    int nextId;
                    if (result == DBNull.Value)
                    {
                        nextId = 1;
                    }
                    else
                    {
                        nextId = Convert.ToInt32(result) + 1;
                        string query = "INSERT INTO Appointment (Id, PatientName, Reason, DateAppointment) VALUES (@id, @patientName, @reason, @dateAppointment)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", nextId); // Use the nextId variable here
                            command.Parameters.AddWithValue("@patientName", patientName);
                            command.Parameters.AddWithValue("@reason", reason);
                            command.Parameters.AddWithValue("@dateAppointment", dateAppointment);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Appointment details saved successfully.");
            }
            // Clear the appointment data
            ClearAppointmentData();
            textPatientName.Text = "";
            textReason.Text = "";
            dateTimePicker1.Value = DateTime.Now; // Reset the date picker to the current date and time
        }

        private void ClearAppointmentData()
        {
            throw new NotImplementedException();
        }
    }
}
