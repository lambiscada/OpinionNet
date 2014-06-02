 <%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="ShowComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.ShowComments" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

  <form id="Form1" runat="server">
    <p>
      <asp:Label ID="lblNoComments" meta:resourcekey="lblNoComments" runat="server" Visible="false"></asp:Label>
    </p>
    <asp:GridView ID="gvReviews" runat="server" CssClass="info" GridLines="None" 
        AutoGenerateColumns="False">
      <Columns>
        <asp:BoundField DataField="Texto" HeaderText="<%$ Resources:, comment %>" />   
        <asp:BoundField DataField="Fecha" HeaderText="<%$ Resources:, date %>" />  
        <asp:BoundField DataField="userProfile.loginName" HeaderText="<%$ Resources:, usr %>" /> 
        <asp:hyperlinkfield DataNavigateUrlFields="comentarioId" datanavigateurlformatstring="./ModifyComment.aspx?id={0}" text="<%$ Resources:, lnkModifyComment%>" /> 
        <asp:hyperlinkfield DataNavigateUrlFields="comentarioId" datanavigateurlformatstring="./RemoveComment.aspx?id={0}" text="<%$ Resources:, lnkRemoveComment%>" /> 
        
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
