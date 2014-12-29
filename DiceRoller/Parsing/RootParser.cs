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
                { Consts.HelpSwitches, ( arg, remainingArgs ) => Console.WriteLine( "Help" ) },
                { Consts.ConfigurationSwitches, ( arg, remainingArgs ) => new ConfigParser().Pars( remainingArgs ) },
            };

            var result = parser.Pars( args, false );
            if ( result )
                return;

            //TODO: Pars roll

            //TODO: print default error message if roll failed.
        }
    }
}