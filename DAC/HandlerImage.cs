using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.Common;
using DAC;
using System.Configuration;

namespace Images
{
   
    public class HandlerImage : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }
 
        public void ProcessRequest(HttpContext context)
        {
            string connString=string.Empty;
            int? id = int.Parse(context.Request.QueryString["id"]);
            
            if (context.Request.QueryString["connectionString"] == null )
            {
                if (id != null)
                {
                    //connString = ConfigurationManager.ConnectionStrings["WebCommerceOnline.Properties.Settings.connexionBD"].ConnectionString;
                    //GetImage(context, connString, (int)id);
                    GetImage(context, (int)id);
                }
            }
            else
            {
                connString = context.Request.QueryString["connectionString"];
                if (id != null && !string.IsNullOrEmpty(connString))
                {
                    GetImage(context, connString, (int)id);
                }
            }
        }

        /// <summary>
        /// ne marche pas il ne doit pas finir de charger l'autre image qu'il redemande l'utilisation de la connexion qui n'est pas encore libéré
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        private void GetImage(HttpContext context,int id)
        {
            string connString = ConfigurationManager.ConnectionStrings["WebCommerceOnline.Properties.Settings.connexionBD"].ConnectionString;
            string provider = ConfigurationManager.ConnectionStrings["WebCommerceOnline.Properties.Settings.connexionBD"].ProviderName;
            DbProviderFactory dbprovider =  DbProviderFactories.GetFactory(provider);
            DbConnection db = dbprovider.CreateConnection();
            db.ConnectionString = connString;
            db.Open();
            //using (DbConnection db = DbConnexion.CreateInstance.GetDbConnection() )
            using(DbCommand dbCom = db.CreateCommand() )
            {
                try
                {
                    dbCom.CommandText = "SELECT dbo.Picture.PictureBinary, dbo.Picture.MimeType " +
                        "FROM  dbo.Picture INNER JOIN dbo.Product_Picture_Mapping ON dbo.Picture.Id = dbo.Product_Picture_Mapping.PictureId " +
                        " Where Productid=@id and DisplayOrder = 1";
                    dbCom.CommandType = CommandType.Text;
                    DbParameter parameter = dbCom.CreateParameter();
                    parameter.ParameterName = "@id";
                    parameter.DbType = DbType.Int32;
                    parameter.Direction = ParameterDirection.Input;
                    parameter.Value = id;
                    dbCom.Parameters.Add(parameter);
                    using ( DbDataReader dbR = dbCom.ExecuteReader() )
                    {
                        dbR.Read();
                        // Détermination du type de l'image 
                        string typeMime = dbR.GetString(1);
                        context.Response.ContentType = (string.IsNullOrEmpty(typeMime)) ? "image/jpeg" : typeMime;
                        // Mise en cache
                        context.Response.Cache.SetCacheability(HttpCacheability.Public);
                        // Lecture du contenu de l'image
                        // optimisé pour ne pas consommer trop de ressources 
                        // taille du buffer

                        int indexDepart = 0;
                        int tailleBuffer = 1024;
                        long nombreOctets = 0;
                        Byte[] flux = new Byte[1024];

                        nombreOctets = dbR.GetBytes(0, indexDepart, flux, 0, tailleBuffer);

                        while (nombreOctets == tailleBuffer)
                        {

                            context.Response.BinaryWrite(flux);
                            context.Response.Flush();
                            indexDepart += tailleBuffer;
                            nombreOctets = dbR.GetBytes(0, indexDepart, flux, 0, tailleBuffer);
                        }
                        if (nombreOctets > 0)
                        {
                            context.Response.BinaryWrite(flux);
                            context.Response.Flush();
                        }
                        context.Response.End();
                    }
                }
                catch(Exception ex)
                {
                    context.Response.Write(ex.Message);
                }
            }
        }

        private void GetImage(HttpContext context,string connString, int id)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {

                try
                {
                    SqlCommand command = new SqlCommand("SELECT dbo.Picture.PictureBinary, dbo.Picture.MimeType " +
                        "FROM  dbo.Picture INNER JOIN dbo.Product_Picture_Mapping ON dbo.Picture.Id = dbo.Product_Picture_Mapping.PictureId " +
                        " Where Productid=@id and DisplayOrder = 1");
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("@id", id));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    // Lecture
                    reader.Read();
                    // Détermination du type de l'image 
                    string typeMime = reader.GetString(1);
                    context.Response.ContentType = (string.IsNullOrEmpty(typeMime)) ? "image/jpeg" : typeMime ;
                    // Mise en cache
                     context.Response.Cache.SetCacheability(HttpCacheability.Public);
                    // Lecture du contenu de l'image
                    // optimisé pour ne pas consommer trop de ressources 
                    // taille du buffer

                    int indexDepart = 0;
                    int tailleBuffer = 1024;
                    long nombreOctets = 0;
                    Byte[] flux = new Byte[1024];

                    nombreOctets = reader.GetBytes(0, indexDepart, flux, 0, tailleBuffer);

                    while(nombreOctets==tailleBuffer)
                    {

                        context.Response.BinaryWrite(flux);
                        context.Response.Flush();
                        indexDepart += tailleBuffer;
                        nombreOctets = reader.GetBytes(0, indexDepart, flux, 0, tailleBuffer);
                    }
                    if (nombreOctets>0)
                    {
                        context.Response.BinaryWrite(flux);
                        context.Response.Flush();
                    }
                    context.Response.End();
                         
                }
                catch (Exception)
                {
                   
                }

            }
        }
    }
}