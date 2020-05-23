using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ScientificWorkSQL
{
    public partial class SearchPageWithoutDynamicSQL : System.Web.UI.Page
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
                    sbCommand.Append(" AND FirstName = '" +
                        inputFirstname.Value + "'");
                }


                if (inputLastname.Value.Trim() != "")
                {
                    sbCommand.Append(" AND LastName = '" +
                        inputLastname.Value + "'");
                }

                if (inputGender.Value.Trim() != "")
                {
                    sbCommand.Append(" AND Gender = '" +
                        inputGender.Value + "'");
                }

                if (inputSalary.Value.Trim() != "")
                {
                    sbCommand.Append(" AND Salary = " + inputSalary.Value);
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