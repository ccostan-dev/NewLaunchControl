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
    public partial class Form1 : Form
    {
        public bool isLoggedIn = false;
        public Form1()
        {
            InitializeComponent();
        }
        private string connectionString = Settings1.Default.DBLinkNew;

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
            var form2 = new Form1();
            form2.Closed += (s, args) => this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool ValidateLogin(string username, string password)
        {
            string query = "SELECT COUNT(1) FROM LCSLOGIN WHERE LCSUsername = @Username AND LCSPassword = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    int result = (int)command.ExecuteScalar();

                    connection.Close();

                    return result == 1;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (ValidateLogin(username, password))
            {
                if (this.Owner is Form1 mainForm)
                {
                    mainForm.isLoggedIn = true;
                }

                MessageBox.Show("Login successful", "LCS Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
                var form2 = new Form1();
                form2.Closed += (s, args) => this.Close();
            }
            else
            {
                MessageBox.Show("Invalid login credentials. Please try again.", "LCS Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
