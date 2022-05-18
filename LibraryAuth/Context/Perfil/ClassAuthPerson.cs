using LibraryAuth.Models.PerfilAuth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibraryAuth.Context.PerfilAuth
{
    public class ClassAuthPerson : ThrowAuthPerson
    {
        public readonly Bridge _conn;
        public readonly SqlConnection _sqlConnection;

        public ClassAuthPerson()
        {
            _conn = new Bridge();
            _sqlConnection = new SqlConnection(_conn.Connect());
        }

        public new IEnumerable<PersonAuthLibrary> List()
        {
            var allPerson = new List<PersonAuthLibrary>();

            try
            {
                using (SqlCommand command = new SqlCommand("ListPerson", _sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    _sqlConnection.Open();

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader.HasRows)
                        {
                            var personLibrary = new PersonAuthLibrary()
                            {
                                Id = (int)dataReader["Id"],
                                FirstName = (string)dataReader["FirstName"],
                                LastName = (string)dataReader["LastName"],
                                Age = (int)dataReader["Age"],
                                Birthday = (DateTime)dataReader["Birthday"],
                                Genre = (string)dataReader["Genre"],
                                Picture = new PictureAuthLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Tag = (string)dataReader["Tag"],
                                    Path = (string)dataReader["Path"]
                                },                               
                                Contact = new ContactAuthLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Email = (string)dataReader["Email"],
                                    Mobile = (string)dataReader["Mobile"]
                                },
                                Address = new AddressAuthLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Country = (string)dataReader["Country"],
                                    States = (string)dataReader["States"],
                                    City = (string)dataReader["City"],
                                    Neighborhoods = (string)dataReader["Neighborhoods"]
                                }
                            };

                            allPerson.Add(personLibrary);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                _sqlConnection.Close();
            }

            return allPerson;
        }
        public new PersonAuthLibrary Get(int? Id)
        {
            var personLibrary = new PersonAuthLibrary();

            using (SqlCommand command = new SqlCommand("GetPerson", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdPerson", Id);
                _sqlConnection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    personLibrary = new PersonAuthLibrary()
                    {
                        Id = (int)dataReader["Id"],
                        FirstName = (string)dataReader["FirstName"],
                        LastName = (string)dataReader["LastName"],
                        Age = (int)dataReader["Age"],
                        Birthday = (DateTime)dataReader["Birthday"],
                        Genre = (string)dataReader["Genre"],
                        Picture = new PictureAuthLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Tag = (string)dataReader["Tag"],
                            Path = (string)dataReader["Path"]
                        },
                        Contact = new ContactAuthLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Email = (string)dataReader["Email"],
                            Mobile = (string)dataReader["Mobile"]
                        },
                        Address = new AddressAuthLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Country = (string)dataReader["Country"],
                            States = (string)dataReader["States"],
                            City = (string)dataReader["City"],
                            Neighborhoods = (string)dataReader["Neighborhoods"]
                        }
                    };
                }
            }
            _sqlConnection.Close();
            return personLibrary;
        }
        public new void Post(PersonAuthLibrary personLibrary)
        {
            using (SqlCommand command = new SqlCommand("PostPerson", _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // -- Picture
                    command.Parameters.AddWithValue("@Tag", personLibrary.Picture.Tag);
                    command.Parameters.AddWithValue("@Path", personLibrary.Picture.Path);
                    // -- Contacts
                    command.Parameters.AddWithValue("@Email", personLibrary.Contact.Email);
                    command.Parameters.AddWithValue("@Mobile", personLibrary.Contact.Mobile);
                    // -- Address
                    command.Parameters.AddWithValue("@Country", personLibrary.Address.Country);
                    command.Parameters.AddWithValue("@States", personLibrary.Address.States);
                    command.Parameters.AddWithValue("@City", personLibrary.Address.City);
                    command.Parameters.AddWithValue("@Neighborhoods", personLibrary.Address.Neighborhoods);
                    // -- User
                    //command.Parameters.AddWithValue("@UserId", personLibrary.User.Id);
                    // -- Person
                    command.Parameters.AddWithValue("@FirstName", personLibrary.FirstName);
                    command.Parameters.AddWithValue("@LastName", personLibrary.LastName);
                    command.Parameters.AddWithValue("@Age", personLibrary.Age);
                    command.Parameters.AddWithValue("@Birthday", personLibrary.Birthday);
                    command.Parameters.AddWithValue("@Genre", personLibrary.Genre);
                    command.Parameters.AddWithValue("@PictureId", personLibrary.Picture.Id);
                    command.Parameters.AddWithValue("@ContactId", personLibrary.Contact.Id);
                    command.Parameters.AddWithValue("@AddressId", personLibrary.Address.Id);

                    _sqlConnection.Open();
                    int running = command.ExecuteNonQuery();
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
        public new void Put(PersonAuthLibrary personLibrary, int? Id)
        {
            using (SqlCommand command = new SqlCommand("PutPerson", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdPerson", Id);
                // -- Picture
                command.Parameters.AddWithValue("@Tag", personLibrary.Picture.Tag);
                command.Parameters.AddWithValue("@Path", personLibrary.Picture.Path);
                // -- Contacts
                command.Parameters.AddWithValue("@Email", personLibrary.Contact.Email);
                command.Parameters.AddWithValue("@Mobile", personLibrary.Contact.Mobile);
                // -- Address
                command.Parameters.AddWithValue("@Country", personLibrary.Address.Country);
                command.Parameters.AddWithValue("@States", personLibrary.Address.States);
                command.Parameters.AddWithValue("@City", personLibrary.Address.City);
                command.Parameters.AddWithValue("@Neighborhoods", personLibrary.Address.Neighborhoods);
                // -- User
                //command.Parameters.AddWithValue("@UserId", personLibrary.User.Id);
                // -- Person
                command.Parameters.AddWithValue("@FirstName", personLibrary.FirstName);
                command.Parameters.AddWithValue("@LastName", personLibrary.LastName);
                command.Parameters.AddWithValue("@Age", personLibrary.Age);
                command.Parameters.AddWithValue("@Birthday", personLibrary.Birthday);
                command.Parameters.AddWithValue("@Genre", personLibrary.Genre);
                command.Parameters.AddWithValue("@PictureId", personLibrary.Picture.Id);
                command.Parameters.AddWithValue("@ContactId", personLibrary.Contact.Id);
                command.Parameters.AddWithValue("@AddressId", personLibrary.Address.Id);

                _sqlConnection.Open();
                var running = command.ExecuteNonQuery();
            }
            _sqlConnection.Close();
        }
        public new void Delete(int? Id)
        {
            using (SqlCommand command = new SqlCommand("DeletePerson", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdPerson", Id);

                _sqlConnection.Open();
                var running = command.ExecuteNonQuery();
            }
            _sqlConnection.Close();
        }
    }
}