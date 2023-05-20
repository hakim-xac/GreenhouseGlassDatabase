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
    public partial class Form7 : Form
    {
        private Config config_;
        private ulong narrow_width_;
        private ulong middle_width_;
        private ulong wide_width_;
        private ulong height_;
        private TypeGlass type_glass_;

        public void setSite(TypeGlass value)
        {
            type_glass_ = value;
        }

        public Form7()
        {
            InitializeComponent();
            config_ = new Config();
            type_glass_ = TypeGlass.Unknown;
            narrow_width_ = config_.actualSize(Place.BetweenSiteNarrowWidth);
            middle_width_ = config_.actualSize(Place.BetweenSiteMiddleWidth);
            wide_width_ = config_.actualSize(Place.BetweenSiteWideWidth);
            height_ = config_.actualSize(Place.BetweenSiteHeight);

            button1.Text = "Широкое (" + wide_width_ + "*"+ height_ + ")";
            button2.Text = "Среднее (" + middle_width_ + "*"+ height_ + ")";
            button3.Text = "Узкое (" + narrow_width_ + "*"+ height_ + ")";
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            label1.Text = "Замена М/У\r\nУчасток: "+SecondaryMethods.TypeGlassToString(type_glass_);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(ulong.Parse(SecondaryMethods.TypeGlassToString(type_glass_)));
            new_form.setHeight(height_);
            new_form.setWidth(wide_width_);
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(ulong.Parse(SecondaryMethods.TypeGlassToString(type_glass_)));
            new_form.setHeight(height_);
            new_form.setWidth(middle_width_);
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 new_form = new Form5();
            new_form.Owner = this;
            this.Visible = false;
            new_form.setSite(ulong.Parse(SecondaryMethods.TypeGlassToString(type_glass_)));
            new_form.setHeight(height_);
            new_form.setWidth(narrow_width_);
            new_form.ShowDialog();
            this.Visible = true;
            Close();
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            type_glass_ = TypeGlass.Unknown;
        }
    }
}
