#region Usings

using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PortableExtensions;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Program class of the DiceRoller application.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     The main entry point of the DiceRoller application.
        /// </summary>
        /// <param name="args">The start arguments.</param>
        public static void Main( String[] args )
        {
            var parser = new ArgumentParser();
            try
            {
                parser.ParsArguments( args );
            }
            catch ( Exception ex )
            {
                Console.Write( "Parsing failed:" );
                Console.WriteLine( ex );
                throw;
            }
        }
    }

    /// <summary>
    ///     Class containing the logic to pars the start arguments.
    /// </summary>
    public class ArgumentParser
    {
        /// <summary>
        ///     Parses the start arguments.
        /// </summary>
        /// <param name="args">The start arguments.</param>
        public void ParsArguments( String[] args )
        {
            //Check if arguments are null or empty
            args.ThrowIfNull( () => args );
            if ( args.NotAny() )
                throw new ArgumentException( "No arguments specified" );

            //Check if arguments are targeting configuration.
            if ( args[0] == Consts.ConfigurationSwitch )
            {
                ParsConfiguration( args.Skip( 1 )
                                       .ToArray() );
            }
        }

        /// <summary>
        ///     Parses configuration related start arguments.
        /// </summary>
        /// <param name="args">The start arguments, without the configuration switch.</param>
        private void ParsConfiguration( String[] args )
        {
        }
    }

    /// <summary>
    ///     Class containing all constant values.
    /// </summary>
    public static class Consts
    {
        #region Paths

        public static readonly String ConfigurationFilePath = "DiceRollerConfiguration.json";

        #endregion

        /// <summary>
        ///     Switch for accessing the configuration.
        /// </summary>
        public const String ConfigurationSwitch = "config";

        #region Constructor

        /// <summary>
        ///     Initialize the <see cref="Consts" /> class.
        /// </summary>
        static Consts()
        {
            //Get path to configuration file
            ConfigurationFilePath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ),
                                                  ConfigurationFilePath );
        }

        #endregion
    }

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
        public ConsoleColor ResultColor { get; set; }

        /// <summary>
        ///     Gets or sets the error color.
        /// </summary>
        /// <value>The error color.</value>
        public ConsoleColor ErrorColor { get; set; }

        #endregion

        #region Public Members

        /// <summary>
        ///     Restores the default settings.
        /// </summary>
        public void RestoreDefault()
        {
            ResultColor = ConsoleColor.Green;
            ErrorColor = ConsoleColor.Red;
        }

        #endregion
    }

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