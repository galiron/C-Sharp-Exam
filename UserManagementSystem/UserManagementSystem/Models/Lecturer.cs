using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Services;

namespace UserManagementSystem.Models
{
    /// <summary>
    /// Lecturer Model which is used as DataType to resemble a Lecturer in the application
    /// </summary>
    class Lecturer : UniversityMember
    {
        private double _salary;

        public double Salary
        {
            get => _salary;
            set => SetField(ref _salary, value, "Salary");
        }

        public Lecturer() : base()
        {

        }

        /// <summary>
        /// Copies a person
        /// </summary>
        /// <param name="instanceToCopy"></param>
        /// <returns>new instance of given instanceToCopy</returns>
        public override Person copyPerson(Person instanceToCopy)
        {
            return ModelConverter.convertSourceToTargetModel(this, instanceToCopy);
        }
    }
}
