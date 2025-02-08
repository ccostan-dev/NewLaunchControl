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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            Form8 f8 = new Form8();
            f8.Show();
            var form8 = new Form7();
            form8.Closed += (s, args) => this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form9 f9 = new Form9();
            f9.Show();
            var form9 = new Form7();
            form9.Closed += (s, args) => this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form10 f10 = new Form10();
            f10.Show();
            var form10 = new Form7();
            form10.Closed += (s, args) => this.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
            var form2 = new Form7();
            form2.Closed += (s, args) => this.Close();
        }
    }
}
