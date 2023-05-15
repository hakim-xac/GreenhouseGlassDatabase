using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;

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
        private SQLiteConnection db_ = null;
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

        public DataTable selectFromTable(DBData[] dbdt, string field = "")
        {
            string elems = "";
            foreach (var elem in dbdt) elems += elem.V1 + "=" + elem.V2 + " and ";
            if (elems.Length < 4) return new DataTable();

            return field == String.Empty
                ? selectQuery("select * from "+ table_name_+" where "+elems.Substring(0, elems.Length-4))
                : selectQuery("select " + field + " from "+ table_name_ + " where " + elems.Substring(0, elems.Length - 4));
        }



        public bool writeToTable(List<DBData> dbdt, string table_name = "")
        {
            if (dbdt.Count == 0) return false;

            string query = "insert into "+ (table_name == String.Empty ? table_name_ : table_name);
            string names = "";
            string values = "";

            foreach (var elem in dbdt)
            {
                names += "'"+elem.V1+"',";
                values += "'"+elem.V2+"',";
            }
            query += "(" + names.Substring(0, names.Length - 1) + ") values("+values.Substring(0, values.Length - 1) + ")";
            try {
                return executeNonQuery(query);
            }
            catch
            {
                return false;
            }
        }


        public DataTable isIsset(List<DBData> dbdt)
        {
            if (dbdt.Count < 2) return new DataTable();
            string query = "select * from "+table_name_+" where ";
            string elems = "";

            var find_arr = dbdt.GetRange(0, 2);

            foreach (var elem in find_arr) elems += elem.V1 + "="+elem.V2+" and ";

            query += elems.Substring(0, elems.Length - 5) + " limit 1";

            return selectQuery(query);
        }

        public bool updateInTable(List<DBData> dbdt, DataTable dt)
        {
            if (dbdt.Count < 2) return false;
            string query = "update "+table_name_ + " set ";
            string id = dt.Rows[0][0].ToString();
            string fields = "";
            foreach (var elem in dbdt) fields += "'"+elem.V1 + "'='" + elem.V2 + "',";
            query += fields.Substring(0, fields.Length-1) + " where id="+id;
            
            return executeNonQuery(query);
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
            if (!is_open_) return dt;
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
