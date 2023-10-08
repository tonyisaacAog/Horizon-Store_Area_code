using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInfrastructure.Extensions
{
    public static class NumberExtension
    {
        public static int ParseToInt(this object obj)
        {
            int i;
            int.TryParse(obj.ToString(), out i);
            return i;
        }
    }
}
