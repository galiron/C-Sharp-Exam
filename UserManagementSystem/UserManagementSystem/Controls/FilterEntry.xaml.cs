using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoreLinq;
using UserManagementSystem.Collections;
using UserManagementSystem.Generators;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// UserControl managing a single Filter
    /// </summary>
    public partial class FilterEntry : UserControl
    {
        private Binding _propertyBinding;
        private Binding _comparatorBinding;
        private Binding _comparatorValueToCompareBinding;
        private PropertyInfo[] _allProperties;
        private Type _propertyType;
        private Filter _newEntry;
        // only needed to delete this entry
        private FilterOverview _parent;

        /// <summary>
        /// initializes UI elements and sets the necessary events
        /// </summary>
        public FilterEntry()
        {
            InitializeComponent();
            InitializePropertySelection();
            PropertyNameComboBox.SelectionChanged += onPropertyNameComboBoxChanged;
            ComparatorComboBox.SelectionChanged += onComparatorBoxChanged;
        }

        /// <summary>
        /// Constructor which binds the source element (Filter) to the necessary UIElements
        /// and sets the necessary event handlers to deal with selection changes.
        /// </summary>
        /// <param name="dataSourceToBind">The source Filter the entry should manage</param>
        /// <param name="parent">parent of this entry</param>
        public FilterEntry(Filter dataSourceToBind, FilterOverview parent)
        {
            _parent = parent;
            _newEntry = new Filter
            {
                ValueToCompare = dataSourceToBind.ValueToCompare,
                Comparator = dataSourceToBind.Comparator,
                PropertyName = dataSourceToBind.PropertyName
            };
            InitializeComponent();
            InitializePropertySelection();
            PropertyNameComboBox.SelectionChanged += onPropertyNameComboBoxChanged;
            ComparatorComboBox.SelectionChanged += onComparatorBoxChanged;

            _propertyBinding = new Binding
            {
                Source = _newEntry,
                Path = new PropertyPath("PropertyName"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            _comparatorBinding = new Binding
            {
                Source = _newEntry,
                Path = new PropertyPath("Comparator"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            _comparatorValueToCompareBinding = new Binding
            {
                Source = _newEntry,
                Path = new PropertyPath("ValueToCompare"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            BindingOperations.SetBinding(PropertyNameComboBox, ComboBox.TextProperty, _propertyBinding);
            BindingOperations.SetBinding(ComparatorComboBox, ComboBox.TextProperty, _comparatorBinding);
            BindingOperations.SetBinding(PropertyValueTextBox, TextBox.TextProperty, _comparatorValueToCompareBinding);
            dataSourceToBind.OnPropertyChanged("PropertyName");
            dataSourceToBind.OnPropertyChanged("Comparator");
            dataSourceToBind.OnPropertyChanged("ValueToCompare");
        }

        /// <summary>
        /// Fills the Combobox for the propertyselection with values
        /// </summary>
        private void InitializePropertySelection()
        {
            _allProperties = getPropertiesOfAllPersonModels();
            foreach (PropertyInfo property in _allProperties)
            {
                PropertyNameComboBox.Items.Add(property.Name);

            }
        }

        /// <summary>
        /// Searches for all properties available in the PersonModelDictionary
        /// </summary>
        /// <returns>Array of all distinct properties that exists over all Persons</returns>
        private PropertyInfo[] getPropertiesOfAllPersonModels()
        {
            List<Person> allPersonModels = new List<Person>();
            foreach (var keyValuePair in PersonModelDictionary.personClassDictionary)
            {
                allPersonModels.Add((Person) ClassGenerator.CreatePersonClassFromString(keyValuePair.Key));
            }

            List<PropertyInfo> allProperties = new List<PropertyInfo>();
            foreach (Person personModel in allPersonModels)
            {
                PropertyInfo[] propertiesOfThisModel = personModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertiesOfThisModel.ToList().ForEach(prop => allProperties.Add(prop));
            }

            return allProperties.DistinctBy(prop => prop.Name).ToArray();
        }

        // Enabling the comparator ComboBox
        private void enableComparator (object sender, SelectionChangedEventArgs e)
        {
            ComparatorComboBox.IsEnabled = true;
            InitializeComparatorValues();
        }

        /// <summary>
        /// Initialize the values of the comparator ComboBox depending on the properties data type
        /// </summary>
        private void InitializeComparatorValues()
        {
            PropertyInfo selectedProperty = _allProperties.First(prop => prop.Name == PropertyNameComboBox.SelectedValue);
            _propertyType = selectedProperty.PropertyType;
            switch (_propertyType.ToString())
            {
                case "System.String" :
                    ComparatorComboBox.Items.Clear();
                    ComparatorComboBox.Items.Add("Contains");
                    break;
                case "System.Double":
                    addNumberComparator();
                    break;
                case "System.Int32":
                    addNumberComparator();
                    break;
            }
            PropertyValueTextBox.IsEnabled = true;
        }

        // Adds items to the comparator ComboBox which are based on int32 or double
        private void addNumberComparator()
        {
            ComparatorComboBox.Items.Clear();
            ComparatorComboBox.Items.Add(">");
            ComparatorComboBox.Items.Add("<");
            ComparatorComboBox.Items.Add("=");
        }

        /// <summary>
        /// Event handler that disables and resets all ui elements when the propertie selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onPropertyNameComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            PropertyValueTextBox.IsEnabled = false;
            ComparatorComboBox.Text="";
            PropertyValueTextBox.Text = "";
        }

        /// <summary>
        /// Event handler that enables the PropertyValueTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onComparatorBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            PropertyValueTextBox.IsEnabled = true;
        }

        /// <summary>
        /// Event handler that removes this entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _parent.removeEntryFromWrapper(this);
        }
    }
}
