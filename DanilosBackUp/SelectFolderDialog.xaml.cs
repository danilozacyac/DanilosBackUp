using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DanilosBackUp.Utils;

namespace DanilosBackUp
{
    /// <summary>
    /// Interaction logic for SelectFolderDialog.xaml
    /// </summary>
    public partial class SelectFolderDialog
    {
        public SelectFolderDialog()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SelectFolder.TxtPath.TextChanged += new TextChangedEventHandler(TextChanged);
        }
  
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(((TextBox)sender).Text) || !string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                NoPersistentVariables.SelectedFolderPath = SelectFolder.TxtPath.Text;
                RbtnAceptar.IsEnabled = true;
            }
            else
            {
                RbtnAceptar.IsEnabled = false;
                NoPersistentVariables.SelectedFolderPath = String.Empty;
            }
            
            
        }

        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //NoPersistentVariables.SelectedFolderPath = SelectFolder.TxtPath.Text;
            DialogResult = true;
            this.Close();
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}