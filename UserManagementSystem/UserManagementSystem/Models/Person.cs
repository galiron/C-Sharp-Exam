using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    public class Person : IPropertyNotificator
    {

        public Person(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }
        public Person() { }

        virtual public Person copyPerson(Person instanceToCopy)
        {
            instanceToCopy._name = this._name;
            instanceToCopy._surname = this._surname;
            instanceToCopy._age = this._age;
            instanceToCopy._eyeColor = this._eyeColor;
            instanceToCopy._height = this._height;
            instanceToCopy._weight = this._weight;
            return instanceToCopy;
        }

        public Person(List<PropertyInfo> properties)
        {
            properties.ForEach(propertie => Console.WriteLine(propertie));
        }

        // private fields
        // notice that all fields are strings for simplification. int would work as well, but since two way databinding is used here
        // it would use 0 as default and would not allow any other information in those textboxes. 
        private string _name;
        private string _surname;
        private string _age;
        private string _eyeColor;
        private string _height;
        private string _weight;


        // properties handling private fields
        public string Name
        {
            get => _name;
            set { SetField(ref _name, value, "Name"); Console.WriteLine(_name);}

        }

        public string SurName
        {
            get => _surname;
            set { SetField(ref _surname, value, "SurName"); }
        }

        public string Age
        {
            get => _age;
            set { SetField(ref _age, value, "Age"); }
        }

        public string EyeColor
        {
            get => _eyeColor;
            set { SetField(ref _eyeColor, value, "EyeColor"); }
        }

        public string Height
        {
            get => _height;
            set { SetField(ref _height, value, "Height"); }
        }

        public string Weight
        {
            get => _weight;
            set { SetField(ref _weight, value, "Weight"); }
        }



    }
}
