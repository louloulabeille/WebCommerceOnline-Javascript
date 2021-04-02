using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.UI.WebControls
{
    public class PaginateurPageChangingEventArgs : EventArgs
    {
        public int NumeroPage { get; set; } = 0;

        public int NombreLignesParPage { get; set; } = 0;
        public PaginateurPageChangingEventArgs(int numeroPage, int nombreLignesParPage)
        {
            NumeroPage = numeroPage;
            NombreLignesParPage = nombreLignesParPage;
            
        }
    }
}