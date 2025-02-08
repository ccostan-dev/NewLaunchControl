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
    public partial class Form22 : Form
    {
        public Form22()
        {
            InitializeComponent();
            LoadGliders();
            LoadGliderREG();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }
        private string connectionString = Settings1.Default.DBLinkNew;
        private void LoadGliders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT GliderType FROM GLIDERTYPE", connection);
                SqlDataReader reader = command.ExecuteReader();

                comboBox1.Items.Clear();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["GliderType"].ToString());
                }
                reader.Close();
            }
        }
        private bool DeleteAircraftByDetails(string name, string registration)
        {
            string removeGliderQuery = "DELETE FROM GLIDERS WHERE GliderREG = @Registration";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(removeGliderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Registration", registration);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string selectedName = comboBox1.Text;
            List<string> GliderREG = GetGliderREGbyGliderType(selectedName);
            foreach (string REG in GliderREG)
            {
                comboBox2.Items.Add(REG);
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                string selectedName = comboBox1.SelectedItem.ToString();
                string selectedReg = comboBox2.SelectedItem.ToString();
                try
                {
                    bool success = DeleteAircraftByDetails(selectedName, selectedReg);
                    if (success)
                    {
                        MessageBox.Show("The selected glider and its related records have been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the glider.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a glider to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form22_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
