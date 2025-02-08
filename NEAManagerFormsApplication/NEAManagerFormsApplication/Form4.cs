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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NEAManagerFormsApplication
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            List<string> membershipTypes = SelectMembershipType();
            comboBox1.Items.AddRange(membershipTypes.ToArray());

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }
        private string connectionString = Settings1.Default.DBLinkNew;

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            string Surname = textBox2.Text;
            string membershipNumberText = textBox3.Text;
            string MembershipType = comboBox1.Text;
            string EndDate = textBox4.Text;
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) || string.IsNullOrWhiteSpace(membershipNumberText) || string.IsNullOrWhiteSpace(MembershipType) || string.IsNullOrWhiteSpace(EndDate))
            {
                MessageBox.Show("Please fill in all required fields.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(membershipNumberText, out int membershipNumber))
            {
                MessageBox.Show("Membership Number must be a valid number.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool success = AddMember(Name, Surname, membershipNumber, MembershipType, EndDate);
            if (success)
            {
                MessageBox.Show("New member added successfully", "Member Manager (LCS Manager)", MessageBoxButtons.OK);
                    this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to add member. Please try again.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int GetMembershipTypeID(string membershipType)
        {
            string query = "SELECT ID FROM MEMBERTYPE WHERE Type = @Type";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Type", membershipType);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        return id;
                    }
                }
            }

            return -1;
        }
        private bool AddMember(string Name, string Surname, int MembershipNumber, string MembershipType, string MembershipEndDate)
        {

            if (!DateTime.TryParse(MembershipEndDate, out DateTime parsedMembershipEnd))
            {
                MessageBox.Show("Please enter a valid date for the Membership End Date.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DateTime membershipStart = DateTime.Now;

            int memberTypeID = GetMembershipTypeID(MembershipType);
            if (memberTypeID == -1)
            {
                MessageBox.Show("Invalid Membership Type selected.", "Member Manager (LCS Manager)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string query = "INSERT INTO MEMBERCLUB (Name, Surname, MemberTypeID, MembershipStart, MembershipEnd) VALUES (@Name, @Surname, @MemberTypeID, @MembershipStart, @MembershipEnd)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Surname", Surname);
                    command.Parameters.AddWithValue("@MemberTypeID", memberTypeID);
                    command.Parameters.AddWithValue("@MembershipStart", membershipStart);
                    command.Parameters.AddWithValue("@MembershipEnd", parsedMembershipEnd);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
        }
        private List<string> SelectMembershipType()
        {
            string query = "SELECT * FROM MEMBERTYPE";
            List<string> membershipTypes = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        membershipTypes.Add(reader["Type"].ToString());
                    }

                    reader.Close();
                }
            }
            return membershipTypes;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
