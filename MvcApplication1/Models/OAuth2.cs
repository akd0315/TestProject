using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MvcApplication1.Models
{
    public class OAuth2
    {
        public String GetAuthentication(string scope, string redirectUrl)
        {
            LinkedIn lIn = new LinkedIn();
            lIn.Initiate();
            string authUrl = "https://www.linkedin.com/uas/oauth2/authorization";
            var sign = "response_type=code&client_id=" +  lIn.ConsumerKey + "&scope=" + scope + "&state=" +lIn.State + "&redirect_uri=" + redirectUrl;
            return (authUrl + "?" + sign);
        }

        public String VerifyAuthentication(string code, string redirectUrl)
        {
            LinkedIn lIn = new LinkedIn();
            lIn.Initiate();
            string authUrl = "https://www.linkedin.com/uas/oauth2/accessToken";

            var sign = "grant_type=authorization_code&code=" + HttpUtility.UrlEncode(code) + "&redirect_uri=" + HttpUtility.HtmlEncode(redirectUrl) + "&client_id=" + lIn.ConsumerKey + "&client_secret=" + lIn.ConsumerSecret;
            //byte[] byteArray = Encoding.UTF8.GetBytes(sign);

            return(Post(authUrl, sign));
           
        }
        public String GetData(string path, string accessToken)
        {
            string url = "https://api.linkedin.com/" + path;
            string aToken = "oauth2_access_token=" + accessToken;
            return Get(url, aToken);
        }
        public String PostData(string path, string accessToken)
        {
            string url = "https://api.linkedin.com/" + path;
            string aToken = "oauth2_access_token=" + accessToken;
            return Post(url, aToken);
        }
        private String Post(string url, string parameters){
             HttpWebRequest webRequest = WebRequest.Create(url + "?" + parameters) as HttpWebRequest;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            Stream dataStream = webRequest.GetRequestStream();

            String postData = String.Empty;
            byte[] postArray = Encoding.ASCII.GetBytes(postData);

            dataStream.Write(postArray, 0, postArray.Length);
            dataStream.Close();

            WebResponse response = webRequest.GetResponse();
            dataStream = response.GetResponseStream();


            StreamReader responseReader = new StreamReader(dataStream);
            String returnVal = responseReader.ReadToEnd().ToString();
            responseReader.Close();
            dataStream.Close();
            response.Close();
            return returnVal;
        }
        private String Get(string url, string parameters)
        {
            LinkedIn li = new LinkedIn();
            li.Initiate();
            HttpWebRequest webRequest = WebRequest.Create(url + "?" + parameters) as HttpWebRequest;
            webRequest.Method = "GET";


            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();

            Stream resStream = response.GetResponseStream();
            StreamReader responseReader = new StreamReader(resStream);
            String returnVal = responseReader.ReadToEnd().ToString();
            responseReader.Close();
            resStream.Close();
            response.Close();
            return returnVal;
        }
    }
}