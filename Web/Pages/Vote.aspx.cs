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
    public partial class Vote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserSession userSession = (UserSession)Context.Session["userSession"];
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                Response.Redirect(Response.ApplyAppPathModifier("./User/Authentication.aspx?ReturnUrl=../Vote.aspx"));
            }

        }
        protected void BtnVoteClick(object sender, EventArgs e)
        {
            /* Get data. */
            String comment = this.txtComment.Text;
            Int16 vote = Convert.ToInt16(this.ddlVote.SelectedValue);
            string vendedorId = Request.Params.Get("vendedor");
            
            UserSession userSession = (UserSession)Context.Session["userSession"];
            long idUsuario = userSession.UserProfileId;

            /* Do action. */
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            opinadorService.ValorarUsuario(vendedorId, idUsuario, vote, comment);
            Response.Redirect(Response.ApplyAppPathModifier("./Mainpage.aspx"));

        }
    }
}