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
using System.Data;
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Collections;

namespace Es.Udc.DotNet.PracticaIS.Web.Pages
{
    public partial class ShowComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Hide the links */
            lnkAnterior.Visible = false;
            lnkSiguiente.Visible = false;

            /* Get de Service */
            IUnityContainer container =(IUnityContainer)HttpContext.Current. Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            
            int startIndex;
            long idProducto = long.Parse(Request.Params.Get("idProducto"));

            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }
           
            Int32 count = 5;
            if (opinadorService.FindComentariosByProductoId(idProducto, startIndex, count).Count == 0)
                lblNoComments.Visible = true;
            
            List<Comentario> comentarios = opinadorService.FindComentariosByProductoId(idProducto, startIndex, count);
            foreach (Comentario c in comentarios)
            {
                c.UserProfileReference.Load();
            }
            this.gvReviews.DataSource = comentarios;
            this.gvReviews.DataBind();
               
            int numeroDeComentarios = opinadorService.GetNumberOfComentariosByProductoId(idProducto);

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "./ShowComments.aspx?idProducto=" + idProducto + "&startIndex=" + (startIndex - count);

                this.lnkAnterior.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkAnterior.Visible = true;
            }

            /* "Next" link */
            if ((startIndex + count) < numeroDeComentarios)
            {
                String url = "./ShowComments.aspx" + "?idProducto=" + idProducto + "&startIndex=" + (startIndex + count);
                this.lnkSiguiente.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkSiguiente.Visible = true;

            }

        }
    }
}