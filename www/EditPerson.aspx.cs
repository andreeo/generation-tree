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
    public partial class EditPerson : System.Web.UI.Page
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
            User authenticatedUser = (User)Session["authenticatedUser"];
            if (authenticatedUser == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!(authenticatedUser.Role == Role.ADMINISTRATOR || authenticatedUser.Role == Role.MANAGER))
            {
                Response.Redirect("Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                ddlMother.Items.Add(new ListItem("", "-"));
                ddlFather.Items.Add(new ListItem("", "-"));

                foreach (Person p in db.LeePersonas())
                {
                    ddlSelectPerson.Items.Add(new ListItem(p.First_name + " " + p.First_surname + "; id= " + p.Id, ((int)p.Id).ToString()));
                    ddlMother.Items.Add(new ListItem(p.First_name + " " + p.First_surname + "; id= " + p.Id, ((int)p.Id).ToString()));
                    ddlFather.Items.Add(new ListItem(p.First_name + " " + p.First_surname + "; id= " + p.Id, ((int)p.Id).ToString()));
                }
                ddlFather.SelectedValue = null;
                ddlMother.SelectedValue = null;
            }
        }

        protected void btnSelectPerson_Click(object sender, EventArgs e)
        {
            int selectedPersonId = Convert.ToInt32(ddlSelectPerson.SelectedValue);

            Person selectedPerson = db.LeePersona(selectedPersonId);
            if (selectedPerson == null)
            {
                lblError.Text += "Persona no encontrada id: " + selectedPersonId + "\n";
                lblError.Visible = true;
            }
            else
            {
                tbxName.Text = selectedPerson.First_name; tbxName.Enabled = true;
                tbxSecondName.Text = selectedPerson.Second_name; tbxSecondName.Enabled = true;

                tbxSurname.Text = selectedPerson.First_surname; tbxSurname.Enabled = true;
                tbxSecondSurname.Text = selectedPerson.Second_surname; tbxSecondSurname.Enabled = true;

                tbxBirthDate.Text = selectedPerson.Birth_day.ToString(); tbxBirthDate.Enabled = true;

                ddlMother.Enabled = true;
                if (selectedPerson.Mother != null)
                {
                    ddlMother.SelectedValue = selectedPerson.Mother.Id.ToString();
                }

                ddlFather.Enabled = true;
                if (selectedPerson.Father != null)
                {
                    ddlFather.SelectedValue = selectedPerson.Father.Id.ToString();
                }

                btnSave.Enabled = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // borrar mensajes de error
            lblError.Text = "";
            lblError.Visible = false;

            string name = tbxName.Text.Trim();
            string secondName = tbxSecondName.Text.Trim();
            string surname = tbxSurname.Text.Trim();
            string secondSurname = tbxSecondSurname.Text.Trim();
            string birthDateTxt = tbxBirthDate.Text.Trim();


            // validaciones
            bool isValid = true;
            if (name == null || name.Equals(""))
            {
                lblError.Text += "- El nombre es obligatorio\n";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
                isValid = false;
            }
            if (surname == null || surname.Equals(""))
            {
                lblError.Text += "- El apellido es obligatorio\n";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
                isValid = false;
            }
            DateTime birthDate;
            if (!DateTime.TryParse(birthDateTxt, out birthDate))
            {
                lblError.Text += "- La fecha de nacimiento es invalida\n";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
                isValid = false;
            }

            int selectedPersonId = Convert.ToInt32(ddlSelectPerson.SelectedValue);
            Person selectedPerson = db.LeePersona(selectedPersonId);
            lblError.Text += "[encontrado]" + selectedPerson.ToString();
            if (selectedPerson == null)
            {
                lblError.Text += "Persona no encontrada id: " + selectedPersonId + "\n";
                lblError.Visible = true;
            }
            else
            {

                try
                {
                    Person mother = null;
                    Person father = null;
                    if (!ddlMother.SelectedValue.Trim().Equals("-"))
                    {
                        int motherId = Convert.ToInt32(ddlMother.SelectedValue);
                        mother = db.LeePersona(motherId);
                        selectedPerson.Mother = mother;
                    }
                    if (!ddlFather.SelectedValue.Trim().Equals("-"))
                    {
                        int fatherId = Convert.ToInt32(ddlFather.SelectedValue);
                        father = db.LeePersona(fatherId);
                        selectedPerson.Father = father;
                    }
                }
                catch (ApplicationException ex)
                {
                    lblError.Text += "Error ancestro no valido";
                    lblError.Visible = true;
                    return;
                }
                selectedPerson.First_name = name;
                selectedPerson.Second_name = secondName;
                selectedPerson.First_surname = surname;
                selectedPerson.Second_surname = secondSurname;
                selectedPerson.Birth_day = birthDate;
                db.GuardaPersona(selectedPerson);
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}