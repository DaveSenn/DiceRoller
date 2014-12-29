#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using PortableExtensions;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Class containing logic to pars arguments and execute mapped actions.
    /// </summary>
    public class GenericParser : List<ParsAction>
    {
        #region Properties

        /// <summary>
        ///     Default action for unknown values.
        /// </summary>
        public static readonly Action<String, String[]> DefaultAction =
            ( arg, args ) => OutputHelper.PrintError( "No matching option found '{0}' + '{1}'".F( arg, args.StringJoin( " " ) ) );

        /// <summary>
        ///     Gets or sets a value determining whether the parsing is case sensitive or not.
        /// </summary>
        /// <remarks>
        ///     Default is
        ///     <value>false</value>
        ///     .
        /// </remarks>
        /// <value>A value determining whether the parsing is case sensitive or not.</value>
        public Boolean CaseSensitive { get; set; }

        /// <summary>
        ///     Gets or sets a value determining whether the parsing should break after the first match or not.
        /// </summary>
        /// <remarks>
        ///     Default is
        ///     <value>true</value>
        ///     .
        /// </remarks>
        /// <value>A value determining whether the parsing should break after the first match or not.</value>
        public Boolean BreakAfterFirstMatch { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericParser" /> class.
        /// </summary>
        public GenericParser()
        {
            CaseSensitive = false;
            BreakAfterFirstMatch = true;
        }

        #endregion

        #region Public Members

        /// <summary>
        ///     Pars the given argument.
        /// </summary>
        /// <exception cref="ArgumentParsingException">No arguments specified.</exception>
        /// <exception cref="NoDefaultException">No default action specified.</exception>
        /// <param name="args">The arguments to pars.</param>
        /// <param name="requiresDefaultAction">Determines whether the parsing needs a default action, or not. Default is true.</param>
        /// <returns>Returns a value of true if a match was found, otherwise false.</returns>
        public Boolean Pars( String[] args, Boolean requiresDefaultAction = true )
        {
            if ( args.IsNull() || args.NotAny() )
                throw new ArgumentParsingException( "No arguments specified." );

            //Check if a default action is specified.
            if ( requiresDefaultAction && this.NotAny( x => x.IsDefault ) )
                throw new NoDefaultException( "No default action specified." );

            var currentArgument = args[0];
            args = args.Skip( 1 )
                       .ToArray();
            var executeDefaultAction = true;

            foreach ( var action in this )
            {
            }

            foreach ( var action in from action in this
                                    let match = CaseSensitive
                                        ? action.Switches != null && action.Switches.Any( x => x == currentArgument )
                                        : action.Switches != null
                                          && action.Switches.Any( x => x.CompareOrdinalIgnoreCase( currentArgument ) )
                                    where match
                                    select action )
            {
                action.Action.ThrowIfNull( () => action.Action );
                action.Action( currentArgument, args );

                executeDefaultAction = false;
                if ( BreakAfterFirstMatch )
                    return true;
            }

            //Check if default action should get invoked.
            if ( !executeDefaultAction )
                return true;

            //Execute default action
            if ( requiresDefaultAction )
                this.First( x => x.IsDefault )
                    .Action( currentArgument, args );

            //Parsing as failed, no match found.
            return false;
        }

        /// <summary>
        ///     Adds the given action-switches mapping to the parser.
        /// </summary>
        /// <param name="switces">The accepted switches.</param>
        /// <param name="action">The action to execute.</param>
        public void Add( IEnumerable<String> switces, Action<String, String[]> action )
        {
            Add( new ParsAction( switces, action ) );
        }

        /// <summary>
        ///     Adds the given action as default action to the parser.
        /// </summary>
        /// <param name="action">The default action.</param>
        public void Add( Action<String, String[]> action )
        {
            Add( new ParsAction( true, action ) );
        }

        #endregion
    }
}