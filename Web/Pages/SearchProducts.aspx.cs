using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaIS.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaIS.Model;
using System.Xml;
using System.Text;

namespace Es.Udc.DotNet.PracticaIS.Web.Pages
{
    public partial class SearchProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnFindClick(object sender, EventArgs e)
        {

                String productName = this.txtIdentifier.Text;
                String url = String.Format("./ShowProducts.aspx?name={0}", productName);
                Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}