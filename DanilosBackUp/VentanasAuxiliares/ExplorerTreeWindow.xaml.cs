using System;
using System.Linq;
using System.Windows;

namespace DanilosBackUp.VentanasAuxiliares
{
    /// <summary>
    /// Interaction logic for ExplorerTreeWindow.xaml
    /// </summary>
    public partial class ExplorerTreeWindow
    {
        public ExplorerTreeWindow()
        {
            InitializeComponent();
        }

        private void TreeViewExplorer_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
            this.Close();
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
