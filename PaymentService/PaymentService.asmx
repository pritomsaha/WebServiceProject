<%@ WebService Language="C#" Class="PaymentService.PaymentService" %>
using System;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;

namespace PaymentService
{
    [WebService(Namespace = "dse.webservices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
	class PaymentService: WebService
	{
        private IDbConnection dbcon;
        public PaymentService(){
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
        public double getPayment(String transactionNum){
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
            string sql = "SELECT amount FROM payment where transacNum ='"+transactionNum+"';";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            double amount = -1;
            if(reader.Read()){
                amount = (float) reader["amount"];
            }
            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();
            return amount;
        }
	}
}
