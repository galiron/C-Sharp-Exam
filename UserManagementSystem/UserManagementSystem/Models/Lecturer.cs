using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Services;

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

        public override Person copyPerson(Person instanceToCopy)
        {
            return ModelConverter.convertSourceToTargetModel(this, instanceToCopy);
        }

        public Lecturer(List<PropertyInfo> properties) : base(properties)
        {
            properties.ForEach(propertie => Console.WriteLine(propertie.GetValue(propertie)));
        }

        private string _salary;

        public string Salary
        {
            get => _salary;
            set => SetField(ref _salary, value, "Salary");
        }
    }
}
