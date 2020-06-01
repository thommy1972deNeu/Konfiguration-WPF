using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Konfiguration_WPF
{
    /// <summary>
    /// Interaktionslogik für Konfiguration_Senden.xaml
    /// </summary>
    public partial class Konfiguration_Senden : Window
    {
        public Konfiguration_Senden()
        {
            InitializeComponent();
            status.Text = "© 2019 - " + DateTime.Now.Year + " Computerservice Blasius Thomas";
        }
        public static object SystemTyp()
        {
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;

                    if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "0", false))
                    {
                        return "Unbekannt";
                    }
                    else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "1", false))
                    {
                        return "Desktop-PC";
                    }
                    else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "2", false))
                    {
                        return "Laptop";
                    }
                    else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "3", false))
                    {
                        return "Desktop-PC";
                    }
                    else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "4", false))
                    {
                        return "Enterprise-Server";
                    }
                    else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "5", false))
                    {
                        return "SOHO Server";
                    }
                    else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "6", false))
                    {
                        return "SOHO Server";
                    }
                    else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "7", false))
                    {
                        return "Performance Server";
                    }
                    else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "8", false))
                    {
                        return "Slate";
                    }
                    else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "9", false))
                    {
                        return "Maximum";
                    }


                }
                return null;
            }
        }
        public static string HDD1()
        {
            var HDD1String = string.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE0'"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    HDD1String = "Bezeichnung: " + Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));

                }
            }
            return HDD1String;
        }

        public static string HDD2()
        {
            var HDD2String = string.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE1'"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    HDD2String = "Bezeichnung: " + Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));

                }
            }
            return HDD2String;
        }

        public static string HDD3()
        {
            var HDD3String = string.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE2'"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    HDD3String = "Bezeichnung: " + Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));

                }
            }
            return HDD3String;
        }


        public string Entschluesseln(string strDecrypted)
        {
            try
            {
                string EncryptionKey = RegistryWert.Encryption_Key();
                byte[] cipherBytes = Convert.FromBase64String(strDecrypted);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        strDecrypted = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private void absenden_2(object sender, RoutedEventArgs e)
        {
            absenden.Visibility = Visibility.Hidden;

            if (eigene_seriennummer.Text == "")
            {
                MessageBox.Show("Seriennummer muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                eigene_seriennummer.Focus();
                absenden.Visibility = Visibility.Visible;
                return;
                
            }
            if (antivirenprogramm.Text == "Auswahl")
            {
                MessageBox.Show("Antiviren Programm muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                antivirenprogramm.Focus();
                absenden.Visibility = Visibility.Visible;
                return;
            }

            // ##################################################################################################### 
            // ###################   Abfrage nach Passwort habe ich mal Deaktiviert ################################ 
            // ##################################################################################################### 

            //if (zauberwort.Text != ConfigurationManager.AppSettings["GhZfrgWRGwr57456DGWferGF$ZG"].ToString())
            //{
            //  MessageBox.Show("Falsches Passwort !", "Fehler", MessageBoxButton.OK);
            //  zauberwort.Focus();
            //  absenden.Visibility = Visibility.Visible;
            //  return;
            //} 



            string from = ConfigurationManager.AppSettings["email_von_an"].ToString();
            string to = ConfigurationManager.AppSettings["email_von_an"].ToString();
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            string server = ConfigurationManager.AppSettings["email_Server"].ToString();
            string betreff = "Programm-Einstellungen";
            string passwort = Entschluesseln(RegistryWert.Email_Passwort());

            string body = @"Rechner-Konfiguration" + Environment.NewLine;
            body += " ################################################################################" + Environment.NewLine;
            body += "Eigene Seriennummer: " + eigene_seriennummer.Text + Environment.NewLine;
            body += "Antiviren-Software: " + antivirenprogramm.Text + Environment.NewLine;
            body += " ################################################################################" + Environment.NewLine;
            body += "Geräte-Art: " + SystemTyp() + Environment.NewLine;
            body += "Hersteller: " + RegistryWert.Hersteller() + Environment.NewLine;
            body += "Modell 1: " + RegistryWert.Modell1() + Environment.NewLine;
            body += "Modell 2: " + RegistryWert.Modell2() + Environment.NewLine;
            body += "Seriennummer: " + RegistryWert.SERIENNUMMER() + Environment.NewLine;
            body += "Prozessor: " + Environment.NewLine;
            body += "RAM-Total: " + RegistryWert.RAM_TOTAL() + " GB " + RegistryWert.RAM_TYP() + " @ " + RegistryWert.RAM_SPEED() + " MHz" + Environment.NewLine;
            body += "Grafikkarte: " + RegistryWert.GRAFIK1() + " @" + RegistryWert.GRAFIK1_RESOLUTION() + Environment.NewLine;
            body += "Mainboard: " + RegistryWert.MAINBOARD() + Environment.NewLine;
            body += "HDD 1: " + HDD1();
            body += "HDD 2: " + HDD2();
            body += "HDD 3: " + HDD3();
            body += "DVD Laufwerk 1: " + RegistryWert.LW1() + Environment.NewLine;
            body += "DVD Laufwerk 2: " + RegistryWert.LW2() + Environment.NewLine;
            body += " ################################################################################" + Environment.NewLine;
            body += "Windows Version: " + RegistryWert.GetWindwosClientVersion() + Environment.NewLine;
            body += "Windows Lizenz: " + KeyDecoder.GetWindowsProductKeyFromRegistry() + Environment.NewLine;
            body += " ################################################################################" + Environment.NewLine;
            body += "Key: " + Entschluesseln(RegistryWert.Email_Passwort());



            MailMessage message = new MailMessage(from, to, betreff, body);
            var smtpClient = new SmtpClient(server, Convert.ToInt32(port))
            {
                Credentials = new NetworkCredential(from, passwort),
                EnableSsl = true
            };
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
               MessageBox.Show("Exception caught in CreateTimeoutTestMessage():" + ex.ToString(), "Fehler", MessageBoxButton.OK);
            }
            smtpClient.Dispose();
            this.Close();
        }
    }
}
