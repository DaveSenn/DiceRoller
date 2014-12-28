#region Usings

using System;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Profile argument parser.
    /// </summary>
    public class ProfileParser
    {
        #region Public Members

        /// <summary>
        ///     Parses profile related arguments.
        /// </summary>
        /// <exception cref="ArgumentParsingException">No arguments specified.</exception>
        /// <param name="args">The arguments to pars.</param>
        public void Pars( String[] args )
        {
            var parser = new GenericParser
            {
                GenericParser.DefaultAction,
                { Consts.PrintSwitches, ( arg, remainingArgs ) => Console.WriteLine( "Print" ) },
                { Consts.AddSwitches, ( arg, remainingArgs ) => Console.WriteLine( "AddSwitches" ) },
                { Consts.RemoveSwitches, ( arg, remainingArgs ) => Console.WriteLine( "RemoveSwitches" ) },
                { Consts.EditSwitches, ( arg, remainingArgs ) => Console.WriteLine( "EditSwitches" ) },
            };

            parser.Pars( args );
        }

        #endregion
    }
}