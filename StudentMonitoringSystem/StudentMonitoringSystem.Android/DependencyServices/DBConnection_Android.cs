using System;
using System.IO;
using SQLite;
using StudentMonitoringSystem.Database;
using StudentMonitoringSystem.Droid.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(DBConnection_Android))]
namespace StudentMonitoringSystem.Droid.DependencyServices
{
    public class DBConnection_Android : IDatabaseConnection
    {

        public SQLiteConnection DBConnection()
        {
            var dbName = "StudentDb.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            if (!File.Exists(path))
            {

            }
            return new SQLiteConnection(path);
        }
    }
}
