using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Utility;

namespace UserManagementSystem.Collections
{
    public static class PersonModelDictionary
    {
        public static readonly Dictionary<string, string> personClassDictionary  = new Dictionary<string, string>
        {
            {"Person", "UserManagementSystem.Models.Person, UserManagementSystem"},
            {"Student", "UserManagementSystem.Models.Student, UserManagementSystem"},
            {"Lecturer", "UserManagementSystem.Models.Lecturer, UserManagementSystem"}
        };

        public static string GetValue(string key)
        {
            string retrievedValue = null;

            try
            {
                retrievedValue = personClassDictionary[key];
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return retrievedValue;
        }

        public static string GetSimpleClassFromType(string value)
        {
            return value.Replace("UserManagementSystem.Models.", "");
        }
    }
}
