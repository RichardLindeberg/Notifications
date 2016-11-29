namespace Notifications.Domain.NotificationSender
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    using Newtonsoft.Json;

    public class FirebaseNotificationSender : IFirebaseNotificationSender
    {
        public FireBaseNotificationResponse SendNotification(string firebaseToken, string message)
        {
            throw new NotImplementedException();
        }

        public void PushNotification(string message, string title)
        {
            try
            {
                var applicationID = "AIza---------4GcVJj4dI";
                var senderId = "57-------55";
                var deviceId = "euxqdp------ioIdL87abVL";

                var data = new
                               {
                                   to = deviceId,
                                   notification = new
                                                      {
                                                          body = message,
                                                          title,
                                                          icon = "myicon"
                                                      }
                               };
                var json = JsonConvert.SerializeObject(data);
                var byteArray = Encoding.UTF8.GetBytes(json);

                var webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                webRequest.Method = "post";
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                webRequest.ContentLength = byteArray.Length;

                using (var dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (var webResponse = webRequest.GetResponse())
                    {
                        //  webResponse.
                        using (var dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (var tReader = new StreamReader(dataStreamResponse))
                            {
                                var sResponseFromServer = tReader.ReadToEnd();
                                var str = sResponseFromServer;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                var str = ex.Message;
            }
        }
    }
}