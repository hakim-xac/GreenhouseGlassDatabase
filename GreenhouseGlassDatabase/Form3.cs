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
    public partial class Form3 : Form
    {
        private DBWrapper db_;
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
            db_ = new DBWrapper("greenhouse_glass_database.db");

            if (db_ == null && !db_.isOpen())
            {
                MessageBox.Show("Ошибка загрузки базы данных!");
                Application.Exit();
            }

            //SecondaryMethods.fillDataGrid(dataGridView1, db.selectFromTable());
            header_panel.Visible = db_.isEmpty();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            db_?.Close();
        }
    }
}
