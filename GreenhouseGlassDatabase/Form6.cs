using grocery_store;
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
    public partial class Form6 : Form
    {
        private TypeGlass site_;

        public void setSite(TypeGlass value)
        {
            site_ = value;
        }


        public Form6()
        {
            InitializeComponent();
            site_ = TypeGlass.Unknown;
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            site_ = TypeGlass.Unknown;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            header.Text = "Замена на участке: "+SecondaryMethods.TypeGlassToString(site_);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(int.Parse(SecondaryMethods.TypeGlassToString(site_)));
            new_form.setHeight(1555);
            new_form.setWidth(735);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(int.Parse(SecondaryMethods.TypeGlassToString(site_)));
            new_form.setHeight(1580);
            new_form.setWidth(735);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(int.Parse(SecondaryMethods.TypeGlassToString(site_)));
            new_form.setHeight(1860);
            new_form.setWidth(735);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(int.Parse(SecondaryMethods.TypeGlassToString(site_)));
            new_form.setHeight(1880);
            new_form.setWidth(735);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            int site = int.Parse(SecondaryMethods.TypeGlassToString(site_));
            new_form.setSite(site);
            new_form.setHeight(site < 5 ? 1555 : 2238);
            new_form.setWidth(785);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            int site = int.Parse(SecondaryMethods.TypeGlassToString(site_));
            new_form.setSite(site);
            new_form.setHeight(site < 5 ? 1580 : 2258);
            new_form.setWidth(785);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            int site = int.Parse(SecondaryMethods.TypeGlassToString(site_));
            new_form.setSite(site);
            new_form.setHeight(site < 5 ? 1860 : 1495);
            new_form.setWidth(785);
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            int site = int.Parse(SecondaryMethods.TypeGlassToString(site_));
            new_form.setSite(site);
            new_form.setHeight(site < 5 ? 1880 : 1521);
            new_form.setWidth(785);
            new_form.ShowDialog();
            this.Visible = true;
        }
    }
}
