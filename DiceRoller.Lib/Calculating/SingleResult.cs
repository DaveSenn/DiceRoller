using System;

namespace DiceRoller.Lib
{
    /// <summary>
    ///     A single value result.
    /// </summary>
    public class SingleResult : IResult
    {
        #region Implementation of IResult

        /// <summary>
        ///     Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public Int32 Result { get; set; }

        /// <summary>
        ///     Gets or sets the result type.
        /// </summary>
        /// <value>The result type.</value>
        public ResultType Type
        {
            get { return ResultType.SingleValue; }
        }

        #endregion
    }
}