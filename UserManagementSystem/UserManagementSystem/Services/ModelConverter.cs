using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Models;

namespace UserManagementSystem.Services
{
    class ModelConverter
    {
        public ModelConverter()
        {

        }

        public static T convertSourceToTargetModel<S, T>(S source, T target) where S : Person where T : Person
        {
            PropertyInfo[] sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] targetProperties = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Todo: Maybe check if name instead of instance has to be chekced
            PropertyInfo[] sharedProperties = sourceProperties.Where(sourceProperty
                => targetProperties.Select(p => p.Name).Contains(sourceProperty.Name)).ToArray();
            sharedProperties.ToList().ForEach(propertie => propertie.SetValue(target,propertie.GetValue(source)));
            return target;
        }
    }
}
