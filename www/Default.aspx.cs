using ClassLibraryGenTree;
using DBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace www
{
    public partial class Default: System.Web.UI.Page
    {
        IDB db = null;
        User authenticatedUser = null;

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
            authenticatedUser = (User)Session["authenticatedUser"];
            if (authenticatedUser == null)
            {
               Response.Redirect("Login.aspx");
            }
            if (authenticatedUser.Role == Role.ADMINISTRATOR || authenticatedUser.Role == Role.MANAGER) 
            {
                LinkButtonNewPerson.Enabled = true;
                LinkButtonNewPerson.Visible = true;

                LinkButtonEditPerson.Enabled = true;
                LinkButtonEditPerson.Visible = true;
            }

            lblUser.Text = "Bienvenido: " + authenticatedUser.First_name + " " + authenticatedUser.Last_name + " -Rol: " + authenticatedUser.Role +"-";

        }

        protected void LinkButtonSignOut_Click(object sender, EventArgs e)
        {
            Session["authenticatedUser"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void LinkButtonNewPerson_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewPerson.aspx");
        }

        protected void LinkButtonAncestors_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ancestors.aspx");
        }

        protected void LinkButtonEditPerson_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditPerson.aspx");
        }

    }
}