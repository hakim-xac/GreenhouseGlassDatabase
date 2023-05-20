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
            ulong acc_count = 0;
            ulong tmp_count = 0;
            ulong acc_square = 0;
            ulong tmp_square = 0;
            foreach (DataRow elem in dt.Rows)
            {
                if(ulong.TryParse(elem[0].ToString(), out tmp_count)) acc_count += tmp_count;
                if(ulong.TryParse(elem[1].ToString(), out tmp_square)) acc_square += tmp_square;
            }
            return new KeyValuePair<string, string>(acc_count.ToString(), acc_square.ToString());
        }

        private string parseCount(DataTable dt)
        {
            ulong acc_count = 0;
            ulong tmp_count = 0;
            foreach (DataRow elem in dt.Rows)
            {
                if (ulong.TryParse(elem[0].ToString(), out tmp_count)) acc_count += tmp_count;
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


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ulong acc_count = 0;
            ulong acc_square = 0;
            int column_count_i = dataGridView1.Columns["count"].Index;
            int column_square_i = dataGridView1.Columns.Contains("square") 
                ? dataGridView1.Columns["square"].Index
                : 0;

            foreach (DataGridViewRow elems in dataGridView1.SelectedRows)
            {
                ulong tmp = 0;
                if (ulong.TryParse(dataGridView1[column_count_i, elems.Index].Value.ToString(), out tmp)) acc_count += tmp;
                if (column_square_i != 0 && ulong.TryParse(dataGridView1[column_square_i, elems.Index].Value.ToString(), out tmp)) acc_square += tmp;
            }

            label8.Text = acc_count.ToString();
            label6.Text = acc_square.ToString();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            int col_id = dataGridView1.Columns["id"].Index;
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow elems in dataGridView1.SelectedRows)
                {
                    ulong id = 0;
                    string string_id = dataGridView1[col_id, elems.Index].Value.ToString();
                    if (ulong.TryParse(string_id, out id))
                    {
                        db_.deleteFromTable(string_id);
                    }
                }
                Form2_Load(sender, e);
            }
        }
    }
}
