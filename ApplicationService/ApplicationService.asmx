<%@ WebService Language="C#" Class="ApplicationService.ApplicationService" %>
using System;
using System.Web.Services;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ApplicationService
{
    [WebService(Namespace = "dse.webservices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
	class ApplicationService:WebService
	{
        private IDbConnection dbcon;
        public ApplicationService(){
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
        public long apply(string regNum, string transacNum, int degreeId, string email)
        {
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
            string sql = "select id from application where transacNum='"+transacNum+"';";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            if(reader.Read()){
                dbcon.Close();
                return -1;
            }
            reader.Close();
            dbcmd.Dispose();

            sql = "insert into application (regNum, transacNum, degreeId, email, date) values('"+regNum+"', '"+transacNum+"', '"+degreeId+"', '"+email+"', now());";
            dbcmd.CommandText = sql;
            dbcmd.CommandText = sql;
            dbcmd.ExecuteNonQuery();
            
            sql = "SELECT LAST_INSERT_ID();";
            dbcmd.CommandText = sql;
            reader = dbcmd.ExecuteReader();
            long appId = -1;
            if(reader.Read()){
                appId = reader.GetInt64(0);
            }
            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();

            return appId;
        }

        [WebMethod]
        public List<Application> getApplications(){
            List<Application> applicationList = new List<Application>();
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
       
            string sql = "SELECT id, regNum, email, degreeId, date, approved FROM application where approved=false;";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            while(reader.Read()){
                int id = System.Convert.ToInt32(reader["id"]);
                string regNum = (string)reader["regNum"];
                string email = (string)reader["email"];
                int degreeId = System.Convert.ToInt32(reader["degreeId"]);
                DateTime dt = (DateTime)reader["date"];
                string date = dt.ToString("dd-MM-yyyy");
                bool approved = (bool)reader["approved"];
                Application application = new Application(id, regNum, degreeId, email, date, approved);
                applicationList.Add(application);
            }
            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();
            return applicationList;
        }

        [WebMethod]
        public void makeDelivered(int appId){
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
       
            string sql = "update application set approved=1 where id='"+appId+"';";
            dbcmd.CommandText = sql;
            dbcmd.ExecuteReader();
            dbcmd.Dispose();
            dbcon.Close();

        }

        [WebMethod]
        public (int, bool) getApplicationStatus(string regNum, string transacNum){
            int appId = -1;
            bool approved = false;

            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
            string sql = "select id, approved from application where regNum='"+regNum+"' AND transacNum='"+transacNum+"';";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            if(reader.Read()){
                appId = System.Convert.ToInt32(reader["id"]);
                approved = (bool)reader["approved"];
            }
            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();

            return (appId, approved);
        }
       

	}
}
 