using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserManagementSystem.Collections;
using UserManagementSystem.Generators;
using UserManagementSystem.Models;
using UserManagementSystem.Services;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Interaktionslogik für PersonView.xaml
    /// </summary>
    public partial class PersonView : Window
    {
        private Type currentPersonType = null;
        private Person _person;
        public Person PersonToEdit { get; set; }

        public PersonView(Person person)
        {
            DataContext = this;
            PersonToEdit = person;
            currentPersonType = person.GetType();
            List<string> personTypeComboBoxEntries = new List<string>();
            foreach (var keyValuePair in PersonModelDictionary.personClassDictionary)
            {
                personTypeComboBoxEntries.Add(keyValuePair.Key);
            }
            InitializeComponent();
            foreach (var entry in personTypeComboBoxEntries)
            {
                personTypeSelection.Items.Add(entry);
            }

            personTypeSelection.SelectionChanged += new SelectionChangedEventHandler(updateUserControlByUniqueClassFields);
        }

        public void updateUserControlByUniqueClassFields(object sender, SelectionChangedEventArgs e)
        {
            Person selectedPersonType = (Person) ClassGenerator.CreatePersonClassFromString(personTypeSelection.SelectedValue.ToString());
            ModelConverter.convertSourceToTargetModel(PersonToEdit, selectedPersonType);
            Console.WriteLine(selectedPersonType.GetType());
            PropertyInfo[] info = selectedPersonType.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine(info);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PersonToEdit.Name = "a";
            PersonToEdit.SurName = "a";
            PersonToEdit.Age = "a";
            PersonToEdit.EyeColor = "a";
            PersonToEdit.Weight = "a";
            PersonToEdit.Height = "a";
            Console.WriteLine(PersonToEdit.Name);
        }
    }
}
