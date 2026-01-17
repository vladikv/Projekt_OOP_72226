using Projekt_OOP_72226.Models;
using Projekt_OOP_72226.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_OOP_72226
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database.Initialize();


            var flightRepo = new FlightRepository();
            var passengerRepo = new PassengerRepository();
            var reservationRepo = new ReservationRepository();


            while (true)
            {
                Console.WriteLine(" === SYSTEM REZERWACJI LOTÓW === ");
                Console.WriteLine("1. Lista lotów");
                Console.WriteLine("2. Wyszukaj lot");
                Console.WriteLine("3. Zarezerwuj miejsce");
                Console.WriteLine("4. Anuluj rezerwację");
                Console.WriteLine("5. Lista pasażerów lotu");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybór: ");


                var choice = Console.ReadLine();
                Console.Clear();


                switch (choice)
                {
                    case "1":
                        foreach (var f in flightRepo.GetAll())
                            Console.WriteLine(f);
                        break;


                    case "2":
                        Console.Write("Miasto startu: ");
                        string from = Console.ReadLine();


                        Console.Write("Miasto lądowania: ");
                        string to = Console.ReadLine();


                        var results = flightRepo.Search(from, to);


                        foreach (var r in results)
                            Console.WriteLine(r);
                        break;


                    case "3":
                        Console.Write("ID lotu: ");
                        string flightId = Console.ReadLine();

                        var flight = flightRepo.GetById(flightId);
                        if (flight == null)
                        {
                            Console.WriteLine("Błąd: Lot o podanym ID nie istnieje.");
                        }
                        else if (flight.Seats <= 0)
                        {
                            Console.WriteLine($"Błąd: Brak wolnych miejsc na lot {flight.FlightNumber}.");
                        }
                        else
                        {
                            Console.Write("Imię i nazwisko: ");
                            string name = Console.ReadLine();
                            Console.Write("Kod rezerwacji: ");
                            string code = Console.ReadLine();

                            var passenger = new Passenger { Id = Guid.NewGuid().ToString(), FullName = name };
                            passengerRepo.Add(passenger);

                            reservationRepo.Add(new Reservation
                            {
                                Id = Guid.NewGuid().ToString(),
                                ReservationCode = code,
                                FlightId = flightId,
                                PassengerId = passenger.Id
                            });


                            flightRepo.UpdateSeats(flightId, -1);

                            Console.WriteLine("Rezerwacja wykonana. Miejsce zostało zarezerwowane.");
                        }
                        break;


                    case "4":
                        Console.Write("Kod rezerwacji: ");
                        string reservationCode = Console.ReadLine();
                        reservationRepo.Remove(reservationCode);

                        Console.Write("ID lotu: ");
                        flightId = Console.ReadLine();

                        flightRepo.UpdateSeats(flightId, 1);

                        Console.WriteLine("Rezerwacja anulowana.");
                        break;


                    case "5":
                        Console.Write("ID lotu: ");
                        string id = Console.ReadLine();


                        foreach (var p in reservationRepo.GetPassengersByFlight(id))
                            Console.WriteLine(p);
                        break;


                    case "0":
                        return;
                }
            }
        }
    }

   
}
