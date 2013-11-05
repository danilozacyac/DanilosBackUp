using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.Win32;

namespace DanilosBackUp.Utils
{
    public class StartUpCheck
    {


        /// <summary>
        /// Verifica si la clave de la aplicacion ya existe en el registro
        /// </summary>
        /// <returns></returns>
        public static bool IsInstalationComplete()
        {
            return Convert.ToBoolean(AppSettings.GetPropertyValue("IsInstComplete"));
        }


        /// <summary>
        /// Registra la aplicación para que inicie junto con Windows
        /// </summary>
        public void RegisterInStartup()
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            registryKey.SetValue("BackUp", System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public void ReviewLastProcessStatus()
        
        {

            bool isLastProcessComplete = Convert.ToBoolean(AppSettings.GetPropertyValue("IsBackUpComplete"));

            if (isLastProcessComplete == false && String.IsNullOrEmpty(AppSettings.GetPropertyValue("FolderDestino")) == false)
            {
                DoBackgroundWork();
            }
        }

        /// <summary>
        /// Creates a BackgroundWorker class to do work
        /// on a background thread.
        /// </summary>
        private void DoBackgroundWork()
        {
            BackgroundWorker worker = new BackgroundWorker();

            // Tell the worker to report progress.
            worker.WorkerReportsProgress = true;

            worker.ProgressChanged += ProgressChanged;
            worker.DoWork += DoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// The work for the BackgroundWorker to perform.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        void DoWork(object sender, DoWorkEventArgs e)
        {
            new BackUpApi().BackUp();
        }

        /// <summary>
        /// Occurs when the BackgroundWorker reports a progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //pbLoad.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Occurs when the BackgroundWorker has completed its work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            //_backgroundButton.IsEnabled = true;
            //pbLoad.Visibility = Visibility.Collapsed;
        }
    }
}
