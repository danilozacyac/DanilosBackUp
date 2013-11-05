using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DanilosBackUp.Utils;

namespace DanilosBackUp
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }


        private void RbtnPredeterminados_Click(object sender, RoutedEventArgs e)
        {
            LogIn resetConfig = new LogIn();
            resetConfig.ShowDialog();

            if (resetConfig.DialogResult == true)
            {

                AppSettings.UpdateSettingValue("Extension", "");
                AppSettings.UpdateSettingValue("ExecPaths", "");
                AppSettings.UpdateSettingValue("FolderOrigen", "");
                AppSettings.UpdateSettingValue("FolderDestino", "");
                AppSettings.UpdateSettingValue("ComprimirRespaldo", "false");
                AppSettings.UpdateSettingValue("PeriodoRespaldo", "1");
                AppSettings.UpdateSettingValue("LeyendaRecursos", "");
                AppSettings.UpdateSettingValue("LeyendaPeriodo", "");
                AppSettings.UpdateSettingValue("LeyendaHorario", "");
                AppSettings.UpdateSettingValue("SemanaNumericos", "");
                AppSettings.UpdateSettingValue("DiaRespaldoMensual", "");

            }
            else
            {

            }
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            List<String> extensiones = new List<string>();

            foreach (CheckBox check in ExtensionDiscard.Music)
                if (check.IsChecked == false)
                    extensiones.Add(check.Tag.ToString());

            foreach (CheckBox check in ExtensionDiscard.Images)
                if (check.IsChecked == false)
                    extensiones.Add(check.Tag.ToString());

            foreach (CheckBox check in ExtensionDiscard.Videos)
                if (check.IsChecked == false)
                    extensiones.Add(check.Tag.ToString());
            
            String discardExt = "";

            if (extensiones.Count > 0)
                foreach (String item in extensiones)
                    discardExt += item + '|';

            AppSettings.UpdateSettingValue("Extension", discardExt);

            AppSettings.UpdateSettingValue("ComprimirRespaldo", PathTimeConfig.ChkZip.IsChecked.ToString());

            DialogResult = true;

            this.Close();


        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
