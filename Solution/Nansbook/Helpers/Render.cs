using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Helpers
{
    public static class Render
    {
        public static string GetDayOfWeek(int? d){
            string day = string.Empty;

            switch (d) {
                case 0: day = "Sun"; break;
                case 1: day = "Mon"; break;
                case 2: day = "Tue"; break;
                case 3: day = "Wed"; break;
                case 4: day = "Thu"; break;
                case 5: day = "Fri"; break;
                case 6: day = "Sat"; break;
            }

            return day;
        }
    }
}