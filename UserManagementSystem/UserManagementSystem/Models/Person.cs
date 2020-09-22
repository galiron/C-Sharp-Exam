using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Services;

namespace UserManagementSystem.Models
{
    public class Person : IPropertyNotificator
    {
        private string _name;
        private string _surname;
        private int _age;
        private string _eyeColor;
        private double _height;
        private double _weight;

        public string Name
        {
            get => _name;
            set { SetField(ref _name, value, "Name");}

        }

        public string SurName
        {
            get => _surname;
            set { SetField(ref _surname, value, "SurName"); }
        }

        public int Age
        {
            get => _age;
            set { SetField(ref _age, value, "Age"); }
        }

        public string EyeColor
        {
            get => _eyeColor;
            set { SetField(ref _eyeColor, value, "EyeColor"); }
        }

        public double Height
        {
            get => _height;
            set { SetField(ref _height, value, "Height"); }
        }

        public double Weight
        {
            get => _weight;
            set { SetField(ref _weight, value, "Weight"); }
        }

        public Person(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }
        public Person() { }

        virtual public Person copyPerson(Person instanceToCopy)
        {
            return ModelConverter.convertSourceToTargetModel(this, instanceToCopy);
        }
    }
}
