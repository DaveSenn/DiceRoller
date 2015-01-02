#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiceRoller.Extensions;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Class containing some extension methods.
    /// </summary>
    public static class Ex
    {
        /// <summary>
        ///     Appends the given value to the string builder, adds a space before the value if the string builder is not empty.
        /// </summary>
        /// <param name="sb">The string builder.</param>
        /// <param name="value">The value to append.</param>
        public static void AppendWithSpace( this StringBuilder sb, String value )
        {
            if ( sb.Length > 0 )
                sb.Append( " " );
            sb.Append( value );
        }

        /// <summary>
        ///     Converts the given operator to it's string equivalent.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <returns>Returns the operator's string equivalent.</returns>
        public static String GetStringOperator( this IOperator op )
        {
            switch ( op.OperatorType )
            {
                case RollOperator.Plus:
                    return "+";
                case RollOperator.Minus:
                    return "-";
                case RollOperator.Times:
                    return "*";
                case RollOperator.Divide:
                    return "/";
                case RollOperator.Map:
                    return "=";
                default:
                    throw new ArgumentOutOfRangeException( op.GetName( () => op ), "Unknown operator specified." );
            }
        }

        /// <summary>
        ///     Gets a result based on the operator, left and right value.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>Returns the result.</returns>
        public static Int32 GetResult( this IOperator op, Int32 left, Int32 right )
        {
            switch ( op.OperatorType )
            {
                case RollOperator.Plus:
                    return left + right;
                case RollOperator.Minus:
                    return left - right;
                case RollOperator.Times:
                    return left * right;
                case RollOperator.Divide:
                    return left / right;
                case RollOperator.Map:
                    throw new InvalidOperationException( "Operation not supported for map operator." );
                default:
                    throw new ArgumentOutOfRangeException( op.GetName( () => op ), "Unknown operator specified." );
            }
        }

        /// <summary>
        ///     Gets the first operator of the specified type.
        /// </summary>
        /// <param name="roll">The roll containing the operators.</param>
        /// <param name="operators">The operator types to search for.</param>
        /// <returns>Returns all matching elements.</returns>
        public static IRollPart GetFirstMatchingOperator( this IEnumerable<IRollPart> roll, IEnumerable<RollOperator> operators )
        {
            return roll
                .FirstOrDefault( x => x.Type == RollPartType.Operator && ( x as IOperator ).OperatorType.IsIn( operators ) );
        }
    }
}