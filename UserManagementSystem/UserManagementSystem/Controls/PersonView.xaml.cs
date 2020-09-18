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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserManagementSystem.Collections;
using UserManagementSystem.Generators;
using UserManagementSystem.Models;
using UserManagementSystem.Services;
using ComboBox = System.Windows.Controls.ComboBox;

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
        private Person _backupPerson;

        public PersonView(Person person)
        {
            _backupPerson = person;
            _indexOfPersonInAppStatePersonCollection = AppState.Persons.IndexOf(person);
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
            foreach (var entry in personTypeComboBoxEntries)
            {
                personTypeSelection.Items.Add(entry);
            }
            personTypeSelection.SelectedItem = currentPersonTypeKey != "" ? currentPersonTypeKey : "Person";

            personTypeSelection.SelectionChanged += new SelectionChangedEventHandler(onUpdateUserControl);
            updateUserControl();
        }


        private void updateUserControlByUniqueClassFields()
        {
            PropertyInfo[] varyingPropeties = findVaryingFields();
            instanciatePropertyEntries(varyingPropeties);

        }

        private void updateUserControlByCommonClassFields()
        {
            PropertyInfo[] commonPropeties = findCommonFields();
            instanciatePropertyEntries(commonPropeties);

        }

        private void instanciatePropertyEntries(PropertyInfo[] propertiesToInstanciate)
        {
            PropertyInfo[] sourceProperties = PersonToEdit.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Person personWithAlteredType =
                PersonToEdit.copyPerson(
                    (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.Text));
            sourceProperties.ToList().ForEach((property) =>
            {
                PropertyList.Children.Add(new PropertyEntry(personWithAlteredType, property.Name));
            });
        }

        private void onUpdateUserControl(object sender, SelectionChangedEventArgs e)
        {
            updateUserControl();

        }
        private void updateUserControl()
        {
            PropertyList.Children.Clear();
            updateUserControlByCommonClassFields();
            updateUserControlByUniqueClassFields();

        }

        // could've abstracted those two functions because they're pretty similar but it would cause confusion due to no fitting naming
        private PropertyInfo[] findVaryingFields()
        {
            PropertyInfo[] sourceProperties = PersonToEdit.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Person selectedPersonType = (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.SelectedValue.ToString());
            ModelConverter.convertSourceToTargetModel(PersonToEdit, selectedPersonType);
            PropertyInfo[] targetProperties = selectedPersonType.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return targetProperties.Where(targetProperty => !sourceProperties.Select(p => p.Name).Contains(targetProperty.Name)).ToArray();
        }

        private PropertyInfo[] findCommonFields()
        {
            PropertyInfo[] sourceProperties = PersonToEdit.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Person selectedPersonType = (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.SelectedValue.ToString());
            ModelConverter.convertSourceToTargetModel(PersonToEdit, selectedPersonType);
            PropertyInfo[] targetProperties = selectedPersonType.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return targetProperties.Where(targetProperty => sourceProperties.Select(p => p.Name).Contains(targetProperty.Name)).ToArray();
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
            PersonToEdit = _backupPerson;
            this.Close();
        }
    }
}
