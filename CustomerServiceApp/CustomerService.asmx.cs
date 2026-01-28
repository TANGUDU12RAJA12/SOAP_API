using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace CustomerServiceApp
{
    /// <summary>
    /// Summary description for CustomerService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CustomerService : System.Web.Services.WebService
    {


        public  string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        [WebMethod]
        public List<string> GetAllCustomers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<string> lst = new List<string>();
                string result = "";
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from dbo.Customers", conn);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                var reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                    int id = reader.GetInt32(0);
                    string Name = reader.GetString(1);
                    string Email = reader.GetString(2);
                    result = $"Id : {id} , Name: {Name} , Email : {Email}";
                    lst.Add(result);
                    }
                return lst;
            }
        }

        [WebMethod]
        public string CreateCustomer(string name, string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers (Name, Email) VALUES (@name, @email); SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                conn.Open();
                cmd.ExecuteScalar();
                return "Customer created successfully";
            }
        }

        [WebMethod]
        public string UpdateCustomer(int id, string name, string email)
        {
            // 🔐 STEP 1: Read token from HTTP header
            string token = HttpContext.Current.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(token))
            {
                throw new SoapException(
                    "Token missing",
                    SoapException.ClientFaultCode
                );
            }

            // Remove Bearer keyword
            token = token.Replace("Bearer ", "");

            // 🔐 STEP 2: Validate token
            if (!TokenValidator.IsValid(token))
            {
                throw new SoapException(
                    "Invalid or Expired Token",
                    SoapException.ClientFaultCode
                );
            }

            // ✅ STEP 3: Database Update Logic
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Customers SET Name=@name, Email=@email WHERE Id=@id",
                    conn
                );

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                return rows > 0
                    ? "Customer Updated Successfully"
                    : "Customer Not Found";
            }
        }

        [WebMethod]
        public string DeleteCustomer(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0 ? "Deleted" : "Not found";
            }
        }
    }
}
