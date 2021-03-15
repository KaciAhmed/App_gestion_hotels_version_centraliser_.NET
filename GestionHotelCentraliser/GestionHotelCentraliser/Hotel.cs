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
        
     

        public int NbChambresTotal
        {
            get => Chambres.Count;
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

        public Hotel(string nom, int numero, string rue, string ville, string pays, string positionGPS, string lieuDit, float nbEtoile, IList<Chambre> chambres)
        {
            Nom = nom;
            Numero = numero;
            Rue = rue;
            Ville = ville;
            Pays = pays;
            PositionGPS = positionGPS;
            LieuDit = lieuDit;
            NbEtoile = nbEtoile;
            Chambres = chambres;
        }

   
    }
}
