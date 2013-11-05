using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using DanilosBackUp.Utils;

namespace DanilosBackUp.VentanasAuxiliares
{
    /// <summary>
    /// Interaction logic for WinBackUpFolders.xaml
    /// </summary>
    public partial class WinBackUpFolders
    {
        public WinBackUpFolders()
        {
            InitializeComponent();
        }

        private void WinBackFolder_Loaded(object sender, RoutedEventArgs e)
        {
            RbtnQuitar.IsEnabled = (LstCarpetasRespaldo.Items.Count > 0) ? true : false;
            RbtnQuitarTodos.IsEnabled = (LstCarpetasRespaldo.Items.Count > 0) ? true : false;

            List<String> carpetasRespaldo = AppSettings.GetPropertiesList("FolderOrigen");

            TxtDestino.Text = AppSettings.GetPropertyValue("FolderDestino");

            foreach (String path in carpetasRespaldo)
                LstCarpetasRespaldo.Items.Add(path);
        }

        private void RbtnSource_Click(object sender, RoutedEventArgs e)
        {
            ExplorerTreeWindow selDialog = new ExplorerTreeWindow();
            selDialog.ShowDialog();

            if (selDialog.DialogResult.HasValue && selDialog.DialogResult.Value)
            {
                TxtOrigen.Text = ExplorerControl.PublicAccessInfo.FolderPath;
            }
        }

        private void RbtnDestiny_Click(object sender, RoutedEventArgs e)
        {
            ExplorerTreeWindow selDialog = new ExplorerTreeWindow();
            selDialog.ShowDialog();

            if (selDialog.DialogResult.HasValue && selDialog.DialogResult.Value)
            {
                TxtDestino.Text = ExplorerControl.PublicAccessInfo.FolderPath;
            }
        }

        private void RbtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(TxtOrigen.Text))
            {
                LstCarpetasRespaldo.Items.Add(TxtOrigen.Text);
                RbtnQuitar.IsEnabled = (LstCarpetasRespaldo.Items.Count > 0) ? true : false;
                RbtnQuitarTodos.IsEnabled = (LstCarpetasRespaldo.Items.Count > 0) ? true : false;

                TxtOrigen.Text = "";
            }
            else
            {
                MessageBox.Show("La ruta que ingreso no existe, favor de verificar", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RbtnQuitar_Click(object sender, RoutedEventArgs e)
        {
            if (LstCarpetasRespaldo.SelectedIndex != -1)
            {
                LstCarpetasRespaldo.Items.RemoveAt(LstCarpetasRespaldo.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Seleccione al menos un elemento para quitar de la lista", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RbtnQuitarTodos_Click(object sender, RoutedEventArgs e)
        {
            LstCarpetasRespaldo.Items.Clear();
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(TxtDestino.Text))
            {
                MessageBox.Show("La ruta de destino que ingreso no existe, favor de verificar", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            String folderPaths = "";
            
            foreach (String item in LstCarpetasRespaldo.Items)
                folderPaths += item + '|';

            AppSettings.UpdateSettingValue("FolderOrigen", folderPaths);
            AppSettings.UpdateSettingValue("FolderDestino", TxtDestino.Text);
            AppSettings.UpdateSettingValue("LeyendaRecursos", "Los respaldos se guardarán en la siguiente dirección: \n");

            DialogResult = true;
            this.Close();
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void LstCarpetasRespaldo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RbtnQuitar.IsEnabled = (LstCarpetasRespaldo.Items.Count > 0) ? true : false;
            RbtnQuitarTodos.IsEnabled = (LstCarpetasRespaldo.Items.Count > 0) ? true : false;
        }
    }
}