using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using System.Data.Common;
using System.Data;
using System.Web;
using Newtonsoft.Json;

namespace DAC
{
    public class ProduitDAC : IHttpHandler
    {
        private static ProduitDAC _instance = null;
        private static object _verrou = new object();

        private ProduitDAC() { }

        public static ProduitDAC Instance {
            get{
                lock(_verrou)
                {
                    if ( _instance == null )
                    {
                        _instance = new ProduitDAC();
                    }
                    return _instance;
                }
            }
        }

        public HashSet<Product> GetProductAllByCategory( int idCategory )
        {
            using ( DbConnection db  = DbConnexion.CreateInstance.GetDbConnection() )
            using ( DbCommand dbCom = db.CreateCommand() )
            {
                dbCom.CommandText = "select distinct p.* from Product p inner join Product_Category_Mapping pcm on p.Id = pcm.ProductId where CategoryId = @categoryId";
                dbCom.CommandType = CommandType.Text;
                DbParameter param = dbCom.CreateParameter();
                param.ParameterName = "@categoryId";
                param.DbType = DbType.Int32;
                param.Direction = ParameterDirection.Input;
                param.Value = idCategory;
                dbCom.Parameters.Add(param);

                return ChargeListeProduct(dbCom);
            }
        }

        /// <summary>
        /// méthode qui retour un produit par rapport à son id
        /// </summary>
        /// <param name="idProduit"></param>
        /// <returns></returns>
        public Product GetProductById ( int idProduit )
        {
            using ( DbConnection db = DbConnexion.CreateInstance.GetDbConnection() )
            using ( DbCommand dbCom = db.CreateCommand() )
            {
                dbCom.CommandText = "select * from Product where Id=@idProduit";
                dbCom.CommandType = CommandType.Text;
                DbParameter param = dbCom.CreateParameter();
                param.ParameterName = "@idProduit";
                param.Value = idProduit;
                param.DbType = DbType.Int32;
                dbCom.Parameters.Add(param);
                using ( DbDataReader dbData = dbCom.ExecuteReader() )
                {
                    return dbData.Read()?ChargeProduit(dbData):null;
                }
            }
        }

        private HashSet<Product> ChargeListeProduct ( DbCommand db )
        {
            using ( DbDataReader dbData =  db.ExecuteReader() )
            {
                HashSet<Product> products = new HashSet<Product>();
                while ( dbData.Read() )
                {
                    products.Add(ChargeProduit(dbData));
                }
                return products;
            }
        }

        private Product ChargeProduit(DbDataReader db)
        {
            Product prod = new Product();
            prod.Id = (int)db["Id"];
            prod.ProductTypeId = (int)db["ProductTypeId"];
            prod.ParentGroupedProductId = (int)db["ParentGroupedProductId"];
            prod.VisibleIndividually = (bool)db["VisibleIndividually"];
            prod.Name = db["Name"].ToString();
            prod.ShortDescription = db["ShortDescription"].ToString();
            prod.FullDescription = db["FullDescription"].ToString();
            prod.AdminComment = db["AdminComment"].ToString();
            prod.ProductTemplateId = (int)db["ProductTemplateId"];
            prod.ShowOnHomePage = (bool)db["ShowOnHomePage"];
            prod.MetaKeywords = db["MetaKeywords"].ToString();
            prod.MetaDescription = db["MetaDescription"].ToString();
            prod.MetaTitle = db["MetaTitle"].ToString();
            prod.IsRental = (bool)db["IsRental"];
            prod.RentalPriceLength = (int)db["RentalPriceLength"];
            prod.RentalPricePeriodId = (int)db["RentalPricePeriodId"];
            prod.IsTaxExempt = (bool)db["IsTaxExempt"];
            prod.TaxCategoryId = (int)db["TaxCategoryId"];
            prod.StockQuantity = (int)db["StockQuantity"];
            prod.DisplayStockAvailability = (bool)db["DisplayStockAvailability"];
            prod.DisplayStockQuantity = (bool)db["DisplayStockQuantity"];
            prod.MinStockQuantity = (int)db["MinStockQuantity"];
            prod.OrderMinimumQuantity = (int)db["OrderMinimumQuantity"];
            prod.OrderMaximumQuantity = (int)db["OrderMaximumQuantity"];
            prod.DisableBuyButton = (bool)db["DisableBuyButton"];
            prod.DisableWishlistButton = (bool)db["DisableWishlistButton"];
            prod.Price = (decimal)db["Price"];
            prod.OldPrice = (decimal)db["OldPrice"];

            /// gestion des null
            if (db["SpecialPrice"].GetType().Name != "DBNull")
                prod.SpecialPrice = (decimal)db["SpecialPrice"];
            if (db["SpecialPrice"].GetType().Name != "DBNull") 
                prod.SpecialPriceStartDateTimeUtc = (DateTime)db["SpecialPriceStartDateTimeUtc"];
            if (db["SpecialPriceEndDateTimeUtc"].GetType().Name != "DBNull")
                prod.SpecialPriceEndDateTimeUtc = (DateTime)db["SpecialPriceEndDateTimeUtc"];

            prod.Weight = (decimal)db["Weight"];
            prod.Length = (decimal)db["Length"];
            prod.Width = (decimal)db["Width"];
            prod.Height = (decimal)db["Height"];
            /// gestion des null
            if (db["AvailableStartDateTimeUtc"].GetType().Name != "DBNull")
                prod.AvailableStartDateTimeUtc = (DateTime)db["AvailableStartDateTimeUtc"];
            if (db["AvailableEndDateTimeUtc"].GetType().Name != "DBNull")
                prod.AvailableEndDateTimeUtc = (DateTime)db["AvailableEndDateTimeUtc"];

            prod.DisplayOrder = (int)db["DisplayOrder"];
            prod.Published = (bool)db["Published"];
            prod.Deleted = (bool)db["Deleted"];
            prod.CreatedOnUtc = (DateTime)db["CreatedOnUtc"];
            prod.UpdatedOnUtc = (DateTime)db["UpdatedOnUtc"];

            return prod;
        }

        #region HttpHandler
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            int? id = int.Parse(context.Request.QueryString["idCategory"]);

            if (id != null)
            {
                GetJsonCategoryProduct(context, (int)id);
            }
        }

        /// <summary>
        /// methode qui va chercher la liste des produits par categorie et retourne un flux json
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idCategory"></param>
        private void GetJsonCategoryProduct(HttpContext context, int idCategory)
        {
            HashSet<Product> products = GetProductAllByCategory(idCategory);
            string json = JsonConvert.SerializeObject(products);
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.Write(json);
            context.Response.Flush();
            context.Response.End();
        }
        #endregion
    }
}
