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
using System.Xml.Linq;

namespace NEAManagerFormsApplication
{
    public partial class Form21 : Form
    {
        private string connectionString = Settings1.Default.DBLinkNew;
        public Form21()
        {
            InitializeComponent();
            LoadGliders();
            LoadSeatType();
            LoadGliderREG();
            LoadPropulsionType();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }
        private int? selectedGliderTypeID;
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public string name;
        private void LoadGliders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT GliderType FROM GLIDERTYPE WHERE GliderType IS NOT NULL", connection);
                SqlDataReader reader = command.ExecuteReader();

                comboBox1.Items.Clear();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["GliderType"].ToString());
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

                comboBox2.Items.Clear();

                while (reader.Read())
                {
                    if (reader["SingleSeat"] != DBNull.Value && Convert.ToInt32(reader["SingleSeat"]) > 0)
                    {
                        comboBox2.Items.Add("Single Seater");
                    }
                    if (reader["DualSeat"] != DBNull.Value && Convert.ToInt32(reader["DualSeat"]) > 0)
                    {
                        comboBox2.Items.Add("Dual Seater");
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

                comboBox3.Items.Clear();

                while (reader.Read())
                {
                    if (reader["SelfPropelled"] != DBNull.Value && Convert.ToInt32(reader["SelfPropelled"]) > 0)
                    {
                        comboBox3.Items.Add("Self Propelled");
                    }
                    if (reader["NotApplicable"] != DBNull.Value && Convert.ToInt32(reader["NotApplicable"]) > 0)
                    {
                        comboBox3.Items.Add("Not Applicable");
                    }
                }

                reader.Close();
            }
        }
        private int? GetGliderTypeID(string gliderType)
        {
            string query = "SELECT ID FROM GLIDERTYPE WHERE GliderType = @GliderType";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GliderType", gliderType);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int gliderTypeID))
                    {
                        return gliderTypeID;
                    }
                }
            }

            return null;
        }
        private int? GetGliderSeatID(int GliderSeatID)
        {
            string GliderREG = comboBox4.Text;

            string query = "SELECT SeatID FROM GLIDERS WHERE GliderREG = @GliderREG";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GliderREG", GliderREG);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int seatID))
                    {
                        GliderSeatID = seatID;
                    }
                }
            }

            return GliderSeatID;
        }
        private int? GetGliderPowerSourceID(int PowerSourceID)
        {
            string GliderREG = comboBox4.Text;

            string query = "SELECT PowerSourceID FROM GLIDERS WHERE GliderREG = @GliderREG";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GliderREG", GliderREG);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int PowerSID))
                    {
                        PowerSourceID = PowerSID;
                    }
                }
            }

            return PowerSourceID;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            name = comboBox1.SelectedItem?.ToString();
            string selectedGliderType = comboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedGliderType))
            {
                selectedGliderTypeID = GetGliderTypeID(selectedGliderType);
            }
            else
            {
                selectedGliderTypeID = null;
            }
        }
        private void LoadGliderREG()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT GT.GliderType, G.GliderREG FROM GLIDERTYPE GT INNER JOIN GLIDERS G ON GT.ID = G.GliderTypeID";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Close();
            }
        }
        private List<string> GetGliderREGbyGliderType(string gliderType)
        {
            List<string> gliderREGList = new List<string>();
            int? gliderTypeID = null;
            string getTypeIDQuery = "SELECT ID FROM GLIDERTYPE WHERE GliderType = @gliderType";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(getTypeIDQuery, connection);
                command.Parameters.AddWithValue("@gliderType", gliderType);
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    gliderTypeID = (int)result;
                }
            }
            if (gliderTypeID.HasValue)
            {
                string getRegQuery = "SELECT GliderREG FROM GLIDERS WHERE GliderTypeID = @gliderTypeID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(getRegQuery, connection);
                    command.Parameters.AddWithValue("@gliderTypeID", gliderTypeID.Value);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gliderREGList.Add(reader["GliderREG"].ToString());
                        }
                    }
                }
            }

            return gliderREGList;
        }
        private int? gliderTypeID = null;
        private string GetGliderREGByGliderType(string gliderType, string gliderREG)
        {
            int? gliderTypeID = null;

            string query = "SELECT GliderREG FROM GLIDERTYPE WHERE GliderType = @GliderType";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GliderREG", gliderREG);
                command.Parameters.AddWithValue("@GliderType", gliderType);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    gliderTypeID = (int)result;
                }
            }

            return gliderREG;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SeatType = comboBox2.Text;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PropulsionType = comboBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private bool EditGliderRegistration(string gliderType, string EditedRegistration, string OldRegistration)
        {
            string RetrieveGliderTypeID = "SELECT ID FROM GLIDERTYPE WHERE GliderType = @GliderType";
            string UpdateGlider = @"UPDATE GLIDERS SET GliderREG = @NewRegistration WHERE ID IN (SELECT TOP 1 ID FROM GLIDERS WHERE GliderTypeID = @GliderTypeID AND GliderREG = @CurrentRegistration)"; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int? gliderTypeId = null;
                        using (SqlCommand checkCommand = new SqlCommand(RetrieveGliderTypeID, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@GliderType", gliderType);
                            object existingId = checkCommand.ExecuteScalar();
                            if (existingId != null)
                            {
                                gliderTypeId = (int)existingId;
                            }
                            else
                            {
                                MessageBox.Show("Error: Glider type not found!", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        using (SqlCommand UpdateGliderCommand = new SqlCommand(UpdateGlider, connection, transaction))
                        {
                            UpdateGliderCommand.Parameters.AddWithValue("@NewRegistration", EditedRegistration);
                            UpdateGliderCommand.Parameters.AddWithValue("@GliderTypeID", gliderTypeId);
                            UpdateGliderCommand.Parameters.AddWithValue("@CurrentRegistration", OldRegistration);

                            int rowsAffected = UpdateGliderCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                MessageBox.Show("Error: No matching glider found to update.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error updating aircraft: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }
        private bool EditGliderSeats(string name, string GliderREG, int SeatID)
        {
            string RetrieveGliderTypeID = "SELECT ID FROM GLIDERTYPE WHERE GliderType = @Name";
            string UpdateGliderSeat = "UPDATE GLIDERS SET SeatID = @SeatID WHERE GliderREG = @GliderREG AND GliderTypeID = @GliderTypeID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int gliderTypeId;
                        using (SqlCommand checkCommand = new SqlCommand(RetrieveGliderTypeID, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@Name", name);
                            object existingId = checkCommand.ExecuteScalar();
                            if (existingId != null)
                            {
                                gliderTypeId = (int)existingId;
                            }
                            else
                            {
                                MessageBox.Show("Error: Glider type not found!", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }

                        using (SqlCommand updateSeatCommand = new SqlCommand(UpdateGliderSeat, connection, transaction))
                        {
                            updateSeatCommand.Parameters.AddWithValue("@SeatID", SeatID);
                            updateSeatCommand.Parameters.AddWithValue("@GliderREG", GliderREG);
                            updateSeatCommand.Parameters.AddWithValue("@GliderTypeID", gliderTypeId);

                            int rowsAffected = updateSeatCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                MessageBox.Show("Error: No matching glider found to update.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error updating aircraft: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }
        private bool EditGliderPowerSource(string name, string GliderREG, int PowerSourceID)
        {
            string RetrieveGliderTypeID = "SELECT ID FROM GLIDERTYPE WHERE GliderType = @Name";
            string UpdateGliderSeat = "UPDATE GLIDERS SET PowerSourceID = @PowerSourceID WHERE GliderREG = @GliderREG AND GliderTypeID = @GliderTypeID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int gliderTypeId;
                        using (SqlCommand checkCommand = new SqlCommand(RetrieveGliderTypeID, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@Name", name);
                            object existingId = checkCommand.ExecuteScalar();
                            if (existingId != null)
                            {
                                gliderTypeId = (int)existingId;
                            }
                            else
                            {
                                MessageBox.Show("Error: Glider type not found!", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }

                        using (SqlCommand updateSeatCommand = new SqlCommand(UpdateGliderSeat, connection, transaction))
                        {
                            updateSeatCommand.Parameters.AddWithValue("@PowerSourceID", PowerSourceID);
                            updateSeatCommand.Parameters.AddWithValue("@GliderREG", GliderREG);
                            updateSeatCommand.Parameters.AddWithValue("@GliderTypeID", gliderTypeId);

                            int rowsAffected = updateSeatCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                MessageBox.Show("Error: No matching glider found to update.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error updating aircraft: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            string selectedName = comboBox1.Text;
            List<string> GliderREG = GetGliderREGbyGliderType(selectedName);
            foreach (string REG in GliderREG)
            {
                comboBox4.Items.Add(REG);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!selectedGliderTypeID.HasValue)
            {
                MessageBox.Show("Please select a Glider Type first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int? gliderSeatID = GetGliderSeatID(selectedGliderTypeID.Value);
            if (!gliderSeatID.HasValue)
            {
                label14.Text = "Error";
                return;
            }
            if (gliderSeatID.Value == 1)
            {
                label14.Text = "Single Seater";
            }
            else if (gliderSeatID.Value == 2)
            {
                label14.Text = "Dual Seater";
            }
            else
            {
                label14.Text = "No Option Selected";
            }
            int? PowerSourceID = GetGliderPowerSourceID(selectedGliderTypeID.Value);
            if (!PowerSourceID.HasValue)
            {
                label16.Text = "Error";
                return;
            }
            if (PowerSourceID.Value == 1)
            {
                label16.Text = "Self Propelled";
            }
            else if (PowerSourceID.Value == 2)
            {
                label16.Text = "Not Applicable";
            }
            else
            {
                label16.Text = "No Option Selected";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string GliderType = comboBox1.Text;
            string EditedRegistration = textBox1.Text;
            string OldRegistration = comboBox4.Text;
            bool success = EditGliderRegistration(GliderType, EditedRegistration, OldRegistration);
            if (success)
            {
                MessageBox.Show("Aircraft REG (Registration) edited successfully.", "Success", MessageBoxButtons.OK);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to add aircraft. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int SeatID;
            if (comboBox2.Text == "Single Seater")
            {
                SeatID = 1;
            }
            else if (comboBox2.Text == "Dual Seater")
            {
                SeatID = 2;
            }
            else
            {
                MessageBox.Show("Please select a valid seat type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string GliderREG = comboBox4.Text;
            if (string.IsNullOrEmpty(GliderREG))
            {
                MessageBox.Show("The registration for the selected glider has not been chosen. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success = EditGliderSeats(name, GliderREG, SeatID);
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
        private void button5_Click(object sender, EventArgs e)
        {
            int PowerSourceID;
            if (comboBox3.Text == "Self Propelled")
            {
                PowerSourceID = 1;
            }
            else if (comboBox3.Text == "Not Applicable")
            {
                PowerSourceID = 2;
            }
            else
            {
                MessageBox.Show("Please select a valid seat type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string GliderREG = comboBox4.Text;
            if (string.IsNullOrEmpty(GliderREG))
            {
                MessageBox.Show("The registration for the selected glider has not been chosen. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success = EditGliderPowerSource(name, GliderREG, PowerSourceID);
            if (success)
            {
                MessageBox.Show("Power source changed successfully.", "Success", MessageBoxButtons.OK);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to add aircraft. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}