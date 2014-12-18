#region Usings

using System;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Class containing all constant values.
    /// </summary>
    public static class Consts
    {
        #region Paths

        public static readonly String ConfigurationFilePath = "DiceRollerConfiguration.json";

        #endregion

        #region Switches

        /// <summary>
        ///     Switch for accessing the configuration.
        /// </summary>
        public const String ConfigurationSwitch = "config";

        /// <summary>
        ///     Switch for printing something to the standard output.
        /// </summary>
        public const String Print = "print";

        /// <summary>
        ///     Switch to reset data.
        /// </summary>
        public const String Restore = "restore";

        /// <summary>
        ///     Switch to get path to data.
        /// </summary>
        public const String Path = "path";

        /// <summary>
        ///     Switch to access profile data.
        /// </summary>
        public const String Profile = "profile";

        #endregion

        #region Constructor

        /// <summary>
        ///     Initialize the <see cref="Consts" /> class.
        /// </summary>
        static Consts()
        {
            //Get path to configuration file
            ConfigurationFilePath = System.IO.Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ),
                                                  ConfigurationFilePath );
        }

        #endregion
    }
}