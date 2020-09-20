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
        private static List<Filter> _filter = new List<Filter>();

        public static PersonCollection Persons
        {
            get => _personCollection;
            set => _personCollection = value;
        }
        public static List<Filter> Filters
        {
            get => _filter;
            set => _filter = value;
        }
    }
}
