using System;
using System.Collections.Generic;

namespace GestionHotelCentraliser
{
    class Program
    {
        private static IList <Hotel> initialiserHotels(int nombreHotelsMax)
        {
            return Hotel.creerHotels(nombreHotelsMax);
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
            Hotel hotelChoisis = null;
            Chambre chambreChoisis = null;
            int numChambreChoisis;
            string nomClient;
            string prenomClient;
            string infoCarteCreditClient = "";
            Client client = null;
            Reservation reservation;



            IList<Hotel> hotelsResultat = new List<Hotel>();

            while(continuer==1)
            {
                // saisie
                villeSejour = saisie("ville");
                dateArrivee = saisie("date d'arriver");
                dateDepart = saisie("date de depart");
                prixMin = float.Parse(saisie("prix minimum"));
                prixMax = float.Parse(saisie("prix maximum"));
                nombreEtoiles = float.Parse(saisie("nombre d'etoile minimum"));
                nombrePersonnes = int.Parse(saisie("nombre de personne a heberger entre 1 et 3 (refaite une reservation pour plus de personne)"));

                // recherche selon les criteres ci dessus
                hotelsResultat = rechercherHotels(tousLesHotels,villeSejour,dateArrivee,dateDepart,prixMin,prixMax,nombreEtoiles,nombrePersonnes);

                // affichage des hotels satisfaisant la requete precedente
                foreach(Hotel hotel in hotelsResultat)
                {
                    afficherHotelDisponible(hotel, nombrePersonnes);
                }
                
                // saisie des informations pour la reservation
                do
                {
                    if (hotelsResultat.Count > 0)
                    {
                        
                        Console.WriteLine("Veuillez saisir le nom de l'hotel dans lequel vous souhaitez effectuer une reservation");
                        nomHotelChoisis = Console.ReadLine();
                        hotelChoisis = chercherHotelParNom(tousLesHotels, nomHotelChoisis);
                        if (hotelChoisis == null)
                        {
                            Console.WriteLine("le nom de l'hotel saisis est incorrecte");
                        }
                    }else
                    {
                        Console.WriteLine("Aucun resultat.");

                        // trois initialisation parce que si il n'y a pas d'hotel a choisir, les deux boucle qui suivent sont inutiles
                        hotelChoisis = new Hotel();
                        chambreChoisis = new Chambre();
                        client = new Client();
                    }
                } while (hotelChoisis == null);
                
                do
                { 
                    Console.WriteLine("Veuillre saisir le numero de la chambre que vous souhaiter reserver");
                    numChambreChoisis = int.Parse(Console.ReadLine());
                    chambreChoisis = chercherChambreParNumero(hotelChoisis, numChambreChoisis, nombrePersonnes);
                    if (chambreChoisis == null)
                    {
                        Console.WriteLine("le numero de la chambre saisis est incorrecte");
                    }
                } while (chambreChoisis == null);

                if(client == null)
                {
                    Console.WriteLine("Veuillre saisir votre nom svp");
                    nomClient = Console.ReadLine();
                    Console.WriteLine("Veuillre saisir votre prenom svp");
                    prenomClient = Console.ReadLine();
                    Console.WriteLine("Veuillez inserez les informations de la catre de credit de paiement");
                    infoCarteCreditClient = Console.ReadLine();
                    client = new Client(nomClient, prenomClient);
                }
                reservation = new Reservation(dateArrivee, dateDepart, nombrePersonnes, infoCarteCreditClient, client, chambreChoisis);
                client.Reservations.Add(reservation);
                Console.WriteLine("********************Transaction effecuter avec succees**********************");
                Console.WriteLine("Pour effectuer une nouvelle reservation tapez 1 / autre pour quitter");
                continuer = int.Parse(Console.ReadLine());
            }
        }
        private static string saisie(string nomAfficher)
        {
            Console.WriteLine("Saisir " + nomAfficher + " ?");
            string res = Console.ReadLine();
            return res;
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
                    Console.WriteLine($"Nom = {hotel.Nom} , a l'adresse {hotel.Numero}, {hotel.Rue} , {hotel.Ville} , {hotel.Pays}, {hotel.PositionGPS} , {hotel.LieuDit} , au prix de {chambre.Prix:C}, Nombre d'etoiles = {hotel.NbEtoile}, Nombre de lits proposer = {chambre.NbLit}, chambre N° = {chambre.Numero}");
                }
            }

        }
        public static IList <Hotel> rechercherHotels(IList<Hotel> tousLesHotels,string villeSejour, string dateArrivee, string dateDepart, float prixMin, float prixMax, float nombreEtoiles, int nombrePersonnes)
        {
            IList<Hotel> hotelsResultat = new List<Hotel>();
            foreach(Hotel hotel in tousLesHotels)
            {
                if(hotel.satisfaitContrainte(villeSejour, prixMin, prixMax, nombreEtoiles, nombrePersonnes))
                {
                    hotelsResultat.Add(hotel);
                }
            }
            return hotelsResultat;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Gestion d'Hotel Centraliser !");
            const int NombreHotels = 5;
            IList<Hotel>listHotels = initialiserHotels(NombreHotels);
            TraitrerClient(listHotels);
        }
    }
}