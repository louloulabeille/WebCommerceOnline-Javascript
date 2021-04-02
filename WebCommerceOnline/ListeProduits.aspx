<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListeProduits.aspx.cs" Inherits="WebCommerceOnline.ListeProduits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.15.2/css/all.css" integrity="sha384-vSIIfh2YWi9wW0r9iZe7RJPrKwp6bG+s9QZMoITbCckVJqGCCRhc+ccxNcdpHuYu" crossorigin="anonymous">
    <style type="text/css">
        #MainContent_nav_menu_produit{
            padding-left: 0px;
            padding-right: 0px;
        }
        .fa-shopping-basket {
            color: aliceblue;
        }
    </style>
    <div class="container-md bg-dark" id="nav_menu_produit" runat="server">
    
    </div>
    <div class="container-md" id="product">

    </div>
    <script defer type="text/javascript" src="Scripts/js/product.js"></script>
</asp:Content>
