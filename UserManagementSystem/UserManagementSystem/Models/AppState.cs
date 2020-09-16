using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Collections;

namespace UserManagementSystem.Models
{
    static class AppState
    {
        private static PersonCollection _personCollection = new PersonCollection();

        public static PersonCollection Persons
        {
            get => _personCollection;
        }
    }
}
