using System;
using System.IO;

namespace AcessHome.Data
{
    public class Constants
    {
        public const string DatabaseFileName = "AccessHome.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create;

        /// <summary>
        /// static initialization its only needed to acces the propierty
        /// to get the path and create the database if it doesn't exist.
        /// </summary>
        public static string DataBasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFileName);
            }
        }
    }
}