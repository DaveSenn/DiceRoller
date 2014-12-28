#region Usings

using System;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Root argument parser.
    /// </summary>
    public class RootParser
    {
        /// <summary>
        ///     First level argument parsing.
        /// </summary>
        /// <exception cref="ArgumentParsingException">No arguments specified.</exception>
        /// <param name="args">The start arguments.</param>
        public void Pars( String[] args )
        {
            var parser = new GenericParser
            {
                GenericParser.DefaultAction,
                { Consts.HelpSwitches, ( arg, remainingArgs ) => Console.WriteLine( "Help" ) },
                { Consts.ConfigurationSwitches, ( arg, remainingArgs ) => new ConfigParser().Pars( remainingArgs ) },
            };

            parser.Pars( args );
        }
    }
}