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

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.BetweenSitesOneSecond);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.BetweenSitesThirdFourth);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.BetweenSitesFifthSixth);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.BetweenSitesSeventhEighth);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.One);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.Second);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.Third);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.Fourth);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.Fifth);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.Sixth);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.Seventh);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form6 new_form = new Form6();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(TypeGlass.Eighth);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
        }
    }
}
