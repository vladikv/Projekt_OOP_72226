using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_OOP_72226.Models
{
    public class Reservation
    {
        public string Id { get; set; }
        public string ReservationCode { get; set; }
        public string FlightId { get; set; }
        public string PassengerId { get; set; }

        public Flight Flight
        {
            get => default;
            set
            {
            }
        }

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

        public Flight Flight1
        {
            get => default;
            set
            {
            }
        }
    }
}
