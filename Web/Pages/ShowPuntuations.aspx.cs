using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using Es.Udc.DotNet.PracticaIS.Model.ComentarioDao;
using Es.Udc.DotNet.PracticaIS.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Configuration;
using Es.Udc.DotNet.PracticaIS.Model;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.Practices.Unity;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Collections;



namespace Es.Udc.DotNet.PracticaIS.Web.Pages
{
    public partial class ShowPuntuations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex;
            string vendedorId = Request.Params.Get("vendedor");

            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            int count = 10;
            /* Get de Service */
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            ValoracionBlock valoraciones = new ValoracionBlock(null,0,0);
            cellLoginName.Text = vendedorId;
            try
            {
                valoraciones = opinadorService.FindValoracionesAndNoteByVendedor(vendedorId, startIndex, count);
            }
            catch (InstanceNotFoundException)
            {
                cellAverage.Visible = false;
            }
            
            cellAverage.Text =valoraciones.AverageValoracion.ToString();
            cellNumerOfVotes.Text = valoraciones.NumValoraciones.ToString();
        }

        protected void BtnShowValoracionesClick(object sender, EventArgs e)
        {
            String   vendedor = Request.Params.Get("vendedor");;
            String url = String.Format("./ShowValoraciones.aspx?vendedorId={0}", vendedor);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
 
        
    }
}