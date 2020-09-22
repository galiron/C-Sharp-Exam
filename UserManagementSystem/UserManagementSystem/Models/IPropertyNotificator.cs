using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    /// <summary>
    /// Class to make Person able to notify changes (relevant for Two-Way Databinding and update cycle)
    /// </summary>
    public class IPropertyNotificator: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Implemented interface function of INotifyPropertyChanged to react on property changes
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Generic function to set property fields considering the INotifyPropertyChanged interface for DataBinding
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">reference to field name</param>
        /// <param name="value">value to be set</param>
        /// <param name="propertyName">propertyName which manages the field</param>
        protected void SetField<T>(ref T field, T value, string propertyName)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }
    }
}
