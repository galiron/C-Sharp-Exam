using System.Windows;
using System.Windows.Controls;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Logic for the PersonEntry.xaml
    /// </summary>
    public partial class PersonEntry : UserControl
    {
        private Person _person;
        private PersonOverview _parent;

        /// <summary>
        /// Initialization for all PersonEntry UI Elements
        /// </summary>
        /// <param name="person">Person that this entry represents</param>
        /// <param name="parent">Parent</param>
        public PersonEntry(Person person, PersonOverview parent)
        {
            _parent = parent;
            _person = person;
            InitializeComponent();
            this.userLabel.Content =_person.Name + " " +_person.SurName;
            this.viewUserButton.Content = "View User";
        }

        /// <summary>
        /// Event handler to call the PersonView with the entries person
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewUser(object sender, RoutedEventArgs e)
        {
            PersonView personView = new PersonView(_person);
            personView.Show();
        }

        /// <summary>
        /// Event handler to remove this entry and it's person
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            AppState.Persons.Remove(_person);
            _parent.removeEntry(this);
        }
    }
}
