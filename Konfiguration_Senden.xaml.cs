﻿using Konfiguration_WPF.API;
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



            MessageBox.Show("Pass: " + Pfade.MAIL_PASS() + Environment.NewLine + "Server: " + Pfade.MAIL_SERVER() + Environment.NewLine + "User: " + Pfade.MAIL_USER() + Environment.NewLine + "Username:" + Pfade.MAIL_USERNAME() + Environment.NewLine + "To: " +  Pfade.MAIL_TO());


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

            string passw = Pfade.MAIL_PASS();
            string from = Pfade.MAIL_USER();
            string username = //Pfade.MAIL_USERNAME();
            string to = Pfade.MAIL_TO();
            string server = Pfade.MAIL_SERVER();

            MailMessage message = new MailMessage(from, to, "Einstellungen", body);
            var smtpClient = new SmtpClient(server, 587)
            {
                Credentials = new NetworkCredential(username, passw),
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

        private void kd_nachname_GotFocus(object sender, RoutedEventArgs e)
        {
            kd_nachname.Clear();
        }

        private void kd_vorname_GotFocus(object sender, RoutedEventArgs e)
        {
            kd_vorname.Clear();
        }

        private void kd_plz_GotFocus(object sender, RoutedEventArgs e)
        {
            kd_plz.Clear();
        }

        private void kd_ort_GotFocus(object sender, RoutedEventArgs e)
        {
            kd_ort.Clear();
        }
        private void kd_strasse_GotFocus(object sender, RoutedEventArgs e)
        {
            kd_strasse.Clear();
        }
        private void kd_hsnr_GotFocus(object sender, RoutedEventArgs e)
        {
            kd_hsnr.Clear();
        }
        private void kd_nachname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (kd_nachname.Text.Length == 0)
                kd_nachname.Text = "Nachname";
        }

        private void kd_vorname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (kd_vorname.Text.Length == 0)
                kd_vorname.Text = "Vorname";
        }

        private void kd_hsnr_LostFocus(object sender, RoutedEventArgs e)
        {
            if (kd_hsnr.Text.Length == 0)
                kd_hsnr.Text = "HsNr";
        }

        private void kd_plz_LostFocus(object sender, RoutedEventArgs e)
        {
            if (kd_plz.Text.Length == 0)
                kd_plz.Text = "PLZ";
        }

        private void kd_ort_LostFocus(object sender, RoutedEventArgs e)
        {
            if (kd_ort.Text.Length == 0)
                kd_ort.Text = "Wohnort";
        }

        private void kd_strasse_LostFocus(object sender, RoutedEventArgs e)
        {
            if (kd_strasse.Text.Length == 0)
                kd_strasse.Text = "Strasse";
        }

        private void kd_email_GotFocus(object sender, RoutedEventArgs e)
        {
            kd_email.Clear();
        }

        private void kd_email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (kd_email.Text.Length == 0)
                kd_email.Text = "Ihre EMail Adresse";
        }
    }
}
