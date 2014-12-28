#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using PortableExtensions;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Class managing the application configuration.
    /// </summary>
    public class DiceRollerConfigurationManager
    {
        #region Fields

        /// <summary>
        ///     Object used to synchronize threads.
        /// </summary>
        private readonly Object _syncRoot = new Object();

        /// <summary>
        ///     The configuration.
        /// </summary>
        private DiceRollerConfiguration _configuration;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the configuration of the DiceRoller application.
        /// </summary>
        public DiceRollerConfiguration Configuration
        {
            get
            {
                lock ( _syncRoot )
                {
                    return _configuration ?? ( _configuration = LoadConfiguration() );
                }
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        ///     Loads the configuration.
        /// </summary>
        /// <returns>Returns the configuration.</returns>
        private DiceRollerConfiguration LoadConfiguration()
        {
            if ( File.Exists( Consts.ConfigurationFilePath ) )
                try
                {
                    return
                        JsonConvert.DeserializeObject<DiceRollerConfiguration>( File.ReadAllText( Consts.ConfigurationFilePath ) );
                }
                catch ( Exception ex )
                {
                    OutputHelper.PrintError( "Failed to read configuration:{0}{1}".F( Environment.NewLine, ex.Message ) );
                }

            var configuration = new DiceRollerConfiguration();
            RestoreDefault( configuration );
            return configuration;
        }

        /// <summary>
        ///     Saves the current configuration.
        /// </summary>
        /// <returns>Returns a value of true if the configuration was saved successfully; otherwise false.</returns>
        private Boolean SaveConfiguration()
        {
            try
            {
                //Check if configuration was possible modified.
                if ( _configuration == null && File.Exists( Consts.ConfigurationFilePath ) )
                    return true;

                var json = JsonConvert.SerializeObject( Configuration );
                lock ( _syncRoot )
                {
                    File.WriteAllText( Consts.ConfigurationFilePath, json );
                }
                return true;
            }
            catch ( Exception ex )
            {
                OutputHelper.PrintError( "Failed to save configuration:{0}{1}".F( Environment.NewLine, ex.Message ) );
                return false;
            }
        }

        /// <summary>
        ///     Restores the default settings.
        /// </summary>
        /// <param name="configuration">The configuration to restore.</param>
        private void RestoreDefault( DiceRollerConfiguration configuration )
        {
            configuration.ResultColor = ConsoleColor.Green;
            configuration.ErrorColor = ConsoleColor.Red;
            configuration.Profiles = new Dictionary<String, String>();
        }

        #endregion

        #region Public Members

        /// <summary>
        ///     Restores the default settings.
        /// </summary>
        /// <returns>Returns a value of true if the configuration was restored successfully; otherwise false.</returns>
        public Boolean RestoreDefault()
        {
            RestoreDefault( Configuration );
            return SaveConfiguration();
        }

        /// <summary>
        ///     Gets the value of the property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>Returns the value of the property with the given name as string, or null if the property does not exist.</returns>
        public String GetProperty( String propertyName )
        {
            var property = Configuration.GetType()
                                        .GetProperty( propertyName,
                                                      BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance );

            if ( property == null )
            {
                OutputHelper.PrintError( "Property '{0}' does not exist".F( propertyName ) );
                return null;
            }

            var value = property.GetValue( this, null );
            return value == null ? "<null>" : value.ToString();
        }

        /// <summary>
        ///     Sets the property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The new value.</param>
        /// <returns>Returns a value of true if the property was set successfully; otherwise false.</returns>
        public Boolean SetProperty( String propertyName, String value )
        {
            //TODO: Improve 
            if ( propertyName.CompareOrdinalIgnoreCase( Configuration.GetName( x => x.ErrorColor ) ) )
            {
                ConsoleColor color;
                if ( Enum.TryParse( value, out color ) )
                {
                    Configuration.ErrorColor = color;
                    return SaveConfiguration();
                }
                OutputHelper.PrintError( "Invalid value for type: {0}".F( typeof (ConsoleColor).Name ) );
                return false;
            }
            if ( propertyName.CompareOrdinalIgnoreCase( Configuration.GetName( x => x.ResultColor ) ) )
            {
                ConsoleColor color;
                if ( Enum.TryParse( value, out color ) )
                {
                    Configuration.ResultColor = color;
                    return SaveConfiguration();
                }
                OutputHelper.PrintError( "Invalid value for type: {0}".F( typeof (ConsoleColor).Name ) );
                return false;
            }

            OutputHelper.PrintError( "Property '{0}' does not exist".F( propertyName ) );
            return false;
        }

        /// <summary>
        ///     Gets the profile with the given name.
        /// </summary>
        /// <param name="profileName">The name of the profile.</param>
        /// <returns>Returns the profile with the given name, or null if the profile does not exist.</returns>
        public String GetProfile( String profileName )
        {
            if ( Configuration.Profiles.ContainsKey( profileName ) )
                return Configuration.Profiles[profileName];

            OutputHelper.PrintError( "Profile '{0}' does not exist.".F( profileName ) );
            return null;
        }

        /// <summary>
        ///     Removes the profile with the given name.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Profile does not exist.</exception>
        /// <param name="profileName">The name of the profile.</param>
        /// <returns>Returns a value of true if the profile was removed successfully; otherwise false.</returns>
        public Boolean RemoveProfile( String profileName )
        {
            if ( !Configuration.Profiles.ContainsKey( profileName ) )
            {
                OutputHelper.PrintError( "Profile '{0}' does not exist.".F( profileName ) );
                return false;
            }

            Configuration.Profiles.Remove( profileName );
            return SaveConfiguration();
        }

        /// <summary>
        ///     Adds the given profile.
        /// </summary>
        /// <exception cref="ArgumentException">Profile with same name already exists.</exception>
        /// <param name="profileName">The name of the profile.</param>
        /// <param name="profile">The profile to add.</param>
        /// <returns>Returns a value of true if the profile was added successfully; otherwise false.</returns>
        public Boolean AddProfile( String profileName, String profile )
        {
            if ( Configuration.Profiles.ContainsKey( profileName ) )
            {
                OutputHelper.PrintError( "Profile with name '{0}' already exists.".F( profileName ) );
                return false;
            }

            //TODO: CHECK if profile is valid.
            Configuration.Profiles.Add( profileName, profile );
            return SaveConfiguration();
        }

        /// <summary>
        ///     Edits the profile with the given name.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Profile does not exist.</exception>
        /// <param name="profileName">The name of the profile.</param>
        /// <param name="profile">The new profile value.</param>
        /// <returns>Returns a value of true if the profile was edited successfully; otherwise false.</returns>
        public Boolean EditProfile( String profileName, String profile )
        {
            if ( !Configuration.Profiles.ContainsKey( profileName ) )
            {
                OutputHelper.PrintError( "Profile '{0}' does not exist.".F( profileName ) );
                return false;
            }

            //TODO: CHECK if profile is valid.
            Configuration.Profiles[profileName] = profile;
            return SaveConfiguration();
        }

        #endregion
    }
}