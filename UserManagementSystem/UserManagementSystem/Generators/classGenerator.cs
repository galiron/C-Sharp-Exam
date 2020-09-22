using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Collections;
using UserManagementSystem.Models;

namespace UserManagementSystem.Generators
{
    /// <summary>
    /// Generator for Classes used in this Application
    /// </summary>
    static class ClassGenerator
    {
        /// <summary>
        /// Creates an instance of a class by a given simple class name
        /// </summary>
        /// <param name="className">Simple class name of a Person Type i.e. "Person" or "Student</param>
        /// <returns></returns>
        public static Object CreatePersonClassFromString(string className)
        {
            string objectToInstantiate = PersonModelDictionary.GetValue(className);

            var objectType = Type.GetType(objectToInstantiate);

            return Activator.CreateInstance(objectType);
        }
    }
}
