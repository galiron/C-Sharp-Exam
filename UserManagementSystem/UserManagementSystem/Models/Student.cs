using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Collections;
using UserManagementSystem.Generators;
using UserManagementSystem.Services;

namespace UserManagementSystem.Models
{
    class Student: UniversityMember
    {

        private double _averageGrade;

        public double AverageGrade
        {
            get => _averageGrade;
            set => SetField(ref _averageGrade, value, "AverageGrade");
        }

        public Student(int matriculationNumber, string name, string surname): base(matriculationNumber, name, surname)
        {
        }

        public Student():base()
        {
        }

        public override Person copyPerson(Person instanceToCopy)
        {
            return ModelConverter.convertSourceToTargetModel(this, instanceToCopy);
        }

        public Student(List<PropertyInfo> properties): base(properties)
        {
            properties.ForEach(propertie => Console.WriteLine(propertie.GetValue(propertie)));
        }
    }
}
