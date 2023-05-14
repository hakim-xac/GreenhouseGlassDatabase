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

        private void Form2_Load(object sender, EventArgs e)
        {
            switch (table_type_)
            {
                case TableType.General:
                    db_ = new DBWrapper("greenhouse_glass_database.db", "general");
                    break;
                case TableType.ReplacementData:
                    db_ = new DBWrapper("greenhouse_glass_database.db", "replacement_data");
                    break;
            }
            if (db_ == null && !db_.isOpen())
            {
                MessageBox.Show("Ошибка загрузки базы данных!");
                Application.Exit();
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
