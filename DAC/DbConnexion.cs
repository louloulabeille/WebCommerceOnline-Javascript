using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;


namespace DAC
{
    public class DbConnexion
    {
        #region propriétés de la classe utilisation de design partern singleton
        public static DbConnexion _instance = null;
        private DbConnection _dbCon = null;
        private static object _verrou = new object();
        private static string _dbStringConnexion = string.Empty;
        private static string _dbProvider = string.Empty;
        #endregion

        #region constructeur privé
        private DbConnexion() { }
        #endregion

        #region encapsulation 
        public static string DbStringConnexion { get => _dbStringConnexion; set => _dbStringConnexion = value; }
        public static string DbProvider { get => _dbProvider; set => _dbProvider = value; }

        public static DbConnexion CreateInstance
        {
            get
            {
                lock (_verrou)
                {
                    if (_instance == null)
                    {
                        _instance = new DbConnexion();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region méthode

        /// <summary>
        /// utilisation de Dbconnexion il faut utiliser le DbproviderFactories
        /// qui est un design pattern factory et creer la classe de connexion puis lui passer la connexion string
        /// </summary>
        /// <returns></returns>
        public DbConnection GetDbConnection()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(_dbProvider);
            _dbCon = factory.CreateConnection();
            _dbCon.ConnectionString = _dbStringConnexion;
            _dbCon.Open();
            return _dbCon;
        }

        public void CloseDbConnection()
        {
            if (_dbCon?.State != System.Data.ConnectionState.Closed)
                _dbCon.Close();
        }

        #endregion

    }
}
