using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend
{
    public class Utils
    {
        public static string GetCredsFromFullName(string fullName)
        {
            var nameArr = fullName.Split(" ");
            var res = fullName;
            if (nameArr.Length == 3) 
            {
                res = nameArr[0] + " " + nameArr[1].ToArray()[0].ToString().ToUpper() + ". "
                    + nameArr[2].ToArray()[0].ToString().ToUpper() + ".";
            }
            return res;
        }
    }
}
