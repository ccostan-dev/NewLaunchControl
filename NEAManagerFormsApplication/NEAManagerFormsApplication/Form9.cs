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

namespace NEAManagerFormsApplication
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            List<string> membershipTypes = SelectLCSAccounts();
            comboBox1.DataSource = null;
            comboBox1.DataSource = membershipTypes;
            comboBox1.Refresh();
        }
        private string connectionString = Settings1.Default.DBLinkNew;
        private List<string> SelectLCSAccounts()
        {
            string query = "SELECT LCSUsername FROM LCSLOGIN";
            List<string> membershipTypes = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string username = reader["LCSUsername"].ToString();

                        string displayString = $"{username}";
                        membershipTypes.Add(displayString);
                    }
                    reader.Close();
                }
            }
            return membershipTypes;
        }
        private bool EditLCSUsername(string newUsername, string existingUsername)
        {
            string query = "UPDATE LCSLOGIN SET LCSUsername = @newUsername WHERE LCSUsername = @existingUsername";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newUsername", newUsername);
                    command.Parameters.AddWithValue("@existingUsername", existingUsername);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        private bool EditLCSPassword(string newPassword, string username)
        {
            string query = "UPDATE LCSLOGIN SET LCSPassword = @newPassword WHERE LCSUsername = @username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newPassword", newPassword);
                    command.Parameters.AddWithValue("@username", username);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem?.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string newUsername = textBox1.Text;
            if (string.IsNullOrWhiteSpace(newUsername))
            {
                MessageBox.Show("No information, or an invalid form of information has been entered. Please try again.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedItem = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedItem))
            {
                MessageBox.Show("Please select a valid item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string existingUsername = selectedItem;

            bool success = EditLCSUsername(newUsername, existingUsername);

            if (success)
            {
                MessageBox.Show("Member name edited successfully", "Member Manager (LCS Manager)", MessageBoxButtons.OK);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to edit member name. Please try again.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newPassword = textBox2.Text;
            string confirmPassword = textBox3.Text;
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please enter and confirm your new password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string selectedItem = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedItem))
            {
                MessageBox.Show("Please select a valid item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] parts = selectedItem.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                MessageBox.Show("Selected item format is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string existingPassword = parts[parts.Length - 1].Trim('(', ')');
            bool success = EditLCSPassword(newPassword, existingPassword);

            if (success)
            {
                MessageBox.Show("Password updated successfully", "Member Manager (LCS Manager)", MessageBoxButtons.OK);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to update password. Please try again.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
