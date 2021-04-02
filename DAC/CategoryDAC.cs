using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using System.Data.Common;
using System.Data;
using System.Data.Odbc;

namespace DAC
{
    public class CategoryDAC
    {
        #region design pattern 
        private static CategoryDAC _instance = null;
        private static object _verrou = new object();

        private CategoryDAC() { }

        public static CategoryDAC Instance
        {
            get
            {
                lock(_verrou)
                {
                    if ( _instance == null )
                    {
                        _instance =  new CategoryDAC();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        #region methode DAC
        /// <summary>
        /// retourne toutes les categories
        /// </summary>
        /// <param name="boolSousCategorys">bool pour avoir les sous categorys</param>
        /// <param name="boolProducts">bool pour avoir les produits liés aux categories</param>
        /// <returns></returns>
        public HashSet<Category> GetCategoryParentAll (bool boolSousCategorys = false, bool boolProducts=false)
        {
            using (DbConnection bdC = DbConnexion.CreateInstance.GetDbConnection() )
            using (DbCommand dbCommand = bdC.CreateCommand() )
            { 
                dbCommand.CommandText = @"select * from Category where ParentCategoryId = 0";
                dbCommand.CommandType = CommandType.Text;
                return AlimenteListe(dbCommand , boolSousCategorys, boolProducts);
            }
        }

        /// <summary>
        /// méthode qui prend un dbDataReader en paramètre et retour une liste de category
        /// </summary>
        /// <param name="dbDR"></param>
        /// <returns></returns>
        private HashSet<Category> AlimenteListe ( DbCommand dbC,bool boolSousCategorys = false, bool boolProducts = false)
        {
            HashSet<Category> categories = new HashSet<Category>();
            using (DbDataReader dbEr = dbC.ExecuteReader() )
            {
                while ( dbEr.Read() )
                {
                    categories.Add(ChargeCategorie(dbEr,boolSousCategorys, boolProducts));
                }
            }

            return categories;
        }

        /// <summary>
        /// charge la categorie
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private Category ChargeCategorie (DbDataReader db , bool boolSousCategorys = false, bool boolProducts = false)
        {
            Category cat = new Category();
            cat.Id = (int)db["Id"];
            cat.Name = db["Name"].ToString();
            cat.Description = db["Description"].ToString();
            cat.CategoryTemplateId = (int)db["CategoryTemplateId"];
            cat.MetaKeywords = db["MetaKeywords"].ToString();
            cat.MetaDescription = db["MetaDescription"].ToString();
            cat.MetaTitle = db["MetaTitle"].ToString();
            cat.ParentCategoryId = (int)db["ParentCategoryId"];
            cat.PictureId = (int)db["PictureId"];
            cat.PageSize = (int)db["PageSize"];
            cat.AllowCustomersToSelectPageSize = (bool)db["AllowCustomersToSelectPageSize"];
            cat.PageSizeOptions = db["PageSizeOptions"].ToString();
            cat.PriceRanges = db["PriceRanges"].ToString();
            cat.ShowOnHomePage = (bool)db["ShowOnHomePage"];
            cat.IncludeInTopMenu = (bool)db["IncludeInTopMenu"];
            cat.HasDiscountsApplied = (bool)db["HasDiscountsApplied"];
            cat.SubjectToAcl = (bool)db["SubjectToAcl"];
            cat.LimitedToStores = (bool)db["LimitedToStores"];
            cat.Published = (bool)db["Published"];
            cat.Deleted = (bool)db["Deleted"];
            cat.DisplayOrder = (int)db["DisplayOrder"];
            cat.CreatedOnUtc = (DateTime)db["CreatedOnUtc"];
            cat.UpdatedOnUtc = (DateTime)db["UpdatedOnUtc"];
            if ( boolSousCategorys && cat.ParentCategoryId == 0 )
            {
                cat.Categories = GetSousCategorieAll(cat.Id);
            }
            if (boolProducts && cat.ParentCategoryId != 0)
            {
                cat.Products = ProduitDAC.Instance.GetProductAllByCategory(cat.Id);
            }

            return cat;
        }

        public HashSet<Category> GetSousCategorieAll (int idCategoryParent)
        {
            using (DbConnection bdC = DbConnexion.CreateInstance.GetDbConnection())
            using (DbCommand dbCommand = bdC.CreateCommand())
            {
                dbCommand.CommandText = @"select * from Category where ParentCategoryId = @idCategoryParent";
                dbCommand.CommandType = CommandType.Text;
                DbParameter param = dbCommand.CreateParameter();
                param.DbType = DbType.Int32;
                param.Value = idCategoryParent;
                param.ParameterName = "@idCategoryParent";
                param.Direction = ParameterDirection.Input;
                dbCommand.Parameters.Add(param);

                return AlimenteListe(dbCommand);
            }
        }
        #endregion

    }
}
