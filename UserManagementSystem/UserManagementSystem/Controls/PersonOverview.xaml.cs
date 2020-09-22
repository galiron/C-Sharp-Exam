using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Xml;
using Newtonsoft.Json;
using UserManagementSystem.Collections;
using UserManagementSystem.Generators;
using UserManagementSystem.Models;
using Formatting = Newtonsoft.Json.Formatting;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// Logic for the PersonOverview.xaml
    /// </summary>
    public partial class PersonOverview : UserControl
    {
        private Window _personView;

        /// <summary>
        /// Register event handlers to keep the overview's PersonEntries updated
        /// </summary>
        public PersonOverview()
        {
            FilterOverview.filtersClosed += updatePersonTable;
            PersonCollection.personAdded += updatePersonTable;
            PersonCollection.personSet += updatePersonTable;
            PersonCollection.personremoved += updatePersonTable;
            InitializeComponent();
            
        }

        /// <summary>
        /// Reloads all PersonEntries from the AppState
        /// </summary>
        public void updatePersonTable()
        {
            List<Filter> filters = AppState.Filters;
            PersonTable.Children.RemoveRange(0,PersonTable.Children.Count);
            AppState.Persons.ToList().ForEach(p =>
            {
                if(filterCanBeApplied(p,filters))
                PersonTable.Children.Add(new PersonEntry(p, this));
            });
        }

        /// <summary>
        /// Validates if a person passes all set filters
        /// </summary>
        /// <param name="personToFilter">Person to be validated</param>
        /// <param name="allFilters">Filters to be applied</param>
        /// <returns></returns>
        private bool filterCanBeApplied(Person personToFilter, List<Filter> allFilters)
        {
            PropertyInfo[] propertiesOfPerson = personToFilter.GetType().GetProperties();
            List<bool> filtersCouldBeApplied = new List<bool>();
            allFilters.ForEach( filter => {
                PropertyInfo match = propertiesOfPerson.Where(prop => prop.Name == filter.PropertyName).FirstOrDefault();
                if (match == null)
                {
                    filtersCouldBeApplied.Add(false);
                }
                else
                {
                    switch (filter.Comparator)
                    {
                        case ">":
                            try
                            {
                                filtersCouldBeApplied.Add(Convert.ToDouble(match.GetValue(personToFilter)) >
                                                          Convert.ToDouble((filter.ValueToCompare)));
                            }
                            catch (Exception e)
                            {
                                filtersCouldBeApplied.Add(true);
                            }

                            break;
                        case "<":
                            try
                            {
                                filtersCouldBeApplied.Add(Convert.ToDouble(match.GetValue(personToFilter)) <
                                                          Convert.ToDouble((filter.ValueToCompare)));
                            }
                            catch (Exception e)
                            {
                                filtersCouldBeApplied.Add(true);
                            };
                            break;
                        case "=":
                            try
                            {
                                filtersCouldBeApplied.Add(Convert.ToDouble(match.GetValue(personToFilter)) ==
                                                          Convert.ToDouble((filter.ValueToCompare)));
                            }
                            catch (Exception e)
                            {
                                filtersCouldBeApplied.Add(true);
                            }
                            break;
                        case "Contains":
                            filtersCouldBeApplied.Add(match.GetValue(personToFilter).ToString().Contains(Convert.ToString(filter.ValueToCompare)));
                            break;
                        default:
                            throw new ArgumentException("not allowed filter argument detected");
                            break;
                    }
                }
            });

            // special filter which does not comply to the Filter model
            Person targetType = (Person) ClassGenerator.CreatePersonClassFromString(AppState.PersonTypeFilter);
            bool isTypeOf = targetType.GetType().IsAssignableFrom(personToFilter.GetType());
            filtersCouldBeApplied.Add(isTypeOf);

            bool isValid = filtersCouldBeApplied.Contains(false);
            if (isValid)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// event handler to create a new Person instance and delegate it to the PersonView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            Person newPerson = (Person) ClassGenerator.CreatePersonClassFromString("Person");
            _personView = new PersonView(newPerson);
            _personView.Show();
        }

        /// <summary>
        /// Event handler to import users from a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importUsers_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            try
            {
                string dataFromJsonFile = File.ReadAllText(@"./../../Data/storage.json");
                AppState.Persons = JsonConvert.DeserializeObject<PersonCollection>(dataFromJsonFile, settings);
                updatePersonTable();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        /// <summary>
        /// Event handler to export users to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportUsers_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
        };
            string serializedJson = JsonConvert.SerializeObject(AppState.Persons, settings);
            string path = @"./../../Data/storage.json";
            File.WriteAllText(@"./../../Data/storage.json", serializedJson);
        }

        /// <summary>
        /// Event handler that opens the FilterOverview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manageFilter_Click(object sender, RoutedEventArgs e)
        {
            Window filterOverview = new FilterOverview();
            filterOverview.Show();
        }

        /// <summary>
        /// Function to remove an Entry from the PersonOverview
        /// </summary>
        /// <param name="personEntry"></param>
        public void removeEntry(PersonEntry personEntry)
        {
            this.PersonTable.Children.Remove(personEntry);
        }
    }
}
