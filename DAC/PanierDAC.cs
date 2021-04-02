using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Ecommerce.Models;
using BOL;
using static Ecommerce.Models.Panier;
using System.Web.SessionState;

namespace DAC
{
    public class PanierDAC : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            int? id = int.Parse(context.Request.QueryString["idProduct"]);
            int? qte = int.Parse(context.Request.QueryString["qte"]);
            int? code = int.Parse(context.Request.QueryString["code"]);


            if ( id != null && qte!=null && code!=null)
            {
                if ( id != 0 )
                {
                    InJsonPanierProduct(context, (int)id, (int)qte, (int)code );    //--ajout
                } 
                GetJsonPanier(context);
            }
        }

        /// <summary>
        /// méthode qui récupère le panier et le met à jour
        /// </summary>
        /// <param name="context">contexte de session</param>
        /// <param name="id">id du produit</param>
        /// <param name="qte">quantité du produit</param>
        /// <param name="code">méthode qui doit être utilisé sur le produit 0-> ajout,1->modification,2->supression</param>
        private void InJsonPanierProduct( HttpContext context, int id, int qte, int code)
        {
            Product pro = ProduitDAC.Instance.GetProductById(id);
            Ligne produit = new Ligne();
            Panier p = new Panier();
            produit.IdProduit = id;
            produit.NomProduit = pro.Name;
            produit.Prix = pro.Price;
            produit.Quantite = qte;

            if (context.Session["panier"] is Panier)
            {
                p = context.Session["panier"] as Panier;
            }
            else {
                context.Session.Add("panier", p);
            }

            switch (code)
            {
                case 0:
                    p.Add(produit);
                    break;
                case 1:
                    p.Update(id, qte);
                    break;
                default:
                    p.Delete(id);
                    break;
            }
            context.Session.Remove("panier");
            context.Session.Add("panier", p);
        }

        /// <summary>
        /// retourne le json du panier
        /// </summary>
        /// <param name="context"></param>
        private void GetJsonPanier (HttpContext context)
        {
            string json;
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            
            if ( context.Session["panier"] is Panier )
            {
                json = JsonConvert.SerializeObject(context.Session["panier"]);
            }
            else
            {
                Panier p = new Panier();
                json = JsonConvert.SerializeObject(p);
            }
            
            context.Response.Write(json);
            context.Response.Flush();
            context.Response.End();
        }
    }
}
