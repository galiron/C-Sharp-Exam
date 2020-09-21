using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    public class Filter : IPropertyNotificator
    {
        private string _propertyName;
        private string _comparator;
        private Object _valueToCompare;

        public Filter()
        {

        }

        public Filter(string comparator, Object valueToCompare)
        {
            this._comparator = comparator;
            this._valueToCompare = valueToCompare;
        }

        public string PropertyName
        {
            get => _propertyName;
            set => SetField(ref _propertyName, value, "PropertyName");
        }

        public string Comparator
        {
            get => _comparator;
            set => SetField(ref _comparator, value, "Comparator");
        }

        public object ValueToCompare
        {
            get => _valueToCompare;
            set => SetField(ref _valueToCompare, value, "ValueToCompare");
        }
    }
}
