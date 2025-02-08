using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEAManagerFormsApplication
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
            "Are you sure that you want to enter Login Account Manager?",
            "LCS Manager - Login Account Manager",
            MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Form11 f11 = new Form11();
                f11.Show();
                this.Hide();
                var form11 = new Form2();
                form11.Closed += (s, args) => this.Close();
            }
            else
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
            "Are you sure that you want to enter Member Manager?",
            "LCS Manager - Member Manager",
            MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Form3 f3 = new Form3();
                f3.Show();
                this.Hide();
                var form3 = new Form2();
                form3.Closed += (s, args) => this.Close();
            }
            else
            {

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
            "Are you sure that you want to enter LCS Account Manager?",
            "LCS Manager - LCS Account Manager",
            MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Form7 f7 = new Form7();
                f7.Show();
                this.Hide();
                var form7 = new Form2();
                form7.Closed += (s, args) => this.Close();
            }
            else
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
            "Are you sure that you want to enter Fees and Charges Manager?",
            "LCS Manager - Fees and Charges Manager",
            MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Form15 f15 = new Form15();
                f15.Show();
                this.Hide();
                var form15 = new Form2();
                form15.Closed += (s, args) => this.Close();
            }
            else
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
            "Are you sure that you want to enter Aircraft Manager?",
            "LCS Manager - Aircraft Manager",
            MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Form19 f19 = new Form19();
                f19.Show();
                this.Hide();
                var form19 = new Form2();
                form19.Closed += (s, args) => this.Close();
            }
            else
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
            "Are you sure that you want to enter Log Manager?",
            "LCS Manager - Log Manager",
            MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Form23 f23 = new Form23();
                f23.Show();
                var form23 = new Form2();
                form23.Closed += (s, args) => this.Close();
            }
            else
            {

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
            "Are you sure that you want to enter More Options?",
            "LCS Manager - More Options",
            MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Form24 f24 = new Form24();
                f24.Show();
                this.Hide();
                var form24 = new Form2();
                form24.Closed += (s, args) => this.Close();
            }
            else
            {

            }
        }
    }
}
