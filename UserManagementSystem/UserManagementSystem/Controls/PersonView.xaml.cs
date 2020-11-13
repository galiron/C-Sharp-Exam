using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using UserManagementSystem.Collections;
using UserManagementSystem.Generators;
using UserManagementSystem.Models;
using UserManagementSystem.Services;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Logic for the PersonView.xaml
    /// </summary>
    public partial class PersonView : Window
    {
        private Type _currentPersonType = null;
        private int _indexOfPersonInAppStatePersonCollection;
        private Person _personToEdit;
        private Person _backupPerson;

        public Person PersonToEdit
        {
            get => _personToEdit;
            set => _personToEdit = value;
        }

        /// <summary>
        /// Initializing UI Elements and necessary data to manage the given Person
        /// </summary>
        /// <param name="person">Person to be managed</param>
        public PersonView(Person person)
        {
            _backupPerson = person;
            _indexOfPersonInAppStatePersonCollection = AppState.Persons.IndexOf(person);
            PersonToEdit = person;
            _currentPersonType = person.GetType();

            InitializeComponent();
            initializeComboBox();
        }

        /// <summary>
        /// Initializing the personTypeSelection ComboBox with items from the PersonModelDictionary 
        /// </summary>
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

            personTypeSelection.DropDownClosed += new EventHandler(onUpdateUserControl);
            updateUserControl();
        }


        /// <summary>
        /// Gets unique Class fields (which distinguish from the source Model) and adds them to the View
        /// </summary>
        private void updateUserControlByUniqueClassFields()
        {
            PropertyInfo[] varyingPropeties = findVaryingFields();
            instanciatePropertyEntries(varyingPropeties);

        }

        /// <summary>
        /// Gets common Class fields (which are common wih source Model) and adds them to the View
        /// </summary>
        private void updateUserControlByCommonClassFields()
        {
            PropertyInfo[] commonPropeties = findCommonFields();
            instanciatePropertyEntries(commonPropeties);

        }


        /// <summary>
        /// Creates a new PropertyEntries from a given Array
        /// </summary>
        /// <param name="propertiesToInstanciate"></param>
        private void instanciatePropertyEntries(PropertyInfo[] propertiesToInstanciate)
        {
            propertiesToInstanciate.ToList().ForEach((property) =>
            {
                PropertyList.Children.Add(new PropertyEntry(PersonToEdit, property.Name));
            });
        }

        /// <summary>
        /// Event handler to update the user controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onUpdateUserControl(object sender, EventArgs e)
        {
            updateUserControl();

        }

        /// <summary>
        /// Function to update the UI element of the View
        /// </summary>
        private void updateUserControl()
        {
            PersonToEdit =  PersonToEdit.copyPerson(
                    (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.Text));
            PropertyList.Children.Clear();
            updateUserControlByCommonClassFields();
            updateUserControlByUniqueClassFields();

        }

        // could've abstracted those two functions because they're pretty similar but it would cause confusion due to no fitting naming
        /// <summary>
        /// Function to find all varying fields of the current and target model
        /// </summary>
        /// <returns></returns>
        private PropertyInfo[] findVaryingFields()
        {
            PropertyInfo[] sourceProperties = _backupPerson.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Person selectedPersonType = (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.SelectedValue.ToString());
            ModelConverter.convertSourceToTargetModel(_backupPerson, selectedPersonType);
            PropertyInfo[] targetProperties = selectedPersonType.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] sharedProperties = targetProperties.Where(targetProperty => !sourceProperties.Select(p => p.Name).Contains(targetProperty.Name)).ToArray();
            return sharedProperties;
        }

        /// <summary>
        /// Function to find all common fields of the current and target model
        /// </summary>
        /// <returns></returns>
        private PropertyInfo[] findCommonFields()
        {
            PropertyInfo[] sourceProperties = _backupPerson.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Person selectedPersonType = (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.SelectedValue.ToString());
            ModelConverter.convertSourceToTargetModel(_backupPerson, selectedPersonType);
            PropertyInfo[] targetProperties = selectedPersonType.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return targetProperties.Where(targetProperty => sourceProperties.Select(p => p.Name).Contains(targetProperty.Name)).ToArray();
        }


        /// <summary>
        /// Event handler to save the Person of this view to the AppState
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, RoutedEventArgs e)
        {

            if (this._indexOfPersonInAppStatePersonCollection == -1)
            {
                AppState.Persons.Add(PersonToEdit);
            }
            else
            {
                AppState.Persons[_indexOfPersonInAppStatePersonCollection] = PersonToEdit;
            }

            this.Close();
        }

        /// <summary>
        /// Event Handler to close the view and ensure to not persist the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            // PersonToEdit still holds a reference to the AppState, so the backup needs to be loaded.
            PersonToEdit = _backupPerson;
            this.Close();
        }
    }
}
