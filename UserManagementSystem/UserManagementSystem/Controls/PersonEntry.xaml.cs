using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using UserManagementSystem.Models;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Interaktionslogik für PersonEntry.xaml
    /// </summary>
    public partial class PersonEntry : UserControl
    {
        private Person _person;
        private PersonOverview _parent;
        public PersonEntry(Person person, PersonOverview parent)
        {
            _parent = parent;
            _person = person;
            InitializeComponent();
            this.userLabel.Content =_person.Name + " " +_person.SurName;
            this.viewUserButton.Content = "View User";
        }

        private void viewUser(object sender, RoutedEventArgs e)
        {
            PersonView personView = new PersonView(_person);
            personView.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            _parent.removeEntry(this);
            AppState.Persons.Remove(_person);
        }
    }
}
