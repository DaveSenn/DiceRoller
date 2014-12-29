#region Usings

using System;
using PortableExtensions;

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
                { Consts.PrintSwitches, ( arg, remainingArgs ) => PrintProfile( remainingArgs ) },
                { Consts.AddSwitches, ( arg, remainingArgs ) => AddProfile( remainingArgs ) },
                { Consts.RemoveSwitches, ( arg, remainingArgs ) => RemoveProfile( remainingArgs ) },
                { Consts.EditSwitches, ( arg, remainingArgs ) => EditProfile( remainingArgs ) },
            };

            parser.Pars( args );
        }

        #endregion

        #region Private Members

        /// <summary>
        ///     Prints the profile with the name specified in the arguments.
        /// </summary>
        /// <param name="remainingArgs">The arguments.</param>
        private void PrintProfile( String[] remainingArgs )
        {
            if ( remainingArgs.NotAny() )
            {
                Container.ConfigurationManager.Configuration.Profiles.ForEach(
                    x => OutputHelper.PrintMessage( "{0}\t\t{1}".F( x.Key, x.Value ) ) );
                return;
            }

            var profile = Container.ConfigurationManager.GetProfile( remainingArgs[0] );
            if ( profile != null )
                OutputHelper.PrintMessage( profile );
        }

        /// <summary>
        ///     Adds the profile represented by the given arguments.
        /// </summary>
        /// <param name="remainingArgs">The arguments.</param>
        private void AddProfile( String[] remainingArgs )
        {
            if ( remainingArgs.Length != 2 )
            {
                OutputHelper.PrintError( "Invalid argument length (2 arguments required: Profile-name and Profile)." );
                return;
            }

            if ( Container.ConfigurationManager.AddProfile( remainingArgs[0], remainingArgs[1] ) )
                OutputHelper.PrintMessage( "Profile '{0}' with value '{1}' added.".F( remainingArgs[0], remainingArgs[1] ) );
        }

        /// <summary>
        ///     Removes the profile with the name specified in the arguments.
        /// </summary>
        /// <param name="remainingArgs">The arguments.</param>
        private void RemoveProfile( String[] remainingArgs )
        {
            if ( remainingArgs.NotAny() )
            {
                OutputHelper.PrintError( "No profile name specified." );
                return;
            }

            if ( Container.ConfigurationManager.RemoveProfile( remainingArgs[0] ) )
                OutputHelper.PrintMessage( "Profile '{0}' removed.".F( remainingArgs[0] ) );
        }

        /// <summary>
        ///     Adds the profile represented by the given arguments.
        /// </summary>
        /// <param name="remainingArgs">The arguments.</param>
        private void EditProfile( String[] remainingArgs )
        {
            if ( remainingArgs.Length != 2 )
            {
                OutputHelper.PrintError( "Invalid argument length (2 arguments required: Profile-name and Profile)." );
                return;
            }

            if ( Container.ConfigurationManager.EditProfile( remainingArgs[0], remainingArgs[1] ) )
                OutputHelper.PrintMessage( "Profile '{0}' edited, new value is '{1}'.".F( remainingArgs[0], remainingArgs[1] ) );
        }

        #endregion
    }
}