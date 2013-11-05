using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DanilosBackUp.Utils;
using DanilosBackUp.VentanasAuxiliares;

namespace DanilosBackUp.MyUserControls
{
    /// <summary>
    /// Interaction logic for MusicExtensionDiscard.xaml
    /// </summary>
    public partial class MusicExtensionDiscard : UserControl
    {
        public List<CheckBox> Music = null;
        public List<CheckBox> Images = null;
        public List<CheckBox> Videos = null;


        public MusicExtensionDiscard()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Music = new List<CheckBox>() { audiocd, cda, cdda, mid, midi, mp3, ogg, ogm, wav, wma };
            Videos = new List<CheckBox>() { avi, div, divx, dvd, mov, movie, mp4, mpa, mpeg, wmv };
            Images = new List<CheckBox>() { bmp, jpe, jpeg, jpg, png, pngt, gif, psd, tif, tiff };


            List<String> extensiones = AppSettings.GetPropertiesList("Extension");

            foreach (CheckBox check in Music)
                if (extensiones.Contains(check.Tag))// check.IsChecked == false)
                    check.IsChecked = false;

            foreach (CheckBox check in Images)
                if (extensiones.Contains(check.Tag))// check.IsChecked == false)
                    check.IsChecked = false;

            foreach (CheckBox check in Videos)
                if (extensiones.Contains(check.Tag))// check.IsChecked == false)
                    check.IsChecked = false;

        }

        private void RbtnExcepciones_Click(object sender, RoutedEventArgs e)
        {
            WinExcepciones exep = new WinExcepciones();
            exep.ShowDialog();
        }
    }
}
