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
        private Config config_;

        public void setSite(TypeGlass value)
        {
            site_ = value;
            config_ = new Config();
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
            new_form.setSite(ulong.Parse(SecondaryMethods.TypeGlassToString(site_)));
            new_form.setHeight(config_.actualSize(Place.HeightFirstRowButtAreaInSide));
            new_form.setWidth(config_.actualSize(Place.ButtWidth));
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(ulong.Parse(SecondaryMethods.TypeGlassToString(site_)));
            new_form.setHeight(config_.actualSize(Place.HeightFirstRowButtAreaOutSide));
            new_form.setWidth(config_.actualSize(Place.ButtWidth));
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(ulong.Parse(SecondaryMethods.TypeGlassToString(site_)));
            new_form.setHeight(config_.actualSize(Place.HeightSecondRowButtAreaInSide));
            new_form.setWidth(config_.actualSize(Place.ButtWidth));
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(ulong.Parse(SecondaryMethods.TypeGlassToString(site_)));
            new_form.setHeight(config_.actualSize(Place.HeightSecondRowButtAreaOutSide));
            new_form.setWidth(config_.actualSize(Place.ButtWidth));
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            ulong site = ulong.Parse(SecondaryMethods.TypeGlassToString(site_));
            new_form.setSite(site);
            new_form.setHeight(site < 5 
                ? config_.actualSize(Place.HeightFirstRowFirstAreaInSide) 
                : config_.actualSize(Place.HeightFirstRowSecondAreaInSide));
            new_form.setWidth(config_.actualSize(Place.SideWidth));
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            ulong site = ulong.Parse(SecondaryMethods.TypeGlassToString(site_));
            new_form.setSite(site);
            new_form.setHeight(site_ < TypeGlass.Fifth
                ? config_.actualSize(Place.HeightFirstRowFirstAreaOutSide)
                : config_.actualSize(Place.HeightFirstRowSecondAreaOutSide));
            new_form.setWidth(config_.actualSize(Place.SideWidth));
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            ulong site = ulong.Parse(SecondaryMethods.TypeGlassToString(site_));
            new_form.setSite(site);
            new_form.setHeight(site_ < TypeGlass.Fifth
                ? config_.actualSize(Place.HeightSecondRowFirstAreaInSide)
                : config_.actualSize(Place.HeightSecondRowSecondAreaInSide));
            new_form.setWidth(config_.actualSize(Place.SideWidth));
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            ulong site = ulong.Parse(SecondaryMethods.TypeGlassToString(site_));
            new_form.setSite(site);
            new_form.setHeight(site_ < TypeGlass.Fifth
                ? config_.actualSize(Place.HeightSecondRowFirstAreaOutSide)
                : config_.actualSize(Place.HeightSecondRowSecondAreaOutSide));
            new_form.setWidth(config_.actualSize(Place.SideWidth));
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }
    }
}
