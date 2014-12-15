#region Usings

using System;
using System.Linq;
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
            //Find action to perform
            switch ( args[0] )
            {
                case Consts.Print:
                    //Print the current configuration
                    Console.WriteLine( "Configuration: {0}{1}", Environment.NewLine, DiceRollerConfigurationManager.Configuration );
                    return;
                case Consts.Path:
                    //Print location of configuration file
                    Console.WriteLine( Consts.ConfigurationFilePath );
                    return;
                case Consts.Reset:
                    //Reset the settings
                    DiceRollerConfigurationManager.Configuration.RestoreDefault();
                    DiceRollerConfigurationManager.SaveConfiguration();
                    Console.WriteLine( "Configuration reseted" );
                    return;
            }

            //Check for get value
            try
            {
                switch ( args.Length )
                {
                    case 1:
                        Console.WriteLine( DiceRollerConfigurationManager.Configuration.GetProperty( args[0] ) );
                        break;
                    case 2:
                        DiceRollerConfigurationManager.Configuration.SetProperty( args[0], args[1] );
                        DiceRollerConfigurationManager.SaveConfiguration();
                        break;
                }
                return;
            }
            catch
            {
                Console.Error.WriteLine( "Invalid configuration property." );
                return;
            }
            Console.Error.WriteLine( "Invalid configuration arguments." );
        }
    }
}