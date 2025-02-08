using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEAManagerFormsApplication
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            List<string> membershipTypes = SelectMembers();
            comboBox1.DataSource = membershipTypes;
        }
        private string connectionString = Settings1.Default.DBLinkNew;
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private List<string> SelectMembers()
        {
            string query = "SELECT Name, Surname, MemberTypeID FROM MEMBERCLUB";
            List<string> membershipTypes = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        string surname = reader["Surname"].ToString();
                        string memberTypeID = reader["MemberTypeID"].ToString();

                        string displayString = $"{name} {surname} ({memberTypeID})";
                        membershipTypes.Add(displayString);
                    }
                    reader.Close();
                }
            }
            return membershipTypes;
        }
        private bool EditMemberName(string newName, string memberTypeID)
        {
            string query = "UPDATE MEMBERCLUB SET Name = @NewName WHERE MemberTypeID = @MemberTypeID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewName", newName);
                    command.Parameters.AddWithValue("@MemberTypeID", memberTypeID);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }
        private bool EditMemberSurname(string newSurname, string memberTypeID)
        {
            string query = "UPDATE MEMBERCLUB SET Surname = @NewSurname WHERE MemberTypeID = @MemberTypeID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewSurname", newSurname);
                    command.Parameters.AddWithValue("@MemberTypeID", memberTypeID);

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newName = textBox1.Text;
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("No information, or an invalid form of information has been entered. Please try again.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string selectedItem = comboBox1.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string memberTypeID = parts[parts.Length - 1].Trim('(', ')');
            bool success = EditMemberName(newName, memberTypeID);
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

        private void button2_Click(object sender, EventArgs e)
        {
            string newSurname = textBox2.Text;
            if (string.IsNullOrWhiteSpace(newSurname))
            {
                MessageBox.Show("No information, or an invalid form of information has been entered. Please try again.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string selectedItem = comboBox1.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string memberTypeID = parts[parts.Length - 1].Trim('(', ')');
            bool success = EditMemberSurname(newSurname, memberTypeID);
            if (success)
            {
                MessageBox.Show("Member surname edited successfully", "Member Manager (LCS Manager)", MessageBoxButtons.OK);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to edit member surname. Please try again.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
