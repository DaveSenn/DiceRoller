using System;

namespace DiceRoller.Lib
{
    /// <summary>
    ///     A map result value.
    /// </summary>
    public class MapResult : IResult
    {
        #region Implementation of IResult

        /// <summary>
        ///     Gets or sets the left result value.
        /// </summary>
        /// <value>The left result value.</value>
        public Int32 Left { get; set; }

        /// <summary>
        ///     Gets or sets the right result value.
        /// </summary>
        /// <value>The right result value.</value>
        public Int32 Right { get; set; }

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