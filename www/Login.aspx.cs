using DBLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace www
{
    public partial class Login : System.Web.UI.Page
    {
        IDB db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = (IDB)Session["db"]; // cargamos la base de datos desde la session
            if (db == null)
            {
                // si no existe la base de datos la inicializamos y guardamos en la sesion
                if (db == null)
                {
                    db = new DB();
                    Session["db"] = db;
                }
            }
            // si hay un usuario logeado redirigimos
            if (Session["authenticatedUser"] != null)
            {
                Response.Redirect("Default.aspx");
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Text = "";
            lblErrorMsg.Visible = false;
            string email = tbxEmail.Text.Trim();
            string password = tbxPassword.Text.Trim();

            if (db.ValidaUsuario(email, password))
            {
                ClassLibraryGenTree.User user = db.LeeUsuario(email);
                Session["authenticatedUser"] = user;
                Response.BufferOutput = true;

                Response.Redirect("Default.aspx");

            }
            else
            {
                lblErrorMsg.Text = "Error: Email / Password erroneos";
                lblErrorMsg.ForeColor = Color.Red;
                lblErrorMsg.Visible = true;
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUp.aspx");
        }
    }
}