using System;
using System.Data;
using System.Security;
using MySql.Data.MySqlClient;

namespace ZimLabs.Database.MySql
{
    /// <summary>
    /// Provides functions / properties for the interaction with a MySql database
    /// </summary>
    public sealed class Connector : IDisposable
    {
        /// <summary>
        /// Contains the value which indicates if the class was already disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Contains the settings
        /// </summary>
        private readonly DatabaseSettings _settings;

        /// <summary>
        /// Contains the connection string
        /// </summary>
        private SecureString _connectionString;

        /// <summary>
        /// Contains the connection
        /// </summary>
        private MySqlConnection _connection;

        /// <summary>
        /// Gets the current connection
        /// </summary>
        public MySqlConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed ||
                    _connection.State == ConnectionState.Broken)
                    CreateConnection();

                return _connection;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="settings">The settings for the database connection</param>
        public Connector(DatabaseSettings settings)
        {
            _settings = settings;

            CreateConnectionString();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="server">The name / path of the server</param>
        /// <param name="database">The name of the database</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        /// <param name="port">The port (optional)</param>
        public Connector(string server, string database, string userId, string password, uint port = 3306) : this(
            new DatabaseSettings(server, database, userId, password.ToSecureString(), port))
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="server">The name / path of the server</param>
        /// <param name="database">The name of the database</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        /// <param name="port">The port (optional)</param>
        public Connector(string server, string database, string userId, SecureString password, uint port = 3306) : this(
            new DatabaseSettings(server, database, userId, password, port))
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        public Connector(SecureString connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Creates the connection string
        /// </summary>
        private void CreateConnectionString()
        {
            _connectionString = new MySqlConnectionStringBuilder
            {
                Server = _settings.Server,
                Database = _settings.Database,
                UserID = _settings.UserId,
                Password = _settings.Password.ToInsecureString(),
                Port = _settings.Port
            }.ConnectionString.ToSecureString();
        }

        /// <summary>
        /// Creates a new connection
        /// </summary>
        private void CreateConnection()
        {
            _connection = new MySqlConnection(_connectionString.ToInsecureString());
            _connection.Open();
        }

        /// <summary>
        /// Closes the connection
        /// </summary>
        public void CloseConnection()
        {
            _connection?.Close();
        }

        /// <summary>
        /// Switches the database
        /// </summary>
        /// <param name="database">The name of the database</param>
        /// <exception cref="ArgumentNullException">Occurs when the database name is null or empty</exception>
        public void SwitchDatabase(string database)
        {
            if (string.IsNullOrEmpty(database))
                throw new ArgumentNullException(nameof(database));

            _connection?.ChangeDatabase(database);
        }

        /// <summary>
        /// Disposes the class
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            _connectionString?.Dispose();
            _connection?.Close();
            _connection?.Dispose();

            _disposed = true;
        }
    }
}
