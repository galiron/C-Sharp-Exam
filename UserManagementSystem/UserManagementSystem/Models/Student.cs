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
    /// <summary>
    /// Student Model which is used as DataType to resemble a Student in the application
    /// </summary>
    class Student: UniversityMember
    {
        private double _averageGrade;

        public double AverageGrade
        {
            get => _averageGrade;
            set => SetField(ref _averageGrade, value, "AverageGrade");
        }

        public Student():base()
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
