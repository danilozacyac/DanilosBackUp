using System;
using System.Linq;
using System.Windows;
using DanilosBackUp.Utils;

namespace DanilosBackUp
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn
    {
        private String currentUser = "";
        private String currentPass = "";

        public LogIn()
        {
            InitializeComponent();
        }

        private void Log_Loaded(object sender, RoutedEventArgs e)
        {
            this.currentUser = AppSettings.GetPropertyValue("Usuario");
            this.currentPass = AppSettings.GetPropertyValue("Contra");
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string tempUser = Cripto.Desencriptar(currentUser);
            string tempPass = Cripto.Desencriptar(currentPass);

            if (tempUser.Equals(TxtUsuario.Text) && tempPass.Equals(TxtPassword.Password))
            {
                DialogResult = true;
                this.Close();
            }else
            {
                MessageBox.Show("Verifica tus credenciales de inicio de sesión", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);

                tempUser = String.Empty;
                tempPass = String.Empty;
            }


        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
