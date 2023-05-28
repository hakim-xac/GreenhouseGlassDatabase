using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenhouseGlassDatabase
{
    public enum TableType
    {
        General
            , ReplacementData
    }

    public enum TypeGlass{
        Unknown
            , One
            , Second
            , Third
            , Fourth
            , Fifth
            , Sixth
            , Seventh
            , Eighth
    }

    public enum Place
    {
        ButtWidth
            , SideWidth
            , HeightFirstRowFirstAreaInSide
            , HeightFirstRowFirstAreaOutSide
            , HeightSecondRowFirstAreaInSide
            , HeightSecondRowFirstAreaOutSide
            , HeightFirstRowSecondAreaInSide
            , HeightFirstRowSecondAreaOutSide
            , HeightSecondRowSecondAreaInSide
            , HeightSecondRowSecondAreaOutSide
            , HeightFirstRowButtAreaInSide
            , HeightFirstRowButtAreaOutSide
            , HeightSecondRowButtAreaInSide
            , HeightSecondRowButtAreaOutSide
            , BetweenSiteNarrowWidth
            , BetweenSiteMiddleWidth
            , BetweenSiteWideWidth
            , BetweenSiteHeight

    }

    public enum SortDirtection
    {
        Ascending
            , Descending
    }
}
