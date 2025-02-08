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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent(); 
            List<string> membershipTypes = SelectMembers();
            comboBox1.DataSource = membershipTypes;
        }
        private string connectionString = Settings1.Default.DBLinkNew;

        private void button1_Click(object sender, EventArgs e)
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
        private bool DeleteMemberByDetails(string name, string surname, string memberTypeID)
        {
            string query = "DELETE FROM MEMBERCLUB WHERE Name = @Name AND Surname = @Surname AND MemberTypeID = @MemberTypeID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Surname", surname);
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

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();

            string[] parts = selectedItem.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3)
            {
                MessageBox.Show("Invalid member format selected.");
                return;
            }

            string name = parts[0];
            string surname = parts[1];
            string memberTypeID = parts[2].Trim('(', ')');

            bool isDeleted = DeleteMemberByDetails(name, surname, memberTypeID);
            if (isDeleted)
            {
                MessageBox.Show("Member deleted successfully.");
                comboBox1.DataSource = SelectMembers();
            }
            else
            {
                MessageBox.Show("Failed to delete member. Member not found.");
            }
        }
    }
}
