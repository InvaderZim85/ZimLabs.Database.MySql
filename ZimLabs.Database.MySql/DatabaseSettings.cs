using System.Security;

namespace ZimLabs.Database.MySql
{
    /// <summary>
    /// Represents the settings for the database connection
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Gets or sets the name of the server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the name of the database
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public SecureString Password { get; set; }

        /// <summary>
        /// Gets or sets the port
        /// </summary>
        public uint Port { get; set; }
    }
}
