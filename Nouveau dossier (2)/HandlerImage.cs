using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;

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

            string connString = context.Request.QueryString["connectionString"];
            int? id = int.Parse(context.Request.QueryString["id"]);
           
            if (id != null && !string.IsNullOrEmpty(connString))
            {
              GetImage(context,connString, (int)id);
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
                        " Where Productid=@id and DisplayOrder = 1 ");
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