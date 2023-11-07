using ClassLibraryGenTree;
using DBLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace www
{
    public partial class NewPerson : System.Web.UI.Page
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
        }

        protected void lblSave_Click(object sender, EventArgs e)
        {
            // borrar mensajes de error
            lblErrors.Text = "";
            lblErrors.Visible = false;


            string firstName = tbxName.Text.Trim();
            string secondName = tbxSecondName.Text.Trim();
            string surname = tbxSurname.Text.Trim();
            string secondSurname = tbxSecondSurname.Text.Trim();
            string birthDateTxt = tbxBirhtDate.Text.Trim();

            // validaciones
            bool isValid = true;
            if (firstName == null || firstName.Equals(""))
            {
                lblErrors.Text += "- El nombre es obligatorio\n";
                lblErrors.ForeColor = Color.Red;
                lblErrors.Visible = true;
                isValid = false;
            }
            if (surname == null || surname.Equals(""))
            {
                lblErrors.Text += "- El apellido es obligatorio\n";
                lblErrors.ForeColor = Color.Red;
                lblErrors.Visible = true;
                isValid = false;
            }
            DateTime birthDate;
            if (!DateTime.TryParse(birthDateTxt, out birthDate))
            {
                lblErrors.Text += "- La fecha de nacimiento es invalida\n";
                lblErrors.ForeColor = Color.Red;
                lblErrors.Visible = true;
                isValid = false;
            }


            if (isValid)
            {
                Person newPerson = new Person(0,firstName,secondName,surname,secondSurname, birthDate);
                bool saved = db.GuardaPersona(newPerson);
                if (saved)
                {
                    Response.Redirect("Default.aspx");
                }
                lblErrors.Text += "Ha ocurrido un error\n";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}