using System;
using System.Collections.Generic;
using System.Text;

namespace GestionHotelCentraliser
{
    class Hotel
    {
        public string Nom { get; set; }
        public int Numero { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public string PositionGPS { get; set; }
        public string LieuDit { get; set; }
        public float NbEtoile { get; set; }
        public IList<Chambre> Chambres { get; set; }
        /// <summary>
        /// Petite usine pour un ensemble d'hotels.
        /// </summary>
        /// <param name="nombreHotelsMax">Le nombre total d'hotel souhaiter</param>
        /// <returns>Un ensemble d'hotel avec des informations aleatoires.</returns>
        public static IList<Hotel> creerHotels(int nombreHotelsMax)
        {
            IList<Hotel> hotels = new List<Hotel>();
            Hotel hotelReceveur = null;
            for (int i = 0; i < nombreHotelsMax; i++)
            {
                hotelReceveur = creerHotel(i);
                hotels.Add(hotelReceveur);
            }
            return hotels;
        }
        /// <summary>
        /// Petite usine pour un hotel.
        /// </summary>
        /// <param name="numeroHotel">le numero affecter a l'hotel </param>
        /// <returns>Un hotel avec champs aleatoires.</returns>
        private static Hotel creerHotel(int numeroHotel)
        {
            Hotel hotel;
            String nomHotel = "Hotel " + MyRandom.RandomNumberFloat(0, numeroHotel);
            float nbEtoile = MyRandom.RandomNumberFloat(0, 5);
            int nbChambres = 10;
            hotel = new Hotel(nomHotel,
                              MyRandom.RandomNumber(0, 125),
                              "rue " + MyRandom.RandomNumber(0, 125),
                              "ville " + MyRandom.RandomNumber(0, 2),
                              "pays " + MyRandom.RandomNumber(0, 125),
                              "positionGPS " + MyRandom.RandomNumber(0, 125),
                              "lieu Dit " + MyRandom.RandomNumber(0, 125),
                              nbEtoile);
            hotel.Chambres = Chambre.creerChambresAleatoire(nbChambres); ;
            return hotel;
        }
        public float getPrixMinChambresDisponible(int nbLit)
        {
            float prixMin =-1;
            bool b = false;
            foreach(Chambre c in Chambres)
            {
                if (!b)
                {
                    if (c.NbLit == nbLit && c.EstLibre)
                    {
                        prixMin = c.Prix;
                        b = true;
                    }
                }
                else
                {
                    if (c.NbLit == nbLit && c.EstLibre && c.Prix < prixMin)
                    {
                        prixMin = c.Prix;
                    }
                }
            }
            return prixMin;
        }

        public float getPrixMaxChambresDisponibles(int nbLit)
        {
            float prixMax = -1;
            bool b = false;
            foreach (Chambre c in Chambres)
            {
                if (!b)
                {
                    if (c.NbLit == nbLit && c.EstLibre)
                    {
                        prixMax = c.Prix;
                        b = true;
                    }
                }
                else
                {
                    if (c.NbLit == nbLit && c.EstLibre && prixMax < c.Prix)
                    {
                        prixMax = c.Prix;
                    }
                }
            }
            return prixMax;
        }
        public Hotel()
        {
            Chambres = new List<Chambre>();
        }
        public Hotel(string nom, int numero, string rue, string ville, string pays, string positionGPS, string lieuDit, float nbEtoile)
        {
            Nom = nom;
            Numero = numero;
            Rue = rue;
            Ville = ville;
            Pays = pays;
            PositionGPS = positionGPS;
            LieuDit = lieuDit;
            NbEtoile = nbEtoile;
            Chambres = new List<Chambre>();
        }
        /// <summary>
        /// Verifie si un ensemble de contrainte est satisfiable par l'instance de l'hotel.
        /// </summary>
        /// <param name="villeSejour"></param>
        /// <param name="prixMin"></param>
        /// <param name="prixMax"></param>
        /// <param name="nombreEtoiles"></param>
        /// <param name="nombrePersonnes"></param>
        /// <returns>Vrai si toutes les contraintes sont satisfaites, faux dans tous les autres cas.</returns>
        internal bool satisfaitContrainte(string villeSejour, float prixMin, float prixMax, float nombreEtoiles, int nombrePersonnes)
        {
            float prixMinTemp;
            float prixMaxTemp;
            if (Ville.Equals(villeSejour) && nombreEtoiles <= NbEtoile)
            {
                prixMinTemp = getPrixMinChambresDisponible(nombrePersonnes);
                prixMaxTemp = getPrixMaxChambresDisponibles(nombrePersonnes);
                if (prixMin <= prixMinTemp && prixMaxTemp <= prixMax)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
