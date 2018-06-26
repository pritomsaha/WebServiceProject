<%@ WebService Language="C#" Class="StudentVerifierService.ResultService" %>
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;

namespace StudentVerifierService
{
    [WebService(Namespace = "dse.webservices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
	class ResultService:WebService
	{
        private IDbConnection dbcon;
        public ResultService(){
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
        public Result getUpdatedResult(string regNum, int degreeId){

            (string degreeName, int totalSemester) = getDegreeInfo(degreeId);
            Result result = null;
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();

            string sql = "SELECT cgpa, passYear, MAX(semester) as semester FROM result where cgpa is not NULL and regNum ='"+regNum+"' AND degreeId ='"+degreeId+"' group by cgpa, passYear;";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();

            if(reader.Read()){
                result = new Result();
                result.regNum = regNum;
                result.degree = degreeName;
                result.cgpa = (float)reader["cgpa"];
                result.passYear = (int)reader["passYear"];
                result.semester = (int)reader["semester"];
                result.totalSemester = totalSemester;
            }
            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();
            return result;
        }

        [WebMethod]
        public bool checkStudentInResult(string regNum, int degreeId){

            bool ret = false;
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
            
            string sql = "SELECT id FROM result where regNum ='"+regNum+"' AND degreeId ='"+degreeId+"';";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            if(reader.Read()){ret = true;}

            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();

            return ret;
        }


        [WebMethod]
        public List<Degree> getDegreeList(){
           
            List<Degree> degreeList = new List<Degree>();

            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();

            string sql = "SELECT id, fullName, shortName FROM degreeProgram;";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            while(reader.Read()){
                int id = System.Convert.ToInt32(reader["id"]);
                string degreeName = (string)reader["shortName"];
                Degree degree = new Degree(id, degreeName);
                degreeList.Add(degree);
            }
            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();
            return degreeList;
        }

        public (string, int) getDegreeInfo(int degreeId){
            string degreeName = "";
            int totalSemester = -1;
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
            string sql = "select CONCAT(fullName,' (', shortName, ')') as degreeName, semesterNum from degreeProgram where id ='"+degreeId+"';";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            if(reader.Read()){
                degreeName = (string)reader["degreeName"];
                totalSemester = (int)reader["semesterNum"];
            }
            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();
            return (degreeName, totalSemester);
        }
	}
}
