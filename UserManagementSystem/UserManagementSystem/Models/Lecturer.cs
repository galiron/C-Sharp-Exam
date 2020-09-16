using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class Lecturer: UniversityMember
    {
        public Lecturer(string matriculationNumber, string name, string surname): base(matriculationNumber,name, surname)
        {
        }

        public Lecturer() : base()
        {

        }

        public Lecturer(List<PropertyInfo> properties) : base(properties)
        {
            properties.ForEach(propertie => Console.WriteLine(propertie.GetValue(propertie)));
        }
    }
}
