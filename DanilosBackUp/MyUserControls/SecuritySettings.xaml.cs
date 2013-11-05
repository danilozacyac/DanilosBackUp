using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DanilosBackUp.Utils;

namespace DanilosBackUp.MyUserControls
{
    /// <summary>
    /// Interaction logic for SecuritySettings.xaml
    /// </summary>
    public partial class SecuritySettings : UserControl
    {
        private String currentUser = "";
        private String currentPass = "";

        public SecuritySettings()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.currentUser = AppSettings.GetPropertyValue("Usuario");
            this.currentPass = AppSettings.GetPropertyValue("Contra");
        }

        private void RbtnGuardarUsuario_Click(object sender, RoutedEventArgs e)
        {
            String tempUser = Cripto.Desencriptar(currentUser);

            if (!currentUser.Equals(TxtUsuarioAnterior.Text))
            {
                MessageBox.Show("El nombre del usuario anterior es incorrecto, verifique");
                return;
            }

            if (String.IsNullOrWhiteSpace(TxtNuevoUsuario.Text) || String.IsNullOrEmpty(TxtNuevoUsuario.Text))
            {
                MessageBox.Show("Ingrese el nuevo nombre de usuario");
                return;
            }

            tempUser = Cripto.Encriptar(TxtNuevoUsuario.Text);

            AppSettings.UpdateSettingValue("Usuario", tempUser);

            tempUser = null;

            MessageBox.Show("Usuario modificado satisfactoriamente");

            TxtNuevoUsuario.Text = String.Empty;
            TxtUsuarioAnterior.Text = String.Empty;
        }

        private void RbtnGuardarPass_Click(object sender, RoutedEventArgs e)
        {
            String tempPass = Cripto.Desencriptar(currentPass);

            if (!currentPass.Equals(PassAnterior.Password))
            {
                MessageBox.Show("La contraseña anterior es incorrecta, verifique");
                return;
            }

            if (String.IsNullOrWhiteSpace(PassNueva.Password) || String.IsNullOrEmpty(PassNueva.Password) 
                                            || String.IsNullOrWhiteSpace(PassConfirma.Password) || String.IsNullOrEmpty(PassConfirma.Password))
            {
                MessageBox.Show("Los campos de contraseña y confirmar contraseña no pueden estar en blanco");
                return;
            }

            if (!PassNueva.Password.Equals(PassConfirma.Password))
            {
                MessageBox.Show("Los campos de contraseña y confirmar contraseña no coinciden");
                return;
            }

            tempPass = Cripto.Encriptar(PassNueva.Password);

            AppSettings.UpdateSettingValue("Contra", tempPass);

            tempPass = null;

            MessageBox.Show("Contraseña modificada satisfactoriamente");

            PassAnterior.Password = String.Empty;
            PassNueva.Password = String.Empty;
            PassConfirma.Password = String.Empty;
        }
    }
}
