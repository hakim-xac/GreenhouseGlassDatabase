﻿using grocery_store;
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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void callIsEmptyDatabase()
        {
            DBWrapper db = new DBWrapper("greenhouse_glass_database.db", "general");
            var cnt = db.selectFromTable("id").Rows.Count;
            if (cnt == 0)
            {
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Config config = new Config();
            callIsEmptyDatabase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 new_form = new Form4();
            new_form.Owner = this;
            this.Visible = false;
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 new_form = new Form3();
            new_form.Owner = this;
            this.Visible = false;
            new_form.ShowDialog();
            this.Visible = true;
            if (new_form.isAddedToDataBase())
            {
                button1.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 new_form = new Form2();
            new_form.table_type_ = TableType.General;
            new_form.Owner = this;
            this.Visible = false;
            new_form.ShowDialog();
            this.Visible = true;
            callIsEmptyDatabase();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 new_form = new Form2();
            new_form.table_type_ = TableType.ReplacementData;
            new_form.Owner = this;
            this.Visible = false;
            new_form.ShowDialog();
            this.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creator by Andrey Khakimov\r\n" +
                "Greenhouse Glass Database: version 1.1");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form8 new_form = new Form8();
            new_form.Owner = this;
            this.Visible = false;
            new_form.ShowDialog();
            this.Visible = true;
        }
    }
}
