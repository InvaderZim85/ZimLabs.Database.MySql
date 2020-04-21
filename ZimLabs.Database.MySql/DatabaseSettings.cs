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

        /// <summary>
        /// Creates a new empty instance of the <see cref="DatabaseSettings"/>
        /// </summary>
        public DatabaseSettings(){ }

        /// <summary>
        /// Creates a new instance of the <see cref="DatabaseSettings"/>
        /// </summary>
        /// <param name="server">The name / path of the server</param>
        /// <param name="database">The name of the database</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        /// <param name="port">The port (optional)</param>
        public DatabaseSettings(string server, string database, string userId, SecureString password, uint port = 3306)
        {
            Server = server;
            Database = database;
            UserId = userId;
            Password = password;
            Port = port;
        }
    }
}
