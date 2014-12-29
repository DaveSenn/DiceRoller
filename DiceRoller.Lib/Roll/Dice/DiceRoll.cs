#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiceRoller.Lib.Lib;
using PortableExtensions;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Class dice representing a dice roll.
    /// </summary>
    public class DiceRoll : ValueGroupBase, IValueGroup
    {
        #region Fields

        /// <summary>
        ///     List of all roll results, grouped by roll.
        /// </summary>
        private readonly List<List<Int32>> _rollResults = new List<List<Int32>>();

        /// <summary>
        ///     Object used to synchronize threads.
        /// </summary>
        private readonly Object _syncRoot = new Object();

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the dice to roll.
        /// </summary>
        /// <value>The dice to roll.</value>
        public Dice Dice { get; set; }

        /// <summary>
        ///     Gets or sets the number of rolls.
        /// </summary>
        /// <value>The number of rolls.</value>
        public Int32 NumberOfRolls { get; set; }

        /// <summary>
        ///     Gets or sets the roll modifier.
        /// </summary>
        /// <value>The roll modifier.</value>
        public RollModifier RollModifier { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="DiceRoll" /> class.
        /// </summary>
        public DiceRoll()
        {
            Dice = new Dice();
            NumberOfRolls = 1;
            RollModifier = new RollModifier { RollModification = RollModification.None };
        }

        #endregion

        #region Implementation of IValueGroup

        /// <summary>
        ///     Gets the result of the value group.
        /// </summary>
        /// <returns>Returns the value.</returns>
        public override Int32 GetValue()
        {
            lock ( _syncRoot )
            {
                //Initialize for new roll
                _rollResults.Clear();
                var result = 0;

                //Lop for number of rolls
                for ( var i = 0; i < NumberOfRolls; i++ )
                    switch ( RollModifier.RollModification )
                    {
                        case RollModification.None:
                            //Get roll result
                            var rollResult = GetRolLResult();

                            //Set result and add roll to log
                            result += rollResult;
                            var logEntry = _rollResults.FirstOrDefault();
                            if ( logEntry == null )
                                _rollResults.Add( new List<Int32> { rollResult } );
                            else
                                logEntry.Add( rollResult );

                            break;
                        case RollModification.Worst:
                        case RollModification.Best:

                            var tempLog = new List<Int32>();
                            //Loop =>  modification quantifier
                            for ( var c = 0; c < RollModifier.ModificationQuantifier; c++ )
                                //Add roll result to temp log
                                tempLog.Add( GetRolLResult() );

                            //Add highest or lowest value to result
                            result += RollModifier.RollModification == RollModification.Best
                                ? tempLog.Max()
                                : tempLog.Min();

                            //Add temp log to log
                            _rollResults.Add( tempLog );
                            break;
                        default:
                            throw new DiceRollException(
                                "Unknown roll modification '{0}' specified".F( RollModifier.RollModification ),
                                this );
                    }

                return result;
            }
        }

        /// <summary>
        ///     Gets the log containing the information how the value was created.
        /// </summary>
        /// <returns>Returns the log.</returns>
        public override String GetLog()
        {
            var sb = new StringBuilder();
            sb.Append( "[" );

            //Check if roll had no modifiers.. if so use simple layout
            if ( _rollResults.Count == 1 )
                sb.Append( _rollResults[0].OrderBy( x => x )
                                          .StringJoin( ", " ) );
            else
                //Use group layout
                _rollResults.ForEach( x =>
                {
                    //Add comma if sb contains already one or more groups
                    if ( sb.Length > 1 )
                        sb.Append( ", " );

                    //Add group
                    sb.Append( "(" );
                    sb.Append( x.OrderBy( y => y )
                                .StringJoin( ", " ) );
                    sb.Append( ")" );
                } );

            sb.Append( "]" );
            return sb.ToString();
        }

        #endregion

        #region Private Members

        /// <summary>
        ///     Gets the result of a roll.
        /// </summary>
        /// <returns>The result of a roll.</returns>
        private Int32 GetRolLResult()
        {
            return RandomValueEx.GetRandomInt32( 1, Dice.Sides + 1 );
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override String ToString()
        {
            //Get string representing modifier.
            var modification = RollModifier.RollModification == RollModification.None
                ? String.Empty
                : RollModifier.RollModification == RollModification.Best
                    ? "b{0}"
                    : "w{0}";
            if ( modification.IsNotEmpty() )
                modification = RollModifier.ModificationQuantifier > 2
                    ? modification.F( RollModifier.ModificationQuantifier )
                    : modification.F( String.Empty );

            //Get string representing dice roll
            return "{0}d{1}{2}".F( NumberOfRolls, Dice.Sides, modification );
        }

        #endregion
    }
}