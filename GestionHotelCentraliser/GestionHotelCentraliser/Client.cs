using System;
using System.Collections.Generic;
using System.Text;

namespace GestionHotelCentraliser
{
    class Client
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public IList<Reservation> Reservations { get; set; }

        public Client()
        {
            Reservations = new List<Reservation>();
        }

        public Client(string nom, string prenom, string adresse, string telephone, IList<Reservation> reservations)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Telephone = telephone;
            Reservations = reservations;
        }
    }
}
