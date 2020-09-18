using System;
using System.Collections.Generic;
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
        public PropertyEntry(string labelName)
        {
            Binding propertyBinding = new Binding(); 
            propertyBinding.Source = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(PersonView), 1);
            propertyBinding.Path = new PropertyPath("PersonToEdit.Name");
            propertyBinding.Mode = BindingMode.TwoWay;
            InitializeComponent();
            //this.PropertyLabel.Content = labelName;
            this.PropertyInput.SetBinding(TextBlock.TextProperty, propertyBinding); // <--- How do I apply this binding to my Input
        }
    }
}
