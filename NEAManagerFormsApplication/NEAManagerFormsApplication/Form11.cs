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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form12 f12 = new Form12();
            f12.Show();
            var form12 = new Form11();
            form12.Closed += (s, args) => this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
            var form2 = new Form11();
            form2.Closed += (s, args) => this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form13 f13 = new Form13();
            f13.Show();
            var form13 = new Form11();
            form13.Closed += (s, args) => this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form14 f14 = new Form14();
            f14.Show();
            var form14 = new Form11();
            form14.Closed += (s, args) => this.Close();
        }
    }
}
