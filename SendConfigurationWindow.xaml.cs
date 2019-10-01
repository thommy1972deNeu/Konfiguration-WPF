using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;

namespace Konfiguration_WPF
{
    /// <summary>
    /// Interaktionslogik für SendConfigurationWindow.xaml
    /// </summary>
    public partial class SendConfigurationWindow : Window
    {
        private ConfiguratorViewModel _configuratorViewModel;

        public SendConfigurationWindow()
        {
            InitializeComponent();
            status.Text = "© 2019 - " + DateTime.Now.Year + " Computerservice Blasius Thomas";
        }
       

        private void SendMailClick(object sender, RoutedEventArgs e)
        {
            _configuratorViewModel = (ConfiguratorViewModel)DataContext;

            btn_SendMail.Visibility = Visibility.Hidden;

            if (eigene_seriennummer.Text == "")
            {
                MessageBox.Show("Seriennummer muss angegeben werden !", "Fehler", MessageBoxButton.OK);
                eigene_seriennummer.Focus();
                btn_SendMail.Visibility = Visibility.Visible;
                return;
                
            }

            if (antivirenprogramm.Text == "Auswahl")
            {
                MessageBox.Show("Antiviren Programm muss angegeben werden !", "Fehler", MessageBoxButton.OK);
                antivirenprogramm.Focus();
                btn_SendMail.Visibility = Visibility.Visible;
                return;
            }

            // ##################################################################################################### 
            // ###################   Abfrage nach Passwort habe ich mal Deaktiviert ################################ 
            // ##################################################################################################### 

            //if (zauberwort.Text != ConfigurationManager.AppSettings["GhZfrgWRGwr57456DGWferGF$ZG"].ToString())
            //{
            //  MessageBox.Show("Falsches Passwort !", "Fehler", MessageBoxButton.OK);
            //  zauberwort.Focus();
            //  btn_SendMail.Visibility = Visibility.Visible;
            //  return;
            //} 



            var from    = ConfigurationManager.AppSettings["email_von_an"];
            var to      = ConfigurationManager.AppSettings["email_von_an"];
            var port    = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            var server  = ConfigurationManager.AppSettings["email_Server"];
            var betreff = "Programm-Einstellungen";

            var emailPassword = Cryptography.Decrypt(_configuratorViewModel.Email_PasswordEncrypted);


            using (var mailMessage = new MailMessage(@from, to, betreff, BuildEmailMessage()))
            {
                using (var smtpClient = new SmtpClient(server, Convert.ToInt32(port))
                { Credentials = new NetworkCredential(@from, emailPassword),EnableSsl = true})
                {
                    try
                    {
                        smtpClient.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception caught in CreateTimeoutTestMessage():" + ex, "Fehler", MessageBoxButton.OK);
                    }

                }
            }

            Close();
        }

        private string BuildEmailMessage()
        {
            var message = new StringBuilder();

            message.Append(@"Rechner-Konfiguration" + Environment.NewLine);

            message.Append(" ################################################################################" + Environment.NewLine);
            message.Append("Eigene Seriennummer: " + eigene_seriennummer.Text + Environment.NewLine);
            message.Append("Antiviren-Software: " + antivirenprogramm.Text + Environment.NewLine);
            message.Append(" ################################################################################" + Environment.NewLine);
            message.Append("Geräte-Art: " + _configuratorViewModel.SystemTyp + Environment.NewLine);
            message.Append("Hersteller: " + _configuratorViewModel.Manufacturer + Environment.NewLine);
            message.Append("Modell 1: " + _configuratorViewModel.Modell1 + Environment.NewLine);
            message.Append("Modell 2: " + _configuratorViewModel.Modell2 + Environment.NewLine);
            message.Append("Seriennummer: " + _configuratorViewModel.SerialNumber + Environment.NewLine);
            message.Append("Prozessor: " + Environment.NewLine);
            message.Append("RAM-Total: " + _configuratorViewModel.TotalPhysicalMemory + " GB " + _configuratorViewModel.RamType + " @ " + _configuratorViewModel.RamSpeed + " MHz" + Environment.NewLine);
            message.Append("Grafikkarte: " + _configuratorViewModel.GraphicCard + " @" + _configuratorViewModel.GraphicCardResolution + Environment.NewLine);
            message.Append("Mainboard: " + _configuratorViewModel.Mainboard + Environment.NewLine);
            message.Append("HDD 1: " + _configuratorViewModel.HDD1);
            message.Append("HDD 2: " + _configuratorViewModel.HDD2);
            message.Append("HDD 3: " + _configuratorViewModel.HDD3);
            message.Append("DVD Laufwerk 1: " + _configuratorViewModel.RomDrive1 + Environment.NewLine);
            message.Append("DVD Laufwerk 2: " + _configuratorViewModel.RomDrive2 + Environment.NewLine);
            message.Append(" ################################################################################" + Environment.NewLine);
            message.Append("Windows Version: " + _configuratorViewModel.OsVersion + Environment.NewLine);
            message.Append("Windows Lizenz: " + KeyDecoder.GetWindowsProductKeyFromRegistry() + Environment.NewLine);
            message.Append(" ################################################################################" + Environment.NewLine);
            message.Append("Key: " + Cryptography.Decrypt(_configuratorViewModel.Email_PasswordEncrypted));

            return message.ToString();

        }
    }
}
