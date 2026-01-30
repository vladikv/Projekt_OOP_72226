using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Data.SqlClient;

using Projekt_OOP_72226.Models;

namespace Projekt_OOP_72226
{
    public class FlightRepository
    {
        public Flight Flight
        {
            get => default;
            set
            {
            }
        }

        public Flight Flight1
        {
            get => default;
            set
            {
            }
        }

        public Database Database
        {
            get => default;
            set
            {
            }
        }

        public List<Flight> GetAll()
        {
            var list = new List<Flight>();

            using (var conn = Database.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Flights";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Flight
                        {
                            Id = reader.GetString(0),
                            FlightNumber = reader.GetString(1),
                            FromCity = reader.GetString(2),
                            ToCity = reader.GetString(3),
                            Date = reader.GetString(4),
                            Seats = reader.GetInt32(5),
                            Price = reader.GetDouble(6)
                        });
                    }
                }
            }

            return list;
        }

        public List<Flight> Search(string from, string to)
        {
            var list = new List<Flight>();


            using (var conn = Database.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Flights WHERE FromCity=@from OR ToCity=@to";
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);


                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        list.Add(new Flight
                        {
                            Id = reader.GetString(0),
                            FlightNumber = reader.GetString(1),
                            FromCity = reader.GetString(2),
                            ToCity = reader.GetString(3),
                            Date = reader.GetString(4),
                            Seats = reader.GetInt32(5),
                            Price = reader.GetDouble(6)
                        });
                    }
            }

            return list;
        }

        public Flight GetById(string id)
        {
            using (var conn = Database.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Flights WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Flight
                        {
                            Id = reader.GetString(0),
                            FlightNumber = reader.GetString(1),
                            FromCity = reader.GetString(2),
                            ToCity = reader.GetString(3),
                            Date = reader.GetString(4),
                            Seats = reader.GetInt32(5),
                            Price = reader.GetDouble(6)
                        };
                    }
                }
            }
            return null;
        }
        public void UpdateSeats(string flightId, int change)
        {
            using (var conn = Database.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE Flights SET Seats = Seats + @change WHERE Id = @id";
                cmd.Parameters.AddWithValue("@change", change);
                cmd.Parameters.AddWithValue("@id", flightId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
