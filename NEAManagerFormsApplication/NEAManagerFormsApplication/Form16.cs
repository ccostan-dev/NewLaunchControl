using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEAManagerFormsApplication
{
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
            LoadMembershipTypes();
        }
        private string connectionString = Settings1.Default.DBLinkNew;

        private void LoadMembershipTypes()
        {
            List<string> membershipTypes = SelectMembershipTypes();

            comboBox1.DataSource = null;
            comboBox1.DataSource = membershipTypes;
            comboBox1.Refresh();

            comboBox2.DataSource = null;
            comboBox2.DataSource = membershipTypes;
            comboBox2.Refresh();
        }

        private List<string> SelectMembershipTypes()
        {
            string query = "SELECT Type FROM MEMBERTYPE";
            List<string> membershipTypes = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string type = reader["Type"].ToString();
                        membershipTypes.Add(type);
                    }
                    reader.Close();
                }
            }
            return membershipTypes;
        }

        private bool AddMembershipType(string type, string charge, string duration)
        {
            string query = "INSERT INTO MEMBERTYPE (Type, Charge, ChargeDuration) VALUES (@Type, @Charge, @Duration)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Type", type);
                    command.Parameters.AddWithValue("@Charge", float.TryParse(charge, out var parsedCharge) ? parsedCharge : 0.0f);
                    command.Parameters.AddWithValue("@Duration", int.TryParse(duration, out var parsedDuration) ? parsedDuration : 0);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }

        private bool EditMembershipPrice(string charge)
        {
            string query = "UPDATE MEMBERTYPE SET Charge = @Charge WHERE Type = @Type";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Charge", float.TryParse(charge, out var parsedCharge) ? parsedCharge : 0.0f);

                    string selectedType = comboBox2.SelectedItem?.ToString();
                    if (string.IsNullOrEmpty(selectedType))
                    {
                        MessageBox.Show("Please select a membership type to update.");
                        return false;
                    }
                    command.Parameters.AddWithValue("@Type", selectedType);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }
        private bool EditMembershipDuration(string Duration)
        {
            string query = "UPDATE MEMBERTYPE SET ChargeDuration = @Duration WHERE Type = @Type";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Duration", int.TryParse(Duration, out var parsedCharge) ? parsedCharge : 0.0f);

                    string selectedType = comboBox2.SelectedItem?.ToString();
                    if (string.IsNullOrEmpty(selectedType))
                    {
                        MessageBox.Show("Please select a membership type to update.");
                        return false;
                    }
                    command.Parameters.AddWithValue("@Type", selectedType);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }

        private bool DeleteMemberTypeByDetails(string type)
        {
            string query = "DELETE FROM MEMBERTYPE WHERE Type = @Type";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Type", type);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string selectedType = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedType))
            {
                MessageBox.Show("Please select a membership type to delete.");
                return;
            }

            bool isDeleted = DeleteMemberTypeByDetails(selectedType);

            if (isDeleted)
            {
                MessageBox.Show("Membership type deleted successfully.");
                LoadMembershipTypes();
            }
            else
            {
                MessageBox.Show("Failed to delete membership type. Membership type not found.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string type = textBox1.Text;
            string charge = textBox2.Text;
            string duration = textBox3.Text;
            if (AddMembershipType(type, charge, duration))
            {
                MessageBox.Show("Membership type added successfully.", "Fees and Charges Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMembershipTypes();
            }
            else
            {
                MessageBox.Show("Failed to add membership type. Please try again.", "Fees and Charges Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string charge = textBox4.Text;
            if (EditMembershipPrice(charge))
            {
                MessageBox.Show("Membership charge changed successfully.", "Fees and Charges Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to change membership charge. Please try again.", "Fees and Charges Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string Duration = textBox5.Text;
            if (EditMembershipDuration(Duration))
            {
                MessageBox.Show("Membership charge changed successfully.", "Fees and Charges Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to change membership charge. Please try again.", "Fees and Charges Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form16_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Form24 f24 = new Form24();
            //f24.Show();
            //var form24 = new Form16();
            //form24.Closed += (s, args) => this.Close();
        }
    }
}