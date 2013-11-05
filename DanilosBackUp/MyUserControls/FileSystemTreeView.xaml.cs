using System;
using System.Linq;
using System.Windows.Controls;
using DanilosBackUp.FileSystemApi;
using Telerik.Windows.Controls;

namespace DanilosBackUp.MyUserControls
{
    /// <summary>
    /// Interaction logic for FileSystemTreeView.xaml
    /// </summary>
    public partial class FileSystemTreeView : UserControl
    {
        private String newFolderName = "";

        public FileSystemTreeView()
        {
            InitializeComponent();
        }

        private void radTreeView_LoadOnDemand(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            e.Handled = true;
            RadTreeViewItem expandedItem = e.OriginalSource as RadTreeViewItem;
            if (expandedItem == null)
                return;

            Drive drive = expandedItem.Item as Drive;
            if (drive != null)
            {
                ServiceFacade.Instance.LoadChildren(drive);
                return;
            }

            Directory directory = expandedItem.Item as Directory;
            if (directory != null)
            {
                ServiceFacade.Instance.LoadChildren(directory);
            }
        }

        private void radTreeView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                Directory directorio = radTreeView.SelectedItem as Directory;

                if (directorio != null)
                    TxtPath.Text = directorio.FullPath;
                else
                    TxtPath.Text = null;
                
        }

        //private void RbtnCarpetaNueva_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    RadWindow.Prompt("Enter your name:", this.OnClosed);
        //}
        //private void OnClosed(object sender, WindowClosedEventArgs e)
        //{
        //    newFolderName = ((RadWindow)sender).PromptResult;

        //    if (!string.IsNullOrEmpty(newFolderName) && !string.IsNullOrWhiteSpace(newFolderName))
        //    {
        //        Directory folder = (Directory)radTreeView.SelectedItem;

        //        System.IO.Directory.CreateDirectory(folder.FullPath + @"\" + newFolderName); 
        //    }
            
        //}
    }
}
