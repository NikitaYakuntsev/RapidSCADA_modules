using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Common
{
    public static class HttpRequest
    {
        public static string WebRequest(string url, string post = null, string referer = null, CookieContainer cookie = null)
        {
            HttpWebResponse httpWebResponse = null;
            StreamReader streamReader = null;
            try
            {
                var httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);

                httpWebRequest.Method = ((post != null) ? "POST" : "GET");
                httpWebRequest.Accept = ((post != null) ? "*/*" : "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                if (post != null)
                {
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                }

                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; Valve Steam Client/1415758987; ) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.86 Safari/537.36";
                httpWebRequest.Headers["Accept-Encoding"] = "gzip,deflate";
                httpWebRequest.Headers["Accept-Language"] = "en-us,en";
                httpWebRequest.Headers["Accept-Charset"] = "iso-8859-1,*,utf-8";
                httpWebRequest.CookieContainer = cookie;
                httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
                if (post != null)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write("json=" + post);
                    }
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                    return streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    var httpWebResponse2 = (HttpWebResponse)ex.Response;
                    //return new StreamReader(httpWebResponse2.GetResponseStream()).ReadToEnd();
                    throw ex;
                }
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }
            return null;
        }
    }
}
