﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
            PersonCollection.personAdded += updatePersonTable;
            PersonCollection.personSet += updatePersonTable;
            PersonCollection.personremoved += updatePersonTable;
            InitializeComponent();
            
        }

        public void updatePersonTable()
        {
            PersonTable.Children.RemoveRange(0,PersonTable.Children.Count);
            AppState.Persons.ToList().ForEach(p => PersonTable.Children.Add(new PersonEntry(p)));
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
    }
}
