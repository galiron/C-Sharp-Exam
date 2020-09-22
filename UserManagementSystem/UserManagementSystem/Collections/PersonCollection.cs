using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Models;

namespace UserManagementSystem.Collections
{
    /// <summary>
    /// Collection with events to handle changes to the state
    /// </summary>
    class PersonCollection : Collection<Person>
    {

        static public event Action personAdded;
        static public event Action personSet;
        static public event Action personremoved;

        public PersonCollection()
        {
        }

        // overriden insert with additional error handling
        protected override void InsertItem(int index, Person item)
        {
            if (item != null)
            {
                base.InsertItem(index,item);
                if (personAdded != null)
                {
                    personAdded();
                }
            }
            else
            {
                Console.WriteLine("Item is null, could not insert item at index: {0}", index);
            }
        }

        // overriden  set with additional error handling
        protected override void SetItem(int index, Person item)
        {
            if (item != null)
            {
                base.SetItem(index, item);
                if (personAdded != null)
                {
                    personSet();
                }
            }
            else
            {
                Console.WriteLine("Item is null, could not set item at index: {0}", index);
            }
        }

        // overriden remove with additional error handling
        protected override void RemoveItem(int index)
        {
                base.RemoveItem(index);
                if (personAdded != null)
                {
                    personremoved();
                }
        }
    }
}
