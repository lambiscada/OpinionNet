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
    public partial class RemoveComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
               Response.Redirect(Response.ApplyAppPathModifier("./User/Authentication.aspx?ReturnUrl=../RemoveComment.aspx"));
            }

            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            IComentarioDao comentarioDao = container.Resolve<IComentarioDao>();
         
            long idComment = long.Parse(Request.Params.Get("id"));
            long user = comentarioDao.Find(idComment).usrId;
            if (!user.Equals(SessionManager.GetUserSession(Context).UserProfileId))
            {
                Response.Redirect(Response.ApplyAppPathModifier("./NonExistAutorization.aspx"));
            }

            opinadorService.DeleteComentario(idComment);
            Response.Redirect(Response.ApplyAppPathModifier("./MainPage.aspx"));
         
        }
    }
}