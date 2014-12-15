using System;
using System.IO;
using Newtonsoft.Json;

namespace DiceRoller
{
    /// <summary>
    ///     Class managing the application configuration.
    /// </summary>
    public static class DiceRollerConfigurationManager
    {
        #region Fields

        /// <summary>
        ///     The configuration.
        /// </summary>
        private static DiceRollerConfiguration _configuration;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the configuration of the DiceRoller application.
        /// </summary>
        public static DiceRollerConfiguration Configuration
        {
            get { return _configuration ?? ( _configuration = LoadConfiguration() ); }
        }

        #endregion

        #region Private Members

        /// <summary>
        ///     Loads the configuration.
        /// </summary>
        /// <returns>Returns the configuration.</returns>
        private static DiceRollerConfiguration LoadConfiguration()
        {
            if ( File.Exists( Consts.ConfigurationFilePath ) )
            {
                try
                {
                    return
                        JsonConvert.DeserializeObject<DiceRollerConfiguration>( File.ReadAllText( Consts.ConfigurationFilePath ) );
                }
                catch ( Exception ex )
                {
                    Console.Error.WriteLine( "Failed to read configuration:" );
                    Console.Error.WriteLine( ex );
                }
            }

            var configuration = new DiceRollerConfiguration();
            configuration.RestoreDefault();
            return configuration;
        }

        /// <summary>
        ///     Saves the current configuration.
        /// </summary>
        private static void SaveConfiguration()
        {
            try
            {
                //Check if configuration can have changed.
                if ( _configuration == null && File.Exists( Consts.ConfigurationFilePath ) )
                    return;

                var json = JsonConvert.SerializeObject( Configuration );
                File.WriteAllText( Consts.ConfigurationFilePath, json );
            }
            catch ( Exception ex )
            {
                Console.Error.WriteLine( "Failed to save configuration:" );
                Console.Error.WriteLine( ex );
            }
        }

        #endregion
    }
}