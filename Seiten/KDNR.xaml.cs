using Konfiguration_WPF.API;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Konfiguration_WPF.Seiten
{
    /// <summary>
    /// Interaktionslogik für KDNR.xaml
    /// </summary>
    public partial class KDNR : Window
    {
       
        public KDNR()
        {
            InitializeComponent();

            txt_KDNR.Text = RegistryWert.Registry_Lesen("Kundennummer").ToString();
            txt_Email.Text = RegistryWert.Registry_Lesen("KD-Email").ToString();
            txt_Nachname.Text = RegistryWert.Registry_Lesen("KD-Nachname").ToString();
            txt_Vorname.Text = RegistryWert.Registry_Lesen("KD-Vorname").ToString();
            txt_Strasse.Text = RegistryWert.Registry_Lesen("KD-Strasse").ToString();
            txt_HsNr.Text = RegistryWert.Registry_Lesen("KD-HsNr").ToString();
            txt_PLZ.Text = RegistryWert.Registry_Lesen("KD-PLZ").ToString();
            txt_Ort.Text =  RegistryWert.Registry_Lesen("KD-Ort").ToString();
        }

        private void txt_KDNR_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pfade pf = new Pfade();
                Dictionary<string, string> getParameters = new Dictionary<string, string>();
                getParameters.Add("kdnr", txt_KDNR.Text);
                getParameters.Add("vorname", txt_Vorname.Text);
                getParameters.Add("nachname", txt_Nachname.Text);
                getParameters.Add("strasse", txt_Strasse.Text);
                getParameters.Add("hsnr", txt_HsNr.Text);
                getParameters.Add("plz", txt_PLZ.Text);
                getParameters.Add("ort", txt_Ort.Text);
                getParameters.Add("email", txt_Email.Text);
                getParameters.Add("UUID", RegistryWert.COMPUTER_SYSTEM_UUID());

                string kdnr_response =  pf.HTTPSRequestGet(pf.Pfad_Kd, getParameters);
                string[] data = kdnr_response.Split(':');

                if(kdnr_response.Length > 1)
                {
                    RegistryWert.Registry_Eintrag("Kundennummer", txt_KDNR.Text);
                    RegistryWert.Registry_Eintrag("KD-Nachname", data[0]);
                    RegistryWert.Registry_Eintrag("KD-Vorname", data[1]);
                    RegistryWert.Registry_Eintrag("KD-Strasse", data[2]);
                    RegistryWert.Registry_Eintrag("KD-HsNr", data[3]);
                    RegistryWert.Registry_Eintrag("KD-PLZ", data[4]);
                    RegistryWert.Registry_Eintrag("KD-Ort", data[5]);
                    RegistryWert.Registry_Eintrag("KD-Email", data[6]);

                    MessageBox.Show("Die Daten wurde gespeichert !" + Environment.NewLine + "Das Programm muss zum Speichern neu gestartet werden !", "Programm - Meldung!", MessageBoxButton.OK, MessageBoxImage.Information);

                   

                } else
                {
                    MessageBox.Show("Wir konnten leider keine Personendaten zu ihrer angegebenden Kundenummer finden !" + Environment.NewLine + "Bitte versuchen Sie es noch einmal oder Kontaktieren Sie mich.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
      
        }
    }
}
