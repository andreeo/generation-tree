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
    public partial class Ancestors : System.Web.UI.Page
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
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                foreach (Person p in db.LeePersonas())
                {
                    ddlPersons.Items.Add(new ListItem(p.First_name + " " + p.First_surname, ((int)p.Id).ToString()));
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Clear
            TreeViewAncestors.Nodes.Clear();
            lblError.Text = "";
            lblError.Visible = true;


            string depthTxt = tbxDepth.Text; // should be int // should be >=1
            int idPerson = Convert.ToInt32(ddlPersons.SelectedValue);
            int depth;
            if (Int32.TryParse(depthTxt, out depth))
            {
                if (depth > 0)
                {
                    Person p = db.LeePersona(idPerson);
                    if (p == null)
                    {
                        lblError.Text += "Persona no encontrada id: " + idPerson + "\n";
                        lblError.Visible = true;
                    }
                    else
                    {
                        TreeNode rootNode = new TreeNode(p.ToString(), p.Id.ToString());
                        TreeViewAncestors.Nodes.Add(rootNode);
                        BuildTreeView(rootNode, p, depth, 1);
                        TreeViewAncestors.ExpandAll();
                    }
                }
                else
                {
                    lblError.Text += "La profundidad debe ser un numero mayor a 1 \n ";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text += "La profundidad debe ser un numero \n ";
                lblError.Visible = true;
            }


        }
        private void BuildTreeView(TreeNode parentNode, Person person, int maxDepth, int currentDepth)
        {
            if (person == null || currentDepth > maxDepth)
            {
                return;
            }

            Person father = person.Father;
            TreeNode fatherNode = null;
            if (father != null)
            {
                fatherNode = new TreeNode($"[Padre] {father}", father.Id.ToString());
                parentNode.ChildNodes.Add(fatherNode);
            }
;
            Person mother = person.Mother;
            TreeNode motherNode = null;
            if (mother != null)
            {
                motherNode = new TreeNode($"[Madre] {person.Mother}", person.Mother.Id.ToString());
                parentNode.ChildNodes.Add(motherNode);
            }


            BuildTreeView(fatherNode, person.Father, maxDepth, currentDepth + 1);
            BuildTreeView(motherNode, person.Mother, maxDepth, currentDepth + 1);
        }

    }
}