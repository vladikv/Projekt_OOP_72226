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
    }
}
