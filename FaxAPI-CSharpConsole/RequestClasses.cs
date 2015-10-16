using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;

namespace FaxAPI_CSharpConsole
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }

        public string GetAccessToken(string getTokenUrl)
        {
            string tokenUrl = getTokenUrl;
            var request = WebRequest.Create(getTokenUrl);
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            string CLIENT_ID = ConfigurationManager.AppSettings["client_id"];
            string CLIENT_SECRET = ConfigurationManager.AppSettings["client_secret"]; ;
            string requestBody = @"client_id=" + CLIENT_ID + "&client_secret=" + CLIENT_SECRET + "&grant_type=client_credentials";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] buffer = encoding.GetBytes(requestBody);
            request.ContentLength = buffer.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(buffer,0,buffer.Length);
            }

            var objAccessToken = new AccessToken();
            using (var httpResponse = (HttpWebResponse)request.GetResponse())
            {
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var dataStream = httpResponse.GetResponseStream();
                    var streamReader = new StreamReader(dataStream);
                    var result = streamReader.ReadToEnd();

                    JavaScriptSerializer parser = new JavaScriptSerializer();

                    var response = parser.Deserialize<AccessToken>(result);
                    objAccessToken.access_token = response.access_token;
                    return objAccessToken.access_token;
                }
                else
                {
                    return null;
                }
                    
            }
           
        }

    }

    public class SendFaxResponse
    {
        public string Item1 { get; set; }
        public string Item2 { get; set; }
    }

    public class SenderDetail
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Company { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }

    }

    public class RecipientDetail
    {
        public string Name { get; set; }
        public string FaxNumber { get; set; }
    }

    public class ResponseStatus
    {
        public string Status { get; set; }
    }

    public class DeleteStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
