#region Usings

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace DiceRoller
{
    /// <summary>
    ///     Class containing all constant values.
    /// </summary>
    public static class Consts
    {
        #region Paths

        /// <summary>
        ///     Path to the configuration file.
        /// </summary>
        public static readonly String ConfigurationFilePath = "DiceRollerConfiguration.json";

        #endregion

        #region Switches

        /// <summary>
        ///     Switches accepted for accessing configuration section.
        /// </summary>
        public static readonly List<String> ConfigurationSwitches = new List<String>
        {
            "c",
            "config",
            "configuration"
        };

        /// <summary>
        ///     Switches accepted for any printing/sowing action.
        /// </summary>
        public static readonly List<String> PrintSwitches = new List<String>
        {
            "show",
            "display",
            "print",
        };

        /// <summary>
        ///     Switches accepted for any restore/reset action.
        /// </summary>
        public static readonly List<String> RestoreSwitches = new List<String>
        {
            "r",
            "reset",
            "restore",
        };

        /// <summary>
        ///     Switches accepted for accessing the configuration path.
        /// </summary>
        public static readonly List<String> PathSwitches = new List<String>
        {
            "path",
            "location",
            "name",
            "filename",
        };

        /// <summary>
        ///     Switches accepted for accessing the profile section.
        /// </summary>
        public static readonly List<String> ProfileSwitches = new List<String>
        {
            "p",
            "profile",
            "prof",
        };

        #endregion

        #region Constructor

        /// <summary>
        ///     Initialize the <see cref="Consts" /> class.
        /// </summary>
        static Consts()
        {
            //Get path to configuration file
            ConfigurationFilePath = Path.Combine(
                Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ),
                ConfigurationFilePath );
        }

        #endregion
    }
}