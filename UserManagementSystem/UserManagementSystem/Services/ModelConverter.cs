using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Services
{
    class ModelConverter
    {
        public ModelConverter()
        {

        }

        public static T convertSourceToTargetModel<S, T>(S source, T target)
        {
            Console.WriteLine(source.GetType());
            PropertyInfo[] sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] targetProperties = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine(targetProperties[0] == sourceProperties[0]);
            PropertyInfo[] sharedProperties = sourceProperties.Where(sourceProperty
                => targetProperties.Contains(sourceProperty)).ToArray();
            sharedProperties.ToList().ForEach(propertie => propertie.SetValue(target,propertie.GetValue(source)));
            Console.WriteLine(target);

            //target = (T)Activator.CreateInstance(typeof(T), sharedProperties.ToList());
               //target.GetType().GetProperties().Single(prop => prop.Name == "Name").SetValue(sourceProperties[0].GetValue());
            return target;
        }
    }
}
