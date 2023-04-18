using Q1WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Q1WebApp.Data
{
    public class AppDAO
    {
        private static string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Q1WebAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AuthenticatePerson(PersonLoginCredModel credModel)
        {
            bool authenticate = false;

            string findUserQuery = "SELECT * FROM dbo.Person WHERE Name = @Name AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(findUserQuery, conn);

                cmd.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, 50).Value = credModel.Username;
                cmd.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = credModel.Password;
                try
                {
                    conn.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    authenticate = sqlDataReader.HasRows;
                    sqlDataReader.Close();
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return authenticate;
        }

        public int GetPersonId(PersonLoginCredModel personLoginCred)
        {
            int idNum = 1;
            string getPersonQry = "SELECT Id FROM dbo.Person WHERE Name = @Name AND Password = @Password";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand sql = new SqlCommand(getPersonQry, conn);
                sql.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, 50).Value = personLoginCred.Username;
                sql.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = personLoginCred.Password;
                try
                {
                    conn.Open();
                    SqlDataReader sqlDataReader = sql.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            idNum = sqlDataReader.GetInt32(0);
                        }
                    }
                    sqlDataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return idNum;

        }

        public PersonModel GetPerson(PersonLoginCredModel personLoginCred)
        {
            PersonModel person = new PersonModel();
            string getPersonQry = "SELECT * FROM dbo.Person WHERE Name = @Name AND Password = @Password";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand sql = new SqlCommand(getPersonQry, conn);
                sql.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, 50).Value = personLoginCred.Username;
                sql.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = personLoginCred.Password;
                try
                {
                    conn.Open();
                    SqlDataReader sqlDataReader = sql.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read()) {
                            person.Id = sqlDataReader.GetInt32(0);
                            person.Name = sqlDataReader.GetString(1);
                            person.Surname = sqlDataReader.GetString(2);
                            person.Password = sqlDataReader.GetString(3);
                            person.LastLogin = sqlDataReader.GetDateTime(4);
                        }
                    }
                    sqlDataReader.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return person;
        }

        public InfoModel GetInfo(PersonLoginCredModel personLoginCred)
        {
            int personId = GetPersonId(personLoginCred);
            InfoModel personInfo = new InfoModel();

            string getInfoQry = "SELECT * FROM dbo.Info WHERE PersonId = @personId";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand sql = new SqlCommand(getInfoQry, conn);
                sql.Parameters.Add("@personId", System.Data.SqlDbType.Int, 50).Value = personId;
                try
                {
                    conn.Open();
                    SqlDataReader sqlDataReader = sql.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            personInfo.InfoId = sqlDataReader.GetInt32(0);
                            personInfo.PersonId = sqlDataReader.GetInt32(1);
                            personInfo.TelNo = sqlDataReader.GetString(2);
                            personInfo.CellNo = sqlDataReader.GetString(3);
                            personInfo.AddressLine1 = sqlDataReader.GetString(4);
                            personInfo.AddressLine2 = sqlDataReader.GetString(5);
                            personInfo.AddressLine3 = sqlDataReader.GetString(6);
                            personInfo.AddressCode = sqlDataReader.GetString(7);
                            personInfo.PostalAddress1 = sqlDataReader.GetString(8);
                            personInfo.PostalAddress2 = sqlDataReader.GetString(9);
                            personInfo.PostalCode = sqlDataReader.GetString(10);
                        }
                    }
                    sqlDataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return personInfo;
        }

        public PersonInfoViewModel GetPersonInfo(PersonLoginCredModel personLoginCred) 
        {
            PersonInfoViewModel infoViewModel = new PersonInfoViewModel();
            infoViewModel.personModel = GetPerson(personLoginCred);
            infoViewModel.infoModel = GetInfo(personLoginCred);
            return infoViewModel;
        }

        public PersonInfoViewModel UpDatePersonInfo(PersonInfoViewModel personInfo)
        {
            if (personInfo.infoModel == null && personInfo.personModel == null)
            {
                return null;
            }
            PersonInfoViewModel success = null;

            string updatePersonQry = "UPDATE dbo.Person SET Password = @Password WHERE Id = @Id";

            string updateInfoQry = "UPDATE dbo.Info SET TelNo = @TelNo, CellNo = @CellNo, AddressLine1 = @AddressLine1, " +
                "AddressLine2 = @AddressLine2, AddressLine3 = @AddressLine3, AddressCode = @AddressCode, PostalAddress1 = @PostalAddress1, " +
                "PostalAddress2 = @PostalAddress2, PostalCode = @PostalCode WHERE InfoId = @InfoId";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand sqlUpdatePerson = new SqlCommand(updatePersonQry, conn);
                sqlUpdatePerson.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = personInfo.personModel.Password;
                sqlUpdatePerson.Parameters.Add("@Id", System.Data.SqlDbType.VarChar, 50).Value = personInfo.personModel.Id;

                SqlCommand sqlUpdateInfo = new SqlCommand(updateInfoQry, conn);
                sqlUpdateInfo.Parameters.Add("@TelNo", System.Data.SqlDbType.VarChar, 10).Value = personInfo.infoModel.TelNo;
                sqlUpdateInfo.Parameters.Add("@CellNo", System.Data.SqlDbType.VarChar, 10).Value = personInfo.infoModel.CellNo;
                sqlUpdateInfo.Parameters.Add("@AddressLine1", System.Data.SqlDbType.VarChar, 30).Value = personInfo.infoModel.AddressLine1;
                sqlUpdateInfo.Parameters.Add("@AddressLine2", System.Data.SqlDbType.VarChar, 30).Value = personInfo.infoModel.AddressLine2;
                sqlUpdateInfo.Parameters.Add("@AddressLine3", System.Data.SqlDbType.VarChar, 30).Value = personInfo.infoModel.AddressLine3;
                sqlUpdateInfo.Parameters.Add("@AddressCode", System.Data.SqlDbType.VarChar, 10).Value = personInfo.infoModel.AddressCode;
                sqlUpdateInfo.Parameters.Add("@PostalAddress1", System.Data.SqlDbType.VarChar, 30).Value = personInfo.infoModel.PostalAddress1;
                sqlUpdateInfo.Parameters.Add("@PostalAddress2", System.Data.SqlDbType.VarChar, 30).Value = personInfo.infoModel.PostalAddress2;
                sqlUpdateInfo.Parameters.Add("@PostalCode", System.Data.SqlDbType.VarChar, 10).Value = personInfo.infoModel.PostalCode;
                sqlUpdateInfo.Parameters.Add("@InfoId", System.Data.SqlDbType.VarChar, 10).Value = personInfo.infoModel.InfoId;

                try
                {
                    conn.Open();
                    int personId = sqlUpdatePerson.ExecuteNonQuery();
                    int infoId = sqlUpdateInfo.ExecuteNonQuery();
                    
                    PersonLoginCredModel credModel = new PersonLoginCredModel(personInfo.personModel.Name, personInfo.personModel.Password);
                    success = GetPersonInfo(credModel);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return success;
        }

    }
}