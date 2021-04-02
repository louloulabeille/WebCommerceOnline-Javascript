<%@ Control Language="C#" AutoEventWireup="true"
 CodeBehind="PaginateurHTML.ascx.cs" 
    Inherits="System.Web.UI.WebControls.PaginateurHTML" %>

<nav id="paginateur" class="container">
    <ul class="pagination">
        <li class="btn-info active"><a href="#"><span id="pageActive" runat="server"></span></a></li>
        <li class="btn-info"><asp:LinkButton ID="premierePage" OnCommand="page_Command" runat="server" Text="Première"></asp:LinkButton></li>
        <li class="previous"><asp:LinkButton ID="pagePrecedente" OnCommand="page_Command" runat="server" text="Précédente"><span aria-hidden="true">&larr;</span></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton1" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton2" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton3" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton4" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton5" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton6" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton7" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton8" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton9" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li><asp:LinkButton ID="LinkButton10" runat="server" OnCommand="page_Command"></asp:LinkButton></li>
        <li class="next">
            <asp:LinkButton ID="pageSuivante" OnCommand="page_Command" runat="server" text="Suivante">
                <span aria-hidden="true">&rarr;</span>
            </asp:LinkButton>
        </li>
        <li class="btn-info"><asp:LinkButton ID="dernierePage" OnCommand="page_Command" runat="server" Text="Dernière"></asp:LinkButton></li>
        <li>
            <asp:Label ID="Label1" runat="server" Style="display: inline" Text="Lignes / page :"></asp:Label>
            <input runat="server" type="text" id="txtnbPages" class="form-control" Style="display: inline;max-width:3em;padding-left:0px;text-align:center"  value="15" onkeydown="return (event.keycode!=13);" />
        </li>
    </ul>
</nav>
<script type="text/javascript">
    $('body').keypress(function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            e.bubbles = false;
            e.stopPropagation();
            return false;
        }
    });
    </script>




