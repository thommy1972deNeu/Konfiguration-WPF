using Konfiguration_WPF.API;
using Microsoft.VisualBasic.CompilerServices;
using Renci.SshNet.Messages;
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


            eigene_kdnr.Text = RegistryWert.Registry_Lesen("Kundennummer");
            eigene_kdnr.IsEnabled = false;

            kd_nachname.Text = RegistryWert.Registry_Lesen("KD-Nachname");
            kd_vorname.Text = RegistryWert.Registry_Lesen("KD-Vorname");
            kd_strasse.Text = RegistryWert.Registry_Lesen("KD-Strasse");
            kd_hsnr.Text = RegistryWert.Registry_Lesen("KD-HsNr");
            kd_plz.Text = RegistryWert.Registry_Lesen("KD-PLZ");
            kd_ort.Text = RegistryWert.Registry_Lesen("KD-Ort");
            kd_email.Text = RegistryWert.Registry_Lesen("KD-Email");

            kd_vorname.IsEnabled = (string.IsNullOrEmpty(kd_vorname.Text)) ? true : false;
            kd_nachname.IsEnabled = (string.IsNullOrEmpty(kd_nachname.Text)) ? true : false;
            kd_strasse.IsEnabled = (string.IsNullOrEmpty(kd_strasse.Text)) ? true : false;
            kd_hsnr.IsEnabled = (string.IsNullOrEmpty(kd_hsnr.Text)) ? true : false;
            kd_plz.IsEnabled = (string.IsNullOrEmpty(kd_plz.Text)) ? true : false;
            kd_ort.IsEnabled = (string.IsNullOrEmpty(kd_ort.Text)) ? true : false;
            kd_email.IsEnabled = (string.IsNullOrEmpty(kd_email.Text)) ? true : false;

            status.Text = "© 2019 - " + DateTime.Now.Year + " Computerservice Blasius Thomas";
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
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
            catch
            {
                return null;
            }
        }


        private void absenden_2(object sender, RoutedEventArgs e)
        {
            Absenden_Button.IsEnabled = false;

            if (kd_nachname.Text == "")
            {
                MessageBox.Show("Ihr Nachname muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                kd_nachname.Focus();
                return;
            }
            if (kd_vorname.Text == "")
            {
                MessageBox.Show("Ihr Vorname muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                kd_vorname.Focus();
                return;
            }

            if (kd_strasse.Text == "")
            {
                MessageBox.Show("Ihre Strasse muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                kd_strasse.Focus();
                return;
            }

            if (kd_hsnr.Text == "")
            {
                MessageBox.Show("Ihre Hausnummer muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                kd_hsnr.Focus();
                return;
            }

            if (kd_plz.Text == "")
            {
                MessageBox.Show("Ihre PLZ muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                kd_plz.Focus();
                return;
            }

            if (kd_ort.Text == "")
            {
                MessageBox.Show("Ihr Ort muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                kd_ort.Focus();
                return;
            }

            if (kd_email.Text == "")
            {
                MessageBox.Show("Ihre Email Adresse muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                kd_email.Focus();
                return;
            }

            if (antivirenprogramm.Text == "Auswahl")
            {
                MessageBox.Show("Antiviren Programm muss angegeben werden !", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                antivirenprogramm.Focus();
                return;
            }

            string body = @"Rechner-Konfiguration" + Environment.NewLine;
            body += "Kundenname: " + kd_nachname.Text + " " + kd_vorname.Text + Environment.NewLine;
            body += "KD Adresse: " + Environment.NewLine;
            body += kd_strasse.Text + " " + kd_hsnr.Text + Environment.NewLine;
            body += kd_plz.Text + " " + kd_ort.Text + Environment.NewLine;
            body += "Kundenummer: " + eigene_kdnr.Text + Environment.NewLine;
            body += " ################################################################################" + Environment.NewLine;
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

            Pfade.MAIL_USER();


            MailMessage message = new MailMessage(Pfade.Mail_from, Pfade.Mail_to, "Einstellungen von " + RegistryWert.Registry_Lesen("KD-Nachname") + " " + RegistryWert.Registry_Lesen("KD-Vornname"), body);
            var smtpClient = new SmtpClient(Pfade.Mail_server, Pfade.Mail_port)
            {
                Credentials = new NetworkCredential(Pfade.Mail_username, Pfade.Mail_pass),
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

            MessageBox.Show("Email wurde gesendet !", "Email gesendet", MessageBoxButton.OK, MessageBoxImage.Information);

        }

 
    }
}
