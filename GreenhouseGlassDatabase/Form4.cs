using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenhouseGlassDatabase
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.ShowDialog();
            this.Visible = true;
        }
    }
}
