using System;
using System.Runtime.InteropServices;
using System.Security;

namespace ZimLabs.Database.MySql
{
    /// <summary>
    /// Provides several helper functions
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Converts a secure string into a normal string
        /// </summary>
        /// <param name="value">The secure string</param>
        /// <returns>The insecure string</returns>
        /// <exception cref="ArgumentNullException">Will be thrown when the parameter is null</exception>
        public static string ToInsecureString(this SecureString value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        /// <summary>
        /// Converts a string into a secure string
        /// </summary>
        /// <param name="value">The string</param>
        /// <returns>The secure string</returns>
        /// <exception cref="ArgumentNullException">Will be thrown when the parameter is null</exception>
        public static SecureString ToSecureString(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var secureString = new SecureString();

            foreach (var c in value)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }

        /// <summary>
        /// Converts the settings based on the connection type into an info string
        /// </summary>
        /// <param name="type">The info type</param>
        /// <param name="settings">The settings</param>
        /// <returns>The converted info</returns>
        internal static string GetConnectionInfo(ConnectionInfoType type, DatabaseSettings settings)
        {
            var server = $"Server: {settings.Server}";
            var database = $"Database: {settings.Database}";
            var user = $"User: {settings.UserId}";
            var port = $"Port: {settings.Port}";
            var timeout = $"Timeout: {settings.ConnectionTimeout}";

            return (int)type switch
            {
                0 => "",
                1 => server,
                2 => database,
                3 => $"{server}; {database}",
                4 => user,
                5 => $"{server}; {user}",
                6 => $"{database}; {user}",
                7 => $"{server}; {database}; {user}",
                8 => port,
                9 => $"{server}; {port}",
                10 => $"{database}; {port}",
                11 => $"{server}; {database}; {port}",
                12 => $"{user}; {port}",
                13 => $"{server}; {user}; {port}",
                14 => $"{database}; {user}; {port}",
                15 => $"{server}; {database}; {user}; {port}",
                16 => timeout,
                17 => $"{server}; {timeout}",
                18 => $"{database}; {timeout}",
                19 => $"{server}; {database}; {timeout}",
                20 => $"{user}; {timeout}",
                21 => $"{server}; {user}; {timeout}",
                22 => $"{database}; {user}; {timeout}",
                23 => $"{server}; {database}; {user}; {timeout}",
                24 => $"{port}; {timeout}",
                25 => $"{server}; {port}; {timeout}",
                26 => $"{database}; {port}; {timeout}",
                27 => $"{server}; {database}; {port}; {timeout}",
                28 => $"{user}; {port}; {timeout}",
                29 => $"{server}; {user}; {port}; {timeout}",
                30 => $"{database}; {user}; {port}; {timeout}",
                31 => $"{server}; {database}; {user}; {port}; {timeout}",
                32 => "",
                _ => ""
            };
        }
    }
}
