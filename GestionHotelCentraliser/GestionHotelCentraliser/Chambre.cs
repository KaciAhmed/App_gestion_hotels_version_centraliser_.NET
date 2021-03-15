using System;
using System.Collections.Generic;
using System.Text;

namespace GestionHotelCentraliser
{
    class Chambre
    {
       public int Numero { get; set; }
       public int NbLit { get; set; }
       public float Prix { get; set; }

       public bool EstLibre { get; set; }

       public Hotel Hotel { get; set; }
       public IList <Reservation> HistoriqueReservations { get; set; }

        public Chambre()
        {
            HistoriqueReservations = new List<Reservation>();
        }

        public Chambre(int numero, int nbLit, float prix, bool estLibre)
        {
            Numero = numero;
            NbLit = nbLit;
            Prix = prix;
            EstLibre = estLibre;
            HistoriqueReservations = new List<Reservation>();
        }

        public Chambre(int numero, int nbLit, float prix, Hotel hotel, IList<Reservation> historiqueReservations)
        {
            Numero = numero;
            NbLit = nbLit;
            Prix = prix;
            Hotel = hotel;
            HistoriqueReservations = historiqueReservations;
        }


    }
}
