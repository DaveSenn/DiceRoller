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
            InitializeApplication();

            try
            {
                //var parser = new ArgumentParser();
                //parser.ParsArguments( args );
            }
            catch ( Exception ex )
            {
                OutputHelper.PrintError( "Parsing failed:{0}{1}".F( Environment.NewLine, ex ) );
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
}