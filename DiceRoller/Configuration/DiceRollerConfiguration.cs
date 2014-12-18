#region Usings

using System;
using System.Collections.Generic;
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

        /// <summary>
        ///     Gets or sets the defined profiles.
        /// </summary>
        /// <value>The profiles.</value>
        public Dictionary<String, String> Profiles { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Restores the default settings.
        /// </summary>
        public void RestoreDefault()
        {
            ResultColor = ConsoleColor.Green;
            ErrorColor = ConsoleColor.Red;
            Profiles = new Dictionary<String, String>();
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
                .GetProperty( propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance );

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
        public void SetProperty( String propertyName, String value )
        {
            if ( propertyName.CompareOrdinalIgnoreCase( this.GetName( () => ErrorColor ) ) )
            {
                ConsoleColor color;
                if ( Enum.TryParse( value, out color ) )
                {
                    ErrorColor = color;
                    return;
                }
                OutputHelper.PrintError( "Invalid value for type: {0}".F( typeof (ConsoleColor).Name ) );
            }
            else if ( propertyName.CompareOrdinalIgnoreCase( this.GetName( () => ResultColor ) ) )
            {
                ConsoleColor color;
                if ( Enum.TryParse( value, out color ) )
                {
                    ResultColor = color;
                    return;
                }
                OutputHelper.PrintError( "Invalid value for type: {0}".F( typeof (ConsoleColor).Name ) );
            }

            throw new ArgumentException( "Property does not exist.", propertyName.GetName( () => propertyName ) );
        }

        /// <summary>
        ///     Gets the profile with the given name.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Profile does not exist.</exception>
        /// <param name="profileName">The name of the profile.</param>
        /// <returns>Returns the profile with the given name.</returns>
        public String GetProfile( String profileName )
        {
            if ( Profiles.ContainsKey( profileName ) )
                return Profiles[profileName];

            throw new IndexOutOfRangeException( "Profile '{0}' does not exist.".F( profileName ) );
        }

        /// <summary>
        ///     Removes the profile with the given name.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Profile does not exist.</exception>
        /// <param name="profileName">The name of the profile.</param>
        public void RemoveProfile( String profileName )
        {
            if ( Profiles.ContainsKey( profileName ) )
                Profiles.Remove( profileName );

            throw new IndexOutOfRangeException( "Profile '{0}' does not exist.".F( profileName ) );
        }

        /// <summary>
        ///     Adds the given profile.
        /// </summary>
        /// <exception cref="ArgumentException">Profile with same name already exists.</exception>
        /// <param name="profileName">The name of the profile.</param>
        /// <param name="profile">The profile to add.</param>
        public void AddProfile( String profileName, String profile )
        {
            if ( Profiles.ContainsKey( profileName ) )
                throw new ArgumentException( "Profile with same name already exists.", profileName.GetName( () => profile ) );

            //TODO: CHECK if profile is valid.
            Profiles.Add( profileName, profile );
        }

        /// <summary>
        ///     Edits the profile with the given name.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Profile does not exist.</exception>
        /// <param name="profileName">The name of the profile.</param>
        /// <param name="profile">The new profile value.</param>
        public void EditProfile(String profileName, String profile)
        {
            if (!Profiles.ContainsKey(profileName))
                throw new IndexOutOfRangeException("Profile does not exist.");

            //TODO: CHECK if profile is valid.
            Profiles[profileName] = profile;
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