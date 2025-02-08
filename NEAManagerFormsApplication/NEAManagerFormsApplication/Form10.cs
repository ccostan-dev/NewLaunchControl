﻿using System;
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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            List<KeyValuePair<string, string>> membershipTypes = SelectLCSAccount();
            comboBox1.DataSource = membershipTypes;
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";

        }
        private string connectionString = Settings1.Default.DBLinkNew;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private List<KeyValuePair<string, string>> SelectLCSAccount()
        {
            string query = "SELECT LCSUsername, LCSPassword FROM LCSLOGIN";
            List<KeyValuePair<string, string>> membershipTypes = new List<KeyValuePair<string, string>>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string username = reader["LCSUsername"].ToString();
                        string password = reader["LCSPassword"].ToString();
                        membershipTypes.Add(new KeyValuePair<string, string>(username, password));
                    }
                    reader.Close();
                }
            }
            return membershipTypes;
        }
        private bool DeleteLCSAccountByDetails(string Username, string Password)
        {
            string query = "DELETE FROM LCSLOGIN WHERE LCSUsername = @Username AND LCSPassword = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Password", Password);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Button clicked!");

            var selectedItem = (KeyValuePair<string, string>)comboBox1.SelectedItem;
            string username = selectedItem.Key;
            string password = selectedItem.Value;

            bool isDeleted = DeleteLCSAccountByDetails(username, password);

            if (isDeleted)
            {
                MessageBox.Show("Member deleted successfully.");
                comboBox1.DataSource = SelectLCSAccount();
            }
            else
            {
                MessageBox.Show("Failed to delete member. Member not found.");
            }
        }
    }
}

