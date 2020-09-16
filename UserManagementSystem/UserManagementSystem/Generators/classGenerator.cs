using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Collections;
using UserManagementSystem.Models;

namespace UserManagementSystem.Generators
{
    static class ClassGenerator
    {
        public static Object CreatePersonClassFromString(string className)
        {
            string objectToInstantiate = PersonModelDictionary.getValue(className);

            var objectType = Type.GetType(objectToInstantiate);

            return Activator.CreateInstance(objectType);
        }
    }
}
