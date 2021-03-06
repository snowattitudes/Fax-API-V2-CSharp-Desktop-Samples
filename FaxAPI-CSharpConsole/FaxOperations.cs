﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace FaxAPI_CSharpConsole
{
    public class FaxOperations
    {
        public void SendFaxSimpleModel()
        {

            string filePath = @"C:\Users\Snow Attitudes\Documents\Visual Studio 2013\Projects\FaxAPI-CSharpDesktopSamples\FaxAPI-CSharpConsole\Fax Docs\";
            string fileName1 = filePath + "TestFaxFromBulgaria.txt";
            string fileName2 = filePath + "SampleFaxDoc.pdf";
            using (var stream1 = File.Open(fileName1, FileMode.Open, FileAccess.Read))
            using (var stream2 = File.Open(fileName2, FileMode.Open, FileAccess.Read))
            {
                var files = new[] 
                    {
                        new UploadFileOnly
                        {
                            Name = "file",
                            Filename = "TestFaxFromBulgaria.txt",
                            ContentType = "text/plain",
                            Stream = stream1
                        },
                        new UploadFileOnly
                        {
                            Name = "file",
                            Filename = "SampleFaxDoc.pdf",
                            ContentType = "application/pdf",
                            Stream = stream2
                        }
                    };

                var accessToken = new AccessToken();
                string getTokenUrl = @"https://api.onlinefaxes.com/v2/oauth2/token";
                string ACCESS_TOKEN = accessToken.GetAccessToken(getTokenUrl);
                UTF8Encoding utf8Encoder = new UTF8Encoding();
                //var encodedACCESS_TOKEN = utf8Encoder.

                if (ACCESS_TOKEN != null)
                {
                    var clientRequestHeaders = new NameValueCollection
                    {
                        { "Authorization", @"ofx " + ACCESS_TOKEN }
                    };
                    string senderName = @"SeeSharpConsoleFax";
                    string senderCompanyName = @"DotNetGroup Org";
                    string faxSubject = @"Send Fax Simple Model with CSharp Console";
                    string faxNotes = @"Send Fax Simple Model with CSharp Console";

                    string recipientName = @"OFX";
                    string faxNumber = @"1(720)4039716";

                    string queryString = "senderName=" + senderName
                        + "&senderCompanyName=" + senderCompanyName
                        + "&faxSubject=" + faxSubject
                        + "&faxNotes=" + faxNotes
                        + "&recipientName=" + recipientName
                        + "&recipientFaxNo=" + faxNumber;
                    string encodedQueryString = HttpUtility.UrlEncode(queryString);
                    encodedQueryString = encodedQueryString.Replace("%26", @"&");
                    encodedQueryString = encodedQueryString.Replace("%3d", @"=");
                    encodedQueryString = encodedQueryString.Replace("+", @"%20");
                    string FaxUrl = @"https://api.onlinefaxes.com/v2/fax/async/sendfax/simplemodel?" + encodedQueryString;

                    var methodFile = new UploadFileOnly();
                    string result = methodFile.UploadFilesOnly(FaxUrl, files, clientRequestHeaders);
                    //add System.Web.Extensions
                    JavaScriptSerializer parser = new JavaScriptSerializer();
                    var response = parser.Deserialize<SendFaxResponse>(result);
                    Console.WriteLine("Fax sent with status : {0} and fax id of {1} ", response.Item1, response.Item2);
                    Console.WriteLine("Please strike any key");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Access Token is not valid. Please try again or get access token with valid Id");
                    Console.ReadLine();
                }



            }
        }
        public void SendFaxComplexModel()
        {

            string filePath = @"C:\Users\Snow Attitudes\Documents\Visual Studio 2013\Projects\FaxAPI-CSharpDesktopSamples\FaxAPI-CSharpConsole\Fax Docs\";
            string fileName1 = filePath + "TestFaxFromJapan.txt";
            string fileName2 = filePath + "SampleFaxDoc.pdf";
            using (var stream1 = File.Open(fileName1, FileMode.Open, FileAccess.Read))
            using (var stream2 = File.Open(fileName2, FileMode.Open, FileAccess.Read))
            {
                var files = new[] 
                    {
                        new UploadFileWithKeyValuesPair
                        {
                            Name = "file",
                            Filename = "TestFaxFromJapan.txt",
                            ContentType = "text/plain",
                            Stream = stream1
                        },
                        new UploadFileWithKeyValuesPair
                        {
                            Name = "file",
                            Filename = "SampleFaxDoc.pdf",
                            ContentType = "application/pdf",
                            Stream = stream2
                        }
                    };

                var accessToken = new AccessToken();
                string getTokenUrl = @"https://api.onlinefaxes.com/v2/oauth2/token";
                string ACCESS_TOKEN = accessToken.GetAccessToken(getTokenUrl);
                UTF8Encoding utf8Encoder = new UTF8Encoding();
                //var encodedACCESS_TOKEN = utf8Encoder.

                if (ACCESS_TOKEN != null)
                {
                    var clientRequestHeaders = new NameValueCollection
                    {
                        { "Authorization", @"ofx " + ACCESS_TOKEN }
                    };

                    var SenderDetail = new SenderDetail();


                    SenderDetail.Id = 0;
                    SenderDetail.Name = @"SeeSharpConsoleFax";
                    SenderDetail.Company = @"DotNetGroup Org";
                    SenderDetail.Subject = @"Send Fax Complex Model with CSharp Console";
                    SenderDetail.Notes = @"Send Fax Complex Model with CSharp Console";

                    //1st technique with default class constructor
                    var recipient1 = new RecipientDetail
                        {
                            Name = @"OFXES_1",
                            FaxNumber = @"1(720)4039716"
                        };

                    //2nd technique with normal property-value assignment
                    var recipient2 = new RecipientDetail();
                    recipient2.Name = @"OFXES_2";
                    recipient2.FaxNumber = @"1(720)4039716";

                    var RecipientList = new List<RecipientDetail>();
                    RecipientList.Add(recipient1);
                    RecipientList.Add(recipient2);

                    JavaScriptSerializer parser = new JavaScriptSerializer();

                    var jsonSenderDetail = parser.Serialize(SenderDetail);
                    var jsonRecipientList = parser.Serialize(RecipientList);

                    var paramValues = new NameValueCollection
                    {
                        { "SenderDetail", jsonSenderDetail },
                        { "RecipientList", jsonRecipientList },
                    };

                    string FaxUrl = @"https://api.onlinefaxes.com/v2/fax/async/sendfax/complexmodel";

                    var methodFile = new UploadFileWithKeyValuesPair();
                    string result = methodFile.UploadFilesWithParams(FaxUrl, files, paramValues, clientRequestHeaders);
                    //add System.Web.Extensions

                    var response = parser.Deserialize<SendFaxResponse>(result);
                    Console.WriteLine("Fax sent with status : {0} and fax id of {1} ", response.Item1, response.Item2);
                    Console.WriteLine("Please strike any key");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Access Token is not valid. Please try again or get access token with valid Id");
                    Console.ReadLine();
                }



            }
        }
        public void GetFaxStatus(string FAX_ID)
        {
            var accessToken = new AccessToken();
            string getTokenUrl = @"https://api.onlinefaxes.com/v2/oauth2/token";
            string ACCESS_TOKEN = accessToken.GetAccessToken(getTokenUrl);
            UTF8Encoding utf8Encoder = new UTF8Encoding();
            //var encodedACCESS_TOKEN = utf8Encoder.

            if (ACCESS_TOKEN != null)
            {
                var oauthHeader = new NameValueCollection
                    {
                        { "Authorization", @"ofx " + ACCESS_TOKEN }
                    };
                string faxId = FAX_ID;
                string queryString = "faxId=" + faxId;
                string encodedQueryString = HttpUtility.UrlEncode(queryString);
                string FaxUrl = @"https://api.onlinefaxes.com/v2/fax/async/getfaxstatus?" + queryString;

                var request = WebRequest.Create(FaxUrl);
                request.Headers.Add(oauthHeader);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] buffer = encoding.GetBytes(queryString);
                request.ContentLength = buffer.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var dataStream = httpResponse.GetResponseStream();
                        var streamReader = new StreamReader(dataStream);
                        var result = streamReader.ReadToEnd();

                        JavaScriptSerializer parser = new JavaScriptSerializer();

                        var data = parser.Deserialize<ResponseStatus>(result);
                        Console.WriteLine("Fax sent with status : {0} and fax id of {1} ", data.Status, faxId);
                        Console.WriteLine("Please strike any key");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Internal Server Error");
                        Console.ReadLine();
                    }

                }

            }
            else
            {
                Console.WriteLine("Access Token is not valid. Please try again or get access token with valid Id");
                Console.ReadLine();
            }

        }
        public void GetFaxDetail(string FAX_ID)
        {
            var accessToken = new AccessToken();
            string getTokenUrl = @"https://api.onlinefaxes.com/v2/oauth2/token";
            string ACCESS_TOKEN = accessToken.GetAccessToken(getTokenUrl);
            UTF8Encoding utf8Encoder = new UTF8Encoding();
            //var encodedACCESS_TOKEN = utf8Encoder.

            if (ACCESS_TOKEN != null)
            {
                var oauthHeader = new NameValueCollection
                    {
                        { "Authorization", @"ofx " + ACCESS_TOKEN }
                    };
                string faxId = FAX_ID;
                string queryString = "faxId=" + faxId;
                string encodedQueryString = HttpUtility.UrlEncode(queryString);
                string FaxUrl = @"https://api.onlinefaxes.com/v2/fax/async/getfaxdetail?" + queryString;

                var request = WebRequest.Create(FaxUrl);
                request.Headers.Add(oauthHeader);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] buffer = encoding.GetBytes(queryString);
                request.ContentLength = buffer.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var dataStream = httpResponse.GetResponseStream();
                        var streamReader = new StreamReader(dataStream);
                        var result = streamReader.ReadToEnd();

                        JavaScriptSerializer parser = new JavaScriptSerializer();

                        var data = result;
                        Console.WriteLine("Fax sent with Id of : {0} and detail of \n {1} ", faxId, data);
                        Console.WriteLine("Please strike any key");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Internal Server Error");
                        Console.ReadLine();
                    }

                }

            }
            else
            {
                Console.WriteLine("Access Token is not valid. Please try again or get access token with valid Id");
                Console.ReadLine();
            }

        }
        public void DownloadFax(string FAX_ID)
        {
            var accessToken = new AccessToken();
            string getTokenUrl = @"https://api.onlinefaxes.com/v2/oauth2/token";
            string ACCESS_TOKEN = accessToken.GetAccessToken(getTokenUrl);
            UTF8Encoding utf8Encoder = new UTF8Encoding();
            //var encodedACCESS_TOKEN = utf8Encoder.

            if (ACCESS_TOKEN != null)
            {
                var oauthHeader = new NameValueCollection
                    {
                        { "Authorization", @"ofx " + ACCESS_TOKEN }
                    };
                string faxId = FAX_ID;
                string queryString = "faxId=" + faxId;
                string encodedQueryString = HttpUtility.UrlEncode(queryString);
                string FaxUrl = @"https://api.onlinefaxes.com/v2/fax/async/downloadfaxfile?" + queryString;

                var request = WebRequest.Create(FaxUrl);
                request.Headers.Add(oauthHeader);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] buffer = encoding.GetBytes(queryString);
                request.ContentLength = buffer.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var dataStream = httpResponse.GetResponseStream();
                        var streamReader = new StreamReader(dataStream);
                        var result = streamReader.ReadToEnd();

                        JavaScriptSerializer parser = new JavaScriptSerializer();

                        var data = parser.Deserialize<ResponseStatus>(result);
                        Console.WriteLine("Download path for fax id of {0}: {1} ", faxId, data.Status);
                        Console.WriteLine("Please strike any key");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Internal Server Error");
                        Console.ReadLine();
                    }

                }

            }
            else
            {
                Console.WriteLine("Access Token is not valid. Please try again or get access token with valid Id");
                Console.ReadLine();
            }

        }
        public void DeleteFax(string FAX_ID)
        {
            var accessToken = new AccessToken();
            string getTokenUrl = @"https://api.onlinefaxes.com/v2/oauth2/token";
            string ACCESS_TOKEN = accessToken.GetAccessToken(getTokenUrl);
            UTF8Encoding utf8Encoder = new UTF8Encoding();
            //var encodedACCESS_TOKEN = utf8Encoder.

            if (ACCESS_TOKEN != null)
            {
                var oauthHeader = new NameValueCollection
                    {
                        { "Authorization", @"ofx " + ACCESS_TOKEN }
                    };
                string faxId = FAX_ID;
                string queryString = "faxId=" + faxId;
                string encodedQueryString = HttpUtility.UrlEncode(queryString);
                string FaxUrl = @"https://api.onlinefaxes.com/v2/fax/async/deletefax?" + queryString;

                var request = WebRequest.Create(FaxUrl);
                request.Headers.Add(oauthHeader);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] buffer = encoding.GetBytes(queryString);
                request.ContentLength = buffer.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var dataStream = httpResponse.GetResponseStream();
                        var streamReader = new StreamReader(dataStream);
                        var result = streamReader.ReadToEnd();

                        JavaScriptSerializer parser = new JavaScriptSerializer();

                        var data = parser.Deserialize<ResponseStatus>(result);
                        Console.WriteLine("Status deletion for fax id of {0}: {1} ", faxId, data.Status);
                        Console.WriteLine("Please strike any key");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Internal Server Error");
                        Console.ReadLine();
                    }

                }

            }
            else
            {
                Console.WriteLine("Access Token is not valid. Please try again or get access token with valid Id");
                Console.ReadLine();
            }

        }
        public void GetFaxList(string FOLDER_ID, bool IS_DOWNLOADED)
        {
            var accessToken = new AccessToken();
            string getTokenUrl = @"https://api.onlinefaxes.com/v2/oauth2/token";
            string ACCESS_TOKEN = accessToken.GetAccessToken(getTokenUrl);
            UTF8Encoding utf8Encoder = new UTF8Encoding();
            //var encodedACCESS_TOKEN = utf8Encoder.

            if (ACCESS_TOKEN != null)
            {
                var oauthHeader = new NameValueCollection
                    {
                        { "Authorization", @"ofx " + ACCESS_TOKEN }
                    };
                string folderId = FOLDER_ID;
                bool isDownloaded = IS_DOWNLOADED;
                string queryString = "folderId=" + folderId + "&isdownloaded=" + isDownloaded;
                //string encodedQueryString = HttpUtility.UrlEncode(queryString);
                string FaxUrl = @"https://api.onlinefaxes.com/v2/fax/async/getfaxlist?" + queryString;

                var request = WebRequest.Create(FaxUrl);
                request.Headers.Add(oauthHeader);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] buffer = encoding.GetBytes(queryString);
                request.ContentLength = buffer.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var dataStream = httpResponse.GetResponseStream();
                        var streamReader = new StreamReader(dataStream);
                        var result = streamReader.ReadToEnd();

                        JavaScriptSerializer parser = new JavaScriptSerializer();

                        var data = result;
                        Console.WriteLine("List of Fax under Folder Sent Box ({0}): \n {1} ", folderId, data);
                        Console.WriteLine("Please strike any key");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Internal Server Error");
                        Console.ReadLine();
                    }

                }

            }
            else
            {
                Console.WriteLine("Access Token is not valid. Please try again or get access token with valid Id");
                Console.ReadLine();
            }

        }



    }
}