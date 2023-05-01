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
        private DBWrapper db;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            db = new DBWrapper("greenhouse_glass_database.db");

            if (db == null && !db.isOpen())
            {
                MessageBox.Show("Ошибка загрузки базы данных!");
                Application.Exit();
            }

            SecondaryMethods.fillDataGrid(dataGridView1, db.selectFromTable());

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            db?.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
