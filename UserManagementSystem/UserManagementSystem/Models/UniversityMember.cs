using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Services;

namespace UserManagementSystem.Models
{
    abstract class UniversityMember: Person
    {
        private int _matriculationnumber;

        public int MatriculationNumber
        {
            get => _matriculationnumber;
            set => SetField(ref _matriculationnumber, value, "MatriculationNumber");
        }

        public UniversityMember() : base()
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
