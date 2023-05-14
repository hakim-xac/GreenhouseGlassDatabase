using System.Collections.Generic;
using System.Linq;

namespace GreenhouseGlassDatabase
{
    internal class Config
    {
        private Dictionary<Place, int> actual_base_;
        private static Dictionary<Place, int> database_ = new Dictionary<Place, int>() {
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


        public Config()
        {
            actual_base_ = database_.ToDictionary(entry => entry.Key, entry => entry.Value);
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