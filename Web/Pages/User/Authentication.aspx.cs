using System;
using System.Web.Security;
using System.Web.UI.WebControls;

using Es.Udc.DotNet.PracticaIS.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaIS.Model.UserService.Exceptions;

namespace Es.Udc.DotNet.PracticaIS.Web.Pages.User
{

    public partial class Authentication : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lblPasswordError.Visible = false;
            lblLoginError.Visible = false;
          
        }

        /// <summary>
        /// Handles the Click event of the btnLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void BtnLoginClick(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                try
                {
                    SessionManager.Login(Context, txtLogin.Text,
                        txtPassword.Text, checkRememberPassword.Checked);

                    FormsAuthentication.
                        RedirectFromLoginPage(txtLogin.Text,
                            checkRememberPassword.Checked);
                }
                catch (InstanceNotFoundException)
                {
                    lblLoginError.Visible = true;
                }
                catch (IncorrectPasswordException)
                {
                    lblPasswordError.Visible = true;
                }

            }
            if (!Page.IsPostBack)
            {
                string urlReturn = Request.QueryString["ReturnUrl"];
                Response.Redirect(urlReturn);
            }
        }
    }
}
