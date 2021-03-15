using System;
using System.Collections.Generic;

namespace GestionHotelCentraliser
{
    class Program
    {
        // Generates a random number within a range.
        public static int RandomNumber(int min, int max)  
        {
            Random _random = new Random();
            return _random.Next(min, max);  
        }

        public static float RandomNumberFloat(int min, int max)  
        {
            Random _random = new Random();
            return (float) _random.Next(min, max);  
        }

        private static IList <Hotel> initialiserHotels(int nombreHotels)
        {
            IList<Hotel> hotels = new List<Hotel>();
            Chambre chambre;
            int prix;
            int nbLit;
            int nbChambres = 10;
            Hotel hotel;
            for (int i=0;i<nombreHotels; i++)
            {
               
                hotel = new Hotel("Hotel "+i,i,"rue "+i,"ville "+i,"pays "+i,"positionGPS "+i,"lieu Dit "+i,RandomNumberFloat(0,5));
                for(int j = 0; j < nbChambres; j++)
                {
                    prix = RandomNumber(0, 100);
                    nbLit = RandomNumber(1,3);
                    chambre = new Chambre(j, nbLit, prix, true);
                    hotel.Chambres.Add(chambre);

                }
                hotels.Add(hotel);
            }
            return hotels;
        }

        public static void TraitrerClient(IList<Hotel> tousLesHotels)
        {
            string villeSejour;
            string dateArrivee;
            string dateDepart;
            float prixMin;
            float prixMax;
            float nombreEtoiles;
            int nombrePersonnes;
            int continuer = 1;
            
            IList<Hotel> hotelsResultat = new List<Hotel>();

            while(continuer==1)
            {
                Console.WriteLine("Ville de sejour ? ");
                villeSejour = Console.ReadLine();
                Console.WriteLine("Date d'arriver ? DD/MM/YY ");
                dateArrivee = Console.ReadLine();
                Console.WriteLine("Date de depart ? DD/MM/YY ");
                dateDepart = Console.ReadLine();
                Console.WriteLine("Prix minimum de la chambre ? ");
                prixMin = float.Parse(Console.ReadLine());
                Console.WriteLine("Prix maximum de la chambre ? ");
                prixMax = float.Parse(Console.ReadLine());
                Console.WriteLine("Nombre d'etoile ? ");
                nombreEtoiles = float.Parse(Console.ReadLine());
                Console.WriteLine("Nombre de personne a heberger (entre 1 et 3), vous pouvez faire plusieurs reservations ? ");
                nombrePersonnes = int.Parse(Console.ReadLine());
                hotelsResultat = rechercherHotels(tousLesHotels,villeSejour,dateArrivee,dateDepart,prixMin,prixMax,nombreEtoiles,nombrePersonnes);
                // affichage des hotels résultats
                foreach(Hotel hotel in hotelsResultat)
                {
                    afficherHotelDisponible(hotel, nombrePersonnes);
                }
                Console.WriteLine("Pour effectuer une nouvelle reservation tapez 1");
                continuer = int.Parse(Console.ReadLine());
            }
        }

        public static void afficherHotelDisponible(Hotel hotel, int nbLit)
        {
            foreach(Chambre chambre in hotel.Chambres)
            {
                if(chambre.NbLit == nbLit)
                {
                    Console.WriteLine("Hotel : Nom = {0} , Numero = {1} , Rue ={2} , Ville = {3} , Pays = {4}, PositionGPS = {5} , lieuDit = {6} , Prix = {7}, Nombre d'étoiles = {8}",hotel.Nom, hotel.Numero, hotel.Rue, hotel.Ville, hotel.Pays, hotel.PositionGPS, hotel.LieuDit, chambre.Prix, hotel.NbEtoile);
                }
            }

        }

        public static IList <Hotel> rechercherHotels(IList<Hotel> tousLesHotels,string villeSejour, string dateArrivee, string dateDepart, float prixMin, float prixMax, float nombreEtoiles, int nombrePersonnes)
        {
            IList<Hotel> hotelsResultat = new List<Hotel>();
            float prixMinTemp;
            float prixMaxTemp;

            foreach(Hotel hotel in tousLesHotels)
            {
                // nbEtoile , ville , prix min <prix <prix Max, nbchambre = nbPersonne

                if (hotel.Ville.Equals(villeSejour) && nombreEtoiles <= hotel.NbEtoile)
                {
                    prixMinTemp = hotel.getPrixMinChambresDisponible(nombrePersonnes);
                    prixMaxTemp = hotel.getPrixMaxChambresDisponibles(nombrePersonnes);
                    if(prixMin <= prixMinTemp && prixMaxTemp <= prixMax )
                    {
                        hotelsResultat.Add(hotel);
                    }
                    
                }
            }
            return hotelsResultat;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Gestion d'Hotel Centraliser !");

            // init
            IList<Hotel>listHotels = initialiserHotels(5);
            // saisie + recherche
            TraitrerClient(listHotels);
            // saisie info user
            // reserver

        }
    }
}