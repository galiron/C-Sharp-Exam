using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
