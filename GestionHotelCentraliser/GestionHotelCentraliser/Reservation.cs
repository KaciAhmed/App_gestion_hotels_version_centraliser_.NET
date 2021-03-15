using System;
using System.Collections.Generic;
using System.Text;

namespace GestionHotelCentraliser
{
    class Reservation
    {
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int NbPersonne { get; set; }

        public Client Client { get; set; }

        public IList<Chambre> Chambres { get; set; }

        public Reservation()
        {
            Chambres = new List<Chambre>();
        }

        public Reservation(DateTime dateDebut, DateTime dateFin, int nbPersonne, Client client, IList<Chambre> chambres)
        {
            DateDebut = dateDebut;
            DateFin = dateFin;
            NbPersonne = nbPersonne;
            Client = client;
            Chambres = chambres;
        }

        // vérifier que le nombre  de personne corespond au nobre de lits des chambres réserver

        public float calculerPrix()
        {
            float prixTotal=0;
            foreach(Chambre c in Chambres)
            {
                prixTotal += c.Prix;
            }
            return prixTotal;
        }
    }
}
