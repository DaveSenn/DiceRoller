#region Usings

using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PortableExtensions;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Class representing the configuration of the DiceRoller application.
    /// </summary>
    public class DiceRollerConfiguration
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the result color.
        /// </summary>
        /// <value>The result color.</value>
        [JsonConverter( typeof (StringEnumConverter) )]
        public ConsoleColor ResultColor { get; set; }

        /// <summary>
        ///     Gets or sets the error color.
        /// </summary>
        /// <value>The error color.</value>
        [JsonConverter( typeof (StringEnumConverter) )]
        public ConsoleColor ErrorColor { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Restores the default settings.
        /// </summary>
        public void RestoreDefault()
        {
            ResultColor = ConsoleColor.Green;
            ErrorColor = ConsoleColor.Red;
        }

        /// <summary>
        ///     Gets the value of the property with the given name.
        /// </summary>
        /// <exception cref="ArgumentException">Configuration property does not exist.</exception>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>Returns the value of the property with the given name.</returns>
        public String GetProperty( String propertyName )
        {
            var property = GetType()
                .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if ( property == null )
                throw new ArgumentException( "Configuration property does not exist.", propertyName.GetName( () => propertyName ) );

            var value = property.GetValue( this, null );
            return value == null ? "<null>" : value.ToString();
        }

        /// <summary>
        ///     Sets the property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The new value.</param>
        public void SetProperty(String propertyName, String value)
        {
            if ( propertyName.CompareOrdinalIgnoreCase( this.GetName( () => ErrorColor ) ) )
            {
                ConsoleColor color;
                if ( Enum.TryParse( value, out color ) )
                {
                    ErrorColor = color;
                    return;
                }
                Console.Error.WriteLine( "Invalid value for type: {0}", typeof (ConsoleColor).Name );
            }
            else if ( propertyName.CompareOrdinalIgnoreCase( this.GetName( () => ResultColor ) ) )
            {
                ConsoleColor color;
                if ( Enum.TryParse( value, out color ) )
                {
                    ResultColor = color;
                    return;
                }
                Console.Error.WriteLine( "Invalid value for type: {0}", typeof (ConsoleColor).Name );
            }
            
            throw new ArgumentException("Property does not exist.", propertyName.GetName(() => propertyName));
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override String ToString()
        {
            return JsonConvert.SerializeObject( this, Formatting.Indented );
        }

        #endregion
    }
}