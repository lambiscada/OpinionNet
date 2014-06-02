<%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="ShowProducts.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.ShowProducts" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Xml ID="aspXML" runat="server" />
    <asp:HyperLink ID="lnkAnterior" runat="server" Text="<%$ Resources:Common, lnkAnterior %>" NavigateUrl="./ShowProducts.aspx?pro="/>
    </t>
    </t>
    </t>
    </t>
    <asp:HyperLink ID="lnkSiguiente" runat="server" Text="<%$ Resources:Common, lnkSiguiente %>" NavigateUrl="./ShowProducts.aspx?pro="/>


</asp:Content>