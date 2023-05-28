using grocery_store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GreenhouseGlassDatabase.DBWrapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace GreenhouseGlassDatabase
{
    public partial class Form3 : Form
    {
        private DBWrapper db_;
        private DataTable dt_;
        private bool is_empty_database_;
        private bool is_added_to_database_;

        public bool isAddedToDataBase()
        {
            return is_added_to_database_;
        }

        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            db_ = new DBWrapper("greenhouse_glass_database.db", "general");

            if (db_ == null && !db_.isOpen())
            {
                MessageBox.Show("Ошибка загрузки базы данных!");
                Application.Exit();
            }

            header_panel.Visible = true;
            if (db_.isEmpty())
            {
                is_empty_database_ = true;
                return;
            }

            button1.Enabled = false;
            header_panel.Visible = false;
            dt_ = db_.selectFromTable("size_width, size_height");
            SecondaryMethods.fillComboBox(comboBox2, dt_);
            is_added_to_database_ = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int _)) textBox1.Clear();
            if (textBox1.Text.Length > 0) button1.Enabled = true;
            else button1.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out int _)) textBox2.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out int _)) textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ulong count = 0;
            if(!ulong.TryParse(textBox1.Text, out count))
            {
                MessageBox.Show("Ошибка преобразованеия данных!\r\nПовторите ввод!");
                return;
            }
            ulong width =0;
            ulong height =0;
            if (!is_empty_database_)
            {
                 width = ulong.Parse(dt_.Rows[comboBox2.SelectedIndex][0].ToString());
                 height = ulong.Parse(dt_.Rows[comboBox2.SelectedIndex][1].ToString());
            }

            ulong tmp_width =0;
            ulong tmp_height =0;
            if (ulong.TryParse(textBox2.Text, out tmp_width) && ulong.TryParse(textBox3.Text, out tmp_height))
            {
                width = tmp_width;
                height = tmp_height;
            }

            if(width == 0 || height == 0 || count == 0 || count > 1000)
            {
                MessageBox.Show("Ошибка добавления данных в базу!\r\n" +
                    "Ширина и высота должны быть больше 0\r\n" +
                    ", а также количество не должно выходить из диапазона [0; 1000]!");
                return;
            }

            var find_arr = new List<DBWrapper.DBData>();
            find_arr.Add(new DBWrapper.DBData("size_width", width.ToString()));
            find_arr.Add(new DBWrapper.DBData("size_height", height.ToString()));
            find_arr.Add(new DBWrapper.DBData("count", count.ToString()));
            find_arr.Add(new DBWrapper.DBData("square", (height * width * count).ToString()));

            var find_data_table = db_.isIsset(find_arr);
            
            if (find_data_table.Rows.Count > 0)
            {
                ulong new_count = ulong.Parse(find_data_table.Rows[0][3].ToString()) + ulong.Parse(find_arr[2].V2);
                var new_arr = new List<DBWrapper.DBData>();
                new_arr.Add(new DBWrapper.DBData("size_width", width.ToString()));
                new_arr.Add(new DBWrapper.DBData("size_height", height.ToString()));
                new_arr.Add(new DBWrapper.DBData("count", new_count.ToString()));
                new_arr.Add(new DBWrapper.DBData("square", (height * width * new_count).ToString()));
                
                if (!db_.updateInTable(new_arr, find_data_table))
                {
                    MessageBox.Show("Ошибка добавления данных в базу!");
                    return;
                }
                header_panel.Visible = false;
                MessageBox.Show("Данные успешно изменены!");
            }
            else
            {
                if (!db_.writeToTable(find_arr))
                {
                    MessageBox.Show("Ошибка добавления данных в базу!");
                    return;
                }

                header_panel.Visible = false;
                comboBox2.Items.Add(width.ToString() + "*" + height.ToString());
                comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
                is_added_to_database_ = true;
                MessageBox.Show("Данные успешно добавлены!");
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            is_empty_database_ = true;
        }
    }
}
