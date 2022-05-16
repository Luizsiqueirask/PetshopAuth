using LibraryAuth.Models.PerfilAuth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibraryAuth.Context.PerfilAuth.User
{
    public class ClassAuthUser : ThrowAuthUser
    {
        public readonly Bridge _conn;
        public readonly SqlConnection _sqlConnection;

        public ClassAuthUser()
        {
            _conn = new Bridge();
            _sqlConnection = new SqlConnection(_conn.Connect());
        }

        public new IEnumerable<UserAuthLibrary> List()
        {
            var allUser = new List<UserAuthLibrary>();

            try
            {
                using (SqlCommand command = new SqlCommand("ListUser", _sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    _sqlConnection.Open();

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader.HasRows)
                        {
                            allUser.Add(new UserAuthLibrary()
                            {
                                Id = (int)dataReader["Id"],
                                Username = (string)dataReader["Username"],
                                Password = (string)dataReader["Password"]
                            });
                        }
                    }
                    _sqlConnection.Close();
                }
            }
            finally
            {
                _sqlConnection.Close();
            }
            return allUser;
        }
        public new UserAuthLibrary Get(int? Id)
        {
            var userAuthLibrary = new UserAuthLibrary();

            using (SqlCommand command = new SqlCommand("GetUser", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdUser", Id);
                _sqlConnection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    userAuthLibrary = new UserAuthLibrary()
                    {
                        Id = (int)dataReader["Id"],
                        Username = (string)dataReader["Username"],
                        Password = (string)dataReader["Password"]
                    };
                }
                _sqlConnection.Close();
                return userAuthLibrary;
            }
        }
        public new void Post(UserAuthLibrary userAuthLibrary)
        {
            using (SqlCommand command = new SqlCommand("PostUser", _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    _sqlConnection.Open();

                    command.Parameters.AddWithValue("@Username", userAuthLibrary.Username);
                    command.Parameters.AddWithValue("@Password", userAuthLibrary.Password);

                    int running = command.ExecuteNonQuery();
                    _sqlConnection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQLException: " + ex.Message);
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }
        public new void Put(UserAuthLibrary userAuthLibrary, int? Id)
        {
            using (SqlCommand command = new SqlCommand("PutUser", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdUser", Id);
                _sqlConnection.Open();

                command.Parameters.AddWithValue("@Username", userAuthLibrary.Username);
                command.Parameters.AddWithValue("@Password", userAuthLibrary.Password);

                var running = command.ExecuteNonQuery();
                _sqlConnection.Close();
            }
        }
        public new void Delete(int? Id)
        {
            using (SqlCommand command = new SqlCommand("DeleteUser", _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdUser", Id);

                    _sqlConnection.Open();
                    var running = command.ExecuteNonQuery();
                    _sqlConnection.Close();
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }
    }
}