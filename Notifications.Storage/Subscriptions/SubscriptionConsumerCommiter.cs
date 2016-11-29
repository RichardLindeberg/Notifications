namespace Notifications.Storage.Subscriptions
{
    using System.Data;
    using System.Data.SqlClient;
    using Domain.Subscription;

    public class SubscriptionConsumerCommiter : ISubscriptionConsumerCommiter
    {
        private readonly string _connectionString;

        public SubscriptionConsumerCommiter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetCheckPointToken()
        {
            using (var sqlConn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("MultiActiveTokenProhibiter_Commits_LastCommit", sqlConn))
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

        public void StoreCheckPointToken(string checkPointToken)
        {
            using (var sqlConn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("MultiActiveTokenProhibiter_Commits_AddCommit", sqlConn))
            {
                sqlConn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CheckPointToken", SqlDbType.NVarChar).Value = checkPointToken;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
