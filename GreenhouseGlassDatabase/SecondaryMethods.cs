using GreenhouseGlassDatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

        public static void fillDataGrid(DataGridView dg, DataTable dt, bool is_modified = false)
        {
            dg.DataSource = dt;
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

    }
}
