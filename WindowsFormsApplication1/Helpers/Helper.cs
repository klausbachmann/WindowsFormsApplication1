using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuicContentLoader.Helpers
{
    public class Helper
    {
        public static int getOffset(int day)
        {
            switch (day)
            {
                case 3: return -630;
                case 4: return -610;
                case 5: return -590;
                case 6: return -575;
                case 7: return -560;
                case 8: return -545;
                case 9: return -530;
                case 10: return -510;
                case 11: return -495;
                case 12: return -480;
                case 13: return -465;
                case 14: return -450;
                case 15: return -435;
                case 16: return -420;
                case 17: return -405;
                case 18: return -390;
                case 19: return -375;
                case 20: return -360;
                case 21: return -345;
                case 22: return -330;
                case 23: return -310;
                case 24: return -295;
                case 25: return -280;
                case 26: return -265;
                case 27: return -250;
                case 28: return -230;
                case 29: return -215;
                case 30: return -200;
                case 31: return -185;
                case 32: return -160;
                case 33: return -145;
                case 34: return -130;
                case 35: return -115;
                case 36: return -100;
                case 37: return -85;
                case 38: return -70;
                case 39: return -55;
                case 40: return -40;
                case 41: return -15;
                default: return 0;
            }
        }
    }
}
