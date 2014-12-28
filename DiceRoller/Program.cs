#region Usings

using System;
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
            InitializeApplication();

            try
            {
                //var parser = new ArgumentParser();
                //parser.ParsArguments( args );
            }
            catch ( ArgumentParsingException ex )
            {
                OutputHelper.PrintError( "Parsing failed:{0}{1}".F( Environment.NewLine, ex ) );
            }
            catch ( Exception ex )
            {
                OutputHelper.PrintError("Unexpected exception occurred :{0}{1}".F(Environment.NewLine, ex));
            }
        }

        /// <summary>
        ///     Initialize the application.
        /// </summary>
        private static void InitializeApplication()
        {
            Container.ConfigurationManager = new DiceRollerConfigurationManager();
        }
    }

    /// <summary>
    /// Root argument parser.
    /// </summary>
    public class RootParser
    {
        /// <summary>
        /// First level argument parsing.
        /// </summary>
        /// <param name="args">The start arguments.</param>
        public void Pars( String[] args )
        {
            var parser = new GenericParser
            {
                { Consts.HelpSwitches, x => Console.WriteLine( "Help" ) }
            };
        }
    }
}