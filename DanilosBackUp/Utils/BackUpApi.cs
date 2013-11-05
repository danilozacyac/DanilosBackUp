using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;

namespace DanilosBackUp.Utils
{
    public class BackUpApi
    {
        /// <summary>
        /// extensiones de archivos multimedia permitidas durante el respaldo
        /// </summary>
        private readonly List<string> extValidas = null;

        /// <summary>
        /// Tipos de archivos que no se copiaran durante el respaldo
        /// </summary>
        private readonly List<string> extNoValidas = null;

        /// <summary>
        /// Lista de las carpetas que se van a respaldar
        /// </summary>
        private readonly List<string> sourcePaths = null;

        /// <summary>
        /// Ruta de la carpeta donde se almacenaran los respaldos
        /// </summary>
        private readonly String destinyPath = "";

        public BackUpApi()
        {
            sourcePaths = AppSettings.GetPropertiesList("FolderOrigen");
            destinyPath = AppSettings.GetPropertyValue("FolderDestino");
            extValidas = AppSettings.GetPropertiesList("Extension");

            extNoValidas = new List<string>() { "audiocd", "cda", "cdda", "mid", "midi", "mp3", "ogg", "ogm", "wav", "wma", "avi", "div", "divx", "dvd", "mov", "movie", "mp4", "mpa", "mpeg", "wmv", "bmp", "jpe", "jpeg", "jpg", "png", "pngt", "gif", "psd", "tif", "tiff" };

            foreach (string extension in extValidas)
                extNoValidas.Remove(extension);
        }

        public void BackUp()
        {
            if (!String.IsNullOrEmpty(destinyPath))
            {

                foreach (string currenSourcetDirectory in sourcePaths.Where(t => !t.Equals(String.Empty)).ToList())
                {
                    try
                    {
                        bool isCurrentDirectoryExistDestiny = Directory.Exists(destinyPath + @"\" + new DirectoryInfo(currenSourcetDirectory).Name);

                        if (isCurrentDirectoryExistDestiny)
                        {
                            this.RespaldoRecursivo(currenSourcetDirectory, destinyPath + @"\" + new DirectoryInfo(currenSourcetDirectory).Name);
                        }
                        else
                        {
                            Directory.CreateDirectory(destinyPath + @"\" + new DirectoryInfo(currenSourcetDirectory).Name);
                            this.RespaldoRecursivo(currenSourcetDirectory, destinyPath + @"\" + new DirectoryInfo(currenSourcetDirectory).Name);
                        }
                    }
                    catch (DirectoryNotFoundException)
                    {
                        MessageBox.Show("No se encontro la ruta \"" + currenSourcetDirectory + "\". Favor de verificar"); 
                    }
                }

                
            }


        }

        private void RespaldoRecursivo(String sourcePath, String destinyPath)
        {
            try
            {
                this.FileBackUp(sourcePath, destinyPath);
                string [] dirList = Directory.GetDirectories(sourcePath);

                foreach(string currenSourcetDirectory in dirList)
                {
                    bool isCurrentDirectoryExistDestiny = Directory.Exists(destinyPath + @"\" + new DirectoryInfo(currenSourcetDirectory).Name);

                    if (isCurrentDirectoryExistDestiny)
                    {
                        this.RespaldoRecursivo(currenSourcetDirectory, destinyPath + @"\" + new DirectoryInfo(currenSourcetDirectory).Name);
                    }
                    else
                    {
                        Directory.CreateDirectory(destinyPath + @"\" + new DirectoryInfo(currenSourcetDirectory).Name);
                        this.RespaldoRecursivo(currenSourcetDirectory, destinyPath + @"\" + new DirectoryInfo(currenSourcetDirectory).Name);
                    }

                }


            }catch(UnauthorizedAccessException){}
        }



        public void ZipBackUp()
        {
            string tempDir = this.GetTemporaryDirectory();
            this.RecursiveDirectoryBackUp(sourcePaths, tempDir);

            Uri folderToArchiveUri = new Uri(tempDir);

            var filesToArchive = Directory.EnumerateFiles(tempDir, "*.*", SearchOption.AllDirectories);
            using (var fs = new FileStream(destinyPath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    filesToArchive
                                  .ToList()
                                  .ForEach(
                        file =>
                        {
                            var relativePath = folderToArchiveUri.MakeRelativeUri(new Uri(Path.GetFullPath(file)));
                            var relativePathText = relativePath.ToString().Replace("%20", " ");
                            archive.CreateEntryFromFile(file, relativePathText);
                        });
                }
                //Genera el archivo ZIP sin respetar la jerarquia de directorios
                //using (var archive = new ZipArchive(fs, ZipArchiveMode.Create))
                //{
                //    filesToArchive
                //        .ToList()
                //        .ForEach(
                //            file =>
                //            {
                //                var fileName = Path.GetFileName(file);
                //                archive.CreateEntryFromFile(file, fileName);
                //            });
                //}
            }

            this.DeleteTemporaryDirectory(tempDir);
        }

        /// <summary>
        /// Método recursivo que respalda todas las subcarpetas de la carpeta origen
        /// </summary>
        /// <param name="source">Ruta de la carpeta que se va a respaldar</param>
        /// <param name="destiny">Ruta de la ubicación del respaldo</param>
        private void RecursiveDirectoryBackUp(String source, String destiny)
        {
            try
            {
                string[] dirList = Directory.GetDirectories(source);

                foreach (string directory in dirList)
                {
                    if (!Directory.Exists(destiny + @"\" + new DirectoryInfo(directory).Name))
                    {
                        Directory.CreateDirectory(destiny + @"\" + new DirectoryInfo(directory).Name);

                        this.RecursiveDirectoryBackUp(source + @"\" + new DirectoryInfo(directory).Name, destiny + @"\" + new DirectoryInfo(directory).Name);

                        this.FileBackUp(directory, destiny);
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
        }

        /// <summary>
        /// Permite respaldar mas de una carpeta en la misma llamada del método
        /// </summary>
        /// <param name="source">Lista de las carpetas que se van a respaldar</param>
        /// <param name="destiny">Ruta de la ubicación del respaldo</param>
        private void RecursiveDirectoryBackUp(List<String> source, String destiny)
        {
            foreach (string currentDirectory in source)
            {
                if (!Directory.Exists(destiny + @"\" + new DirectoryInfo(currentDirectory).Name))
                {
                    Directory.CreateDirectory(destiny + @"\" + new DirectoryInfo(currentDirectory).Name);
                }


                this.FileBackUp(currentDirectory, destiny + @"\" + new DirectoryInfo(currentDirectory).Name);

                string[] dirList = Directory.GetDirectories(currentDirectory);

                foreach (string directory in dirList)
                {
                    if (!Directory.Exists(destiny + @"\" + new DirectoryInfo(directory).Name))
                    {
                        Directory.CreateDirectory(destiny + @"\" + new DirectoryInfo(directory).Name);

                        this.RecursiveDirectoryBackUp(currentDirectory + @"\" + new DirectoryInfo(directory).Name, destiny + @"\" + new DirectoryInfo(directory).Name);

                        this.FileBackUp(directory, destiny + @"\" + new DirectoryInfo(directory).Name);
                    }
                }
            }
        }

        private void FileBackUp(String directory, String destiny)
        {
            try
            {
                string[] files = Directory.GetFiles(directory);

                foreach (string file in files)
                {
                    try
                    {
                        string ext = Path.GetExtension(file).Replace(".", "");

                        if (!extNoValidas.Contains(ext.ToLower()))
                        {
                            //bool exists = File.Exists(destiny + @"\" + new DirectoryInfo(directory).Name + @"\" + Path.GetFileName(file));
                            bool exists = File.Exists(destiny + @"\" + Path.GetFileName(file));

                            if (exists)
                            {
                                FileInfo sourceFile = new FileInfo(file);
                                FileInfo destinyFile = new FileInfo(destiny +  @"\" + Path.GetFileName(file));

                                if (sourceFile.LastWriteTime != destinyFile.LastWriteTime)
                                {
                                    File.Delete(destiny + @"\"  + Path.GetFileName(file));
                                    File.Copy(file, destiny + @"\" + Path.GetFileName(file));
                                }
                            }
                            else
                            {
                                File.Copy(file, destiny + @"\" + Path.GetFileName(file));
                            }
                        }
                    }
                    catch (IOException) { }
                }
            }
            catch (UnauthorizedAccessException) { }
        }

        /// <summary>
        /// Genera un directorio temporal donde se realiza el respaldo para posteriormente 
        /// comprimir el archivo
        /// </summary>
        /// <returns></returns>
        public string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        /// <summary>
        /// Elimina el directorio temporal creado para el respaldo
        /// </summary>
        /// <param name="path"></param>
        public void DeleteTemporaryDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }

        //private String DateComplete(int entero)
        //{
        //    if (entero < 10)
        //        return "0" + entero;
        //    else
        //        return entero.ToString();
        //}

        public static void BackUpProcessVerify()
        {
            bool isBackUpCompress = Convert.ToBoolean(AppSettings.GetPropertyValue("ComprimirRespaldo"));

            int tipoRespaldo = Convert.ToInt16(AppSettings.GetPropertyValue("PeriodoRespaldo"));

            string[] horaRespaldo = AppSettings.GetPropertyValue("LeyendaHorario").Split(':');

            TimeSpan? tiempoRespaldo = new TimeSpan(Convert.ToInt32(horaRespaldo[0]), Convert.ToInt32(horaRespaldo[1]), 0);

            if (tipoRespaldo == 1) //Diario
            {
                BackUpApi.VerifiedBackUpTimeOk(isBackUpCompress, tiempoRespaldo);
            }
            else if (tipoRespaldo == 2) //Semanal
            {
                List<String> diasSemanaRespaldo = AppSettings.GetPropertyValue("SemanaNumericos").ToString().Split(',').ToList();
                int diaSemana = (int)DateTime.Now.DayOfWeek + 1;

                if (diasSemanaRespaldo.Contains(diaSemana.ToString()))
                {
                    BackUpApi.VerifiedBackUpTimeOk(isBackUpCompress, tiempoRespaldo);
                }
            }
            else if (tipoRespaldo == 3) //Mensual
            {
                DateTime diaRespaldo = DateTime.Parse(AppSettings.GetPropertyValue("DiaRespaldoMensual"));

                if (diaRespaldo.Day == DateTime.Now.Day)
                {
                    BackUpApi.VerifiedBackUpTimeOk(isBackUpCompress, tiempoRespaldo);
                }
            }
        }

        private static void VerifiedBackUpTimeOk(bool isBackUpCompress, TimeSpan? tiempoRespaldo)
        {
            bool cierto = false;

            while (!cierto)
            {
                DateTime current = DateTime.Now;
                TimeSpan systemTime = current.TimeOfDay;

                if (tiempoRespaldo <= systemTime)
                {
                    cierto = true;

                    AppSettings.UpdateSettingValue("IsBackUpComplete", "false");

                    if (isBackUpCompress)
                        new BackUpApi().ZipBackUp();
                    else
                        new BackUpApi().BackUp();

                    AppSettings.UpdateSettingValue("IsBackUpComplete", "true");
                }
            }
        }
    }
}