#region Usings

using System;
using DiceRoller.Extensions;
using DiceRoller.Lib;

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
                new RootParser().Pars( args );
            }
            catch ( ArgumentParsingException ex )
            {
                OutputHelper.PrintError( "Parsing failed:{0}{1}".F( Environment.NewLine, ex.Message ) );
            }
            catch ( Exception ex )
            {
                OutputHelper.PrintError( "Unexpected exception occurred :{0}{1}".F( Environment.NewLine, ex ) );
            }
        }

        /// <summary>
        ///     Initialize the application.
        /// </summary>
        private static void InitializeApplication()
        {
            Container.ConfigurationManager = new DiceRollerConfigurationManager();
            Container.RollParser = new RollParser();
            Container.RollCalculator = new RollCalculator();
        }
    }
}