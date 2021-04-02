using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web.UI.WebControls
{
    #region Type AfterSelectEventArgs
    /// <summary>
    /// Données de l'événement sur fin d'exécution de la sélection
    /// des lignes à retenir
    /// </summary>
    public class AfterSelectEventArgs : EventArgs
    {
        public int NombreTotalLignes { get; set; } = 0;
        public AfterSelectEventArgs(int nombreTotalLignes) => this.NombreTotalLignes = nombreTotalLignes;
    }
    #endregion
    [ToolboxData("<{0}:SqlDatasourcePagination runat='server'></{0}:SqlDatasourcePagination>")]
    public class SqlDatasourcePagination : SqlDataSource
    {
        #region Evénements
        public event EventHandler<AfterSelectEventArgs> AfterSelect;
        internal virtual void OnAfterSelect(AfterSelectEventArgs e)
        {
            AfterSelect?.Invoke(this, e);
        }
        #endregion
        
        #region Classe interne dérivée de ViewSqlDataSourceView
        public class SqlDatasourcePaginationView : SqlDataSourceView
        {
            private SqlDatasourcePagination _owner;
            public SqlDatasourcePaginationView(
                SqlDatasourcePagination owner,
                string name,
                HttpContext context)
                : base(owner, name, context)
            {
                _owner = owner;
            }
            protected override void OnSelecting(SqlDataSourceSelectingEventArgs e)
            {
                string commandText = e.Command.CommandText.ToLower();
                string orderBy;
                string select;
                if (!commandText.Contains("order by"))
                {
                    throw new Exception("La pagination nécessite une clause Order By");
                }
                else
                {
                    orderBy = commandText.Substring(commandText.IndexOf("order by"));
                    int nbCaracteres = commandText.Length - 7 - orderBy.Length;
                    select = commandText.Substring(7, nbCaracteres);
                }
                e.Command.CommandText = $"With Lignes As(Select ROW_NUMBER() OVER({orderBy}) as Rang,{select}), " +
                    $" CompterLignes as ( select count(1) as NombreLignes  from Lignes) " +
                     $" SELECT Lignes.*,CompterLignes.NombreLignes " +
                     $" FROM  Lignes,CompterLignes WHERE Rang BETWEEN {(_owner.PageCourante-1)*_owner.PageSize+1} AND {_owner.PageCourante*_owner.PageSize}; ";
                               
                base.OnSelecting(e);
            }
            protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
            {
                IEnumerable dataRows;
                dataRows = base.ExecuteSelect(arguments);
                IEnumerator enumerateur = dataRows.GetEnumerator();
                if (enumerateur.MoveNext())
                {
                    arguments.TotalRowCount = (int)((DataRowView)enumerateur.Current).Row["NombreLignes"];
                }
                else
                { arguments.TotalRowCount = 0; }
                _owner.OnAfterSelect(new AfterSelectEventArgs(arguments.TotalRowCount));
                return dataRows;
            }
        }
       
        protected override SqlDataSourceView CreateDataSourceView(string viewName)
        {
            return new SqlDatasourcePaginationView(
                this, viewName, Context);
        }
        [Category("Pagination")]
        [Browsable(true)]
        [Description("Taille page")]
        [DisplayName("Taille page")]
        [PersistenceMode(PersistenceMode.Attribute)]
        public int PageSize { get; set; } = 15;
        [Category("Pagination")]
        [Browsable(true)]
        [Description("Page initiale")]
        [DisplayName("Page initiale")]
        [PersistenceMode(PersistenceMode.Attribute)]
        public int PageCourante { get; set; } = 1;
        #endregion
    }
}
