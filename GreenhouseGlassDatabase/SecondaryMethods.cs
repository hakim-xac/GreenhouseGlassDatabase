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

        public static bool sortGrid(DataGridView dg, int index, SortDirtection direction)
        {
            if (index <= dg.ColumnCount || index < 0) return false;
            dg.Sort(dg.Columns[index], direction == SortDirtection.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
            return true;
        }

    }
}
