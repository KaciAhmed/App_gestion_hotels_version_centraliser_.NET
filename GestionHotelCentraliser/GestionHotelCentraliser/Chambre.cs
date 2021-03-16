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
        /// <summary>
        /// Petite usine pour un ensemble de chambres.
        /// </summary>
        /// <param name="nombreDeChambres">Nombre total de chambre a creer.</param>
        /// <returns>Un ensemble de chambres aux caracteristique aleatoires.</returns>
        public static IList<Chambre> creerChambresAleatoire(int nombreDeChambres)
        {
            IList<Chambre> chambres = new List<Chambre>();
            for (int j = 0; j < nombreDeChambres; j++)
            {
                int numeroChambre = j;
                chambres.Add(creerChambre(numeroChambre));
            }
            return chambres;
        }
        /// <summary>
        /// Petite usine pour une chambre
        /// </summary>
        /// <param name="numeroChambre">Numero a affecter a la chambre retourner.</param>
        /// <returns>Une chambre aux caracteristique aleatoires.</returns>
        private static Chambre creerChambre(int numeroChambre)
        {
            Chambre chambre;
            int prix;
            int nbLit;
            prix = MyRandom.RandomNumber(0, 100);
            nbLit = MyRandom.RandomNumber(1, 3);
            chambre = new Chambre(numeroChambre, nbLit, prix, true);
            return chambre;
        }
        public Chambre(int numero, int nbLit, float prix, bool estLibre)
        {
            Numero = numero;
            NbLit = nbLit;
            Prix = prix;
            EstLibre = estLibre;
            HistoriqueReservations = new List<Reservation>();
        }

        public Chambre()
        {
            HistoriqueReservations = new List<Reservation>();
        }
    }
}
