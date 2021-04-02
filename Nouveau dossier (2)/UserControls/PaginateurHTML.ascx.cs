using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace System.Web.UI.WebControls
{
    public partial class PaginateurHTML : System.Web.UI.UserControl
    {
        #region "Evénements"
        public event EventHandler<PaginateurPageChangingEventArgs> PageChanging;
        protected virtual void OnPageChanging(PaginateurPageChangingEventArgs e)
        {
            PageChanging?.Invoke(this, e);
        }
        #endregion
        public int NombreLignesPage
        {
            get { 
                int nb =0;
                int.TryParse(txtnbPages.Value,out nb);
                return nb;
            }
            set
            {
                txtnbPages.Value = value.ToString();
            }
        }
        /// <summary>
        /// Génération des éléments de pagination
        /// </summary>
        /// <param name="pageCourante"></param>
        /// <param name="lignesParPage"></param>
        /// <param name="nombreTotalLignes"></param>
        public void GenererLiensPages(int pageCourante, int lignesParPage, int nombreTotalLignes)
        {
            int dernierePage = (nombreTotalLignes % lignesParPage == 0) ? nombreTotalLignes / lignesParPage : (nombreTotalLignes / lignesParPage) + 1;
            pageActive.InnerText = string.Format("Page {0} sur {1}", pageCourante, dernierePage);
            premierePage.CommandArgument = 1.ToString();

            if (pageCourante > 1)
            {
                this.pagePrecedente.CommandArgument = (pageCourante - 1).ToString();
                this.pagePrecedente.Enabled = true;
                this.pagePrecedente.Style.Add("class","");
            }
            else
            {
                this.pagePrecedente.Enabled = false;
                this.pagePrecedente.Style.Add("class", "disabled");
            }

           
            for (int i = 1; i < 11; i++)
            {
                string identifiant = "LinkButton" + i.ToString();
                LinkButton pageNumerotee = this.FindControl(identifiant) as LinkButton;
                int pageEncours = pageCourante + i;
                pageNumerotee.Text = string.Format("{0:### ###}", pageEncours);
                pageNumerotee.CommandArgument = pageEncours.ToString();
                if (pageEncours <= dernierePage)
                {
                    pageNumerotee.Enabled= true;
                    pageNumerotee.Style.Add("class", "");
                }
                else
                {
                    pageNumerotee.Enabled = false;
                    pageNumerotee.Style.Add("class", "disabled");
                }
            }

            this.dernierePage.CommandArgument = dernierePage.ToString();

            if (pageCourante < dernierePage)
            {
                this.pageSuivante.CommandArgument = (pageCourante + 1).ToString();
                this.pageSuivante.Enabled = true;
            }
            else
            {
                this.pageSuivante.Enabled = false;
            }
        }

        /// <summary>
        /// Chargement d'une nouvelle page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Le numéro de la page à charger</param>

        protected void page_Command(object sender, CommandEventArgs e)
        {
            OnPageChanging(new PaginateurPageChangingEventArgs(int.Parse(e.CommandArgument.ToString()), int.Parse(txtnbPages.Value)));
        }
       

    }
}