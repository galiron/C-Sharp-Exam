using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        private Type _currentPersonType = null;
        private int _indexOfPersonInAppStatePersonCollection;
        public Person PersonToEdit { get; set; }

        public PersonView(Person person)
        {
            _indexOfPersonInAppStatePersonCollection= AppState.Persons.IndexOf(person);
            DataContext = this;
            PersonToEdit = person;
            _currentPersonType = person.GetType();

            InitializeComponent();
            initializeComboBox();
        }

        private void initializeComboBox()
        {
            List<string> personTypeComboBoxEntries = new List<string>();
            foreach (var keyValuePair in PersonModelDictionary.personClassDictionary)
            {
                personTypeComboBoxEntries.Add(keyValuePair.Key);
            }
            string currentPersonTypeKey = PersonModelDictionary.GetSimpleClassFromType(_currentPersonType.ToString());
            Console.WriteLine(currentPersonTypeKey);
            foreach (var entry in personTypeComboBoxEntries)
            {
                personTypeSelection.Items.Add(entry);
            }
            if (currentPersonTypeKey != "")
            {
                personTypeSelection.SelectedItem = currentPersonTypeKey;
            }
            else
            {
                personTypeSelection.SelectedItem = "Person";
            }

            personTypeSelection.SelectionChanged += new SelectionChangedEventHandler(updateUserControlByUniqueClassFields);
        }

        public void updateUserControlByUniqueClassFields(object sender, SelectionChangedEventArgs e)
        {
            Person selectedPersonType = (Person) ClassGenerator.CreatePersonClassFromString(personTypeSelection.SelectedValue.ToString());
            ModelConverter.convertSourceToTargetModel(PersonToEdit, selectedPersonType);
            PropertyInfo[] info = selectedPersonType.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            // the copy step is necessary because the Instance of PersonToEdit is not allways the target instance (polymorphism)
            Person newPerson =
                PersonToEdit.copyPerson(
                    (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.Text));
            if (this._indexOfPersonInAppStatePersonCollection == -1)
            {
                AppState.Persons.Add(newPerson);
            }
            else
            {
                AppState.Persons[_indexOfPersonInAppStatePersonCollection] = newPerson;
            }

            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
