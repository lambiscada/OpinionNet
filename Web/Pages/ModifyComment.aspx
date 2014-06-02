<%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="ModifyComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.ModifyComment" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
     <form id="CommentForm" method="post" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclNewComment" runat="server" meta:resourcekey="lclNewComment" />
            </span>
          <span class="entry">
            <asp:TextBox ID="txtNewComment" runat="server" Width="400px" Height="200px" Columns="16" />
            </span>
        </div>
      <div class="button" >
            <asp:Button ID="btnAccept" runat="server" meta:resourcekey="btnModify" OnClick="BtnAccept_Click"/>
    </div>     

    </form>


</asp:Content>
