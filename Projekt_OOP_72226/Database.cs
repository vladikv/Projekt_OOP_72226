using Microsoft.Data.Sqlite;
using System.IO;

namespace Projekt_OOP_72226
{
    public static class Database
    {
        private const string DB_NAME = "flights.db";


        public static SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection($"Data Source={DB_NAME}");
            conn.Open();
            return conn;
        }


        public static void Initialize()
        {
            if (!File.Exists(DB_NAME))
                File.Create(DB_NAME).Close();


            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            { 
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Flights (
                Id TEXT PRIMARY KEY,
                FlightNumber TEXT,
                FromCity TEXT,
                ToCity TEXT,
                Date TEXT,
                Seats INTEGER,
                Price REAL
                );


                CREATE TABLE IF NOT EXISTS Passengers (
                Id TEXT PRIMARY KEY,
                FullName TEXT
                );


                CREATE TABLE IF NOT EXISTS Reservations (
                Id TEXT PRIMARY KEY,
                ReservationCode TEXT,
                FlightId TEXT,
                PassengerId TEXT
                );
                ";
                cmd.ExecuteNonQuery();
            }

            Seed();
        }


        private static void Seed()
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM Flights";
                long count = (long)cmd.ExecuteScalar();

                if (count > 0) return;

                cmd.CommandText = @"
                INSERT INTO Flights VALUES
                ('1','LO101','Warszawa','Londyn','2025-04-01 08:00',160,550),
                ('2','LH202','Berlin','Monachium','2025-04-02 09:30',120,320),
                ('3','AF303','Paryż','Rzym','2025-04-03 11:15',140,410);
                ";

                cmd.ExecuteNonQuery();
            }
        }
    }
}
