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
        /// <exception cref="NoDefaultException">No default action specified.</exception>
        /// <param name="argument">The argument to pars.</param>
        public void Pars( String argument )
        {
            //Check if a default action is specified.
            if ( this.NotAny( x => x.IsDefault ) )
                throw new NoDefaultException( "No default action specified." );

            var executeDefaultAction = true;

            foreach ( var action in from action in this
                                    let match = CaseSensitive
                                        ? action.Switches != null && action.Switches.Any( x => x == argument )
                                        : action.Switches != null
                                          && action.Switches.Any( x => x.CompareOrdinalIgnoreCase( argument ) )
                                    where match
                                    select action )
            {
                action.Action.ThrowIfNull( () => action.Action );
                action.Action( argument );

                executeDefaultAction = false;
                if ( BreakAfterFirstMatch )
                    return;
            }

            //Execute default action
            if ( executeDefaultAction )
                this.First( x => x.IsDefault )
                    .Action( argument );
        }

        /// <summary>
        ///     Adds the given action-switches mapping to the parser.
        /// </summary>
        /// <param name="switces">The accepted switches.</param>
        /// <param name="action">The action to execute.</param>
        public void Add( IEnumerable<String> switces, Action<String> action )
        {
            Add( new ParsAction( switces, action ) );
        }

        /// <summary>
        ///     Adds the given action as default action to the parser.
        /// </summary>
        /// <param name="action">The default action.</param>
        public void Add( Action<String> action )
        {
            Add( new ParsAction( true, action ) );
        }

        #endregion
    }
}