#region Usings

using System;
using DiceRoller.Lib;
using PortableExtensions;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Root argument parser.
    /// </summary>
    public class RootParser
    {
        #region Public Members

        /// <summary>
        ///     First level argument parsing.
        /// </summary>
        /// <exception cref="ArgumentParsingException">No arguments specified.</exception>
        /// <param name="args">The start arguments.</param>
        public void Pars( String[] args )
        {
            var parser = new GenericParser
            {
                { Consts.HelpSwitches, ( arg, remainingArgs ) => Console.WriteLine( "Help" ) },
                { Consts.ConfigurationSwitches, ( arg, remainingArgs ) => new ConfigParser().Pars( remainingArgs ) },
            };

            var result = parser.Pars( args, false );
            if ( result )
                return;

            //Must be a roll.
            ParsRoll( args );
        }

        #endregion

        #region Private Members

        private static void ParsRoll( String[] args )
        {
            Roll roll;

            //Check if argument is profile
            if ( Container.ConfigurationManager.Configuration.Profiles.ContainsKey( args[0] ) )
            {
                //Argument is profile
                var profile = Container.ConfigurationManager.Configuration.Profiles[args[0]];
                try
                {
                    roll = Container.RollParser.ParsRoll( profile );
                }
                catch ( Exception ex )
                {
                    OutputHelper.PrintError( "Failed to pars profile '{0}' ('{1}'). Details:{2}{3}".F(
                        args[0],
                        profile,
                        Environment.NewLine,
                        ex.Message ) );
                    return;
                }
            }
            else
            {
                if ( args.Length > 1 )
                {
                    OutputHelper.PrintError( "Invalid arguments specified, roll can contain spaces." );
                    return;
                }
                try
                {
                    roll = Container.RollParser.ParsRoll( args[0] );
                }
                catch ( Exception ex )
                {
                    OutputHelper.PrintError( "Failed to pars roll '{0}'. Details:{1}{2}".F(
                        args[0],
                        Environment.NewLine,
                        ex.Message ) );
                    return;
                }
            }

            try
            {
                var rollResult = Container.RollCalculator.Calculate( roll );
                OutputHelper.PrintResult( rollResult );
            }
            catch ( Exception ex )
            {
                OutputHelper.PrintError( "Failed to calculate roll. Details:{0}{1}".F( Environment.NewLine, ex.Message ) );
            }
        }

        #endregion
    }
}