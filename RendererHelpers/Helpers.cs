using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace RendererHelpers
{
    public static class Helpers
    {
        /// <summary>
        /// Manages the property changes for custom renderers and effects
        /// </summary>
        /// <param name="e">The PropertyChangedEventArgs parameter for the ElementPropertyChanged event for the render</param>
        /// <param name="type">The type of the view being renderered</param>
        /// <param name="factors">Factors that contains an action for every bindable property that fires when the bindable property changes</param>
        /// <param name="globalUpdater">A global updater fires after each factor updater</param>
        public static void HandlePropertyChanges<T>(PropertyChangedEventArgs e, Dictionary<BindableProperty, Action> factors, Action globalUpdater = null)
        {
            //Get all the bindable properties for the given type in order to compare them with the provided bindable properties
            var bindableFields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            List<BindableProperty> bindabels = new List<BindableProperty>();
            foreach (var field in bindableFields)
            {
                bindabels.Add((BindableProperty)field.GetValue(field));
            }

            //Compare type's bindable properties with the given bindable properties
            foreach (var bindableProp in factors.Keys)
            {
                if (!bindabels
                    .Contains(bindableProp))
                {
                    throw new ArgumentException("All BindableProperty arguments must belong to the provided type");
                }
            }

            //This dictionary stores each given bindable property with it's name in order to get it's relative Action 
            //e.PropertyName = "Foo"
            //*Gets the property with the name 'Foo'*
            //*Gets the Action with it's property key*

            var propertyByNames = new Dictionary<string, BindableProperty>();
            foreach (var bp in factors.Keys)
                propertyByNames.Add(bp.PropertyName, bp);


            //If the changed property is registered to fire a specific Action, then fire it's action and fire the global Action if exists
            if (factors.Keys.Select(k => k.PropertyName).Contains(e.PropertyName))
            {
                //The action of the property been changed
                var action = factors[propertyByNames[e.PropertyName]];

                if (action != null)
                {
                    action.Invoke();

                    //Fires the global updated if exists
                    if (globalUpdater != null)
                        globalUpdater.Invoke();
                }
            }
        }
        }
    }
