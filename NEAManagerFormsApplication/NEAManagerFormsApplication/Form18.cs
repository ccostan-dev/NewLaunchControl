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
    public partial class Form18 : Form
    {
        public Form18()
        {
            InitializeComponent();
        }
        private string connectionString = Settings1.Default.DBLinkNew;

        private bool EditWinchCost(string Charge)
        {
            string query = "UPDATE LAUNCHTYPE SET LaunchCharge = @Charge WHERE Type = @Winch";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Duration", float.TryParse(Charge, out var parsedCharge) ? parsedCharge : 0.0f);

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
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string Charge = textBox1.Text;
            if (EditWinchCost(Charge))
            {
                {

                    MessageBox.Show("Launch charges changed successfully", "Fees and charges Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Failed to change launch charges. Please try again.", "Fees and charges Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
    }
}
