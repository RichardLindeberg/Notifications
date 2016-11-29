namespace Notifications.Storage.ReadModells
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using Messages.Events.Person;

    using Notifications.Messages.Events;

    public class AllTokensReadModellWriterWriter : IHandle<FirebaseTokenAdded>, IHandle<FirebaseTokenRemoved>, IReadModellWriter
    {
        private readonly string _sqlConnection;

        public AllTokensReadModellWriterWriter(string sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void Handle(FirebaseTokenAdded evnt, Guid commitId)
        {
            using (var sqlConn = new SqlConnection(_sqlConnection))
            using (var cmd = new SqlCommand("AllTokens_ReadModell_Add", sqlConn))
            {
                try
                {
                    sqlConn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PersonalNumber", SqlDbType.NChar, 12).Value = evnt.PersonalNumber;
                    cmd.Parameters.Add("@FireBaskeToken", SqlDbType.NVarChar, 400).Value = evnt.FirebaseToken;
                    cmd.Parameters.Add("@NotificationTypeId", SqlDbType.NChar, 12).Value = evnt.NotificationTypeId;
                    cmd.Parameters.Add("@CommitId", SqlDbType.UniqueIdentifier).Value = commitId;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong" + ex.Message);
                    throw;
                }
            }
        }

        public void Handle(FirebaseTokenRemoved evnt, Guid commitId)
        {
            using (var sqlConn = new SqlConnection(_sqlConnection))
            using (var cmd = new SqlCommand("AllTokens_ReadModell_Remove", sqlConn))
            {
                sqlConn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonalNumber", SqlDbType.NChar, 12).Value = evnt.PersonalNumber;
                cmd.Parameters.Add("@FireBaskeToken", SqlDbType.NVarChar, 400).Value = evnt.FirebaseToken;
                cmd.Parameters.Add("@NotificationTypeId", SqlDbType.NChar, 12).Value = evnt.NotificationTypeId;
                cmd.Parameters.Add("@CommitId", SqlDbType.UniqueIdentifier).Value = commitId;
                cmd.ExecuteNonQuery();
            }
        }

        public string GetLastCommit()
        {
            using (var sqlConn = new SqlConnection(_sqlConnection))
            using (var cmd = new SqlCommand("AllTokens_ReadModell_LastCommitId", sqlConn))
            {
                sqlConn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return dr["CommitId"].ToString();
                    }
                    return null;
                }
            }
        }

        public void NewEvent(Event @event, string commitId)
        {
            var addToken = @event as FirebaseTokenAdded;
            if (addToken != null)
            {
                Handle(addToken, Guid.Parse(commitId));
            }

            var removeToken = @event as FirebaseTokenRemoved;
            if (removeToken != null)
            {
                Handle(removeToken, Guid.Parse(commitId));
            }
        }
    }
}