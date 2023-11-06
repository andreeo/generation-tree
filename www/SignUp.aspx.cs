using ClassLibraryGenTree;
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
    public partial class SingUp : System.Web.UI.Page
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

        protected void btnSingUp_Click(object sender, EventArgs e)
        {
            // borrar mensajes de error
            lblEmailError.Visible = false;
            lblErrorPassword.Visible = false;
            lblError.Visible = false;


            string username = tbxUsername.Text.Trim();
            string email = tbxEmail.Text.Trim();
            string firstName = tbxFirstName.Text.Trim();
            string lastName = tbxLastName.Text.Trim();
            string password = tbxPassword.Text.Trim();

            // validaciones
            bool isValid = true;
            if (!Utils.EsEMail(email)) {
                lblEmailError.Text = "- El email debe ser del formato \"name@domai.com\"";
                lblEmailError.ForeColor = Color.Red;
                lblEmailError.Visible = true;
                isValid = false;
            }
            if (password == null || password == "" || Utils.NivelComplejidad(password) < 3) {
                lblErrorPassword.Text = "- La password debe contener almenos 7 caracteres, mayusculas, numeros y simbolos";
                lblErrorPassword.ForeColor = Color.Red; 
                lblErrorPassword.Visible = true;
                isValid = false;
            }

            if (isValid)
            {
                User newUser = new User(0, username, firstName, lastName, email, password, Role.USER, true);
                bool saved = db.GuardaUsuario(newUser);
                if (saved)
                {
                    Response.Redirect("Login.aspx");        
                }
                lblError.Text = "Ha ocurrido un error";
              }
        }
    }
}