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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MoreLinq;
using UserManagementSystem.Collections;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Interaktionslogik für FilterOverview.xaml
    /// </summary>
    public partial class FilterOverview : Window
    {
        static public event Action filtersClosed;
        private string _typeFilter;
        private List<Filter> filters = new List<Filter>();
        public FilterOverview()
        {
            InitializeComponent();
            initFilters();
            initPersonTypeFilter();
        }

        private void initPersonTypeFilter()
        {
            PersonModelDictionary.personClassDictionary.ForEach(keyValue =>
                TypeSelection.Items.Add(keyValue.Key));
            TypeSelection.SelectedItem = AppState.PersonTypeFilter;
        }

        private void initFilters()
        {
            AppState.Filters.ForEach(filter =>
            {
                filtersPanel.Children.Add(new FilterEntry(filter,this));
            });
        }

        private void AddFilter_Click(object sender, RoutedEventArgs e)
        { Filter newFilter = new Filter();
            filtersPanel.Children.Add(new FilterEntry(newFilter, this));
            filters.Add(newFilter);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            filters.Clear();
            
            foreach (FilterEntry child in filtersPanel.Children)
            {
                Filter nF = new Filter();
                nF.ValueToCompare = child.PropertyValueTextBox.Text;
                nF.PropertyName = child.PropertyNameComboBox.Text;
                nF.Comparator = child.ComparatorComboBox.Text;
                filters.Add(nF);
            }
            
            AppState.Filters = filters;
            AppState.PersonTypeFilter = TypeSelection.SelectedItem.ToString();
            filtersClosed();
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            filtersClosed();
            this.Close();
        }

        public void removeEntryFromWrapper(UIElement itemToRemove)
        {
            this.filtersPanel.Children.Remove(itemToRemove);
        }

        private void TypeSelection_DropDownClosed(object sender, EventArgs e)
        {
            _typeFilter = TypeSelection.SelectedItem.ToString();
        }
    }
}
