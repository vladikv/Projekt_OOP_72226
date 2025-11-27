using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_OOP_72226.Models
{
    public class Flight
    {
        public string Id { get; set; }
        public string FlightNumber { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string Date { get; set; }
        public int Seats { get; set; }
        public double Price { get; set; }


        public override string ToString()
        => $"[{Id}] {FlightNumber} | {FromCity} -> {ToCity} | {Date} | {Seats} miejsc | {Price} zł";
    }
}
