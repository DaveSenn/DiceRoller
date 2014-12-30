#region Usings

using System;
using System.Collections.Generic;
using DiceRoller.Lib;
using PortableExtensions;

#endregion

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
                Console.ForegroundColor = Container.ConfigurationManager.Configuration.ErrorColor;

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
                Console.ForegroundColor = Console.ForegroundColor = Container.ConfigurationManager.Configuration.ResultColor;

                Console.WriteLine( resultMessage );
                Console.ForegroundColor = color;
            }
        }

        /// <summary>
        ///     Prints the given roll result.
        /// </summary>
        /// <param name="rollResult">The roll result to print.</param>
        public static void PrintResult( IEnumerable<RollResult> rollResult )
        {
            lock ( SyncRoot )
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = Console.ForegroundColor = Container.ConfigurationManager.Configuration.ResultColor;

                rollResult.ForEach( x =>
                {
                    switch ( x.Result.Type )
                    {
                        case ResultType.SingleValue:
                            var singleValue = x.Result as SingleResult;
                            Console.WriteLine( "{0}\t\t{1}", singleValue.Result, x.Log );
                            break;

                        case ResultType.Map:
                            var mapValue = x.Result as MapResult;
                            Console.WriteLine( "{0} => {1}\t\t{2}", mapValue.Left, mapValue.Right, x.Log );
                            break;

                        default:
                            Console.WriteLine( "Invalid result type received" );
                            break;
                    }
                } );

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