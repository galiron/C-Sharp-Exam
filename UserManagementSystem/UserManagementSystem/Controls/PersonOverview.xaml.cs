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
using UserManagementSystem.Collections;
using UserManagementSystem.Generators;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Interaktionslogik für PersonOverview.xaml
    /// </summary>
    public partial class PersonOverview : UserControl
    {
        private WrapPanel personTable = new WrapPanel();
        private Window personView;

        public PersonOverview()
        {
            PersonCollection.personAdded += updatePersonTable;
            PersonCollection.personSet += updatePersonTable;
            PersonCollection.personremoved += updatePersonTable;
            InitializeComponent();
        }

        public void updatePersonTable()
        {
            PersonTable.Children.RemoveRange(0,PersonTable.Children.Count);
            AppState.Persons.ToList().ForEach(p => this.PersonTable.Children.Add(new PersonEntry(p.Name + " " + p.SurName,"View User")));
        }

        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            Person newPerson = (Person) ClassGenerator.CreatePersonClassFromString("Person");
            personView = new PersonView(newPerson);
            personView.Show();
        }
    }
}
