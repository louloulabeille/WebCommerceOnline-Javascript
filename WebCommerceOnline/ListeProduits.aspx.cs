using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOL;
using DAC;

namespace WebCommerceOnline
{
    public partial class ListeProduits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if ( !IsPostBack )
            {
                try
                {
                    string session = Session.SessionID;
                    HttpCookie cook = Request.Cookies["test"+session];
                    if (cook == null && cook.Value == "test"+session)
                    {
                        Response.Redirect("WebFormActivationCookies.aspx");
                    }
                }
                catch( Exception ex )
                {
                    Response.Redirect("WebFormActivationCookies.aspx");
                }
                
                ChargeMenuProduit();
            }
        }

        protected void ChargeMenuProduit ()
        {
            HashSet<Category> categories = CategoryDAC.Instance.GetCategoryParentAll(true);
            string htmlNav = "<div class=\"row\"><div class=\"col-12 col-md-11\"><nav class=\"navbar navbar-expand-lg navbar-dark\">";
            htmlNav += "<a class=\"navbar-brand\" href=\"#\">Products catalog</a>";
            htmlNav += "<ul class=\"navbar-nav\">";
            foreach (Category item in categories)
            {
                if (item.Categories.Count > 0) // sous menu
                {
                    htmlNav += "<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" data-toggle=\"dropdown\" id=\""+string.Format("id"+item.Name) +"\" href=\"#\">" + item.Name + "</a>";
                    htmlNav += "<div class=\"dropdown-menu\">";
                    foreach (Category cat in item.Categories)
                    {
                        htmlNav += "<a class=\"dropdown-item\" href=\"#\" onclick=\"ClicCategory("+cat.Id+")\">" + cat.Name+"</a>";
                    }
                    htmlNav += "</div></il>";
                }
                else
                {
                    htmlNav += "<li class=\"nav-item\"><a class=\"nav-link\" href=\"#\" onclick=\"ClicCategory(" + item.Id + ")\">" + item.Name+"</a></li>";
                }
            }
            htmlNav += "</ul></nav></div>";
            // -- construction du panier
            htmlNav += "<div class=\"col-12 col-lg-1 text-center text-white\"><p><a class=\"btn btn-primary\" data-toggle=\"collapse\" href=\"#collapsePanier\" role=\"button\" aria-expanded=\"false\" aria-controls=\"collapseExample\">";
            htmlNav += "<i class=\"fas fa-shopping-basket\"></i></a></p><p id=\"qte\">0</p></div></div>";
            htmlNav += "<div class=\"row\"><div class=\"col-0 col-lg-3\"></div><div class=\"col-0 col-lg-3\"></div><div class=\"col-12 col-lg-6\"><div class=\"collapse\" id=\"collapsePanier\" style=\"overflow: visible;\">";
            htmlNav += "<div class=\"card card-body bg-light\" id=\"panierTotal\"></div></div></div></div>"; 
            nav_menu_produit.InnerHtml = htmlNav;
        }

    }
}