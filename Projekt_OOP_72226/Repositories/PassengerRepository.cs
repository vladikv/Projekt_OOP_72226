using Projekt_OOP_72226.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_OOP_72226.Repositories
{
    public class PassengerRepository
    {
        public Database d;
        public Passenger Passenger
        {
            get => default;
            set
            {
            }
        }

        public Passenger Passenger1
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

        public void Add(Passenger p)
        {
            using (var conn = Database.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Passengers VALUES (@id,@name)";
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@name", p.FullName);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
