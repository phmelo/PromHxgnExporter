using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHExporterWebAPI
{
    public static class ExtensionMethodsIOnCall
    {


        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string GetDgroupRegion(this String str)
        {
            string[] strArray = { "1/GOA", "17GBM", "1GBM", "1GMAR", "25GBM", "3GMAR", "D1/1", "D1/M", "8GBM", "D1/8", "13GBM", "D1/13", "D2/13", "D3/13", "12GBM", "1GFMA", "2GMAR", "D3/M", "D4/M", "GBS", "GOA", "P1/GB", "P2/GB", "11GBM", "D1/11", "D1/GO", "D2/11", "D2/GO", "D3/11", "D3/GO", "GOCG", "P1/11", "19GBM", "2GBM", "D1/19", "D1/2", "24GBM", "28GBM", "D/ESC", "D1/24", "D2/24", "14GBM", "D1/4", "D1/10", "D1/14" };
            return strArray.Any(s => str.Contains(s)) ? "CAPITAL" : "FORACAPITAL";
        }


        public static string GetUnitPrefixType(this String str)
        {
            return str;
        }

    }
}
