using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaIS.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaIS.Web.Pages
{
    public partial class AddFavorito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                Response.Redirect(Response.ApplyAppPathModifier("./User/Authentication.aspx?ReturnUrl=../AddFavorito.aspx"));
            }
        }

        protected void BtnAcceptClick(object sender, EventArgs e)
        {
            /* Get data. */
            String bookmark = this.txtFavorito.Text;
            long idProducto = long.Parse(Request.Params.Get("idProducto"));
            string comment = this.txtComment.Text;

            UserSession userSession = (UserSession)Context.Session["userSession"];
            long idUsuario = userSession.UserProfileId;


            /* Do action. */
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            opinadorService.AddFavorito(idUsuario, idProducto, bookmark, comment);
            Response.Redirect(Response.ApplyAppPathModifier("./Mainpage.aspx"));

        }
    }
}