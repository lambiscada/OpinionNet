 <%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="ShowPuntuations.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.ShowPuntuations" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Table CssClass="accountDetails" ID="TableAccountInfo" runat="server">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableHeaderCell ID="cellCaptionLoginName" runat="server" Text="<%$ Resources:, loginName %>"></asp:TableHeaderCell>
            <asp:TableCell ID="cellLoginName" runat="server"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server">
            <asp:TableHeaderCell ID="cellaptionNumberOfVotes" runat="server" Text="<%$ Resources:, numberOfVotes %>"></asp:TableHeaderCell>
            <asp:TableCell ID="cellNumerOfVotes" runat="server"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow3" runat="server">
            <asp:TableHeaderCell ID="cellCaptionAverage" runat="server" Text="<%$ Resources:, average %>"></asp:TableHeaderCell>
            <asp:TableCell ID="cellAverage" runat="server"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
       <form id="ValoracionsForm" method="post" runat="server">
    <asp:Button ID="btnShowValoraciones" runat="server" meta:resourcekey="btnShowValoraciones" OnClick="BtnShowValoracionesClick" />
</form>
</asp:Content>
