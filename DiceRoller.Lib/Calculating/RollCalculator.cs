#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using PortableExtensions;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Class containing the logic to calculate a roll.
    /// </summary>
    public class RollCalculator
    {
        #region Fields

        /// <summary>
        ///     List containing the for basic operators, ordered by priority.
        /// </summary>
        private readonly List<RollOperator> _operators = new List<RollOperator>
        {
            RollOperator.Times,
            RollOperator.Divide,
            RollOperator.Plus,
            RollOperator.Minus,
        };

        #endregion

        #region Public Members

        /// <summary>
        ///     Calculates the given roll(s) and returns the result.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <returns>Returns the result.</returns>
        public IEnumerable<RollResult> Calculate( Roll roll )
        {
            var result = new List<RollResult>();
            for ( var i = 0; i < roll.NumberOfRepetitions; i++ )
            {
                var resultEntry = new RollResult
                {
                    Log = RollGroups( roll )
                };
                _operators.ForEach( x => Calculate( roll, x ) );

                resultEntry.Result = GetResult( roll );
                result.Add( resultEntry );
            }

            return result;
        }

        #endregion

        #region Private Members

        /// <summary>
        ///     Gets the result from the given roll.
        /// </summary>
        /// <exception cref="RollCalculationException">Calculation failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <returns>Returns the result.</returns>
        private IResult GetResult( IReadOnlyList<IRollPart> roll )
        {
            //Return single or mapped value result
            return roll.Count == 1 ? (IResult) GetSingleResult( roll ) : GetMapResult( roll );
        }

        /// <summary>
        ///     Gets a single value result from the given roll.
        /// </summary>
        /// <exception cref="RollCalculationException">Calculation failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <returns>Returns the single value result.</returns>
        private static SingleResult GetSingleResult( IReadOnlyList<IRollPart> roll )
        {
            var valueGroup = roll[0] as IValueGroup;
            if ( valueGroup == null )
                throw new RollCalculationException(
                    "Last group must be a value group. (Failed to convert '{0}' to a value group)".F( roll[0] ) );
            return new SingleResult { Result = valueGroup.GetValue() };
        }

        /// <summary>
        ///     Gets a map result from the given roll.
        /// </summary>
        /// <exception cref="RollCalculationException">Calculation failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <returns>Returns the map result.</returns>
        private static MapResult GetMapResult( IReadOnlyList<IRollPart> roll )
        {
            if ( roll.Count != 3 )
                throw new RollCalculationException(
                    "Resolved to mapped result with '{0}' parts, expected 3 parts.".F( roll.Count ) );

            var left = roll[0] as IValueGroup;
            var right = roll[2] as IValueGroup;

            if ( left == null )
                throw new RollCalculationException( "Could not get mapped result, left value is not a value group." );
            if ( right == null )
                throw new RollCalculationException( "Could not get mapped result, right value is not a value group." );

            return new MapResult
            {
                Left = left.GetValue(),
                Right = right.GetValue()
            };
        }

        /// <summary>
        ///     Rolls the value of each group and creates a log.
        /// </summary>
        /// <exception cref="RollCalculationException">Calculation failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <returns>Returns the roll log.</returns>
        private static String RollGroups( List<IRollPart> roll )
        {
            //String builder used to store the log of each part
            var sb = new StringBuilder();

            //Get the value of each value type part and create the log
            roll.ForEach( x =>
            {
                switch ( x.Type )
                {
                    case RollPartType.ValueGroup:

                        var valueGroup = x as IValueGroup;
                        if ( valueGroup == null )
                            throw new RollCalculationException( "Invalid type specified for group '{0}'".F( x.ToString() ) );
                        valueGroup.GetValue();
                        sb.AppendWithSpace( valueGroup.GetLog() );
                        break;
                    case RollPartType.Operator:
                        var op = x as IOperator;
                        if ( op == null )
                            throw new RollCalculationException( "Invalid type specified for group '{0}'".F( x.ToString() ) );
                        sb.AppendWithSpace( op.GetStringOperator() );
                        break;
                    default:
                        throw new RollCalculationException( "Type of given part is unknown.",
                                                            new ArgumentOutOfRangeException( "Unknown part type." ) );
                }
            } );

            return sb.ToString();
        }

        /// <summary>
        ///     Calculates all groups joined by the given operator.
        /// </summary>
        /// <exception cref="RollCalculationException">Calculation failed.</exception>
        /// <param name="roll">The roll.</param>
        /// <param name="op">The operator.</param>
        private static void Calculate( IList<IRollPart> roll, RollOperator op )
        {
            var operatorPart = roll.GetFirstMatchingOperator( op ) as IOperator;
            while ( operatorPart != null )
            {
                //Get operator, left and right group.
                var index = roll.IndexOf( operatorPart );
                IValueGroup left;
                IValueGroup right;
                try
                {
                    left = roll[index - 1] as IValueGroup;
                    right = roll[index + 1] as IValueGroup;

                    if ( left == null )
                        throw new RollCalculationException( "Could not calculate result, left value is not a value group." );
                    if ( right == null )
                        throw new RollCalculationException( "Could not calculate result, right value is not a value group." );
                }
                catch ( ArgumentOutOfRangeException )
                {
                    throw new RollCalculationException( "Operator found at invalid index '{0}'".F( index ) );
                }

                //Calculate the result
                var resultValue = operatorPart.GetResult( left.GetValue(), right.GetValue() );

                //Remove parts
                var leftIndex = roll.IndexOf( left );
                roll.RemoveRange( operatorPart, left, right );
                roll.Insert( leftIndex, new StaticValue { Value = resultValue } );

                //Get next operator of same type
                operatorPart = roll.GetFirstMatchingOperator( op ) as IOperator;
            }
        }

        #endregion
    }
}