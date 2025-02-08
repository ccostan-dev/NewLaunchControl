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
    public partial class Form20 : Form
    {
        public Form20()
        {
            InitializeComponent();
            LoadGliders();
            LoadSeatType();
            LoadPropulsionType();
        }
        public string name;
        private string connectionString = Settings1.Default.DBLinkNew;
        private void LoadGliders()
        {
            connectionString = Settings1.Default.DBLinkNew;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT GliderType FROM GLIDERTYPE WHERE GliderType IS NOT NULL", connection);
                SqlDataReader reader = command.ExecuteReader();
                comboBox3.Items.Clear();
                while (reader.Read())
                {
                    comboBox3.Items.Add(reader["GliderType"].ToString());
                }

                reader.Close();
            }
        }
        private void LoadSeatType()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DISTINCT SingleSeat, DualSeat FROM GLIDERSEAT";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                comboBox1.Items.Clear();

                while (reader.Read())
                {
                    if (reader["SingleSeat"] != DBNull.Value && Convert.ToInt32(reader["SingleSeat"]) > 0)
                    {
                        comboBox1.Items.Add("Single Seater");
                    }
                    if (reader["DualSeat"] != DBNull.Value && Convert.ToInt32(reader["DualSeat"]) > 0)
                    {
                        comboBox1.Items.Add("Dual Seater");
                    }
                }

                reader.Close();
            }
        }
        private void LoadPropulsionType()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DISTINCT SelfPropelled, NotApplicable FROM GLIDERPOWERSOURCE";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                comboBox2.Items.Clear();

                while (reader.Read())
                {
                    if (reader["SelfPropelled"] != DBNull.Value && Convert.ToInt32(reader["SelfPropelled"]) > 0)
                    {
                        comboBox2.Items.Add("Self Propelled");
                    }
                    if (reader["NotApplicable"] != DBNull.Value && Convert.ToInt32(reader["NotApplicable"]) > 0)
                    {
                        comboBox2.Items.Add("Not Applicable");
                    }
                }

                reader.Close();
            }
        }
        private bool AddAircraft(string name, string registration, int gliderSeatId, int powerSourceId)
        {
            string getGliderTypeIdQuery = "SELECT ID FROM GLIDERTYPE WHERE GliderType = @Name";
            string insertGliderTypeQuery = "INSERT INTO GLIDERTYPE (GliderType, GliderSeatID, PowerSourceID) OUTPUT INSERTED.ID VALUES (@Name, @GliderSeatID, @PowerSourceID)";
            string insertGliderQuery = "INSERT INTO GLIDERS (GliderREG, GliderTypeID, SeatID) VALUES (@Registration, @GliderTypeID, @SeatID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int gliderTypeId;
                        using (SqlCommand checkCommand = new SqlCommand(getGliderTypeIdQuery, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@Name", name);
                            object existingId = checkCommand.ExecuteScalar();

                            if (existingId != null)
                            {
                                gliderTypeId = (int)existingId;
                            }
                            else
                            {
                                using (SqlCommand insertCommand = new SqlCommand(insertGliderTypeQuery, connection, transaction))
                                {
                                    insertCommand.Parameters.AddWithValue("@Name", name);
                                    insertCommand.Parameters.AddWithValue("@GliderSeatID", gliderSeatId);
                                    insertCommand.Parameters.AddWithValue("@PowerSourceID", powerSourceId);
                                    gliderTypeId = (int)insertCommand.ExecuteScalar();
                                }
                            }
                        }

                        using (SqlCommand insertGliderCommand = new SqlCommand(insertGliderQuery, connection, transaction))
                        {
                            insertGliderCommand.Parameters.AddWithValue("@Registration", registration);
                            insertGliderCommand.Parameters.AddWithValue("@GliderTypeID", gliderTypeId);
                            insertGliderCommand.Parameters.AddWithValue("@SeatID", gliderSeatId);

                            insertGliderCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error adding aircraft: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string registration = textBox1.Text;
            int gliderSeatId;
            int powerSourceId;
            if (comboBox1.Text == "Single Seater")
            {
                gliderSeatId = 1;
            }
            else if (comboBox1.Text == "Dual Seater")
            {
                gliderSeatId = 2;
            }
            else
            {
                MessageBox.Show("Please select a valid seat type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox2.Text == "Self Propelled")
            {
                powerSourceId = 1;
            }
            else if (comboBox2.Text == "Not Applicable")
            {
                powerSourceId = 2;
            }
            else
            {
                MessageBox.Show("Please select a valid propulsion type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(registration))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool success = AddAircraft(name, registration, gliderSeatId, powerSourceId);
            if (success)
            {
                MessageBox.Show("New aircraft added successfully.", "Success", MessageBoxButtons.OK);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to add aircraft. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = comboBox3.Text;
            MessageBox.Show("Selected existing glider type.");
            label12.Text = comboBox3.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            name = textBox4.Text;
            MessageBox.Show("New glider type added.");
            label12.Text = textBox4.Text;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}