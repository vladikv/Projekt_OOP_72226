using Microsoft.Data.SqlClient;
using System;


namespace Projekt_OOP_72226
{
    public static class Database
    {
        private const string DB_NAME = @"Data Source=VLADOS;Initial Catalog=Projekt_OOP;
                                        Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";


        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(DB_NAME);
            conn.Open();
            return conn;
        }


        public static void Initialize()
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Flights')
                CREATE TABLE Flights (
                    Id NVARCHAR(50) PRIMARY KEY,
                    FlightNumber NVARCHAR(50),
                    FromCity NVARCHAR(100),
                    ToCity NVARCHAR(100),
                    Date NVARCHAR(50),
                    Seats INT,
                    Price FLOAT
                );

                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Passengers')
                CREATE TABLE Passengers (
                    Id NVARCHAR(50) PRIMARY KEY,
                    FullName NVARCHAR(200)
                );

                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Reservations')
                CREATE TABLE Reservations (
                    Id NVARCHAR(50) PRIMARY KEY,
                    ReservationCode NVARCHAR(50),
                    FlightId NVARCHAR(50),
                    PassengerId NVARCHAR(50)
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
                int count = (int)cmd.ExecuteScalar();

                if (count > 0) return;

                cmd.CommandText = @"
                INSERT INTO Flights (Id, FlightNumber, FromCity, ToCity, Date, Seats, Price) VALUES
                ('1','LO101','Warszawa','Londyn','2025-04-01 08:00',160,550),
                ('2','LH202','Berlin','Monachium','2025-04-02 09:30',120,320),
                ('3','AF303','Paryż','Rzym','2025-04-03 11:15',140,410),
                ('4','W6123','Kraków','Barcelona','2025-05-01 14:20',180,450),
                ('5','FR456','Warszawa','Dublin','2025-05-02 10:00',189,280),
                ('6','LO789','Gdańsk','Oslo','2025-05-03 07:45',78,390),
                ('7','LX111','Zurych','Wiedeń','2025-05-04 12:30',110,620),
                ('8','BA222','Londyn','Nowy Jork','2025-05-05 16:00',250,2100),
                ('9','QR333','Doha','Warszawa','2025-05-06 22:15',300,1850),
                ('10','TK444','Stambuł','Antalya','2025-05-07 09:00',160,150),
                ('11','PS555','Kyiv','Praga','2025-05-08 13:40',145,480),
                ('12','AY666','Helsinki','Rovaniemi','2025-05-09 11:10',90,310),
                ('13','SK777','Kopenhaga','Sztokholm','2025-05-10 08:30',120,240),
                ('14','AZ888','Mediolan','Madryt','2025-05-11 15:50',140,510),
                ('15','KL999','Amsterdam','Lizbona','2025-05-12 18:20',170,590),
                ('16','LO001','Warszawa','Tokio','2025-05-13 23:00',280,3500),
                ('17','UA002','Chicago','San Francisco','2025-05-14 10:45',200,420),
                ('18','FR003','Wrocław','Bolonia','2025-05-15 06:15',189,190),
                ('19','W6004','Katowice','Eindhoven','2025-05-16 12:00',180,220),
                ('20','LO005','Kraków','Tel Awiw','2025-05-17 05:30',160,740),
                ('21','LH006','Frankfurt','Paryż','2025-05-18 19:10',130,480),
                ('22','AF007','Marsylia','Lyon','2025-05-19 14:00',80,120),
                ('23','RY008','Budapeszt','Ateny','2025-05-20 17:45',189,330);";

                cmd.ExecuteNonQuery();
            }
        }
    }
}
