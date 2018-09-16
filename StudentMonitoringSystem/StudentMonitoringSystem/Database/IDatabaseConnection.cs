using System;
using SQLite;

namespace StudentMonitoringSystem.Database
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DBConnection();
    }
}
