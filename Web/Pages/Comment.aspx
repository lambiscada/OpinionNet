<%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="Comment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.Comment" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
     <div id="form">
        <form id="CommentForm" method="post" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclComment" runat="server" meta:resourcekey="lclComment" />
            </span>
          <span class="entry">
            <asp:TextBox ID="txtComment" runat="server" Width="400px" Height="200px" Columns="16" />
            </span>
        </div>

        <div class="field">
            <span class="label">
                <asp:Localize ID="lclTag" runat="server" meta:resourcekey="lclTag" />
            </span><span class="entry">
                <asp:TextBox ID="txtTag" runat="server" Width="200px" Columns="16" 
                meta:resourcekey="txtTag" />
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