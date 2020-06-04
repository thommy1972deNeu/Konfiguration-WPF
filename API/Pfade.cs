using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;

namespace Konfiguration_WPF.API
{



    public class Pfade
    {

        
        public string Pfad_Kd = "https://www.zwpc.de//api//load_data.php";
        public string Pfad_Config = "https://www.zwpc.de//api//config.php";
        public string Pfad_NEWS = "https://www.zwpc.de//api//News.php";


        public string Pfad_Mail_Data = "https://www.zwpc.de//api//mail_data.php";
        

        public static string Mail_server { get; set; }
        public static string Mail_user { get; set; }
        public static string Mail_username { get; set; }
        public static string Mail_from { get; set; }
        public static string Mail_to { get; set; }
        public static int Mail_port { get; set; }
        public static string Mail_pass { get; set; }

        public static string News_Titel { get; set; }

        public static string News_Text { get; set; }

        public string HTTPSRequestGet(string url, Dictionary<string, string> getParameters = null)
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

            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url + ((getParameters != null) ? ("?" + str) : ""));
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

        public static void MAIL_USER()
        {
            Pfade pf = new Pfade();
            Dictionary<string, string> getParameters = new Dictionary<string, string>();
            getParameters.Add("secret", Properties.Resources.Secret);
            string kdnr_response = pf.HTTPSRequestGet(pf.Pfad_Mail_Data, getParameters);
            string[] data = kdnr_response.Split(':');
            if (kdnr_response.Length > 1)
            {
                Mail_pass = data[0];
                Mail_server = data[1];
                Mail_from = data[2];
                Mail_username = data[3];
                Mail_to = data[4];
                Mail_port = Convert.ToInt32(data[5]);

            }

        }



    }

}
