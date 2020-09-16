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
        public event PropertyChangedEventHandler PropertyChanged;

        public Person(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }

        public Person()
        {
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
            set { SetField(ref _name, value, "Name"); }

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
