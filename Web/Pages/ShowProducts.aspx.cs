using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaIS.Model;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Text;
using Es.Udc.DotNet.PracticaIS.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaIS.Model;
using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using System.Data;
using System.Reflection;

namespace Es.Udc.DotNet.PracticaIS.Web.Pages
{
    public partial class ShowProducts : System.Web.UI.Page
    {
      

        protected void Page_Load(object sender, EventArgs e)
        {

            int startIndex;
            String productName = Request.Params.Get("name");
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IOpinadorService opinadorService = container.Resolve<IOpinadorService>();
            /* Get Products*/
            XmlDocument xmlDocument = opinadorService.FindProductos(productName,startIndex);

            aspXML.XPathNavigator = xmlDocument.CreateNavigator();
            aspXML.TransformSource = Server.MapPath("../XSLT/ShowProducts.xsl");

            XsltArgumentList argList = new XsltArgumentList();
            argList.AddParam("name","",GetLocalResourceObject("name"));
            argList.AddParam("price", "", GetLocalResourceObject("price"));
            argList.AddParam("remainingTime", "", GetLocalResourceObject("remainingTime"));
            argList.AddParam("vendedor", "", GetLocalResourceObject("vendedor"));
            argList.AddParam("showVotes", "", GetLocalResourceObject("showVotes"));
            argList.AddParam("favorito", "", GetLocalResourceObject("favorito"));
            

            argList.AddParam("addComment", "", GetLocalResourceObject("addComment"));
            String urlAddComment = Response.ApplyAppPathModifier("./Comment.aspx?idProducto=");
            argList.AddParam("urlAddComment", "",urlAddComment);

            argList.AddParam("showComment", "", GetLocalResourceObject("showComment"));
            String urlShowComment = Response.ApplyAppPathModifier("./ShowComments.aspx?idProducto=");
            argList.AddParam("urlShowComment", "", urlShowComment);

            argList.AddParam("vote", "", GetLocalResourceObject("vote"));
            String urlVote = Response.ApplyAppPathModifier("./Vote.aspx?Vendedor=");
            argList.AddParam("urlVote", "", urlVote);

            argList.AddParam("showPuntuation", "", GetLocalResourceObject("showPuntuation"));
            String urlShowPuntuation = Response.ApplyAppPathModifier("./ShowPuntuations.aspx?Vendedor=");
            argList.AddParam("urlShowPuntuation", "", urlShowPuntuation);

            String urlFavorito = Response.ApplyAppPathModifier("./AddFavorito.aspx?idProducto=");
            argList.AddParam("urlFavorito", "", urlFavorito);
            
            aspXML.TransformArgumentList = argList;

            Boolean existMoreProducts = true;
            int startIndex2 = 0;
            XmlNodeList nodes = xmlDocument.GetElementsByTagName("ExistMoreProducts");
          
         
            if (nodes[0].FirstChild.InnerText == "\nfalse\n")
            {
                existMoreProducts = false;
            }
            int count = 10;
            if (startIndex != 0)
            {
                if (startIndex - count > 0)
                    startIndex = startIndex - count;
                else
                    startIndex = 0;

                lnkAnterior.NavigateUrl = Response.ApplyAppPathModifier(lnkAnterior.NavigateUrl + productName + "&startIndex=" + startIndex);

            }
            else
            {
                lnkAnterior.Visible = false;
            }
           
            if (existMoreProducts)
            {

                startIndex2 = startIndex2 + count;
                lnkSiguiente.NavigateUrl = Response.ApplyAppPathModifier(lnkSiguiente.NavigateUrl + productName + "&startIndex=" + startIndex2);
            }
            else
                lnkSiguiente.Visible = false;
         
        }


    }


}