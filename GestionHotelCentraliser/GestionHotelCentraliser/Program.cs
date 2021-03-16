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
            String nomHotel;
            float nbEtoile;
            for (int i=0;i<nombreHotels; i++)
            {
                nomHotel = "Hotel " + RandomNumberFloat(0, nombreHotels);
                nbEtoile = RandomNumberFloat(0, 5);
                hotel = new Hotel(nomHotel, i,"rue "+i,"ville "+i,"pays "+i,"positionGPS "+i,"lieu Dit "+i,nbEtoile);
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
            string nomHotelChoisis;
            Hotel hotelChoisis;
            Chambre chambreChoisis;
            int numChambreChoisis;
            string nomClient;
            string prenomClient;
            string infoCarteCreditClient="";
            Client client=null;
            Reservation reservation;



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
                Console.WriteLine("Nombre de personne a heberger (entre 1 et 3) ? ");
                nombrePersonnes = int.Parse(Console.ReadLine());
                hotelsResultat = rechercherHotels(tousLesHotels,villeSejour,dateArrivee,dateDepart,prixMin,prixMax,nombreEtoiles,nombrePersonnes);
                // affichage des hotels résultats
                foreach(Hotel hotel in hotelsResultat)
                {
                    afficherHotelDisponible(hotel, nombrePersonnes);
                }
                do
                {
                    hotelChoisis = null;
                    Console.WriteLine("Veuillez saisir le nom de l'hotel dans lequel vous souhaitez effectuer une réservation");
                    nomHotelChoisis = Console.ReadLine();
                    hotelChoisis = chercherHotelParNom(tousLesHotels,nomHotelChoisis);
                    if (hotelChoisis == null)
                    {
                        Console.WriteLine("le nom de l'hotel saisis est incorrecte");
                    }
                } while (hotelChoisis == null);
                do
                {
                    chambreChoisis = null;
                    Console.WriteLine("Veuillre saisir le numéro de la chambre que vous souhaiter réserver");
                    numChambreChoisis = int.Parse(Console.ReadLine());
                    chambreChoisis = chercherChambreParNumero(hotelChoisis, numChambreChoisis, nombrePersonnes);
                    if (chambreChoisis == null)
                    {
                        Console.WriteLine("le numéro de la chambre saisis est incorrecte");
                    }
                } while (chambreChoisis == null);
                 

                if(client == null)
                {
                    Console.WriteLine("Veuillre saisir votre nom svp");
                    nomClient = Console.ReadLine();
                    Console.WriteLine("Veuillre saisir votre prénom svp");
                    prenomClient = Console.ReadLine();
                    Console.WriteLine("Veuillez inserez les informations de la catre de crédit de paiement");
                    infoCarteCreditClient = Console.ReadLine();
                    client = new Client(nomClient, prenomClient);
                }
                reservation = new Reservation(dateArrivee, dateDepart, nombrePersonnes, infoCarteCreditClient, client,chambreChoisis);
                client.Reservations.Add(reservation);
                Console.WriteLine("********************Reservation éffecuter avec succées**********************");
                Console.WriteLine("Pour effectuer une nouvelle reservation tapez 1 / autre pour quitter");
                continuer = int.Parse(Console.ReadLine());
            }
        }
        public static Hotel chercherHotelParNom(IList<Hotel> hotels ,String nom)
        {
            foreach(Hotel hotel in hotels)
            {
                if (hotel.Nom.Equals(nom))
                {
                    return hotel;
                }
            }
            return null;
        }
        public static Chambre chercherChambreParNumero(Hotel hotel, int numero,int nbLit)
        {
            foreach(Chambre chambre in hotel.Chambres)
            {
                if (chambre.Numero == numero && chambre.NbLit==nbLit)
                    return chambre;
            }
            return null;

        }
      
      
        public static void afficherHotelDisponible(Hotel hotel, int nbLit)
        {
            foreach(Chambre chambre in hotel.Chambres)
            {
                if(chambre.NbLit == nbLit)
                {
                    Console.WriteLine("Hotel : Nom = {0} , Numero = {1} , Rue ={2} , Ville = {3} , Pays = {4}, PositionGPS = {5} , lieuDit = {6} , Prix = {7}, Nombre d'étoiles = {8}, Nombre de lits Proposer = {9}, Numéro de la chambre = {10}",hotel.Nom, hotel.Numero, hotel.Rue, hotel.Ville, hotel.Pays, hotel.PositionGPS, hotel.LieuDit, chambre.Prix, hotel.NbEtoile,chambre.NbLit,chambre.Numero);
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
              

                if (hotel.Ville.Equals(villeSejour) && nombreEtoiles <= hotel.NbEtoile)
                {
                    prixMinTemp = hotel.getPrixMinChambresDisponible(nombrePersonnes);
                    prixMaxTemp = hotel.getPrixMaxChambresDisponibles(nombrePersonnes);
                    if(prixMin <= prixMinTemp && prixMaxTemp <= prixMax)
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

    
            IList<Hotel>listHotels = initialiserHotels(5);
            TraitrerClient(listHotels);
        

        }
    }
}