using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using DanilosBackUp.Adicionales;
using Microsoft.Win32;

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
            RegisterInStartup(true);
            this.Hide();
            
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

        private void radButton1_Click(object sender, RoutedEventArgs e)
        {
            //string filePath = @"C:\verificar.mdb";
            //FileInfo file = new FileInfo(filePath);
            ConfigWindow conf = new ConfigWindow();
            conf.ShowDialog();
        }

        private void ImgConfig_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ConfigWindow config = new ConfigWindow();
            config.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
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
}