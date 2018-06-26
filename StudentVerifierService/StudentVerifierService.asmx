<%@ WebService Language="C#" Class="StudentVerifierService.StudentVerifierService" %>
using System;
using System.Collections;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;

namespace StudentVerifierService
{
    [WebService(Namespace = "dse.webservices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
	class StudentVerifierService: WebService
	{
        private IDbConnection dbcon;
        public StudentVerifierService(){
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
        public Student getStudentInfo(string regNum){
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
            Student student = null;

            string sql = "SELECT name, gender, fatherName, motherName, session, instOrDept FROM student where regNum ='"+regNum+"';";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            if(reader.Read()){
                student = new Student();
                student.regNum = regNum;
                student.name = (string)reader["name"];
                student.gender = (string)reader["gender"];
                student.fatherName = (string)reader["fatherName"];
                student.motherName = (string)reader["motherName"];
                student.session = (string)reader["session"];
                student.instOrDept = (string)reader["instOrDept"];
            }
            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();
            return student;
        }

        [WebMethod]
        public bool verifyStudent(string regNum){
            bool ret = false;
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
            Student student = null;

            string sql = "SELECT id FROM student where regNum ='"+regNum+"';";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            if(reader.Read()){
                ret = true;
            }
            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();
            return ret;
        }
        
	}
}
