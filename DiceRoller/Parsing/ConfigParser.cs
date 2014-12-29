#region Usings

using System;
using System.Diagnostics;
using PortableExtensions;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Class containing the logic to pars configuration switches.
    /// </summary>
    public class ConfigParser
    {
        #region Public Members

        /// <summary>
        ///     Parses the given arguments for configuration switches.
        /// </summary>
        /// <param name="args">The arguments to pars.</param>
        public void Pars( String[] args )
        {
            var parser = new GenericParser
            {
                { Consts.PrintSwitches, ( arg, remainingArgs ) => PrintConfiguration() },
                { Consts.PathSwitches, ( arg, remainingArgs ) => PrintConfigurationPath() },
                { Consts.RestoreSwitches, ( arg, remainingArgs ) => RestoreConfiguration() },
                { Consts.OpenSwitches, ( arg, remainingArgs ) => OpenConfigurationFile() },
                { Consts.ProfileSwitches, ( arg, remainingArgs ) => new ProfileParser().Pars( remainingArgs ) },
            };

            var success = parser.Pars( args, false );
            if ( success )
                return;

            //Pars properties
            switch ( args.Length )
            {
                case 1:
                    Console.WriteLine( Container.ConfigurationManager.GetProperty( args[0] ) );
                    return;
                case 2:
                    var result = Container.ConfigurationManager.SetProperty( args[0], args[1] );
                    if ( result )
                        OutputHelper.PrintMessage( "Property '{0}' set to '{1}'".F( args[0], args[1] ) );
                    return;
                default:
                    OutputHelper.PrintError( "Invalid switches, found no matching configuration section." );
                    return;
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        ///     Prints the current configuration.
        /// </summary>
        private void PrintConfiguration()
        {
            OutputHelper.PrintMessage( "Configuration: {0}{1}".F( Environment.NewLine,
                                                                  Container.ConfigurationManager
                                                                           .Configuration ) );
        }

        /// <summary>
        ///     Prints the path to the configuration file.
        /// </summary>
        private void PrintConfigurationPath()
        {
            OutputHelper.PrintMessage( Consts.ConfigurationFilePath );
        }

        /// <summary>
        ///     OPens the configuration file.
        /// </summary>
        private void OpenConfigurationFile()
        {
            try
            {
                Process.Start( Consts.ConfigurationFilePath );
            }
            catch ( Exception ex )
            {
                OutputHelper.PrintError( "Failed to open configuration file. Details:{0}{1}".F( Environment.NewLine, ex.Message ) );
            }
        }

        /// <summary>
        ///     Restores the current configuration.
        /// </summary>
        private void RestoreConfiguration()
        {
            Container.ConfigurationManager.RestoreDefault();
            OutputHelper.PrintMessage( "Configuration restored." );
        }

        #endregion
    }
}