<%@ WebService Language="C#" Class="AuthService.AuthService" %>
using System;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;

namespace AuthService
{
    [WebService(Namespace = "dse.webservices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
	class AuthService : WebService
    {
        private IDbConnection dbcon;
        public AuthService(){
            string connectionString =
                "Server=localhost;" +
                "Database=DSE;" +
                "User ID=root;" +
                "Password=root;" +
                "Pooling=false;"+
                "SslMode=none";
            dbcon = new MySqlConnection(connectionString);   
        }
        [WebMethod]
        public bool login(string username, string password){
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();

            string sql = "SELECT username from Auth where username='"+username+"' AND password='"+password+"';";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            if(reader.Read()){
                return true;
            }
            return false;   
        }
	
	}
}
