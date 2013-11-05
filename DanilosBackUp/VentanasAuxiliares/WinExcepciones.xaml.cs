using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DanilosBackUp.Utils;

namespace DanilosBackUp.VentanasAuxiliares
{
    /// <summary>
    /// Interaction logic for WinExcepciones.xaml
    /// </summary>
    public partial class WinExcepciones
    {
        private int listSelectedIndex = -1;

        public WinExcepciones()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LstPathException.ItemsSource = AppSettings.GetPropertiesList("ExecPaths");

            if (LstPathException.Items.Count == 0)
                RbtnQuitarTodos.IsEnabled = false;
        }

        private void RbtnExaminar_Click(object sender, RoutedEventArgs e)
        {
            ExplorerTreeWindow explorer = new ExplorerTreeWindow();
            explorer.ShowDialog();

            if (explorer.DialogResult.HasValue && explorer.DialogResult.Value)
            {
                TxtPath.Text = ExplorerControl.PublicAccessInfo.FolderPath;
            }
        }

        private void RbtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.Directory.Exists(TxtPath.Text))
            {
                String pathExcep = TxtPath.Text + '|';

                foreach (String item in LstPathException.Items)
                    pathExcep += item + '|';

                AppSettings.UpdateSettingValue("ExecPaths", pathExcep);
                TxtPath.Text = "";

                LstPathException.ItemsSource = AppSettings.GetPropertiesList("ExecPaths");
            }
            else
            {
                MessageBox.Show("No se puede encontrar \"" + TxtPath.Text + "\". Compruebe que la dirección este bien escrita e intentelo de nuevo", "Barra de direcciones", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LstPathException_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listSelectedIndex = LstPathException.SelectedIndex;
            RbtnQuitar.IsEnabled = true;
            RbtnQuitarTodos.IsEnabled = true;
        }

        private void RbtnQuitar_Click(object sender, RoutedEventArgs e)
        {
            List<String> carpetas = AppSettings.GetPropertiesList("ExecPaths");
            carpetas.RemoveAt(listSelectedIndex);

            String pathExcep = "";
            
            foreach (String item in carpetas)
                if (!string.IsNullOrEmpty(item) || !string.IsNullOrWhiteSpace(item))
                    pathExcep += item + '|';

            if (pathExcep.Length > 0)
                pathExcep = pathExcep.Substring(0, pathExcep.Length - 1);

            AppSettings.UpdateSettingValue("ExecPaths", pathExcep);

            LstPathException.ItemsSource = carpetas;
        }

        private void RbtnQuitarTodos_Click(object sender, RoutedEventArgs e)
        {
            LstPathException.ItemsSource = new List<String>();
            AppSettings.UpdateSettingValue("ExecPaths", "");
        }

        private void RbtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPath.Text.Length > 5)
                RbtnAgregar.IsEnabled = true;
            else
                RbtnAgregar.IsEnabled = false;
            
        }
    }
}