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
using static GreenhouseGlassDatabase.DBWrapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GreenhouseGlassDatabase
{
    public partial class Form5 : Form
    {
        private DBWrapper db_;
        private DataTable dt_;
        private ulong width_;
        private ulong height_;
        private ulong site_;

        public void setWidth(ulong value)
        {
            width_ = value;
        }

        public void setHeight(ulong value)
        {
            height_ = value;
        }

        public void setSite(ulong value)
        {
            site_ = value;
        }

        public Form5()
        {
            InitializeComponent();
            width_ = 0;
            height_ = 0;
            site_ = 0;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            width_ = 0;
            height_ = 0;
            site_ = 0;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            db_ = new DBWrapper("greenhouse_glass_database.db", "general");

            if (db_ == null && !db_.isOpen())
            {
                MessageBox.Show("Ошибка загрузки базы данных!");
                Application.Exit();
            }

            dt_ = db_.selectFromTable("size_width, size_height");
            if (dt_.Rows.Count == 0)
            {
                MessageBox.Show("Ошибка!\r\nВ базе данных нет данных, добавьте сначала данные, затем повторите действие!");
                Close(); return;
            }
            SecondaryMethods.fillComboBox(comboBox1, dt_);
            textBox2.Text = width_ != 0 && height_ != 0 ? width_.ToString() : dt_.Rows[0][0].ToString();
            textBox3.Text = width_ != 0 && height_ != 0 ? height_.ToString() : dt_.Rows[0][1].ToString();
            textBox4.Text = site_ != 0 ? site_.ToString() : String.Empty;
            button1.Enabled = false;
            ActiveControl = textBox1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox4.Text == String.Empty)
            {
                MessageBox.Show("Необходимо указать участок!\r\nПовторите ввод!");
                return;
            }
            if(textBox1.Text == String.Empty)
            {
                MessageBox.Show("Необходимо указать количество!\r\nПовторите ввод!");
                return;
            }
            if(textBox2.Text == String.Empty)
            {
                MessageBox.Show("Необходимо указать ширину!\r\nПовторите ввод!");
                return;
            }
            if(textBox3.Text == String.Empty)
            {
                MessageBox.Show("Необходимо указать высоту!\r\nПовторите ввод!");
                return;
            }
            ulong width = 0;
            ulong height = 0;
            ulong count = 0;
            ulong site = 0;

            ulong.TryParse(textBox2.Text, out width);
            ulong.TryParse(textBox3.Text, out height);
            ulong.TryParse(textBox1.Text, out count);
            ulong.TryParse(textBox4.Text, out site);

            var date = dateTimePicker1.Value;

            if(width == 0 || height == 0 || count == 0)
            {
                MessageBox.Show("Ошибка входных данных!\r\n" +
                    "Ширина и высота должны быть больше 0\r\n" +
                    ", а также количество!");
                return;
            }
            if(site < 1 && site > 9)
            {
                MessageBox.Show("Участок должен находится в диапазоне [0; 9], где 9 - АБК!");
                return;
            }
            if (date > DateTime.Now)
            {
                MessageBox.Show("Дата не должна превышать настоящее время!\r\n Повторите ввод!");
                return;
            }

            var dbdt = new List<DBData>();
            dbdt.Add(new DBData("size_width", width.ToString()));
            dbdt.Add(new DBData("size_height", height.ToString()));

            var find_data_table = db_.isIsset(dbdt);

            if (find_data_table.Rows.Count == 0)
            {
                MessageBox.Show("Данный размер отсутствует в базе данных!\r\n Повторите ввод!");
                return;
            }

            if (!ulong.TryParse(find_data_table.Rows[0][3].ToString(), out ulong _))
            {
                MessageBox.Show("Ошибка! Невозможно преобразорвать данные!\r\n Повторите ввод!");
                return;
            }
            ulong db_count = ulong.Parse(find_data_table.Rows[0][3].ToString());
            if (db_count == 0)
            {
                MessageBox.Show("Данного размера стёкла уже закончились! :(\r\nВозможно данный размер вырезали из другого размера! ;)");
                return;
            }

            if (count > db_count)
            {
                MessageBox.Show("Ошибка! В базе данных количество штук размера: "+
                    width.ToString()+"*"+height.ToString()+" = "+ db_count.ToString()+"\r\n"+
                    "Вы же ввели количество равное "+count.ToString()+"\r\n Повторите ввод!");
                return;
            }

            ulong new_count = db_count - count;

            var new_dbdt = new List<DBData>();
            new_dbdt.Add(new DBData("size_width", width.ToString()));
            new_dbdt.Add(new DBData("size_height", height.ToString()));
            new_dbdt.Add(new DBData("count", new_count.ToString()));
            new_dbdt.Add(new DBData("square", (width * height * new_count).ToString()));

            var rep_data = new List<DBData>();
            rep_data.Add(new DBWrapper.DBData("size", width.ToString() + "*" + height.ToString()));
            rep_data.Add(new DBWrapper.DBData("date", date.ToString("yyyy-MM-dd HH:mm:ss")));
            rep_data.Add(new DBWrapper.DBData("count", count.ToString()));
            rep_data.Add(new DBWrapper.DBData("site", site.ToString()));

            if (!db_.writeToTable(rep_data, "replacement_data"))
            {
                MessageBox.Show("Ошибка обновления данных!\r\n Повторите ввод!");
                return;
            }
            if (!db_.updateInTable(new_dbdt, find_data_table))
            {
                MessageBox.Show("Ошибка обновления данных!\r\n Повторите ввод!");
                return;
            }

            textBox1.Clear();
            textBox4.Clear();

            MessageBox.Show("Данные успешно добавлены!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ulong.TryParse(textBox1.Text, out ulong _)) button1.Enabled = true;
            else
            {
                textBox1.Clear();
                button1.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!ulong.TryParse(textBox2.Text, out ulong _)) textBox2.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!ulong.TryParse(textBox3.Text, out ulong _)) textBox3.Clear();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ulong site = 0;
            if (!ulong.TryParse(textBox4.Text, out site)) textBox4.Clear();
            if(site == 0 || site > 9) textBox4.Clear();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex >= 0 && comboBox1.SelectedIndex < dt_.Rows.Count)
            {
                textBox2.Text = dt_.Rows[comboBox1.SelectedIndex][0].ToString();
                textBox3.Text = dt_.Rows[comboBox1.SelectedIndex][1].ToString();
            }
        }
    }
}
