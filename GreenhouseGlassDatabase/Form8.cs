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
    public partial class Form8 : Form
    {
        private Config config_;

        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            config_ = new Config();
            init(config_.actualSize());
            timer1.Enabled = true;
        }

        private bool isCompare(Place place, string value)
        {
            int tmp_value = 0;
            int actual_value = 0;
            if (!int.TryParse(value, out tmp_value)) return false;
            if (!config_.actualSize().TryGetValue(place, out actual_value)) return false;
            return tmp_value == actual_value;
        }

        private bool isNotCompare(Place place, string value) {
            return !isCompare(place, value);
        }

        private void init(Dictionary<Place, int> data)
        {
            textBox15.Text = data[Place.ButtWidth].ToString();
            textBox16.Text = data[Place.SideWidth].ToString();

            textBox24.Text = data[Place.HeightFirstRowButtAreaInSide].ToString();
            textBox23.Text = data[Place.HeightFirstRowButtAreaOutSide].ToString();
            textBox22.Text = data[Place.HeightSecondRowButtAreaInSide].ToString();
            textBox21.Text = data[Place.HeightSecondRowButtAreaOutSide].ToString();

            textBox12.Text = data[Place.HeightFirstRowFirstAreaInSide].ToString();
            textBox5.Text = data[Place.HeightFirstRowFirstAreaOutSide].ToString();
            textBox4.Text = data[Place.HeightSecondRowFirstAreaInSide].ToString();
            textBox2.Text = data[Place.HeightSecondRowFirstAreaOutSide].ToString();

            textBox9.Text = data[Place.HeightFirstRowSecondAreaInSide].ToString();
            textBox8.Text = data[Place.HeightFirstRowSecondAreaOutSide].ToString();
            textBox7.Text = data[Place.HeightSecondRowSecondAreaInSide].ToString();
            textBox6.Text = data[Place.HeightSecondRowSecondAreaOutSide].ToString();

            textBox3.Text = data[Place.BetweenSiteWideWidth].ToString();
            textBox11.Text = data[Place.BetweenSiteMiddleWidth].ToString();
            textBox1.Text = data[Place.BetweenSiteNarrowWidth].ToString();
            textBox10.Text = data[Place.BetweenSiteHeight].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (config_.reset())
            {
                init(config_.actualSize());
                MessageBox.Show("Все размеры сброшены на значения по умолчанию!");
                return;
            }
            MessageBox.Show("Ошибка! Невозможно сбросить данные!");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(config_.setActualSize(Place.ButtWidth,                           textBox15.Text)
            && config_.setActualSize(Place.SideWidth,                           textBox16.Text)
            && config_.setActualSize(Place.HeightFirstRowButtAreaInSide,        textBox24.Text)
            && config_.setActualSize(Place.HeightFirstRowButtAreaOutSide,       textBox23.Text)
            && config_.setActualSize(Place.HeightSecondRowButtAreaInSide,       textBox22.Text)
            && config_.setActualSize(Place.HeightSecondRowButtAreaOutSide,      textBox21.Text)
            && config_.setActualSize(Place.HeightFirstRowFirstAreaInSide,       textBox12.Text)
            && config_.setActualSize(Place.HeightFirstRowFirstAreaOutSide,      textBox5.Text)
            && config_.setActualSize(Place.HeightSecondRowFirstAreaInSide,      textBox4.Text)
            && config_.setActualSize(Place.HeightSecondRowFirstAreaOutSide,     textBox2.Text)
            && config_.setActualSize(Place.HeightFirstRowSecondAreaInSide,      textBox9.Text)
            && config_.setActualSize(Place.HeightFirstRowSecondAreaOutSide,     textBox8.Text)
            && config_.setActualSize(Place.HeightSecondRowSecondAreaInSide,     textBox7.Text)
            && config_.setActualSize(Place.HeightSecondRowSecondAreaOutSide,    textBox6.Text)
            && config_.setActualSize(Place.BetweenSiteWideWidth,                textBox3.Text)
            && config_.setActualSize(Place.BetweenSiteMiddleWidth,              textBox11.Text)
            && config_.setActualSize(Place.BetweenSiteNarrowWidth,              textBox1.Text)
            && config_.setActualSize(Place.BetweenSiteHeight,                   textBox10.Text)
            && config_.save())
            {
                MessageBox.Show("Данные успешно обновлены!");
                Close();
                return;
            }
            MessageBox.Show("Ошибка обновления данных!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Enabled = isNotCompare(Place.HeightFirstRowButtAreaInSide,  textBox24.Text)
                || isNotCompare(Place.HeightFirstRowButtAreaOutSide,            textBox23.Text)
                || isNotCompare(Place.HeightSecondRowButtAreaInSide,            textBox22.Text)
                || isNotCompare(Place.HeightSecondRowButtAreaOutSide,           textBox21.Text)
                || isNotCompare(Place.HeightFirstRowFirstAreaInSide,            textBox12.Text)
                || isNotCompare(Place.HeightFirstRowFirstAreaOutSide,           textBox5.Text)
                || isNotCompare(Place.HeightSecondRowFirstAreaInSide,           textBox4.Text)
                || isNotCompare(Place.HeightSecondRowFirstAreaOutSide,          textBox2.Text)
                || isNotCompare(Place.HeightFirstRowSecondAreaInSide,           textBox9.Text)
                || isNotCompare(Place.HeightFirstRowSecondAreaOutSide,          textBox8.Text)
                || isNotCompare(Place.HeightSecondRowSecondAreaInSide,          textBox7.Text)
                || isNotCompare(Place.HeightSecondRowSecondAreaOutSide,         textBox6.Text)
                || isNotCompare(Place.BetweenSiteWideWidth,                     textBox3.Text)
                || isNotCompare(Place.BetweenSiteMiddleWidth,                   textBox11.Text)
                || isNotCompare(Place.BetweenSiteNarrowWidth,                   textBox1.Text)
                || isNotCompare(Place.BetweenSiteHeight,                        textBox10.Text)
                ;
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
