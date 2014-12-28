#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Class representing a pars action.
    /// </summary>
    public class ParsAction
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the switches mapped to the action.
        /// </summary>
        /// <value>The switches mapped to the action.</value>
        public IEnumerable<String> Switches { get; set; }

        /// <summary>
        ///     Gets or sets a value determining whether the action is the default action or not.
        /// </summary>
        /// <remarks>
        ///     Default is
        ///     <value>false</value>
        ///     .
        /// </remarks>
        /// <value>A value determining whether the action is the default action or not.</value>
        public Boolean IsDefault { get; set; }

        /// <summary>
        ///     Gets or sets the action to execute.
        /// </summary>
        /// <value>The action to execute.</value>
        public Action<String> Action { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ParsAction" /> action.
        /// </summary>
        public ParsAction()
        {
            IsDefault = false;
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ParsAction" /> action.
        /// </summary>
        /// <param name="isDefault">A value determining whether the action is the default action or not.</param>
        /// <param name="action">The action to execute.</param>
        public ParsAction( Boolean isDefault, Action<String> action )
        {
            IsDefault = isDefault;
            Action = action;
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ParsAction" /> action.
        /// </summary>
        /// <param name="switches">The mapped switches.</param>
        /// <param name="action">The action to execute.</param>
        public ParsAction( IEnumerable<String> switches, Action<String> action )
        {
            IsDefault = false;
            Action = action;
            Switches = switches;
        }

        #endregion
    }
}