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
        private Person _personToEdit;
        private Person _backupPerson;

        public Person PersonToEdit
        {
            get => _personToEdit;
            set => _personToEdit = value;
        }

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

            personTypeSelection.DropDownClosed += new EventHandler(onUpdateUserControl);
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
            propertiesToInstanciate.ToList().ForEach((property) =>
            {
                PropertyList.Children.Add(new PropertyEntry(PersonToEdit, property.Name));
            });
        }

        private void onUpdateUserControl(object sender, EventArgs e)
        {
            updateUserControl();

        }
        private void updateUserControl()
        {
            PersonToEdit =  PersonToEdit.copyPerson(
                    (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.Text));
            PropertyList.Children.Clear();
            updateUserControlByCommonClassFields();
            updateUserControlByUniqueClassFields();

        }

        // could've abstracted those two functions because they're pretty similar but it would cause confusion due to no fitting naming
        private PropertyInfo[] findVaryingFields()
        {
            PropertyInfo[] sourceProperties = _backupPerson.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Person selectedPersonType = (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.SelectedValue.ToString());
            ModelConverter.convertSourceToTargetModel(_backupPerson, selectedPersonType);
            PropertyInfo[] targetProperties = selectedPersonType.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] sharedProperties = targetProperties.Where(targetProperty => !sourceProperties.Select(p => p.Name).Contains(targetProperty.Name)).ToArray();
            // can't return at previous line because the inversion by ! would get ignored bug
            return sharedProperties;
        }

        private PropertyInfo[] findCommonFields()
        {
            PropertyInfo[] sourceProperties = _backupPerson.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Person selectedPersonType = (Person)ClassGenerator.CreatePersonClassFromString(personTypeSelection.SelectedValue.ToString());
            ModelConverter.convertSourceToTargetModel(_backupPerson, selectedPersonType);
            PropertyInfo[] targetProperties = selectedPersonType.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return targetProperties.Where(targetProperty => sourceProperties.Select(p => p.Name).Contains(targetProperty.Name)).ToArray();
        }

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

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            PersonToEdit = _backupPerson;
            this.Close();
        }
    }
}
