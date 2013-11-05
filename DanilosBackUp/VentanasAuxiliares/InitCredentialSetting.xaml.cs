using System;
using System.Linq;
using System.Windows;
using DanilosBackUp.Utils;
using Telerik.Windows.Controls;

namespace DanilosBackUp.VentanasAuxiliares
{
    /// <summary>
    /// Interaction logic for InitCredentialSetting.xaml
    /// </summary>
    public partial class InitCredentialSetting
    {
        public InitCredentialSetting()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (TxtUsuario.Text.Trim().Length == 0 || TxtPass.Password.Trim().Length == 0 || TxtConfirma.Password.Trim().Length == 0)
            {
                MessageBox.Show("Debes completar todos los campos", "Atención:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!TxtPass.Password.Equals(TxtConfirma.Password))
            {
                MessageBox.Show("El campo contraseña y confirma contraseña deben de ser iguales", "Atención:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            String criptoUser = Cripto.Encriptar(TxtUsuario.Text);
            String criptoPass = Cripto.Encriptar(TxtPass.Password);

            AppSettings.UpdateSettingValue("Usuario", criptoUser);

            AppSettings.UpdateSettingValue("Contra", criptoPass);

            AppSettings.UpdateSettingValue("IsInstComplete", "true");

            new StartUpCheck().RegisterInStartup();

            DialogResult = true;
            this.Close();

        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void RadWindow_Closed_1(object sender, WindowClosedEventArgs e)
        {
            
        }
    }
}
