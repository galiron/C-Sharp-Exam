﻿using MoreLinq;
using System;
using System.Collections.Generic;
using System.Windows;
using UserManagementSystem.Collections;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controls
{
    /// <summary>
    /// UI logic for the FilterOverview.xaml that handles all the events
    /// </summary>
    public partial class FilterOverview : Window
    {
        static public event Action filtersClosed;
        private List<Filter> filters = new List<Filter>();

        /// <summary>
        /// initializes all UI elements, Filters and the unique personTypeFilter
        /// </summary>
        public FilterOverview()
        {
            InitializeComponent();
            initFilters();
            initPersonTypeFilter();
        }

        /// <summary>
        /// Retrieves all user available model types
        /// </summary>
        private void initPersonTypeFilter()
        {
            PersonModelDictionary.personClassDictionary.ForEach(keyValue =>
                TypeSelection.Items.Add(keyValue.Key));
            TypeSelection.SelectedItem = AppState.PersonTypeFilter;
        }

        /// <summary>
        /// Adds a new entry for each Filter that needs to be managed
        /// </summary>
        private void initFilters()
        {
            AppState.Filters.ForEach(filter =>
            {
                filtersPanel.Children.Add(new FilterEntry(filter,this));
            });
        }

        /// <summary>
        /// Event handler which adds a new Filter Entry to the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFilter_Click(object sender, RoutedEventArgs e)
        { Filter newFilter = new Filter();
            filtersPanel.Children.Add(new FilterEntry(newFilter, this));
            filters.Add(newFilter);
        }

        /// <summary>
        /// Event handler that saves all filter options to the AppState
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            filters.Clear();
            
            foreach (FilterEntry child in filtersPanel.Children)
            {
                Filter filter = new Filter
                {
                    ValueToCompare = child.PropertyValueTextBox.Text,
                    PropertyName = child.PropertyNameComboBox.Text,
                    Comparator = child.ComparatorComboBox.Text
                };
                filters.Add(filter);
            }
            
            AppState.Filters = filters;
            AppState.PersonTypeFilter = TypeSelection.SelectedItem.ToString();
            closeWindow();
        }

        /// <summary>
        /// Event handler that raises an filterClosed event and closes the current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void closeWindow()
        {
            filtersClosed();
            this.Close();
        }

        /// <summary>
        /// Removes a filterEntry entry from the overview
        /// </summary>
        /// <param name="itemToRemove">The FilterEntry to be removed</param>
        public void removeEntryFromWrapper(UIElement itemToRemove)
        {
            this.filtersPanel.Children.Remove(itemToRemove);
        }
    }
}
