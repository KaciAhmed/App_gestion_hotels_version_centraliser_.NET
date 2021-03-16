using System;
using System.Collections.Generic;
using System.Text;

namespace GestionHotelCentraliser
{
    class Client
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public IList<Reservation> Reservations { get; set; }

        public Client()
        {
            Reservations = new List<Reservation>();
        }
        public Client(string nom, string prenom)
        {
            Nom = nom;
            Prenom = prenom;
            Reservations = new List<Reservation>();
        }

        public Client(string nom, string prenom,IList<Reservation> reservations)
        {
            Nom = nom;
            Prenom = prenom;
            Reservations = reservations;
        }
    }
}