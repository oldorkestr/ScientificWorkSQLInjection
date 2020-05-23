using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ScientificWorkSQL
{
    public partial class SearchPageWithDynamicSQL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strConnection = ConfigurationManager
                .ConnectionStrings["connectionStr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                StringBuilder sbCommand = new
                    StringBuilder("Select * from Employees where 1 = 1");

                if (inputFirstname.Value.Trim() != "")
                {
                    sbCommand.Append(" AND FirstName=@FirstName");
                    SqlParameter param = new
                        SqlParameter("@FirstName", inputFirstname.Value);
                    cmd.Parameters.Add(param);
                }
                if (inputLastname.Value.Trim() != "")
                {
                    sbCommand.Append(" AND LastName=@LastName");
                    SqlParameter param = new
                        SqlParameter("@LastName", inputLastname.Value);
                    cmd.Parameters.Add(param);
                }
                if (inputGender.Value.Trim() != "")
                {
                    sbCommand.Append(" AND Gender=@Gender");
                    SqlParameter param = new
                        SqlParameter("@Gender", inputGender.Value);
                    cmd.Parameters.Add(param);
                }
                if (inputSalary.Value.Trim() != "")
                {
                    sbCommand.Append(" AND Salary=@Salary");
                    SqlParameter param = new
                        SqlParameter("@Salary", inputSalary.Value);
                    cmd.Parameters.Add(param);
                }

                cmd.CommandText = sbCommand.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                gvSearchResults.DataSource = rdr;
                gvSearchResults.DataBind();
            }
        }
    }
}