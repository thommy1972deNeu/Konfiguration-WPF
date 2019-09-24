using Microsoft.VisualBasic.CompilerServices;
using System;
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
            lbl_Prozessor.Content = RegistryWert.CPU_NAME() + " (" + RegistryWert.SOCKET() + ")";
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

        private void send_Mail(object sender, RoutedEventArgs e)
        {
            Grid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;
            Konfiguration_Senden ks = new Konfiguration_Senden();
            ks.ShowDialog();
            Splash.Visibility = Visibility.Collapsed;
            Grid.Effect = null;

            //Email.sende_Konfiguration();

        }

        private void In_Database(object sender, RoutedEventArgs e)
        {

        }

        private void MSConfig_oeffnen(object sender, RoutedEventArgs e)
        {

        }
    }


    
}