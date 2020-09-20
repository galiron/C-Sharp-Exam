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
    /// Interaktionslogik für PropertyEntry.xaml
    /// </summary>
    public partial class PropertyEntry : UserControl
    {
        private Binding propertyBinding;
        public PropertyEntry(Person dataSourceToBind, string propertyName)
        {
            DataContext = dataSourceToBind;
            propertyBinding = new Binding();
            InitializeComponent();
            propertyBinding.Source = dataSourceToBind;
            propertyBinding.Path = new PropertyPath(propertyName);
            propertyBinding.Mode = BindingMode.TwoWay;
            propertyBinding.UpdateSourceTrigger = UpdateSourceTrigger.Default;
            BindingOperations.SetBinding(PropertyInput, TextBox.TextProperty, propertyBinding);
            this.PropertyLabel.Content = propertyName;
            dataSourceToBind.OnPropertyChanged(propertyName);
        }
    }
}
