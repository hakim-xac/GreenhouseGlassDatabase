﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace GreenhouseGlassDatabase
{
    internal class DBWrapper
    {
        public class DBData {
            public string name="";
            public string value="";

            public DBData(string v1, string v2)
            {
                V1 = v1;
                V2 = v2;
            }

            public string V1 { get; }
            public string V2 { get; }
        }


        public string table_name_;
        private SQLiteConnection db_;
        private bool is_open_ = false;



        public DBWrapper(string data_base_file_name, string table_name)
        {
            is_open_ = false;
            table_name_ = table_name;
            db_ = new SQLiteConnection("Data Source="+data_base_file_name);
            try
            {
                if (db_ != null)
                {
                    db_.Open();
                    is_open_ = true;
                }
            }
            catch {}
        }

        public bool isOpen()
        {
            return is_open_;
        }
        public bool isEmpty(string field_name = "id")
        {
            return selectQuery("select "+ field_name + " from " + table_name_ + " limit 1").Rows.Count == 0;
        }

        public DataTable selectFromTable(string field = "")
        {
            return field == String.Empty
                ? selectQuery("select * from "+ table_name_)
                : selectQuery("select " + field + " from "+ table_name_);
        }



        public bool writeToTable(DBData[] dbdt)
        {
            if (dbdt.Length == 0) return false;

            string query = "insert into "+ table_name_;
            string names = "";
            string values = "";

            foreach (var elem in dbdt)
            {
                names += "'"+elem.V1+"',";
                values += "'"+elem.V2+"',";
            }
            query += "(" + names.Substring(0, names.Length - 1) + ") values("+values.Substring(0, values.Length - 1) + ")";
            return executeNonQuery(query);
        }


        public DataTable isIsset(DBData[] dbdt)
        {
            if (dbdt.Length < 2) return new DataTable();
            string query = "select * from "+table_name_ +" where ";
            string elems = "";

            DBWrapper.DBData[] find_arr = new DBWrapper.DBData[2];
            Array.Copy(dbdt, 0, find_arr, 0, 2);

            foreach (var elem in find_arr) elems += elem.V1 + "="+elem.V2+" and ";

            query += elems.Substring(0, elems.Length - 5) + " limit 1";

            return selectQuery(query);
        }



        public bool updateInTable(DBData[] dbdt, DataTable dt)
        {
            if (dbdt.Length < 2) return false;
            string query = "update "+table_name_ + " set ";
            string id = dt.Rows[0][0].ToString();
            int a = int.Parse(dt.Rows[0][3].ToString());
            int b = int.Parse(dbdt[2].V2);
            int count = a + b;
            int width = int.Parse(dbdt[0].V2.ToString());
            int height = int.Parse(dbdt[1].V2.ToString());
            dbdt[2] = new DBData(dbdt[2].V1, count.ToString());
            dbdt[3] = new DBData(dbdt[3].V1, (width * height * count).ToString());
                        
            string fields = "";
            foreach (var elem in dbdt) fields += "'"+elem.V1 + "'='" + elem.V2 + "',";
            query += fields.Substring(0, fields.Length-1) + " where id="+id;
            
            return executeNonQuery(query);
        }










        public DataTable selectProductTypes(string field = "")
        {
            return field == String.Empty
                ? selectQuery("select * from product_types")
                : selectQuery("select " + field + " from product_types");
        }

        public DataTable selectIdFromProductTypes(string field_departament_name)
        {
            return selectQuery("select id from product_types where \"Наименование отдела\"=\"" + field_departament_name + "\"");
        }

        public DataTable selectFieldProducts(string field, string value)
        {
            return selectQuery("select * from products where \"" + field + "\"=\"" + value + "\"");
        }

        public DataTable selectFieldProductTypes(string field, string value)
        {
            return selectQuery("select * from product_types where \"" + field + "\"=\"" + value + "\"");
        }

        public DataTable selectProducts(int index)
        {
            return selectQuery("select * from products where id_product_type = " + index);
        }

        public DataTable selectProductsContains(int index, string field_name, string string_find)
        {
            return selectQuery("select * from products where id_product_type=" + index + " and " +
                "\"" + field_name + "\" like \"%" + string_find + "%\"");
        }

        public DataTable selectProductsEquals(int index, string field_name, string string_find)
        {
            return selectQuery("select * from products where id_product_type=" + index + " and " +
                "\"" + field_name + "\"=\"" + string_find + "\"");
        }

        public bool WriteToProductTypes(string name, int row, int col)
        {
            string query = "insert into " +
                "product_types('Наименование отдела', 'Номер ряда хранения', 'Номер секции хранения')" +
                " values('" + name + "', '" + row + "', '" + col + "')";
            return executeNonQuery(query);
        }

        public bool updateProductTypes(string name, int row, int col, int id)
        {
            string query = "update product_types set \"Наименование отдела\" = \"" + name
                + "\", \"Номер ряда хранения\"=" + row
                + " , \"Номер секции хранения\"=" + col
                + " where id =\"" + id + "\"";

            return executeNonQuery(query);
        }

        public bool updateProduct(string name, string provider, string unit
            , float buy_price, float selling_price
            , int coming_product, int remainder_product, int departament_id, int id_product
            , bool updated)
        {
            if (updated) if (isExistsProduct(name, departament_id)) return false;

            string query = "update products set \"Название продукта\" = \"" + name
                + "\", \"Поставщик\"=\"" + provider + "\""
                + " , \"Единица измерения\"=\"" + unit
                + "\", \"Цена покупки\"=\"" + buy_price
                + "\", \"Цена продажи\"=\"" + selling_price
                + "\", \"Поступление товара\"=\"" + coming_product
                + "\", \"Остаток товара\"=\"" + remainder_product
                + "\", \"id_product_type\"=\"" + departament_id + "\""
                + " where id =\"" + id_product + "\"";

            return executeNonQuery(query);
        }

        public bool WriteToProducts(string name, string provider, string unit
            , float buy_price, float selling_price
            , int coming_product, int remainder_product, int departament_id)
        {
            string query = "insert into products(" +
                "'Название продукта', 'Поставщик', 'Единица измерения', " +
                "'Цена покупки', 'Цена продажи', " +
                "'Поступление товара', 'Остаток товара', 'id_product_type'" +
                ")" +
                " values('" + name + "', '" + provider + "', '" + unit + "'" +
                ", '" + buy_price + "', '" + selling_price + "'" +
                ", '" + coming_product + "', '" + remainder_product + "', '" + departament_id + "')";
            return executeNonQuery(query);
        }

        public bool isExistsProductType(string name, int row, int col)
        {
            string query = "select id from product_types where \"Наименование отдела\" = \"" + name
                + "\" or \"Номер ряда хранения\"=" + row
                + " and \"Номер секции хранения\"=" + col
                + " limit 1";
            return selectQuery(query).Rows.Count > 0;
        }
        public bool isExistsProduct(string name, int departament_id)
        {
            string query = "select id from products where \"Название продукта\" = \"" + name + "\""
                + " and \"id_product_type\"=\"" + departament_id + "\"";
            return selectQuery(query).Rows.Count > 0;
        }
        public bool isExistsProduct(int id_product)
        {
            string query = "select id from products where \"id\"=" + id_product;
            return selectQuery(query).Rows.Count > 0;
        }

        public bool deleteProductType(int id)
        {
            executeNonQuery("delete from products where id_product_type=" + id);
            return executeNonQuery("delete from product_types where id = " + id);
        }
        public bool deleteProduct(int id)
        {
            return executeNonQuery("delete from products where id=" + id);
        }

        private bool executeNonQuery(string query)
        {
            SQLiteCommand cmd = db_.CreateCommand();
            cmd.CommandText = query;
            return cmd.ExecuteNonQuery() > 0;
        }

        private DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();
            try
            {
                SQLiteCommand cmd = db_.CreateCommand();
                cmd.CommandText = query;
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt);
            }
            catch { }
            return dt;
        }

        ~DBWrapper()
        {
            Close();
        }

        public void Close()
        {
            db_?.Close();
        }
    }
}
