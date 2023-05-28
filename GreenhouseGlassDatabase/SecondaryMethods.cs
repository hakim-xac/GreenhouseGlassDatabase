using GreenhouseGlassDatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grocery_store
{
    internal static class SecondaryMethods
    {

        public static String TypeGlassToString(TypeGlass tg)
        {
            switch (tg)
            {
                case TypeGlass.One:                         return      "1";
                case TypeGlass.Second:                      return      "2";
                case TypeGlass.Third:                       return      "3";
                case TypeGlass.Fourth:                      return      "4";
                case TypeGlass.Fifth:                       return      "5";
                case TypeGlass.Sixth:                       return      "6";
                case TypeGlass.Seventh:                     return      "7";
                case TypeGlass.Eighth:                      return      "8";
                case TypeGlass.Unknown:                     return      "-";
                default:                                    return      "-";
            }
        }

        public static void fillDataGrid(DataGridView dg, DataTable dt = null, bool is_modified = false)
        {
            dg.DataSource = dt;
            dg.BorderStyle = BorderStyle.None;
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg.Dock = DockStyle.Fill;
            dg.AllowUserToAddRows = false;
            dg.AllowUserToDeleteRows = false;
            dg.AllowUserToResizeColumns = false;
            dg.MultiSelect = true;
            dg.ReadOnly = !is_modified;
            dg.Visible = true;
            dg.AutoGenerateColumns = true;
            dg.RowHeadersVisible = false;
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public static void fillComboBox(System.Windows.Forms.ComboBox cb, DataTable dt)
        {
            cb.Items.Clear();
            foreach ( DataRow dr in dt.Rows) {
                cb.Items.Add(dr[0] + "*" + dr[1]);
            }
            if (cb.Items.Count > 0 ) cb.SelectedIndex = 0;

        }
        public static void fillComboBox(System.Windows.Forms.ComboBox cb, Hashtable ht)
        {
            cb.Items.Clear();
            foreach (DictionaryEntry elem in ht) cb.Items.Add(elem.Key);

            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }

        public static bool fillHashTableFromBD(Hashtable ht, DataTable dt)
        {
            foreach (DataRow row in dt.Rows) ht.Add(row[0].ToString(), row[1].ToString());
            return ht.Count == dt.Rows.Count;
        }

        public static string noSQLInjection(string str)
        {
            return str.Replace("\"", "").Replace("\'", "").Replace(";", "").Replace("%", "");
        }

        public static bool sortGrid(DataGridView dg, int index, SortDirtection direction)
        {
            if (index <= dg.ColumnCount || index < 0) return false;
            dg.Sort(dg.Columns[index], direction == SortDirtection.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
            return true;
        }

        public static void printGrid(DataGridView dg, DateTime dt, string header="")
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (s, e) =>
            {
                int step_height = 50;
                int active_step_height = 0;
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Center;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                /*     foreach (DataGridViewRow elem in dg.Rows)
                     {
                         foreach (DataGridViewCell cell in elem.Cells)
                         {
                             e.Graphics.FillRectangle(new SolidBrush(Color.LightGray)
                             , new Rectangle(10, 10, 100, active_step_height));
                             e.Graphics.DrawRectangle(Pens.Black
                                 , new Rectangle(10, 10, 100, active_step_height));

                             e.Graphics.DrawString(cell.Value as string,
                                 cell.InheritedStyle.Font,
                                 new SolidBrush(cell.InheritedStyle.ForeColor),
                                 new RectangleF(10f, 10f, 100f, 50f), strFormat);

                             *//*row.Cells[col_index].Value = cell.Value;
                             row.Cells[col_index].Selected = false;
                             ++col_index;*//*
                         }

                         active_step_height += step_height;
                     }*/
/*
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray)
                        , new Rectangle(10, 10, 100, active_step_height));
                e.Graphics.DrawRectangle(Pens.Black
                    , new Rectangle(10, 10, 100, active_step_height));*/

                e.Graphics.DrawString("fghfghfghfg",
                    new Font("Arial", 16),
                    new SolidBrush(Color.Black),
                    new RectangleF(10f, 10f, 100f, 50f), strFormat);

                /*
                                Bitmap bmp = new Bitmap(dg.Width, dg.Height);
                                dg.DrawToBitmap(bmp, dg.Bounds);
                                e.Graphics.DrawImage(bmp, new Point(5, 0));*/
            };
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pd;
            dlg.ShowDialog();
        } 

    }
}
