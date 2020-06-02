using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Windows;

namespace Konfiguration_WPF.API
{
    public class Pfade
    {
        public string Pfad_Kd = "https://www.zwpc.de//api/load_data.php";

        public string HTTPSRequestGet(Dictionary<string, string> getParameters = null)
        {
           
            string str = "";
            if (getParameters != null)
            {
                foreach (string str3 in getParameters.Keys)
                {
                    string[] textArray1 = new string[] { str, HttpUtility.UrlEncode(str3), "=", HttpUtility.UrlEncode(getParameters[str3]), "&" };
                    str = string.Concat(textArray1);
                }
            }

            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(Pfad_Kd + ((getParameters != null) ? ("?" + str) : ""));
            request1.AutomaticDecompression = DecompressionMethods.GZip;
            WebResponse response = request1.GetResponse();
            string str2 = null;
            using (Stream stream = response.GetResponseStream())
            {
                str2 = new StreamReader(stream).ReadToEnd();
            }
            response.Close();
            return str2;
        }

    }
}
