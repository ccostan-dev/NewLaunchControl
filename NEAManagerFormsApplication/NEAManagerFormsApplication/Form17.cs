using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace NEAManagerFormsApplication
{
    public partial class Form17 : Form
    {
        public Form17()
        {
            InitializeComponent();

            List<KeyValuePair<int, string>> gliderTypeNames = SelectGliderSeatTypeNames();
            comboBox1.DataSource = new BindingSource(gliderTypeNames, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
        }
        private string connectionString = Settings1.Default.DBLinkNew;


        private List<KeyValuePair<int, string>> SelectGliderSeatTypeNames()
        {
            string query = "SELECT ID, GliderType, GliderSeatID FROM GLIDERTYPE";
            List<KeyValuePair<int, string>> gliderTypes = new List<KeyValuePair<int, string>>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        string type = reader["GliderType"].ToString();
                        int seatType = Convert.ToInt32(reader["GliderSeatID"]);
                        string displayName = $"{type} ({(seatType == 1 ? "Single" : "Double")})";
                        gliderTypes.Add(new KeyValuePair<int, string>(id, displayName));
                    }
                    reader.Close();
                }
            }
            return gliderTypes;
        }

        private decimal GetPriceForGliderTypePerMinByID(int gliderTypeID, string priceColumn, int seatType)
        {
            string query = $"SELECT {priceColumn} FROM GLIDERSCOSTS WHERE GliderTypeID = @GliderTypeID AND SeatType = @SeatType";
            decimal price = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GliderTypeID", gliderTypeID);
                        command.Parameters.AddWithValue("@SeatType", seatType);

                        connection.Open();
                        Console.WriteLine($"Executing SQL: {query}");
                        Console.WriteLine($"Parameters: GliderTypeID = {gliderTypeID}, SeatType = {seatType}");

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            price = Convert.ToDecimal(result);
                            Console.WriteLine($"Fetched Price: {price}");
                        }
                        else
                        {
                            Console.WriteLine("No result found for this combination.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
            return price;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is KeyValuePair<int, string> selectedItem)
            {
                int selectedGliderTypeID = selectedItem.Key; 
                string displayValue = selectedItem.Value;
                Console.WriteLine($"Selected Glider Type ID: {selectedGliderTypeID}, Value: {displayValue}");
                int seatType = displayValue.Contains("Single") ? 1 : 2;
                Console.WriteLine($"Determined Seat Type: {seatType}");

                decimal chargePerMinute = GetPriceForGliderTypePerMinByID(
                    selectedGliderTypeID,
                    seatType == 1 ? "SingleSeatCostPerMin" : "TwoSeatCostPerMin",
                    seatType
                );
                decimal chargePerHour = GetPriceForGliderTypePerMinByID(
                    selectedGliderTypeID,
                    seatType == 1 ? "SingleSeatCostPerHour" : "TwoSeatCostPerHour",
                    seatType
                );

                Console.WriteLine($"Charge per Minute: {chargePerMinute}, Charge per Hour: {chargePerHour}");

                label12.Text = chargePerMinute.ToString("C", new CultureInfo("en-GB"));
                label15.Text = chargePerHour.ToString("C", new CultureInfo("en-GB"));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
