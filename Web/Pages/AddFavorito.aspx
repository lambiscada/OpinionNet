<%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="AddFavorito.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.AddFavorito" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
     <div id="form">
        <form id="AddFavoritoForm" method="post" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclFavorito" runat="server" meta:resourcekey="lclFavorito" />
            </span>
          <span class="entry">
            <asp:TextBox ID="txtFavorito" runat="server" Width="200px" Height="20px" Columns="1" />
            </span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclComment" runat="server" meta:resourcekey="lclComment" />
            </span>
          <span class="entry">
            <asp:TextBox ID="txtComment" runat="server" Width="200px" Height="100px" />
            </span>
        </div>
       
        <div class="button">
            <asp:Button ID="btnAccept" runat="server" meta:resourcekey="btnAccept" OnClick="BtnAcceptClick" />
        </div>
        </form>
    </div>
    <br />
    <br />
  
</asp:Content>