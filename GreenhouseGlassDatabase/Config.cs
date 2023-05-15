using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace GreenhouseGlassDatabase
{
    internal class Config
    {
        private Dictionary<Place, int> actual_base_;
        private static Dictionary<Place, int> database_;
        private DBWrapper db_;


        public Config()
        {
            database_ = new Dictionary<Place, int>() {
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
                int isWrite = 1;
                foreach (KeyValuePair<Place, int> entry in database_)
                {
                    var data = new List<DBWrapper.DBData>();
                    data.Add(new DBWrapper.DBData("name", entry.Key.ToString()) );
                    data.Add(new DBWrapper.DBData("size", entry.Value.ToString()) );
                    isWrite &= (db_.writeToTable(data) ? 1 : 0);
                    if (isWrite == 0)
                    {
                        MessageBox.Show("Ошибка! Файл с настройками не является оригинальным!\r\nПереустановите приложение!");
                        Application.Exit();
                    }
                }
                
            }

            var actual_base_tmp = db_.selectFromTable();
            if (actual_base_tmp.Rows.Count == 0)
            {
                actual_base_ = database_.ToDictionary(entry => entry.Key, entry => entry.Value);
                return;
            }

            actual_base_ = new Dictionary<Place, int>();
            foreach (DataRow row in actual_base_tmp.Rows)
            {
                Place place;
                int size;
                if (!Place.TryParse(row[1].ToString(), out place) || !int.TryParse(row[2].ToString(), out size))
                {
                    actual_base_ = database_.ToDictionary(entry => entry.Key, entry => entry.Value);
                    return;
                }
                actual_base_.Add(place, size);
            }

        }

        ~Config()
        {
            db_?.Close();
        }

        static public Dictionary<Place, int> defaultSize()
        {
            return  database_;
        }
        static public int defaultSize(Place place)
        {
            return database_[place];
        }

        public Dictionary<Place, int> actualSize()
        {
            return actual_base_;
        }
        public int actualSize(Place place)
        {
            return actual_base_[place];
        }
    }
}