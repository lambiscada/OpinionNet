using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaIS.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using Es.Udc.DotNet.PracticaIS.Model.UserService;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaIS.Model.ComentarioDao;

namespace Es.Udc.DotNet.PracticaIS.Web.Pages
{
    public partial class ModifyComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                Response.Redirect(Response.ApplyAppPathModifier("./User/Authentication.aspx?ReturnUrl=ModifyComment.aspx"));

            }  
            UserSession userSession = (UserSession)Context.Session["userSession"];
            long idUsuario = userSession.UserProfileId;
            long idComment = long.Parse(Request.Params.Get("id"));

            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            IComentarioDao comentarioDao = container.Resolve<IComentarioDao>();
            long user = comentarioDao.Find(idComment).usrId;
            if (!user.Equals(idUsuario))
            {
                Response.Redirect(Response.ApplyAppPathModifier("./NonExistAutorization.aspx"));
            }
        }
        protected void BtnAccept_Click(object sender, EventArgs e)
        {
            /* Get data. */
            String comment = this.txtNewComment.Text;
            long idComment = long.Parse(Request.Params.Get("id"));

            UserSession userSession = (UserSession)Context.Session["userSession"];

            long idUsuario = userSession.UserProfileId;

            /* Do action. */
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            opinadorService.ModifyComentarioAndEtiqueta(idComment, comment, null);
            Response.Redirect(Response.ApplyAppPathModifier("./Mainpage.aspx"));

        }
    }
}