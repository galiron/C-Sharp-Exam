using System;
using System.Collections.Generic;
using System.Linq;
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

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Interaktionslogik für PersonEntry.xaml
    /// </summary>
    public partial class PersonEntry : UserControl
    {

        public PersonEntry(string labelContent, string buttonContent)
        {

            InitializeComponent();
            this.userLabel.Content = labelContent;
            this.viewUserButton.Content = buttonContent;
        }

        public PersonEntry()
        {

            InitializeComponent();
            this.userLabel.Content = "dummy";

            this.viewUserButton.Content = "dummy";
        }

    }
}
