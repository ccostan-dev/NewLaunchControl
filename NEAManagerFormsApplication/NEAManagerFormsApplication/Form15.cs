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
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form16 f16 = new Form16();
            f16.Show();
            var form16 = new Form15();
            form16.Closed += (s, args) => this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form17 f17 = new Form17();
            f17.Show();
            var form17 = new Form15();
            form17.Closed += (s, args) => this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
            var form2 = new Form15();
            form2.Closed += (s, args) => this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form18 f18 = new Form18();
            f18.Show();
            var form18 = new Form15();
            form18.Closed += (s, args) => this.Close();
        }
    }
}
