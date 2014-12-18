using System;

namespace DiceRoller
{
    /// <summary>
    ///     Class containing helper methods for writing to the console.
    /// </summary>
    public static class OutputHelper
    {
        #region Fields

        /// <summary>
        ///     Object used to synchronize threads.
        /// </summary>
        private static readonly Object SyncRoot = new Object();

        #endregion

        #region Public Members

        /// <summary>
        ///     Prints an error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public static void PrintError( String errorMessage )
        {
            lock ( SyncRoot )
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = DiceRollerConfigurationManager.Configuration.ErrorColor;

                Console.Error.WriteLine( errorMessage );

                Console.ForegroundColor = color;
            }
        }

        /// <summary>
        ///     Prints a result message.
        /// </summary>
        /// <param name="resultMessage">The result message.</param>
        public static void PrintResult( String resultMessage )
        {
            lock ( SyncRoot )
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = DiceRollerConfigurationManager.Configuration.ResultColor;

                Console.WriteLine( resultMessage );

                Console.ForegroundColor = color;
            }
        }

        /// <summary>
        ///     Prints a message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void PrintMessage( String message )
        {
            lock ( SyncRoot )
            {
                Console.WriteLine( message );
            }
        }

        #endregion
    }
}