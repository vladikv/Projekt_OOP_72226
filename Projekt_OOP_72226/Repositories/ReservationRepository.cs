using Projekt_OOP_72226.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_OOP_72226.Repositories
{
    public class ReservationRepository
    {
        public void Add(Reservation r)
        {
            using (var conn = Database.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Reservations VALUES (@id,@code,@flight,@passenger)";
                cmd.Parameters.AddWithValue("@id", r.Id);
                cmd.Parameters.AddWithValue("@code", r.ReservationCode);
                cmd.Parameters.AddWithValue("@flight", r.FlightId);
                cmd.Parameters.AddWithValue("@passenger", r.PassengerId);


                cmd.ExecuteNonQuery();
            }
        }

        public void Remove(string reservationCode)
        {
            using (var conn = Database.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Reservations WHERE ReservationCode=@code";
                cmd.Parameters.AddWithValue("@code", reservationCode);


                cmd.ExecuteNonQuery();
            }
        }

        public List<Passenger> GetPassengersByFlight(string flightId)
        {
            var list = new List<Passenger>();


            using (var conn = Database.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                SELECT Passengers.Id, Passengers.FullName
                FROM Reservations
                JOIN Passengers ON Reservations.PassengerId = Passengers.Id
                WHERE Reservations.FlightId=@id
                ";


                cmd.Parameters.AddWithValue("@id", flightId);


                using (var reader = cmd.ExecuteReader()) 
                while (reader.Read())
                {
                    list.Add(new Passenger
                    {
                        Id = reader.GetString(0),
                        FullName = reader.GetString(1)
                    });
                }
            }


            return list;
        }

        
        public string GetFlightIdByCode(string code)
        {
            using (var conn = Database.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT FlightId FROM Reservations WHERE ReservationCode = @code";
                cmd.Parameters.AddWithValue("@code", code);
                var result = cmd.ExecuteScalar();
                return result?.ToString(); 
            }
        }
    }
}
