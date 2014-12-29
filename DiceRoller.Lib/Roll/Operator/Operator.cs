#region Usings

using System;

#endregion

namespace DiceRoller.Lib
{
    /// <summary>
    ///     Class representing an operator.
    /// </summary>
    public class Operator : IOperator
    {
        #region Implementation of IRollPart

        /// <summary>
        ///     Gets or sets the type of the roll part.
        /// </summary>
        /// <value>The type of the roll part.</value>
        public RollPartType Type
        {
            get { return RollPartType.Operator; }
        }

        #endregion

        #region Implementation of IOperator

        /// <summary>
        ///     Gets or sets the operator type.
        /// </summary>
        /// <value>The operator type.</value>
        public RollOperator OperatorType { get; set; }

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
            switch ( OperatorType )
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
                    return "=>";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}