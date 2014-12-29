namespace DiceRoller.Lib
{
    /// <summary>
    ///     Enumeration of all possible roll operators.
    /// </summary>
    public enum RollOperator
    {
        /// <summary>
        ///     Plus, add left and right together.
        /// </summary>
        /// <remarks>
        ///     Priority 3
        /// </remarks>
        Plus = 0,

        /// <summary>
        ///     Minus, subtract right from left
        /// </summary>
        /// <remarks>
        ///     Priority 4
        /// </remarks>
        Minus = 1,

        /// <summary>
        ///     Times, multiply left and right.
        /// </summary>
        /// <remarks>
        ///     Priority 1
        /// </remarks>
        Times = 2,

        /// <summary>
        ///     Divide, left divided by right
        /// </summary>
        /// <remarks>
        ///     Priority 2
        /// </remarks>
        Divide = 3,

        /// <summary>
        ///     Map, right to left
        /// </summary>
        /// <remarks>
        ///     Priority 5
        /// </remarks>
        Map = 4
    }
}