using Konfiguration_WPF.Seiten;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace Konfiguration_WPF
{

    public partial class MainWindow : Window
    {


        // ###########################################################
        // ###########################################################
        public MainWindow()
        {

            InitializeComponent();


                    KDNR kundenform = new KDNR();
                    kundenform.Show();
                    kundenform.Topmost = true;
                    this.Visibility = Visibility.Hidden;
   


                // ######################################################################
                // ################# Ausgaben definieren !! #############################
                // ######################################################################




                SystemTyp();
                HDD1();
                HDD2();
                HDD3();


                // #############################################
                // ########### Registry Wert eintragen #########
                // #############################################
                if (Registry.GetValue(ConfigurationManager.AppSettings["Reg_URI"].ToString(), "Datum", null) == null)
                {
                    RegistryKey key;
                    key = Registry.CurrentConfig.CreateSubKey("Computerservice Blasius Thomas");
                    key.SetValue("Serial", RegistryWert.SERIENNUMMER());
                    key.SetValue("Proz_ID", RegistryWert.CPU_ID());
                    key.SetValue("Datum", DateTime.Now);
                    key.Close();
                }
                else
                {
                    Reg_Wert_Serial.Content = Registry.GetValue(ConfigurationManager.AppSettings["Reg_URI"].ToString(), "Serial", true);
                    Reg_Wert_Proz_ID.Content = Registry.GetValue(ConfigurationManager.AppSettings["Reg_URI"].ToString(), "Proz_ID", true);
                    Reg_Wert_Datum.Content = Registry.GetValue(ConfigurationManager.AppSettings["Reg_URI"].ToString(), "Datum", true) + " Uhr";
                }

                RegistryWert.Registry_Eintrag("Impulserhaltungssatz", DateTime.Now.Date.ToString());

                lbl_OS_Lizenznummer.Content = KeyDecoder.GetWindowsProductKeyFromRegistry();
                RegistryWert.Registry_Eintrag("Windows-Lizenz", KeyDecoder.GetWindowsProductKeyFromRegistry());

                lbl_Prozessor.Content = RegistryWert.CPU_NAME() + " (" + RegistryWert.CPU_BIT() + " Bit)";
                RegistryWert.Registry_Eintrag("CPU_Bit", RegistryWert.CPU_BIT() + " Bit");

                lbl_Hersteller.Content = RegistryWert.Hersteller();
                RegistryWert.Registry_Eintrag("PC_Hersteller", RegistryWert.Hersteller());

                lbl_Modell1.Content = RegistryWert.Modell1();
                RegistryWert.Registry_Eintrag("PC_Modell 1", RegistryWert.Modell1());

                lbl_Modell2.Content = RegistryWert.Modell2();
                RegistryWert.Registry_Eintrag("PC_Modell 2", RegistryWert.Modell2());

                lbl_RAM_SLOTS.Content = Convert.ToInt32(RegistryWert.RAM_ANZ()) + " Slots belegt";
                RegistryWert.Registry_Eintrag("RAM_Slots", Convert.ToInt32(RegistryWert.RAM_ANZ()) + " Slots belegt");

                lbl_RAM_TYP.Content = RegistryWert.RAM_TYP() + " @ " + RegistryWert.RAM_SPEED() + " MHz";
                RegistryWert.Registry_Eintrag("RAM_Speed", RegistryWert.RAM_SPEED() + " MHz");

                lbl_RAM_TOTAL.Content = RegistryWert.RAM_TOTAL() + " GB";
                RegistryWert.Registry_Eintrag("RAM_Total", RegistryWert.RAM_TOTAL() + " GB");

                lbl_Seriennummer.Content = RegistryWert.SERIENNUMMER();
                lbl_GRAKA1.Content = RegistryWert.GRAFIK1();
                RegistryWert.Registry_Eintrag("Grafik_Karte", RegistryWert.GRAFIK1());

                lbl_Resolution.Content = RegistryWert.GRAFIK1_RESOLUTION();
                RegistryWert.Registry_Eintrag("Grafik_Auflösung", RegistryWert.GRAFIK1_RESOLUTION());

                lbl_Mainboard.Content = RegistryWert.MAINBOARD();
                RegistryWert.Registry_Eintrag("Mainboaard_Modell", RegistryWert.MAINBOARD());

                lbl_DVD1.Content = RegistryWert.LW1();
                RegistryWert.Registry_Eintrag("Laufwerk 1", RegistryWert.LW1());

                lbl_DVD2.Content = RegistryWert.LW2();
                RegistryWert.Registry_Eintrag("Laufwerk 2", RegistryWert.LW2());

                lbl_OS_Version.Content = RegistryWert.GetWindwosClientVersion();
                RegistryWert.Registry_Eintrag("Windows-Version", RegistryWert.GetWindwosClientVersion());

                // ######################################################################
                // ############### Letzte Eigene Serial eintragen !! ####################
                // ######################################################################



            
        }
            public string Verschluesseln(string strOriginal)
            {
                try
                {

                    string EncryptionKey = RegistryWert.Encryption_Key();
                    byte[] clearBytes = Encoding.Unicode.GetBytes(strOriginal);
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(clearBytes, 0, clearBytes.Length);
                                cs.Close();
                            }
                            strOriginal = Convert.ToBase64String(ms.ToArray());
                        }
                    }
                    return strOriginal;
                }
                catch
                {
                    return null;
                }
            }

            public string Entschluesseln(string strDecrypted)
            {
                try
                {
                    string EncryptionKey = "test123456key";
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


            public void SystemTyp()
            {
                ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
                using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem"))
                {
                    enumerator = managementObjectSearcher.Get().GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ManagementObject current = (ManagementObject)enumerator.Current;

                        if (!Operators.ConditionalCompareObjectEqual(current["Model"], "System Product Name", false))
                        {
                            lbl_Geraete_Art.Content = Conversions.ToString(current["Model"]);
                        }
                        else
                        {
                            lbl_Geraete_Art.Content = "Unbekannt";
                        }
                        if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "0", false))
                        {
                            lbl_Geraete_Art.Content = "Unbekannt";
                        }
                        else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "1", false))
                        {
                            lbl_Geraete_Art.Content = "Desktop-PC";
                        }
                        else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "2", false))
                        {
                            lbl_Geraete_Art.Content = "Laptop";
                        }
                        else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "3", false))
                        {
                            lbl_Geraete_Art.Content = "Desktop-PC";
                        }
                        else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "4", false))
                        {
                            lbl_Geraete_Art.Content = "Enterprise-Server";
                        }
                        else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "5", false))
                        {
                            lbl_Geraete_Art.Content = "SOHO Server";
                        }
                        else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "6", false))
                        {
                            lbl_Geraete_Art.Content = "SOHO Server";
                        }
                        else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "7", false))
                        {
                            lbl_Geraete_Art.Content = "Performance Server";
                        }
                        else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "8", false))
                        {
                            lbl_Geraete_Art.Content = "Slate";
                        }
                        else if (Operators.ConditionalCompareObjectEqual(current["PCSystemType"], "9", false))
                        {
                            lbl_Geraete_Art.Content = "Maximum";
                        }
                    }
                }

            }

            public static string SystemTyp_String()
            {
                ManagementObjectCollection.ManagementObjectEnumerator enumerator;
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

            public void HDD1()
            {
                ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
                using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE0'"))
                {
                    enumerator = managementObjectSearcher.Get().GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ManagementObject current = (ManagementObject)enumerator.Current;
                        lbl_HDD1.Content = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
                        Label lblHDD1 = this.lbl_HDD1;
                        Label label = lblHDD1;
                        lbl_HDD1.Content = Conversions.ToString(Operators.AddObject(label.Content, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
                    }
                }
            }

            public static string HDD1_String()
            {
                var HDD1 = String.Empty;

                ManagementObjectCollection.ManagementObjectEnumerator enumerator;
                using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE0'"))
                {
                    enumerator = managementObjectSearcher.Get().GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ManagementObject current = (ManagementObject)enumerator.Current;
                        HDD1 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
                        HDD1 += Conversions.ToString(Operators.AddObject(HDD1, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
                    }
                    return HDD1;
                }
            }


            public void HDD2()
            {
                ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
                using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE1'"))
                {
                    enumerator = managementObjectSearcher.Get().GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ManagementObject current = (ManagementObject)enumerator.Current;
                        lbl_HDD2.Content = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
                        Label lblHDD1 = this.lbl_HDD2;
                        Label label = lblHDD1;
                        lbl_HDD2.Content = Conversions.ToString(Operators.AddObject(label.Content, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
                    }
                }
            }

            public static string HDD2_String()
            {
                var HDD2 = String.Empty;

                ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
                using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE1'"))
                {
                    enumerator = managementObjectSearcher.Get().GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ManagementObject current = (ManagementObject)enumerator.Current;
                        HDD2 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
                        HDD2 += Conversions.ToString(Operators.AddObject(HDD2, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
                    }
                }
                return HDD2;
            }

            public void HDD3()
            {
                ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
                using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE2'"))
                {
                    enumerator = managementObjectSearcher.Get().GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ManagementObject current = (ManagementObject)enumerator.Current;
                        lbl_HDD3.Content = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
                        Label lblHDD1 = this.lbl_HDD3;
                        Label label = lblHDD1;
                        lbl_HDD3.Content = Conversions.ToString(Operators.AddObject(label.Content, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
                    }
                }
            }

            public static string HDD3_String()
            {
                var HDD3 = String.Empty;

                ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
                using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE2'"))
                {
                    enumerator = managementObjectSearcher.Get().GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ManagementObject current = (ManagementObject)enumerator.Current;
                        HDD3 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
                        HDD3 += Conversions.ToString(Operators.AddObject(HDD3, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
                    }
                }
                return HDD3;
            }


            private void Send_Mail(object sender, RoutedEventArgs e)
            {
                Grid.Effect = new BlurEffect();
                Splash.Visibility = Visibility.Visible;
                Konfiguration_Senden ks = new Konfiguration_Senden();
                ks.ShowDialog();
                Splash.Visibility = Visibility.Collapsed;
                Grid.Effect = null;
            }


            private void MSConfig_oeffnen(object sender, RoutedEventArgs e)
            {
                System.Diagnostics.Process.Start("C:\\Windows\\System32\\msconfig.exe");
            }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KDNR nr1 = new KDNR();
            nr1.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            KDNR nr2 = new KDNR();
            nr2.Show();
        }
    }

    
    
}