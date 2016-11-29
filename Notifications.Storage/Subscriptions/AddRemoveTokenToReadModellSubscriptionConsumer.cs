namespace Notifications.Storage.Subscriptions
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using Notifications.Domain.Subscription;
    using Notifications.Messages.Events;
    using Notifications.Messages.Events.Person;

    public class AddRemoveTokenToReadModellSubscriptionConsumer : IHandle<FirebaseTokenAdded>, IHandle<IFirebaseTokenRemoved>, ISubscriptionConsumer
    {
        private readonly string _sqlConnection;

        public AddRemoveTokenToReadModellSubscriptionConsumer(string sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void Handle(FirebaseTokenAdded evnt, string checkPointToken)
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
                    cmd.Parameters.Add("@checkPointToken", SqlDbType.NVarChar).Value = checkPointToken;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong" + ex.Message);
                    throw;
                }
            }
        }

        public void Handle(IFirebaseTokenRemoved evnt, string checkPointToken)
        {
            using (var sqlConn = new SqlConnection(_sqlConnection))
            using (var cmd = new SqlCommand("AllTokens_ReadModell_Remove", sqlConn))
            {
                sqlConn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonalNumber", SqlDbType.NChar, 12).Value = evnt.PersonalNumber;
                cmd.Parameters.Add("@FireBaskeToken", SqlDbType.NVarChar, 400).Value = evnt.FirebaseToken;
                cmd.Parameters.Add("@NotificationTypeId", SqlDbType.NChar, 12).Value = evnt.NotificationTypeId;
                cmd.Parameters.Add("@CheckPointToken", SqlDbType.NVarChar).Value = checkPointToken;
                cmd.ExecuteNonQuery();
            }
        }

        public string GetCheckPointToken()
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
                        return dr["CheckPointToken"].ToString();
                    }
                    return null;
                }
            }
        }

        public void NewEvent(Event @event, string checkPointToken)
        {
            var addToken = @event as FirebaseTokenAdded;
            if (addToken != null)
            {
                Handle(addToken, checkPointToken);
            }

            var removeToken = @event as IFirebaseTokenRemoved;
            if (removeToken != null)
            {
                Handle(removeToken, checkPointToken);
            }
        }
    }
}