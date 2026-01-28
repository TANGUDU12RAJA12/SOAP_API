using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace CustomerServiceApp
{
    /// <summary>
    /// Summary description for AuthService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AuthService : System.Web.Services.WebService
    {

        [WebMethod]
        public string Login(string username, string password)
        {
            // 🔐 Hardcoded for learning (replace with DB later)
            if (username == "admin" && password == "admin123")
            {
                return GenerateToken(username);
            }
            return "INVALID";
        }

        private string GenerateToken(string username)
        {
            string data = username + "|" + DateTime.Now.AddMinutes(30);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }
    }
}
