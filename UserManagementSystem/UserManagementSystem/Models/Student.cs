﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class Student: UniversityMember
    {
        public Student(string matriculationNumber, string name, string surname): base(matriculationNumber, name, surname)
        {
        }

        public Student():base()
        {
        }

        public Student(List<PropertyInfo> properties): base(properties)
        {
            properties.ForEach(propertie => Console.WriteLine(propertie.GetValue(propertie)));
        }

        private string _averageGrade;

        public string AverageGrade
        {
            get => _averageGrade;
            set => SetField(ref _averageGrade, value, "AverageGrade");
        }
    }
}