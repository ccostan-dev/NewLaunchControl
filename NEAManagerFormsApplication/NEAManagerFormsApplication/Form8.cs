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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NEAManagerFormsApplication
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        private string connectionString = Settings1.Default.DBLinkNew;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private bool InsertUsernameAndPassword(string username, string password)
        {
            string query = "INSERT INTO LCSLOGIN (LCSUsername, LCSPassword) VALUES (@LCSUsername, @LCSPassword) ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LCSUsername", username);
                    command.Parameters.AddWithValue("@LCSPassword", password);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string password1 = textBox3.Text;

            if (password != password1)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "LCS Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (InsertUsernameAndPassword(username, password))
            {
                {

                    MessageBox.Show("Account Added Successfully", "LCS Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Failed to add account. Please try again.", "LCS Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }
