using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInfrastructure.Extensions
{
    public static class ExceptionExtension
    {
        public static List<string> GetExpctionErrors(this Exception ex)
        {
            var Result = new List<string>();
            do
            {
                Result.Add(ex.Message);
                ex = ex.InnerException;
            } while (ex != null);

            return Result;
        }
    }
}
