using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaIS.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaIS.Model;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Configuration;
using Es.Udc.DotNet.PracticaIS.Model;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Web.UI.WebControls;


namespace Es.Udc.DotNet.PracticaIS.Web.Pages
{
    public partial class ShowCommentsByTag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Hide the links */
            lnkAnterior.Visible = false;
            lnkSiguiente.Visible = false;

            /* Get de Service */
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();

            int startIndex;
            string tag = Request.Params.Get("tag");
            Int32 count = 5;
           
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }
            int numeroDeComentarios = opinadorService.FindComentariosByEtiqueta(tag,startIndex,count).Count();

            if (numeroDeComentarios == 0)
                lblNoComments.Visible = true;

            List<Comentario> comentarios = opinadorService.FindComentariosByEtiqueta(tag, startIndex, count);
            foreach (Comentario c in comentarios)
            {
                c.UserProfileReference.Load();
            }
            this.gvComments.DataSource = comentarios;
            this.gvComments.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "./ShowCommentsByTag.aspx?tag=" + tag + "&startIndex=" + (startIndex - count);

                this.lnkAnterior.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkAnterior.Visible = true;
            }

            /* "Next" link */
            if ((startIndex + count) < numeroDeComentarios)
            {
                String url = "./ShowCommentsByTag.aspx" + "?tag=" + tag + "&startIndex=" + (startIndex + count);
                this.lnkSiguiente.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkSiguiente.Visible = true;

            }
        }
    }
}