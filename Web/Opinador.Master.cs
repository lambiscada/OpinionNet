using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaIS.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using Es.Udc.DotNet.PracticaIS.Model.EtiquetaDao;
using Es.Udc.DotNet.ModelUtil.Dao;


namespace Es.Udc.DotNet.PracticaIS.Web
{
    public partial class Opinador : System.Web.UI.MasterPage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;

            }
            else
            {

                if (lblWelcome != null)
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + ((UserSession)Session[USER_SESSION_ATTRIBUTE]).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;

            }

            CloudTag();
        }

        protected void CloudTag()
        {
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            
            foreach (Model.Etiqueta e in opinadorService.FindFrequentEtiquetas())
            {
               // string tagInUrl = Server.UrlEncode(s);
                HyperLink link = new HyperLink();
                link.Text = e.tag;
                link.NavigateUrl = String.Format("./Pages/ShowCommentsByTag.aspx?tag={0}", e.tag);
                link.CssClass = GetCssClass(e.ocurrencias);
                ContentPlaceHolder1.Controls.Add(link);
                ContentPlaceHolder1.Controls.Add(new LiteralControl("  "));
            }
         
        }
        private string GetCssClass(int oft)
        {
            if (oft <= 2)
                return "TagSize1";
            if (oft <= 5)
                return "TagSize2";
            if (oft <= 9)
                return "TagSize3";
            if (oft <= 13)
                return "TagSize4";
            else
                return "TagSize5";
        }
    }
}