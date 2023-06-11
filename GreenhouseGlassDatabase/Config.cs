using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static GreenhouseGlassDatabase.DBWrapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GreenhouseGlassDatabase
{
    internal class Config
    {
        private Dictionary<Place, ulong> actual_base_;
        private static Dictionary<Place, ulong> database_;
        private DBWrapper db_;
        

        public Config()
        {
            database_ = new Dictionary<Place, ulong>() {
                { Place.ButtWidth                           , 735 }
                , { Place.SideWidth                         , 785 }
                , { Place.HeightFirstRowFirstAreaInSide     , 1555 }
                , { Place.HeightFirstRowFirstAreaOutSide    , 1580 }
                , { Place.HeightSecondRowFirstAreaInSide    , 1860 }
                , { Place.HeightSecondRowFirstAreaOutSide   , 1880 }
                , { Place.HeightFirstRowSecondAreaInSide    , 2238 }
                , { Place.HeightFirstRowSecondAreaOutSide   , 2258 }
                , { Place.HeightSecondRowSecondAreaInSide   , 1495 }
                , { Place.HeightSecondRowSecondAreaOutSide  , 1521 }
                , { Place.HeightFirstRowButtAreaInSide      , 1555 }
                , { Place.HeightFirstRowButtAreaOutSide     , 1580 }
                , { Place.HeightSecondRowButtAreaInSide     , 1860 }
                , { Place.HeightSecondRowButtAreaOutSide    , 1880 }
                , { Place.BetweenSiteNarrowWidth            , 285 }
                , { Place.BetweenSiteMiddleWidth            , 635 }
                , { Place.BetweenSiteWideWidth              , 785 }
                , { Place.BetweenSiteHeight                 , 1880 }
            };

            db_ = new DBWrapper("app.config.db", "glass_dimensions");
            if (db_ == null && !db_.isOpen())
            {
                MessageBox.Show("Ошибка чтения базы данных конфигурационного файла!");
                Application.Exit();
            }
            if (db_.isEmpty())
            {
                if (!save(database_))
                {
                    MessageBox.Show("Ошибка! Файл с настройками не является оригинальным!\r\nПереустановите приложение!");
                    Application.Exit();
                }
            }
            

            var actual_base_tmp = db_.selectFromTable();
            if (actual_base_tmp.Rows.Count == 0 || actual_base_tmp.Rows.Count != database_.Count)
            {
                actual_base_ = database_.ToDictionary(entry => entry.Key, entry => entry.Value);
                return;
            }

            actual_base_ = new Dictionary<Place, ulong>();
            foreach (DataRow row in actual_base_tmp.Rows)
            {
                Place place;
                ulong size;
                if (!Place.TryParse(row[1].ToString(), out place) || !ulong.TryParse(row[2].ToString(), out size))
                {
                    actual_base_ = database_.ToDictionary(entry => entry.Key, entry => entry.Value);
                    return;
                }
                actual_base_.Add(place, size);
            }
        }

        public Dictionary<Place, ulong> actualSize()
        {
            return actual_base_;
        }

        public ulong actualSize(Place place)
        {
            return actual_base_[place];
        }

        public bool setActualSize(Place place, string new_value)
        {
            if (actual_base_.TryGetValue(place, out _))
            {
                ulong value;
                if (!ulong.TryParse(new_value, out value)) return false;
                actual_base_[place] = value;
                return true;
            }
            return false;
        }

        private bool save(Dictionary<Place, ulong> dict)
        {
            if (db_.isEmpty()) return insert(dict);
            return update(dict);
        }

        private bool insert(Dictionary<Place, ulong> dict)
        {
            int is_write = 1;
            foreach (KeyValuePair<Place, ulong> entry in dict)
            {
                var data = new List<DBWrapper.DBData>();
                data.Add(new DBWrapper.DBData("name", entry.Key.ToString()));
                data.Add(new DBWrapper.DBData("size", entry.Value.ToString()));
                is_write &= (db_.writeToTable(data) ? 1 : 0);
                if (is_write == 0)
                {
                    return false;
                }
            }
            return is_write == 1;
        }

        private bool update(Dictionary<Place, ulong> dict)
        {
            var new_data = new List<List<DBWrapper.DBData>>();
            foreach (KeyValuePair<Place, ulong> entry in dict)
            {
                var pair = new List<DBWrapper.DBData>();
                pair.Add(new DBWrapper.DBData("name", entry.Key.ToString()));
                pair.Add(new DBWrapper.DBData("size", entry.Value.ToString()));
                new_data.Add(pair);
            }
            return db_.updateInTable(new_data);
        }

        public bool reset()
        {
            actual_base_ = database_.ToDictionary(entry => entry.Key, entry => entry.Value);
            return save(actual_base_);
        }

        public bool save()
        {
            return save(actual_base_);
        }

    }
}
