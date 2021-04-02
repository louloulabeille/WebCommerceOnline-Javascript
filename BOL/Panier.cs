using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace Ecommerce.Models
{
    [Serializable()]
    public class Panier 
    {
        #region classe Ligne
        [Serializable()]
        public class Ligne
        {
            public Ligne()
            {

            }
            public Ligne(int id, string name, decimal price, int qty)
            {
                IdProduit = id;
                NomProduit = name;
                Prix = price;
                Quantite = qty;
            }
            public int IdProduit { get; set; }
            public String NomProduit { get; set; }
            public decimal Prix { get; set; }
            public int Quantite { get; set; }
            public decimal TotalLigne
            {
                get
                {
                    return Quantite * Prix;
                }
            }
            /// <summary>
            /// Compare deux objets pour déterminer l'égalité
            /// De type Ligne et même identifiant produit
            /// </summary>
            /// <returns>Vrai si les deux objets sont égaux</returns>
            public override bool Equals(Object obj)
            {
                Ligne ligneC = obj as Ligne;
                if (ligneC == null) return false;
                return (ligneC.IdProduit == this.IdProduit);
            }

           
            /// <summary>
            /// opérateur relationnel ==
            /// </summary>
            /// <param name="ligneA">Instance ligne</param>
            /// <param name="ligneB">Instance ligne</param>
            /// <returns>Vrai si égaux</returns>
            public static bool operator ==(Ligne ligneA, Ligne ligneB)
            {
                if ((object)ligneA == null) return (object)ligneB == null;
                return ligneA.Equals(ligneB);
            }
            /// <summary>
            ///  opérateur relationnel !=
            /// </summary>
            /// <param name="ligneA">Instance ligne</param>
            /// <param name="ligneB">Instance ligne</param>
            /// <returns>Vrai si différents</returns>
            public static bool operator !=(Ligne ligneA, Ligne ligneB)
            {
                if ((object)ligneA == null) return (object)ligneB != null;
                return !ligneA.Equals(ligneB);
            }
            /// <summary>
            /// Une des règles de conception veut que l'on modifie la méthode GetHashCode
            /// Si la méthode Equals est modifiée
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                // Uniquement si non nul
                return (this.IdProduit != 0) ? this.IdProduit.GetHashCode() : 0;
            }
        }
        #endregion

        private HashSet<Ligne> _lignes;

        public Panier()
        {
            _lignes = new HashSet<Ligne>();
        }
        
        public int QteProduits
        {
            get
            {

                return calculTotProduits(); ;
            }
        }
        public decimal TotalPanier
        {
            get
            {
               return calculTotPanier();
               
            }
        }

        public HashSet<Ligne> Lignes { get => _lignes; set => _lignes = value; }

        private decimal calculTotPanier()
        {
            decimal total = 0;
            foreach (Ligne p in this._lignes)
            {
                total += (p.Quantite * p.Prix);
            }
            return total;
        }
        private int calculTotProduits()
        {
            int total = 0;
            foreach (Ligne p in this._lignes)
            {
                total += p.Quantite;
            }
            return total;
        }
       
        public HashSet<Ligne> Select(Panier panier)
        {
            if (panier != null)
            {
            this._lignes = panier._lignes;
            calculTotPanier();
            }
                return _lignes;
           
        }
        public HashSet<Ligne> Select()
        {
            calculTotPanier();
            return this._lignes;

        }
        public  void Add(Ligne p)
        {
            
            if (!_lignes.Contains(p))
            {
                _lignes.Add(p);
            }
            else
            {
                var ligneExistante = _lignes.FirstOrDefault(l => l.IdProduit == p.IdProduit);
                ligneExistante.Quantite += p.Quantite;
            }
        }
        public void Update(int idProduit, int quantite)
        {
            var ligneExistante = _lignes.FirstOrDefault(l => l.IdProduit == idProduit);
            if (ligneExistante!=null)
            {
                ligneExistante.Quantite = quantite;
            }
            
        }
    
        public void Delete(int idProduit)
        {
              this._lignes.RemoveWhere(l => l.IdProduit == idProduit);
        }
        
    }
}