using grocery_store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GreenhouseGlassDatabase
{

    public partial class Form2 : Form
    {
        private DBWrapper db_;
        private int active_header_cell_;
        private SortDirtection active_direction_sort_;



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
            active_header_cell_ = 2;
            active_direction_sort_ = SortDirtection.Ascending;

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
                    if(panel5.Visible || panel8.Visible) panel3.Height -= panel5.Height;
                    panel5.Visible = false;
                    panel8.Visible = false;
                    label3.Text = parseCount(db_.selectCount());
                    break;
            }
            SecondaryMethods.fillDataGrid(dataGridView1, db_.selectFromTable());
            SecondaryMethods.sortGrid(dataGridView1, active_header_cell_, active_direction_sort_);
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
            if (e.KeyCode == Keys.Delete 
                && MessageBox.Show("Вы точно хотите удалить?", "Удаление данных", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int col_id = dataGridView1.Columns["id"].Index;
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;
            dataGridView1.ClearSelection();
            Color tmp_background_color = dataGridView1.BackgroundColor;

            dataGridView1.BackgroundColor = Color.White;
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (s, ev) =>
            {
                Bitmap bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
                ev.Graphics.DrawImage(bmp, new Point(5, 0));
            };
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pd;
            dlg.ShowDialog();

            dataGridView1.BackgroundColor = tmp_background_color;
            dataGridView1.Rows[0].Selected = true;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0
                || dataGridView1.SelectedRows.Count == 0) return;
            DataGridView dg = new DataGridView();
            SecondaryMethods.fillDataGrid(dg);
            dg.Location = dataGridView1.Location;
            dg.Size = dataGridView1.Size;
            dg.Width = dataGridView1.Width;
            dg.Height = dataGridView1.Height;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                dg.Columns.Add(col.Clone() as DataGridViewColumn);
            }

            DataGridViewRow row = new DataGridViewRow();
            
            foreach (DataGridViewRow elem in dataGridView1.SelectedRows.Cast<DataGridViewRow>().Reverse())
            {
                row = elem.Clone() as DataGridViewRow;
                int col_index = 0;
                foreach (DataGridViewCell cell in elem.Cells)
                {
                    row.Cells[col_index].Value = cell.Value;
                    row.Cells[col_index].Selected = false;
                    ++col_index;
                }
                dg.Rows.Add(row);
            }

            dg.EnableHeadersVisualStyles = false;
            dg.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dg.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dg.DefaultCellStyle.BackColor = Color.White;
            dg.DefaultCellStyle.ForeColor = Color.Black;
            dg.DefaultCellStyle.SelectionBackColor = Color.White;
            dg.DefaultCellStyle.SelectionForeColor = Color.Black;
            dg.BackgroundColor = Color.White;
            dg.ColumnHeadersDefaultCellStyle.Font = dataGridView1.ColumnHeadersDefaultCellStyle.Font;
            dg.DefaultCellStyle.Font = dataGridView1.DefaultCellStyle.Font;

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (s, ev) =>
            {
                Bitmap bmp = new Bitmap(dg.Width, dg.Height);
                dg.DrawToBitmap(bmp, dg.Bounds);
                ev.Graphics.DrawImage(bmp, new Point(5, 0));

            };
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pd;
            dlg.ShowDialog();


        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == active_header_cell_)
            {
                active_direction_sort_ = active_direction_sort_ != SortDirtection.Ascending 
                    ? SortDirtection.Descending 
                    : SortDirtection.Ascending;
            }
            else
            {
                active_direction_sort_ = active_direction_sort_ != SortDirtection.Ascending
                    ? SortDirtection.Ascending
                    : SortDirtection.Descending;
            }
            active_header_cell_ = e.ColumnIndex;
        }
    }
}
