using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using Es.Udc.DotNet.PracticaIS.Model;
using Es.Udc.DotNet.PracticaIS.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaIS.Web.Pages
{
    public partial class ShowFavorites : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(this.Context))
            {
                Response.Redirect("./User/Authentication.aspx?ReturnUrl=../ShowFavorites.aspx");
            }
            /* Hide the links */
            lnkAnterior.Visible = false;
            lnkSiguiente.Visible = false;

            /* Get de Service */
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();

            int startIndex;
            UserSession userSession =  (UserSession)Context.Session["userSession"];
            long idUser = userSession.UserProfileId;

            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            Int32 count = 10;
            int numValor = opinadorService.FindFavoritosByUsrId(idUser, startIndex, count).Count();
            if (numValor == 0)
                lblNoFavoritos.Visible = true;

            this.gvFavoritos.DataSource = opinadorService.FindFavoritosByUsrId(idUser, startIndex, count);
            this.gvFavoritos.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "./ShowFavorites.aspx?" + "&startIndex=" + (startIndex - count);

                this.lnkAnterior.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkAnterior.Visible = true;
            }

            /* "Next" link */
            if ((startIndex + count) < numValor)
            {
                String url = "./ShowFavorites.aspx" + "&startIndex=" + (startIndex + count);
                this.lnkSiguiente.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkSiguiente.Visible = true;

            }
        }
        protected void gvFavoritos_RowCommand(Object sender,  GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Remove")
            {
                UserSession userSession = (UserSession)Context.Session["UserSession"];
                IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
                IOpinadorService opinadorService = container.Resolve<IOpinadorService>();

                var colsNoVisible = gvFavoritos.DataKeys[0].Values[0];
               
                opinadorService.RemoveFavorito(userSession.UserProfileId, Convert.ToInt32(colsNoVisible));
            }
            Response.Redirect("./ShowFavorites.aspx");
          
        }
    }
}