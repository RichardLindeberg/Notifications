namespace Notifications.Storage.ReadModells
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Notifications.Domain.ReadModell;

    public class PersonalNumberAndTokenReadModell : IPersonalNumberAndTokenReadModell
    {
        private readonly string _sqlConnectionString;

        public PersonalNumberAndTokenReadModell(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public IEnumerable<PersonWithToken> GetTokenUsage(string firebaseToken)
        {
            using (var sqlConn = new SqlConnection(_sqlConnectionString))
            using (var cmd = new SqlCommand("[AllTokens_ReadModell_ByToken]", sqlConn))
            {
                sqlConn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FireBaseToken", SqlDbType.NVarChar).Value = firebaseToken;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        yield return ToPersonWithToken(dr);
                    }
                }
            }
        }

        private static PersonWithToken ToPersonWithToken(SqlDataReader dr)
        {
            return new PersonWithToken(
                dr["PersonalNumber"].ToString(),
                dr["FireBaskeToken"].ToString(),
                dr["NotificationTypeId"].ToString());
        }

        public IEnumerable<PersonWithToken> GetPerson(string personalNumber)
        {
            using (var sqlConn = new SqlConnection(_sqlConnectionString))
            using (var cmd = new SqlCommand("[AllTokens_ReadModell_ByPersonalNumber]", sqlConn))
            {
                sqlConn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonalNumber", SqlDbType.NVarChar).Value = personalNumber;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        yield return ToPersonWithToken(dr);
                    }
                }
            }
        }

        public IEnumerable<PersonWithToken> GetAll()
        {
            using (var sqlConn = new SqlConnection(_sqlConnectionString))
            using (var cmd = new SqlCommand("[AllTokens_ReadModell_GetALl]", sqlConn))
            {
                sqlConn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
          
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        yield return ToPersonWithToken(dr);
                    }
                }
            }
        }
    }
}
