using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using Es.Udc.DotNet.PracticaIS.Model;
 
namespace Es.Udc.DotNet.PracticaIS.Web.Pages
{
    public partial class ShowValoraciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Hide the links */
            lnkAnterior.Visible = false;
            lnkSiguiente.Visible = false;

            /* Get de Service */
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            ValoracionBlock valoracionesB = new ValoracionBlock(null, 0, 0);
         

            int startIndex;
            string vendedorId = Request.Params.Get("vendedorId");
            
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            Int32 count = 10;
            valoracionesB = opinadorService.FindValoracionesAndNoteByVendedor(vendedorId, startIndex, count);
            int numValor = valoracionesB.NumValoraciones;
            if ( numValor == 0)
                lblNoValoraciones.Visible = true;
            foreach (Valoracion c in valoracionesB.Valoraciones)
            {
                c.UserProfileReference.Load();
            }

            this.gvValoraciones.DataSource = valoracionesB.Valoraciones;
            this.gvValoraciones.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "./ShowValoraciones.aspx?vendedorId=" + vendedorId + "&startIndex=" + (startIndex - count);

                this.lnkAnterior.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkAnterior.Visible = true;
            }

            /* "Next" link */
            if ((startIndex + count) < numValor)
            {
                String url = "./ShowValoraciones.aspx" + "?vendedorId=" + vendedorId + "&startIndex=" + (startIndex + count);
                this.lnkSiguiente.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkSiguiente.Visible = true;

            }

        }
    }
}