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
        private int width_;
        private int height_;
        private int site_;

        public void setWidth(int value)
        {
            width_ = value;
        }

        public void setHeight(int value)
        {
            height_ = value;
        }

        public void setSite(int value)
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
            db_?.Close();
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
            int width = 0;
            int height = 0;
            int count = 0;
            int site = 0;

            int.TryParse(textBox2.Text, out width);
            int.TryParse(textBox3.Text, out height);
            int.TryParse(textBox1.Text, out count);
            int.TryParse(textBox4.Text, out site);

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

            DBData[] dbdt = { new DBData("size_width", width.ToString())
            , new DBData("size_height", height.ToString()) };

            var find_data_table = db_.isIsset(dbdt);

            if (find_data_table.Rows.Count == 0)
            {
                MessageBox.Show("Данный размер отсутствует в базе данных!\r\n Повторите ввод!");
                return;
            }

            if (!int.TryParse(find_data_table.Rows[0][3].ToString(), out int _))
            {
                MessageBox.Show("Ошибка! Невозможно преобразорвать данные!\r\n Повторите ввод!");
                return;
            }
            int db_count = int.Parse(find_data_table.Rows[0][3].ToString());
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

            int new_count = db_count - count;

            DBData[] new_dbdt = { new DBData("size_width", width.ToString())
            , new DBData("size_height", height.ToString())
            , new DBData("count", new_count.ToString())
            , new DBData("square", (width*height*new_count).ToString()) };

            DBWrapper.DBData[] rep_data = { new DBWrapper.DBData("size", width.ToString()+"*"+height.ToString())
                , new DBWrapper.DBData("date", date.ToString("yyyy-MM-dd HH:mm:ss"))
                , new DBWrapper.DBData("count", count.ToString())
                , new DBWrapper.DBData("site", site.ToString()) };

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
            if (int.TryParse(textBox1.Text, out int _)) button1.Enabled = true;
            else
            {
                textBox1.Clear();
                button1.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out int _)) textBox2.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out int _)) textBox3.Clear();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int site = 0;
            if (!int.TryParse(textBox4.Text, out site)) textBox4.Clear();
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
