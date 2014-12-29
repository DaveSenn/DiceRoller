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

        /// <summary>
        ///     Switches accepted for any open action.
        /// </summary>
        public static readonly List<String> OpenSwitches = new List<String>
        {
            "open",
            "start",
        };

        /// <summary>
        ///     Switches accepted for accessing the help section.
        /// </summary>
        public static readonly List<String> HelpSwitches = new List<String>
        {
            "h",
            "?",
            "help",
        };

        /// <summary>
        ///     Switches accepted for any add action.
        /// </summary>
        public static readonly List<String> AddSwitches = new List<String>
        {
            "a",
            "add",
        };

        /// <summary>
        ///     Switches accepted for any remove action.
        /// </summary>
        public static readonly List<String> RemoveSwitches = new List<String>
        {
            "r",
            "rm",
            "remove",
        };

        /// <summary>
        ///     Switches accepted for any edit action.
        /// </summary>
        public static readonly List<String> EditSwitches = new List<String>
        {
            "e",
            "edit",
            "alter",
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