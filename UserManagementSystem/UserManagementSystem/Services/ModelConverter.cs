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
            PropertyInfo[] sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] targetProperties = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Todo: Maybe check if name instead of instance has to be chekced
            PropertyInfo[] sharedProperties = sourceProperties.Where(sourceProperty
                => targetProperties.Select(p => p.Name).Contains(sourceProperty.Name)).ToArray();
            sharedProperties.ToList().ForEach(propertie => propertie.SetValue(target,propertie.GetValue(source)));

            //target = (T)Activator.CreateInstance(typeof(T), sharedProperties.ToList());
               //target.GetType().GetProperties().Single(prop => prop.Name == "Name").SetValue(sourceProperties[0].GetValue());
            return target;
        }
    }
}
