using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace Konfiguration_WPF
{


    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            // ######################################################################
            // ################# Ausgaben definieren !! #############################
            // ######################################################################
       
            SystemTyp();
            HDD1();
            HDD2();
            HDD3();
            
            lbl_OS_Lizenznummer.Content = KeyDecoder.GetWindowsProductKeyFromRegistry();
            lbl_Prozessor.Content = RegistryWert.CPU_NAME() + " (" + RegistryWert.CPU_BIT() + " Bit)";
            lbl_Hersteller.Content = RegistryWert.Hersteller();
            lbl_Modell1.Content = RegistryWert.Modell1();
            lbl_Modell2.Content = RegistryWert.Modell2();
            lbl_RAM_SLOTS.Content = Convert.ToInt32(RegistryWert.RAM_ANZ()) + " Slots belegt";
            lbl_RAM_TYP.Content = RegistryWert.RAM_TYP() + " @ " + RegistryWert.RAM_SPEED() + " MHz";
            lbl_RAM_TOTAL.Content = RegistryWert.RAM_TOTAL() + " GB";
            lbl_Seriennummer.Content = RegistryWert.SERIENNUMMER();
            lbl_GRAKA1.Content = RegistryWert.GRAFIK1();
            lbl_Resolution.Content = RegistryWert.GRAFIK1_RESOLUTION();
            lbl_Mainboard.Content = RegistryWert.MAINBOARD();
            lbl_DVD1.Content = RegistryWert.LW1();
            lbl_DVD2.Content = RegistryWert.LW2();
            lbl_OS_Version.Content = RegistryWert.GetWindwosClientVersion();
            txt_eigene_serial.Focus();

            // ######################################################################
            // ############### Letzte Eigene Serial eintragen !! ####################
            // ######################################################################
            if (txt_eigene_serial.Text == "")
            {
                txt_eigene_serial.Text = Letzte_Serial().ToString();
                txt_eigene_serial.Visibility = Visibility.Hidden;
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

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
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


        private void send_Mail(object sender, RoutedEventArgs e)
        {
            Grid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            Konfiguration_Senden ks = new Konfiguration_Senden();
            ks.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            Grid.Effect = null;
        }

        public static int Letzte_Serial()
        {
            OleDbConnection con = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            OleDbCommand cmd_letzte = new OleDbCommand("SELECT Eigene_Serial FROM Rechner ORDER BY Eigene_Serial DESC", con);
            con.Open();
            object letz = cmd_letzte.ExecuteScalar();
            con.Close();
            cmd_letzte.Dispose();
            return Convert.ToInt32(letz)+1;
        }


        private void In_Database(object sender, RoutedEventArgs e)
        {
            if (txt_eigene_serial.Text == "")
            {
                MessageBox.Show("Die Eigene Seriennummer sollte doch Eingetragen werden !", "Fehler", MessageBoxButton.OK);
                txt_eigene_serial.Focus();
                return;
            }
                

            OleDbConnection con = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            int i = 0;
            OleDbCommand cmd_suche = new OleDbCommand("SELECT * FROM Rechner WHERE Eigene_Serial = @E_Serial", con);
            cmd_suche.Parameters.AddWithValue("@E_Serial", txt_eigene_serial.Text);
            con.Open();
            OleDbDataReader reader1 = cmd_suche.ExecuteReader();
            while(reader1.Read())
            {
                i++;
            }
            con.Close();
            reader1.Close();
            if(i == 0)
            {
                OleDbCommand cmd = new OleDbCommand("INSERT INTO Rechner (Eigene_Serial, System_Art, KD_Nummer, CPU, CPU_Bit, Hersteller, Modell1, Modell2, HDD1, HDD2, HDD3, RAM_TYP, RAM_GB, RAM_SLOTS, RAM_SPEED, Seriennummer, Grafikkarte, GRAKA_Aufloesung, MB_Hersteller, MB_Modell, DVD1, DVD2, OS_Version, OS_Lizenz, Datum) VALUES (@Eigene_Serial, @System_Art, @KD_Nummer, @CPU, @CPU_Bit, @Hersteller, @Modell1, @Modell2, @HDD1, @HDD2, @HDD3, @RAM_TYP, @RAM_GB, @RAM_SLOTS, @RAM_SPEED, @Seriannummer, @Grafikkarte, @GRAKA_Aufloesung, @MB_Hersteller, @MB_Modell, @DVD1, @DVD2, @OS_Version, OS_Lizenz, Datum)", con);
                cmd.Parameters.AddWithValue("@Eigene_Serial", txt_eigene_serial.Text);
                cmd.Parameters.AddWithValue("@System_Art", SystemTyp_String());
                cmd.Parameters.AddWithValue("@KD_Nummer", "1");
                cmd.Parameters.AddWithValue("@CPU", RegistryWert.CPU_NAME());
                cmd.Parameters.AddWithValue("@CPU_Bit", RegistryWert.CPU_BIT());
                cmd.Parameters.AddWithValue("@Hersteller", RegistryWert.Hersteller());
                cmd.Parameters.AddWithValue("@Modell1", RegistryWert.Modell1());
                cmd.Parameters.AddWithValue("@Modell2", RegistryWert.Modell2());
                cmd.Parameters.AddWithValue("@HDD1", HDD1_String());
                cmd.Parameters.AddWithValue("@HDD2", HDD2_String());
                cmd.Parameters.AddWithValue("@HDD3", HDD3_String());
                cmd.Parameters.AddWithValue("@RAM_TYP", RegistryWert.RAM_TYP());
                cmd.Parameters.AddWithValue("@RAM_GB", RegistryWert.RAM_TOTAL());
                cmd.Parameters.AddWithValue("@RAM_SLOTS", RegistryWert.RAM_ANZ());
                cmd.Parameters.AddWithValue("@RAM_SPEED", RegistryWert.RAM_SPEED());
                cmd.Parameters.AddWithValue("@Seriennummer", RegistryWert.SERIENNUMMER());
                cmd.Parameters.AddWithValue("@Grafikkarte", RegistryWert.GRAFIK1());
                cmd.Parameters.AddWithValue("@GRAKA_Aufloesung", RegistryWert.GRAFIK1_RESOLUTION());
                cmd.Parameters.AddWithValue("@MB_Hersteller", RegistryWert.Hersteller());
                cmd.Parameters.AddWithValue("@MB_Modell", RegistryWert.Modell1());
                cmd.Parameters.AddWithValue("@DVD1", RegistryWert.LW1());
                cmd.Parameters.AddWithValue("@DVD2", RegistryWert.LW2());
                cmd.Parameters.AddWithValue("@OS_Version", RegistryWert.GetWindwosClientVersion());
                cmd.Parameters.AddWithValue("@OS_Lizenz", KeyDecoder.GetWindowsProductKeyFromRegistry());
                cmd.Parameters.AddWithValue("@Datum", DateTime.Now.Day + "." + DateTime.Now.Month + "."+ DateTime.Now.Year);

                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Daten wurden eingetragen !", "Erfolgreich", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Eintragen der Dateien: " + ex.ToString(), "Fehlermeldung !", MessageBoxButton.OK);
                }
                cmd.Dispose();
                con.Close();
            } else
            {
                
                MessageBox.Show("Die Eigene Seriennummer wird bereits Verwendet !", "Fehler", MessageBoxButton.OK);
                txt_eigene_serial.Focus();
                txt_eigene_serial.Text = Letzte_Serial().ToString();


            }






           

        }

        private void MSConfig_oeffnen(object sender, RoutedEventArgs e)
        {
        
        }

        private void Mach_An(object sender, RoutedEventArgs e)
        {

        }
    }


    
}