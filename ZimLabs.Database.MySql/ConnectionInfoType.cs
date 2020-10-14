using System;

namespace ZimLabs.Database.MySql
{
    /// <summary>
    /// Provides the different connection info types
    /// </summary>
    [Flags]
    public enum ConnectionInfoType
    {
        /// <summary>
        /// Shows nothing
        /// </summary>
        None = 0,

        /// <summary>
        /// Shows the name of the server
        /// </summary>
        Server = 1,

        /// <summary>
        /// Shows the name of the database
        /// </summary>
        Database = 2,

        /// <summary>
        /// Shows the user name
        /// </summary>
        User = 4,

        /// <summary>
        /// Shows the port number
        /// </summary>
        Port = 8
    }
}
