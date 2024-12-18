using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.CodeDom;

namespace GridPagingSorting
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
                BindGrid2();
            }
        }

        private void BindGrid()
        {
            string query = "SELECT * FROM Person";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();

                            //for sorting
                            ViewState["dirState"] = dt;
                            ViewState["sortdr"] = "Asc";
                        }
                    }
                }
            }
        }

        private void BindGrid2()
        {
            string query = "SELECT * FROM Person";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                        }
                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dirState"];
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();


            }
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            BindGrid2();
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            BindGrid2();
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value.ToString());
            string query = "DELETE FROM Person WHERE Id=@Id";
            ExecuteQuery(query, new SqlParameter("@Id", id));
            BindGrid2();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //int id = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value.ToString());
            //string firstName = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox1")).Text;
            //string lastName = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox2")).Text;
            //string phone = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox3")).Text;
            //string postalCode = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox4")).Text;
            //int income = Convert.ToInt32(((TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox5")).Text);

            //string query = "UPDATE Person SET FirstName=@FirstName, LastName=@LastName, Phone=@Phone, PostalCode=@PostalCode, Income=@Income WHERE Id=@Id";
            //ExecuteQuery(query, new SqlParameter("@FirstName", firstName), new SqlParameter("@LastName", lastName), new SqlParameter("@Phone", phone), new SqlParameter("@PostalCode", postalCode), new SqlParameter("@Income", income), new SqlParameter("@Id", id));
            //GridView2.EditIndex = -1;


            int id = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = GridView2.Rows[e.RowIndex];

            string firstName = ((TextBox)row.Cells[1].Controls[0]).Text;
            string lastName = ((TextBox)row.Cells[2].Controls[0]).Text;
            string phone = ((TextBox)row.Cells[3].Controls[0]).Text;
            string postalCode = ((TextBox)row.Cells[4].Controls[0]).Text;
            int income = Convert.ToInt32(((TextBox)row.Cells[5].Controls[0]).Text);

            string query = "UPDATE Person SET FirstName=@FirstName, LastName=@LastName, Phone=@Phone, PostalCode=@PostalCode, Income=@Income WHERE Id=@Id";
            ExecuteQuery(query, new SqlParameter("@FirstName", firstName), new SqlParameter("@LastName", lastName), new SqlParameter("@Phone", phone), new SqlParameter("@PostalCode", postalCode), new SqlParameter("@Income", income), new SqlParameter("@Id", id));
            GridView2.EditIndex = -1;




            BindGrid2();
        }

        private void ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            //string query = "INSERT INTO Person (FirstName, LastName, Phone, PostalCode, Income) VALUES (@FirstName, @LastName, @Phone, @PostalCode, @Income)";
            //ExecuteQuery(query, new SqlParameter("@FirstName", "New FirstName"), new SqlParameter("@LastName", "New LastName"), new SqlParameter("@Phone", "1234567890"), new SqlParameter("@PostalCode", "123456"), new SqlParameter("@Income", 0));
            //BindGrid2();

            string firstName = txtNewFirstName.Text;
            string lastName = txtNewLastName.Text;
            string phone = txtNewPhone.Text;
            string postalCode = txtNewPostalCode.Text;
            int income = Convert.ToInt32(txtNewIncome.Text);

            string query = "INSERT INTO Person (FirstName, LastName, Phone, PostalCode, Income) VALUES (@FirstName, @LastName, @Phone, @PostalCode, @Income)";
            ExecuteQuery(query, new SqlParameter("@FirstName", firstName), new SqlParameter("@LastName", lastName), new SqlParameter("@Phone", phone), new SqlParameter("@PostalCode", postalCode), new SqlParameter("@Income", income));
            BindGrid2();

            // Clear the textboxes after inserting
            txtNewFirstName.Text = "";
            txtNewLastName.Text = "";
            txtNewPhone.Text = "";
            txtNewPostalCode.Text = "";
            txtNewIncome.Text = "";
        }

       
    }
}