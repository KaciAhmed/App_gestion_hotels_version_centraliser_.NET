using System;
using System.Collections.Generic;
using System.Text;

namespace GestionHotelCentraliser
{
    class Reservation
    {
        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public int NbPersonne { get; set; }

        public string InfoCarteCreditPaiement { get; set; }
        public Client Client { get; set; }

        public Chambre Chambre { get; set; }


        public float Prix {
            get => Chambre.Prix;
        }
        public Reservation(){}

        public Reservation(string dateDebut, string dateFin, int nbPersonne, string infoCarteCreditPaiement, Client client, Chambre chambre)
        {
            DateDebut = dateDebut;
            DateFin = dateFin;
            NbPersonne = nbPersonne;
            InfoCarteCreditPaiement = infoCarteCreditPaiement;
            Client = client;
            Chambre = chambre;
        }
    }
}
