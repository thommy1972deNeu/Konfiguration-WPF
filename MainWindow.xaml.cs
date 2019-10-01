using System;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Windows;
using System.Windows.Media.Effects;
using Microsoft.Win32;

namespace Konfiguration_WPF
{


    public partial class MainWindow : Window
    {
        private readonly ConfiguratorViewModel _configuratorViewModel;
        public MainWindow()
        {

            InitializeComponent();

            // ######################################################################
            // ################# Ausgaben definieren !! #############################
            // ######################################################################

            _configuratorViewModel = ConfiguratorViewModel.BuildConfiguratorViewModel();

            lbl_Geraete_Art.Content = _configuratorViewModel.SystemTyp;
            //HDD1();
            //HDD2();
            //HDD3();


            // #############################################
            // ########### Registry Wert eintragen #########
            // #############################################
            if (Registry.GetValue(ConfigurationManager.AppSettings["Reg_URI"], "Datum", null) == null)
            {
                var key = Registry.CurrentConfig.CreateSubKey("Computerservice Blasius Thomas");
                key.SetValue("Serial", _configuratorViewModel.SerialNumber);
                key.SetValue("Proz_ID", _configuratorViewModel.ProcessorID);
                key.SetValue("Datum", DateTime.Now);
                key.Close();
            }
            else
            {
                MessageBox.Show("Gerät ist bereits bei uns gewesen !" + Environment.NewLine + "Proz-ID: " + Registry.GetValue(ConfigurationManager.AppSettings["Reg_URI"], "Proz_ID", true) + Environment.NewLine + Environment.NewLine + "Bitte Warten... Konfiguration wird geladen...", "Message", MessageBoxButton.OK);
                Reg_Wert_Serial.Content = Registry.GetValue(ConfigurationManager.AppSettings["Reg_URI"], "Serial", true);
                Reg_Wert_Proz_ID.Content = Registry.GetValue(ConfigurationManager.AppSettings["Reg_URI"], "Proz_ID", true);
                Reg_Wert_Datum.Content = Registry.GetValue(ConfigurationManager.AppSettings["Reg_URI"], "Datum", true) + " Uhr";
            }
            


            lbl_OS_Lizenznummer.Content = KeyDecoder.GetWindowsProductKeyFromRegistry();
            lbl_Prozessor.Content = _configuratorViewModel.CpuName + " (" + _configuratorViewModel.CPUBit + " Bit)";
            lbl_Hersteller.Content = _configuratorViewModel.Manufacturer;
            lbl_Modell1.Content = _configuratorViewModel.Modell1;
            lbl_Modell2.Content = _configuratorViewModel.Modell2;
            lbl_RAM_SLOTS.Content = Convert.ToInt32(_configuratorViewModel.PhysicalMemoryCount) + " Slots belegt";
            lbl_RAM_TYP.Content = _configuratorViewModel.RamType + " @ " + _configuratorViewModel.RamSpeed + " MHz";
            lbl_RAM_TOTAL.Content = _configuratorViewModel.TotalPhysicalMemory + " GB";
            lbl_Seriennummer.Content = _configuratorViewModel.SerialNumber;
            lbl_GRAKA1.Content = _configuratorViewModel.GraphicCard;
            lbl_Resolution.Content = _configuratorViewModel.GraphicCardResolution;
            lbl_Mainboard.Content = _configuratorViewModel.Mainboard;
            lbl_DVD1.Content = _configuratorViewModel.RomDrive1;
            lbl_DVD2.Content = _configuratorViewModel.RomDrive2;
            lbl_OS_Version.Content = _configuratorViewModel.OsVersion;

            // ######################################################################
            // ############### Letzte Eigene Serial eintragen !! ####################
            // ######################################################################


        }

     

        private void OpenSendConfigurationWindow(object sender, RoutedEventArgs e)
        {
            Grid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;

            var sendConfigurationWindow = new SendConfigurationWindow {DataContext = _configuratorViewModel};
            sendConfigurationWindow.ShowDialog();

            Splash.Visibility = Visibility.Collapsed;
            Grid.Effect = null;
        }

       


        private async void In_Database(object sender, RoutedEventArgs e)
        {
                try
                {

                    var oleDbConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                    int i = 0;

                    OleDbDataReader reader1;
                    using (var cmdSuche = new OleDbCommand("SELECT * FROM Rechner WHERE Seriennummer = @snummer", oleDbConnection))
                    {
                        cmdSuche.Parameters.AddWithValue("@snummer", lbl_Seriennummer.Content);
                        oleDbConnection.Open();
                        reader1 = cmdSuche.ExecuteReader();

                        while(reader1.Read())
                        {
                            i++;
                        }

                    }

                    oleDbConnection.Close();
                    reader1.Close();

                    if (i == 0)
                    {
                        var oleDbCommand = new OleDbCommand("INSERT INTO Rechner (System_Art, KD_Nummer, CPU, CPU_Bit, Hersteller, Modell1, Modell2, HDD1, HDD2, HDD3, RAM_TYP, RAM_GB, RAM_SLOTS, RAM_SPEED, Seriennummer, Grafikkarte, GRAKA_Aufloesung, MB_Hersteller, MB_Modell, DVD1, DVD2, OS_Version, OS_Lizenz, Datum) VALUES (@System_Art, @KD_Nummer, @CPU, @CPU_Bit, @Hersteller, @Modell1, @Modell2, @HDD1, @HDD2, @HDD3, @RAM_TYP, @RAM_GB, @RAM_SLOTS, @RAM_SPEED, @Seriannummer, @Grafikkarte, @GRAKA_Aufloesung, @MB_Hersteller, @MB_Modell, @DVD1, @DVD2, @OS_Version, OS_Lizenz, Datum)", oleDbConnection);

                        oleDbCommand.Parameters.AddWithValue("@System_Art", _configuratorViewModel.SystemTyp);
                        oleDbCommand.Parameters.AddWithValue("@KD_Nummer", "1");
                        oleDbCommand.Parameters.AddWithValue("@CPU", _configuratorViewModel.CpuName);
                        oleDbCommand.Parameters.AddWithValue("@CPU_Bit", _configuratorViewModel.CPUBit);
                        oleDbCommand.Parameters.AddWithValue("@Hersteller", _configuratorViewModel.Manufacturer);
                        oleDbCommand.Parameters.AddWithValue("@Modell1", _configuratorViewModel.Modell1);
                        oleDbCommand.Parameters.AddWithValue("@Modell2", _configuratorViewModel.Modell2);
                        oleDbCommand.Parameters.AddWithValue("@HDD1", _configuratorViewModel.HDD1);
                        oleDbCommand.Parameters.AddWithValue("@HDD2", _configuratorViewModel.HDD2);
                        oleDbCommand.Parameters.AddWithValue("@HDD3", _configuratorViewModel.HDD3);
                        oleDbCommand.Parameters.AddWithValue("@RAM_TYP", _configuratorViewModel.RamType);
                        oleDbCommand.Parameters.AddWithValue("@RAM_GB", _configuratorViewModel.TotalPhysicalMemory);
                        oleDbCommand.Parameters.AddWithValue("@RAM_SLOTS", _configuratorViewModel.PhysicalMemoryCount);
                        oleDbCommand.Parameters.AddWithValue("@RAM_SPEED", _configuratorViewModel.RamSpeed);
                        oleDbCommand.Parameters.AddWithValue("@Seriennummer", _configuratorViewModel.SerialNumber);
                        oleDbCommand.Parameters.AddWithValue("@Grafikkarte", _configuratorViewModel.GraphicCard);
                        oleDbCommand.Parameters.AddWithValue("@GRAKA_Aufloesung", _configuratorViewModel.GraphicCardResolution);
                        oleDbCommand.Parameters.AddWithValue("@MB_Hersteller", _configuratorViewModel.Manufacturer);
                        oleDbCommand.Parameters.AddWithValue("@MB_Modell", _configuratorViewModel.Modell1);
                        oleDbCommand.Parameters.AddWithValue("@DVD1", _configuratorViewModel.RomDrive1);
                        oleDbCommand.Parameters.AddWithValue("@DVD2", _configuratorViewModel.RomDrive2);
                        oleDbCommand.Parameters.AddWithValue("@OS_Version", _configuratorViewModel.OsVersion);
                        oleDbCommand.Parameters.AddWithValue("@OS_Lizenz", KeyDecoder.GetWindowsProductKeyFromRegistry());
                        oleDbCommand.Parameters.AddWithValue("@Datum", DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year);

                        await oleDbConnection.OpenAsync();
                        await oleDbCommand.ExecuteNonQueryAsync();
                        
                        oleDbCommand.Dispose();

                        MessageBox.Show("Daten wurden eingetragen !", "Erfolgreich", MessageBoxButton.OK);

                        oleDbConnection.Close();
                    }
                    else
                        MessageBox.Show("Die Seriennummer wird bereits Verwendet !", "Fehler", MessageBoxButton.OK);

                }
                catch { }

        }

        private void OpenMsConfigClick(object sender, RoutedEventArgs e) => OpenMsConfigExecutable();

        private static void OpenMsConfigExecutable()
        {
            if (File.Exists(Environment.SystemDirectory + "msconfig.exe"))
                MessageBox.Show("Existent", "Jip", MessageBoxButton.OK);
            else
                MessageBox.Show("Nicht da", "Fehler", MessageBoxButton.OK);
        }

    }


    
}