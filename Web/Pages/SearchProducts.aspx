<%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="SearchProducts.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.SearchProducts" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="FindForm" method="post" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclIdentifier" runat="server" meta:resourcekey="lblIdentifier" />
            </span><span class="entry">
                <asp:TextBox ID="txtIdentifier" runat="server" Width="200px" Columns="16" 
                meta:resourcekey="txtIdentifierResource1" />
            </span>
           
        </div>
       
        <div class="button">
            <asp:Button ID="btnFind" runat="server" meta:resourcekey="btnFind" OnClick="BtnFindClick" />
        </div>
        </form>
    </div>
    <br />
    <br />
</asp:Content>
