using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using RemotingLinker;
using System.IO;

namespace RemotingServer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        TcpChannel channel;
        int p; // variable contenant le numero de port

        string s; // variable contenant le nom de la session


   /******************** demarrer le serveur *****************************************/
        private void Demarer_Click(object sender, RoutedEventArgs e)
        {
           
            // verification des champs
            

                if (channel == null)
                {


                    
                        if (session.Text != "" && int.TryParse(port.Text, out p))
                        {
                            
                            s = session.Text;

                            // creation du canal de communication
                            channel = new TcpChannel(p);
                            ChannelServices.RegisterChannel(channel, false);
                            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SampleObject), s, WellKnownObjectMode.Singleton);
                        
                            // mise a jours des vues
                            adresse.Text = "tcp://localhost:" + port.Text + "/" + session.Text;
                            status.Text = "  Demarrer...";
                            Demarer.IsEnabled = false;
                            arreter.IsEnabled = true;
                        }
                        else
                        {
         
                                MessageBoxResult result = MessageBox.Show("il y'a eu une erreur,Voulez vous verifier les parametres?", "My App", MessageBoxButton.YesNo);
                                switch (result)
                                {
                                    case MessageBoxResult.Yes:
                                      
                                  
                                        break;
                                    case MessageBoxResult.No:
                                        System.Windows.Application.Current.Shutdown();
                                        break;
                                }

                        }
                   
                }
            
            
        }

        // arret du serveur
        private void arreter_Click(object sender, RoutedEventArgs e)
        {

            if (channel != null)
            {
                // liberation du canal de communication
                ChannelServices.UnregisterChannel(channel);
                channel = null;

                // mise a jours des vues
                status.Text = "    Arreter.";
                adresse.Text = "";
                Demarer.IsEnabled= true;
                arreter.IsEnabled = false;
            }

        }

       
    }
}
