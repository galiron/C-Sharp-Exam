﻿using System;
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
        private string _matriculationnumber;
        public UniversityMember(string matriculationNumber, string name, string surname): base(name, surname)
        {
            this.MatriculationNumber = matriculationNumber;
        }

        public UniversityMember() : base()
        {

        }

        public override Person copyPerson(Person instanceToCopy)
        {
            return ModelConverter.convertSourceToTargetModel(this, instanceToCopy);
        }

        public UniversityMember(List<PropertyInfo> properties) : base(properties)
        {
            properties.ForEach(propertie => Console.WriteLine(propertie.GetValue(propertie)));
        }

        public string MatriculationNumber
        {
            get => _matriculationnumber;
            set => SetField(ref _matriculationnumber, value, "MatriculationNumber");
        }
    }
}
