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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }
        private string connectionString = Settings1.Default.DBLinkNew;

        private bool InsertUsernameAndPassword(string username, string password)
        {
            string query = "INSERT INTO LOGIN (Username, Password) VALUES (@Username, @Password) ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string password1 = textBox3.Text;

            if (password != password1)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Login Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (InsertUsernameAndPassword(username, password))
            {
                {

                    MessageBox.Show("Account Added Successfully", "Login Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Failed to add account. Please try again.", "Login Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
