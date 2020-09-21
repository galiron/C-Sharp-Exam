using System;
using System.Collections.Generic;
using System.Linq;
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
using UserManagementSystem.Models;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Interaktionslogik für FilterOverview.xaml
    /// </summary>
    public partial class FilterOverview : Window
    {
        static public event Action filtersClosed;
        private List<Filter> filters = new List<Filter>();
        public FilterOverview()
        {
            InitializeComponent();
            initFilters();
        }

        private void initFilters()
        {
            AppState.Filters.ForEach(filter =>
            {
                filtersPanel.Children.Add(new FilterEntry(filter));
            });
        }

        private void AddFilter_Click(object sender, RoutedEventArgs e)
        { Filter newFilter = new Filter();
            filtersPanel.Children.Add(new FilterEntry(newFilter));
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
            filtersClosed();
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            filtersClosed();
            this.Close();
        }
    }
}
