using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DanilosBackUp.Utils;

namespace DanilosBackUp.FileSystemApi
{
    public sealed class ServiceFacade
    {
        private static ServiceFacade instance;
        public static ServiceFacade Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceFacade();
                    instance.Initialize();
                }
                return instance;
            }
        }
        public ObservableCollection<Drive> Drives
        {
            get;
            set;
        }

        public ObservableCollection<NetworkPcs> Nets
        {
            get;
            set;
        }

        private void Initialize()
        {
            this.Drives = new ObservableCollection<Drive>();
            foreach (DriveInfo driveInfo in System.IO.DriveInfo.GetDrives())
            {
                this.Drives.Add(new Drive(driveInfo.Name, driveInfo.IsReady));
            }

            this.Nets = new ObservableCollection<NetworkPcs>();

            NetworkBrowser nb = new NetworkBrowser();
            foreach (string pc in nb.GetNetworkComputers())
            {
                this.Nets.Add(new NetworkPcs(pc));
            }
        }

        public void LoadChildren(Drive d)
        {
            foreach (string directory in System.IO.Directory.GetDirectories(d.Name))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                d.Children.Add(new Directory(directory, directoryInfo.Name));
            }
            /*foreach (string file in System.IO.Directory.GetFiles(d.Name))
            {
                FileInfo fileInfo = new FileInfo(file);
                d.Children.Add(new File(file, fileInfo.Name));
            }*/
        }

        public void LoadChildren(Directory d)
        {
            foreach (string directory in System.IO.Directory.GetDirectories(d.FullPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                d.Children.Add(new Directory(directory, directoryInfo.Name));
            }
            /*foreach (string file in System.IO.Directory.GetFiles(d.FullPath))
            {
                FileInfo fileInfo = new FileInfo(file);
                d.Children.Add(new File(file, fileInfo.Name));
            }*/
        }
    }
}
