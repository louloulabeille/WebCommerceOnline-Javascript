using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;
using DAC;

namespace WebCommerceOnline
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code qui s’exécute au démarrage de l’application
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DbConnexion.DbStringConnexion = ConfigurationManager.ConnectionStrings["WebCommerceOnline.Properties.Settings.connexionBD"].ConnectionString;
            DbConnexion.DbProvider = ConfigurationManager.ConnectionStrings["WebCommerceOnline.Properties.Settings.connexionBD"].ProviderName;
            DbConnexion.CreateInstance.GetDbConnection();
        }
        void Session_Start(object sender, EventArgs e)
        { // Code that runs when a new session is started  
            string sessionId = Session.SessionID;
            HttpCookie cook = new HttpCookie("test"+sessionId);
            cook.Value = "true"+sessionId;
            Response.Cookies.Add(cook);
        }

    }
}