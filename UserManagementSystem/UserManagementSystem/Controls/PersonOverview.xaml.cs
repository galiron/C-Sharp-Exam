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
    /// Interaktionslogik für PersonOverview.xaml
    /// </summary>
    public partial class PersonOverview : UserControl
    {
        private Window personView;

        public PersonOverview()
        {
            FilterOverview.filtersClosed += updatePersonTable;
            PersonCollection.personAdded += updatePersonTable;
            PersonCollection.personSet += updatePersonTable;
            PersonCollection.personremoved += updatePersonTable;
            InitializeComponent();
            
        }

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
                    }
                }
            });
            bool isInvalid = filtersCouldBeApplied.Contains(false);
            if (isInvalid)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            Person newPerson = (Person) ClassGenerator.CreatePersonClassFromString("Person");
            personView = new PersonView(newPerson);
            personView.Show();
        }

        private void importUsers_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            string dataFromJsonFile = File.ReadAllText(@"./../storage.json");
            AppState.Persons = JsonConvert.DeserializeObject<PersonCollection>(dataFromJsonFile, settings);
            updatePersonTable();

        }

        private void exportUsers_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
        };
            string serializedJson = JsonConvert.SerializeObject(AppState.Persons, settings);
            Console.WriteLine(serializedJson);
            string path = @"./../storage.json";
            File.WriteAllText(@"./../storage.json", serializedJson);
        }

        private void manageFilter_Click(object sender, RoutedEventArgs e)
        {
            Window filterOverview = new FilterOverview();
            filterOverview.Show();
        }

        public void removeEntry(PersonEntry personEntry)
        {
            this.PersonTable.Children.Remove(personEntry);
        }
    }
}
