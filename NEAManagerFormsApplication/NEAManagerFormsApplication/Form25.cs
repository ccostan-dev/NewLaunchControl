using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NEAManagerFormsApplication
{
    public partial class Form25 : Form
    {
        public Form25()
        {
            InitializeComponent();
            string Originalfilename = Settings1.Default.DBLinkOriginal;
            string CurrentOrNewfilename = Settings1.Default.DBLinkNew;
            label9.Text = Originalfilename;
            label2.Text = CurrentOrNewfilename;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string originalfilename = Settings1.Default.DBLinkOriginal;
            if (input != originalfilename) 
            {
                Settings1.Default.DBLinkNew = input;
                Settings1.Default.Save();
                MessageBox.Show("Database link updated: " + input);
                label2.Text = input;
            }
            else
            {
                MessageBox.Show("Error - same database link entered.");
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
