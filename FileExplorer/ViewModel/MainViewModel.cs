using FileExplorer.Model;
using FileExplorer.View;
using FileExplorer.ViewModel.Helpers;
using Microsoft.VisualBasic.FileIO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;


namespace FileExplorer.ViewModel
{
    internal class MainViewModel : BindingHelper
    {
        private List<string> directoryHistory = new List<string>();
        #region
        private FileItem selected;
        public FileItem Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<FileItem> selectedFiles;
        public ObservableCollection<FileItem> SelectedFiles
        {
            get { return selectedFiles; }
            set
            {
                selectedFiles = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ObservableCollection<FileItem> Files { get; set; }
        public BindableCommand OpenDirectoryCommand { get; set; }
        public BindableCommand NavigateBackCommand { get; set; }
        public BindableCommand DeleteCommand { get; set; }
        public BindableCommand RenameFileCommand { get; set; }
        public BindableCommand CreateFileCommand {  get; set; }

        public MainViewModel()
        {
            OpenDirectoryCommand = new BindableCommand(_ => LoadDirectoryContents());
            NavigateBackCommand = new BindableCommand(_ => NavigateBack());
            DeleteCommand = new BindableCommand(_ => DeleteFile());
            RenameFileCommand = new BindableCommand(_ => RenameFile());
            CreateFileCommand = new BindableCommand(_ => CreateFilex());

            Files = new ObservableCollection<FileItem>();

            LoadDriveInfo();
        }

        public void CreateFilex()
        {
            if (directoryHistory.Count > 0)
            {
                CreateFile createFile = new CreateFile(directoryHistory[^1].ToString());
                createFile.Show();
            }
        }

        public void RenameFile()
        {
            if (Selected != null)
            {
                FileNameInputDialog inputDialogViewModel = new FileNameInputDialog(Selected.Path.ToString());
                inputDialogViewModel.Show();
            }
        }

        private void LoadDriveInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                Files.Add(new FileItem(drive.Name));
            }
        }

        public void DeleteFile()
        {
            if (Selected != null && File.Exists(Selected.Path))
            {
                try
                {
                    File.Delete(Selected.Path);
                    Files.Remove(Selected);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении файла: {ex.Message}");
                }
            }
        }

        public void NavigateBack()
        {
            if (directoryHistory.Count > 1) //тут воняет сильно
            {
                directoryHistory.RemoveAt(directoryHistory.Count-1);
                string previousDirectory = directoryHistory[^1];
                Selected = new FileItem(previousDirectory);
                LoadDirectoryContents();
            }
            else if (directoryHistory.Count <= 1)
            {
                directoryHistory.Clear();
                Files.Clear();
                LoadDriveInfo();
            }
        }

        public void LoadDirectoryContents()
        {
            if (Selected != null)
            {
                string directory = Selected.Path;

                if (File.Exists(directory))
                {
                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = directory,
                            UseShellExecute = true
                        };

                        Process.Start(startInfo);
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при открытии файла");
                    }
                }
                else if (Directory.Exists(directory))
                {
                    Files.Clear();

                    try
                    {
                        string[] directories = Directory.GetDirectories(directory);
                        foreach (string dir in directories)
                        {
                            Files.Add(new FileItem(dir));
                        }

                        string[] files = Directory.GetFiles(directory);
                        foreach (string file in files)
                        {
                            Files.Add(new FileItem(file));
                        }

                        directoryHistory.Add(directory);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка загрузки каталога: " + ex.Message);
                    }
                }
            }
        }
    }
}
