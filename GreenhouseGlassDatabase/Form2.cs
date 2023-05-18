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
    public partial class Form2 : Form
    {
        private DBWrapper db_;


        public Form2()
        {
            InitializeComponent();
        }

        private void assertBD()
        {
            if (db_ == null && !db_.isOpen())
            {
                MessageBox.Show("Ошибка загрузки базы данных!");
                Application.Exit();
            }
        }
        private KeyValuePair<string, string> parseCountAndSquare(DataTable dt)
        {
            int acc_count = 0;
            int tmp_count = 0;
            int acc_square = 0;
            int tmp_square = 0;
            foreach (DataRow elem in dt.Rows)
            {
                if(int.TryParse(elem[0].ToString(), out tmp_count)) acc_count += tmp_count;
                if(int.TryParse(elem[1].ToString(), out tmp_square)) acc_square += tmp_square;
            }
            return new KeyValuePair<string, string>(acc_count.ToString(), acc_square.ToString());
        }

        private string parseCount(DataTable dt)
        {
            int acc_count = 0;
            int tmp_count = 0;
            foreach (DataRow elem in dt.Rows)
            {
                if (int.TryParse(elem[0].ToString(), out tmp_count)) acc_count += tmp_count;
            }
            return acc_count.ToString();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt;
            switch (table_type_)
            {
                case TableType.General:
                    db_ = new DBWrapper("greenhouse_glass_database.db", "general");
                    assertBD();
                    var count_and_square = parseCountAndSquare(db_.selectCountAndSquare());
                    label3.Text = count_and_square.Key;
                    label4.Text = count_and_square.Value;
                    break;
                case TableType.ReplacementData:
                    db_ = new DBWrapper("greenhouse_glass_database.db", "replacement_data");
                    assertBD();
                    dt = db_.selectCount();
                    panel5.Visible = false;
                    panel8.Visible = false;
                    panel3.Height -= panel5.Height;
                    label3.Text = parseCount(db_.selectCount());
                    break;
            }

            SecondaryMethods.fillDataGrid(dataGridView1, db_.selectFromTable());
            header_panel.Visible = db_.isEmpty();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            db_?.Close();
        }

    }
}
