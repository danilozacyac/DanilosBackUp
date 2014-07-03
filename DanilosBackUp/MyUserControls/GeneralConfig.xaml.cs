using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using DanilosBackUp.Utils;
using DanilosBackUp.VentanasAuxiliares;

namespace DanilosBackUp.MyUserControls
{
    /// <summary>
    /// Interaction logic for GeneralConfig.xaml
    /// </summary>
    public partial class GeneralConfig : UserControl
    {
        public GeneralConfig()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            String destino = AppSettings.GetPropertyValue("FolderDestino");

            TxtSourceDestiny.DataContext = AppSettings.GetPropertyValue("LeyendaRecursos") + ((String.IsNullOrEmpty(destino) ) ? "" : destino);
            TxtDiasRespaldo.DataContext = AppSettings.GetPropertyValue("LeyendaPeriodo");
            TxtHorario.DataContext = AppSettings.GetPropertyValue("LeyendaHorario");
            ChkZip.IsChecked = Convert.ToBoolean(AppSettings.GetPropertyValue("ComprimirRespaldo"));
        }

        private void RbtnCambiarUbicacion_Click(object sender, RoutedEventArgs e)
        {
            WinBackUpFolders config = new WinBackUpFolders();
            config.ShowDialog();

            if (config.DialogResult == true)
            {
                TxtSourceDestiny.DataContext = AppSettings.GetPropertyValue("LeyendaRecursos") + AppSettings.GetPropertyValue("FolderDestino");
            }

        }

        private void RbtnDateTimeConfig_Click(object sender, RoutedEventArgs e)
        {
            var blur = new BlurEffect();
            var current = this.Background;
            blur.Radius = 5;
            this.Background = new SolidColorBrush(Colors.DarkGray);
            this.Effect = blur;
            
            

            WinBackUpTimeSetting time = new WinBackUpTimeSetting();
            time.ShowDialog();

            
            TxtDiasRespaldo.DataContext = AppSettings.GetPropertyValue("LeyendaPeriodo");
            TxtHorario.DataContext = AppSettings.GetPropertyValue("LeyendaHorario");

            this.Effect = null;
            this.Background = current;
        }

        private void ChkZip_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
