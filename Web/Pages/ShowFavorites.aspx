 <%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="ShowFavorites.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.ShowFavorites" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

  <form id="Form1" runat="server">
    <p>
      <asp:Label ID="lblNoFavoritos" meta:resourcekey="lblNoFavoritos" runat="server" Visible="false"></asp:Label>
    </p>
    <asp:GridView ID="gvFavoritos"  onrowcommand="gvFavoritos_RowCommand" DataKeyNames="favoritoId" runat="server" CssClass="info" GridLines="None" 
        AutoGenerateColumns="False">
      <Columns>
        <asp:BoundField DataField="nombre" HeaderText="<%$ Resources:, bookmark %>" />  
        <asp:BoundField DataField="fecha" HeaderText="<%$ Resources:, date %>" />   
        <asp:BoundField DataField="comentario" HeaderText="<%$ Resources:, comment %>" /> 
        <asp:ButtonField  buttontype="Button"  commandname="Remove" Text="remove"/>
      </Columns>
    </asp:GridView>
    </br>
  </form>
  </br>
  <!-- "Previous" and "Next" links. -->
  <div class="previousNextLinks">
    <span class="previousLink">
      <asp:HyperLink ID="lnkAnterior" Text="<%$ Resources:Common, lnkAnterior %>" runat="server"
                Visible="False"></asp:HyperLink>
    </span>
    <span class="nextLink">
      <asp:HyperLink ID="lnkSiguiente" Text="<%$ Resources:Common, lnkSiguiente %>" runat="server" Visible="False"></asp:HyperLink>
    </span>
  </div>    
</asp:Content>
