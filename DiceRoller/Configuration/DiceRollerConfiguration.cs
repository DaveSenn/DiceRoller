#region Usings

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Class representing the configuration of the DiceRoller application.
    /// </summary>
    public class DiceRollerConfiguration
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the result color.
        /// </summary>
        /// <value>The result color.</value>
        [JsonConverter( typeof (StringEnumConverter) )]
        public ConsoleColor ResultColor { get; set; }

        /// <summary>
        ///     Gets or sets the error color.
        /// </summary>
        /// <value>The error color.</value>
        [JsonConverter( typeof (StringEnumConverter) )]
        public ConsoleColor ErrorColor { get; set; }

        /// <summary>
        ///     Gets or sets the defined profiles.
        /// </summary>
        /// <value>The profiles.</value>
        public Dictionary<String, String> Profiles { get; set; }

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
            return JsonConvert.SerializeObject( this, Formatting.Indented );
        }

        #endregion
    }
}