 <%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="ShowValoraciones.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.ShowValoraciones" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

  <form id="Form1" runat="server">
    <p>
      <asp:Label ID="lblNoValoraciones" meta:resourcekey="lblNoValoraciones" runat="server" Visible="false"></asp:Label>
    </p>
    <asp:GridView ID="gvValoraciones" runat="server" CssClass="info" GridLines="None" 
        AutoGenerateColumns="False">
      <Columns>
        <asp:BoundField DataField="fecha" HeaderText="<%$ Resources:, date %>" />   
        <asp:BoundField DataField="userProfile.loginName" HeaderText="<%$ Resources:, user %>" />  
        <asp:BoundField DataField="voto" HeaderText="<%$ Resources:, vote %>" /> 
        <asp:BoundField DataField="comentario" HeaderText="<%$ Resources:, comment %>" /> 
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
