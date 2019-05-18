using CRM.API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CRM.API.Services
{
    public class PersonService : IPersonService
    {
        string _connString;

        public PersonService(string connString)
        {
            _connString = connString;
        }

        public PeopleResult GetPeopleAll()
        {
            var result = new PeopleResult();

            using (SqlConnection conn = new SqlConnection(_connString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Person_GetAll";
                cmd.CommandTimeout = 60;
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    result.Data.Add(new Person
                    {
                        id = rdr.GetInt32(0),
                        firstname = rdr.GetString(1),
                        lastname = rdr.GetString(2)
                    });
                }

                conn.Close();
            }

            return result;
        }

        public PeopleResult GetPeopleChanges(Int64 version)
        {
            var result = new PeopleResult();

            using (SqlConnection conn = new SqlConnection(_connString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Person_Changes";
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@SyncVersion", version);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    result.Data.Add(new Person
                    {
                        id = rdr.GetInt32(0),
                        firstname = rdr.GetString(1),
                        lastname = rdr.GetString(2)
                    });
                }
                rdr.NextResult();
                result.Deleted = new List<RemovedPerson>();
                while (rdr.Read())
                {
                    result.Deleted.Add(new RemovedPerson
                    {
                        id = rdr.GetInt32(0)
                    });
                }
                rdr.NextResult();
                if (rdr.Read())
                {
                    result.Version = rdr.GetInt64(0);
                }

                conn.Close();
            }

            return result;
        }
    }
}

