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
        public string Pfad_Mail_User = "https://www.zwpc.de//api//mail_user.php";
        public string Pfad_Mail_Pass = "https://www.zwpc.de//api//mail_pass.php";
        public string Pfad_Mail_Server = "https://www.zwpc.de//api//mail_server.php";

        public Pfade()
        {
   

        }


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

        public static string MAIL_USER()
        {
            Pfade pf = new Pfade();
            Dictionary<string, string> getParameters = new Dictionary<string, string>();
            getParameters.Add("secret", Properties.Resources.Secret);
            string kdnr_response = pf.HTTPSRequestGet(pf.Pfad_Mail_User, getParameters);
            string[] data = kdnr_response.Split(':');
            if (kdnr_response.Length > 1)
            {
                return data[0];
            }
            return null;
        }

        public static string MAIL_PASS()
        {
            Pfade pf = new Pfade();
            Dictionary<string, string> getParameters = new Dictionary<string, string>();
            getParameters.Add("secret", Properties.Resources.Secret);
            string kdnr_response = pf.HTTPSRequestGet(pf.Pfad_Mail_Pass, getParameters);
            string[] data = kdnr_response.Split(':');
            if (kdnr_response.Length > 1)
            {
                return data[0];
            }
            return null;
        }


        public static string MAIL_SERVER()
        {
            Pfade pf = new Pfade();
            Dictionary<string, string> getParameters = new Dictionary<string, string>();
            getParameters.Add("secret", Properties.Resources.Secret);
            string kdnr_response = pf.HTTPSRequestGet(pf.Pfad_Mail_Server, getParameters);
            string[] data = kdnr_response.Split(':');
            if (kdnr_response.Length > 1)
            {
                return data[0];
            }
            return null;
        }
    }

}
