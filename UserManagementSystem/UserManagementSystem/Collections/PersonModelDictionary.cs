using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Collections
{
    /// <summary>
    /// Dictionary where all (non abstract) Person Models need to be referenced for use in the application
    /// </summary>
    public static class PersonModelDictionary
    {

        /// <summary>
        /// Classes referenced here have will be automatically be made available for the user for use.
        /// Restriction: Models have to be non abstract to be usable and are only allowed to contain
        /// properties of Type: double, int32 and string
        /// </summary>
        public static readonly Dictionary<string, string> personClassDictionary  = new Dictionary<string, string>
        {
            {"Person", "UserManagementSystem.Models.Person, UserManagementSystem"},
            {"Student", "UserManagementSystem.Models.Student, UserManagementSystem"},
            {"Lecturer", "UserManagementSystem.Models.Lecturer, UserManagementSystem"}
        };

        /// <summary>
        /// Returns the full classpath with Namespace
        /// </summary>
        /// <param name="key">simple class name i.e. "Person" or "Lecturer"</param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            string retrievedValue = null;

            try
            {
                retrievedValue = personClassDictionary[key];
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
            }

            return retrievedValue;
        }

        /// <summary>
        /// Retrieves simple name from Classpath
        /// </summary>
        /// <param name="value">Type of a Model</param>
        /// <returns></returns>
        public static string GetSimpleClassFromType(string value)
        {
            return value.Replace("UserManagementSystem.Models.", "");
        }
    }
}
