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
    /// Interaktionslogik für FilterEntry.xaml
    /// </summary>
    public partial class FilterEntry : UserControl
    {
        private Binding propertyBinding;
        private Binding comparatorBinding;
        private Binding comparatorValueToCompareBinding;
        private PropertyInfo[] allProperties;
        private Type propertyType;
        private Filter newEntry;
        private FilterOverview _parent;



        public FilterEntry()
        {
            InitializeComponent();
            InitializePropertySelection();
            PropertyNameComboBox.SelectionChanged += onPropertyNameComboBoxChanged;
            ComparatorComboBox.SelectionChanged += onComparatorBoxChanged;
        }

        public FilterEntry(Filter dataSourceToBind, FilterOverview parent)
        {
            _parent = parent;
            newEntry = new Filter
            {
                ValueToCompare = dataSourceToBind.ValueToCompare,
                Comparator = dataSourceToBind.Comparator,
                PropertyName = dataSourceToBind.PropertyName
            };
            InitializeComponent();
            InitializePropertySelection();
            PropertyNameComboBox.SelectionChanged += onPropertyNameComboBoxChanged;
            ComparatorComboBox.SelectionChanged += onComparatorBoxChanged;

            propertyBinding = new Binding
            {
                Source = newEntry,
                Path = new PropertyPath("PropertyName"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            comparatorBinding = new Binding
            {
                Source = newEntry,
                Path = new PropertyPath("Comparator"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            comparatorValueToCompareBinding = new Binding
            {
                Source = newEntry,
                Path = new PropertyPath("ValueToCompare"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            BindingOperations.SetBinding(PropertyNameComboBox, ComboBox.TextProperty, propertyBinding);
            BindingOperations.SetBinding(ComparatorComboBox, ComboBox.TextProperty, comparatorBinding);
            BindingOperations.SetBinding(PropertyValueTextBox, TextBox.TextProperty, comparatorValueToCompareBinding);
            dataSourceToBind.OnPropertyChanged("PropertyName");
            dataSourceToBind.OnPropertyChanged("Comparator");
            dataSourceToBind.OnPropertyChanged("ValueToCompare");
        }

        private void InitializePropertySelection()
        {
            allProperties = getPropertiesOfAllPersonModels();
            foreach (PropertyInfo property in allProperties)
            {
                PropertyNameComboBox.Items.Add(property.Name);

            }
        }

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

        private void enableComparator (object sender, SelectionChangedEventArgs e)
        {
            ComparatorComboBox.IsEnabled = true;
            InitializeComparatorValues();
        }

        private void InitializeComparatorValues()
        {
            PropertyInfo selectedProperty = allProperties.First(prop => prop.Name == PropertyNameComboBox.SelectedValue);
            Console.WriteLine(selectedProperty.PropertyType.ToString());
            propertyType = selectedProperty.PropertyType;
            switch (propertyType.ToString())
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

        private void addNumberComparator()
        {
            ComparatorComboBox.Items.Clear();
            ComparatorComboBox.Items.Add(">");
            ComparatorComboBox.Items.Add("<");
            ComparatorComboBox.Items.Add("=");
        }

        private void onPropertyNameComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            PropertyValueTextBox.IsEnabled = false;
            ComparatorComboBox.Text="";
            PropertyValueTextBox.Text = "";
        }

        private void onComparatorBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            PropertyValueTextBox.IsEnabled = true;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _parent.removeEntryFromWrapper(this);
        }
    }
}
