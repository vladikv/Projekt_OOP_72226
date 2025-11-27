using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_OOP_72226.Models
{
    public class Passenger
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public override string ToString() => FullName;
    }
}
