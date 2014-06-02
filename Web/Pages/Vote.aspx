<%@ Page Language="C#" MasterPageFile="~/Opinador.Master" AutoEventWireup="true"
    Codebehind="Vote.aspx.cs" Inherits="Es.Udc.DotNet.PracticaIS.Web.Pages.Vote" 
    meta:resourcekey="Page" culture="auto" uiculture="auto" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
     <div id="form">
        <form id="VoteForm" method="post" runat="server">
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
                <asp:Localize ID="lclVote" runat="server" meta:resourcekey="lclVote" />
            </span><span class="entry">
                <asp:DropDownList ID="ddlVote" runat="server" Width="200px">
                    <asp:ListItem Value="1"  />
                    <asp:ListItem Value="2"  />
                    <asp:ListItem Value="3"  />
                    <asp:ListItem Value="4"  />
                    <asp:ListItem Value="5"  />              
                </asp:DropDownList>
            </span>
        </div>
       
       
        <div class="button">
            <asp:Button ID="btnVote" runat="server" meta:resourcekey="btnVote" OnClick="BtnVoteClick" />
        </div>
        </form>
    </div>
    <br />
    <br />
  
</asp:Content>