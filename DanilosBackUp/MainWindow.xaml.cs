using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using DanilosBackUp.Adicionales;
using DanilosBackUp.Utils;
using DanilosBackUp.VentanasAuxiliares;
using Microsoft.Win32;
using System.Security;

namespace DanilosBackUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();

            try
            {

                bool isInstalationComplete = StartUpCheck.IsInstalationComplete();


                if (isInstalationComplete == false)
                {
                    InitCredentialSetting settings = new InitCredentialSetting();
                    settings.ShowDialog();

                    if (settings.DialogResult == true)
                    {
                        FancyBalloon balloon = new FancyBalloon();
                        balloon.BalloonText = "Configuración";
                        balloon.TxtInfo.Text = "La aplicación esta lista para utilizarse";
                        //show balloon and close it after 4 seconds
                        MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);

                    }
                    else
                    {
                        Application.Current.Shutdown();
                    }
                }

                new StartUpCheck().ReviewLastProcessStatus();


                DoBackgroundWork();
            }
            catch (SecurityException)
            {
                MessageBox.Show("Ejecute el programa como administrador");
                Application.Current.Shutdown();
            }
        }

        private void RegisterInStartup(bool isChecked)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked)
            {
                registryKey.SetValue("Danilo", System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                registryKey.DeleteValue("Danilo");
            }
        }

        

        

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LogIn log = new LogIn();
            log.ShowDialog();

            bool? result = log.DialogResult;

            if (result == true)
            {
                ConfigWindow config = new ConfigWindow();
                config.ShowDialog();

                FancyBalloon balloon = new FancyBalloon();
                balloon.BalloonText = "Preferencias";
                balloon.TxtInfo.Text = "Los cambios se guardaron satisfactoriamente";
                //show balloon and close it after 4 seconds
                MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
            }
        }

        /// <summary>
        /// Creates a BackgroundWorker class to do work
        /// on a background thread.
        /// </summary>
        private void DoBackgroundWork()
        {
            BackgroundWorker worker = new BackgroundWorker();

            // Tell the worker to report progress.
            worker.WorkerReportsProgress = true;

            worker.ProgressChanged += ProgressChanged;
            worker.DoWork += DoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// The work for the BackgroundWorker to perform.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        void DoWork(object sender, DoWorkEventArgs e)
        {
            BackUpApi.BackUpProcessVerify();
        }

        /// <summary>
        /// Occurs when the BackgroundWorker reports a progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //pbLoad.Value = e.ProgressPercentage;
        }
        
        /// <summary>
        /// Occurs when the BackgroundWorker has completed its work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FancyBalloon balloon = new FancyBalloon();
            balloon.BalloonText = "Preferencias";
            balloon.TxtInfo.Text = "Proceso completado satisfactoriamente";
            //show balloon and close it after 4 seconds
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
            //_backgroundButton.IsEnabled = true;
            //pbLoad.Visibility = Visibility.Collapsed;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            //About about = new About();
            //about.ShowDialog();
        }

        
    }
}