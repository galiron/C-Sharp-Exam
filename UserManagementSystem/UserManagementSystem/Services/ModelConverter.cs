using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Models;

namespace UserManagementSystem.Services
{
    /// <summary>
    /// Static class to convert Model A -> B
    /// </summary>
    class ModelConverter
    {
        public ModelConverter()
        {

        }

        /// <summary>
        /// Transfers all common properties of the source to the target
        /// </summary>
        /// <typeparam name="S">Type containing properties to get converted</typeparam>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="source">Target instance</param>
        /// <param name="target">Source instance</param>
        /// <returns></returns>
        public static T convertSourceToTargetModel<S, T>(S source, T target) where S : Person where T : Person
        {
            PropertyInfo[] sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] targetProperties = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] sharedProperties = sourceProperties.Where(sourceProperty
                => targetProperties.Select(p => p.Name).Contains(sourceProperty.Name)).ToArray();
            sharedProperties.ToList().ForEach(propertie => propertie.SetValue(target,propertie.GetValue(source)));
            return target;
        }
    }
}
