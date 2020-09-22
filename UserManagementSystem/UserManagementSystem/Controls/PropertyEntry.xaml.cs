using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Logic for the PropertyEntry.xaml
    /// </summary>
    public partial class PropertyEntry : UserControl
    {
        private Binding _propertyBinding;

        /// <summary>
        /// Initialization of UI Elements and Databinding 
        /// </summary>
        /// <param name="dataSourceToBind"></param>
        /// <param name="propertyName"></param>
        public PropertyEntry(Person dataSourceToBind, string propertyName)
        {
            DataContext = dataSourceToBind;
            _propertyBinding = new Binding();
            InitializeComponent();
            _propertyBinding.Source = dataSourceToBind;
            _propertyBinding.Path = new PropertyPath(propertyName);
            _propertyBinding.Mode = BindingMode.TwoWay;
            _propertyBinding.UpdateSourceTrigger = UpdateSourceTrigger.Default;
            BindingOperations.SetBinding(PropertyInput, TextBox.TextProperty, _propertyBinding);
            this.PropertyLabel.Content = propertyName;
            dataSourceToBind.OnPropertyChanged(propertyName);
        }
    }
}
