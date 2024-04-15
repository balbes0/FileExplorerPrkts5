using FileExplorer.ViewModel.Helpers;
using FileExplorer.Model;
using System.Windows;
using System.IO;
using System.Windows.Shapes;

namespace FileExplorer.ViewModel
{
    internal class FileNameInputDialogViewModel : BindingHelper
    {
        string Path;
        private readonly Window _window;

        #region
        private string newfilename;
        public string NewFileName
        {
            get { return newfilename; }
            set
            {
                newfilename = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public BindableCommand UpdateCommand { get; set; }
        public BindableCommand CloseCommand { get; set; }

        public FileNameInputDialogViewModel(string path, Window window)
        {
            NewFileName = path;
            _window = window;
            Path = path;
            UpdateCommand = new BindableCommand(_ => UpdateFileName());
            CloseCommand = new BindableCommand(_ => CloseWindow());
        }

        public void UpdateFileName()
        {
            try
            {
                if (File.Exists(Path))
                {
                    File.Move(Path, newfilename);
                    MessageBox.Show("Файл успешно переименован.");
                    _window.Close();
                }
                else if (Directory.Exists(Path))
                {
                    Directory.Move(Path, newfilename);
                    MessageBox.Show("Папка успешно переименована.");
                    _window.Close();
                }
                else
                {
                    MessageBox.Show("Файл или папка не существует.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при переименовании: {ex.Message}");
            }
        }

        private void CloseWindow()
        {
            _window.Close();
        }
    }
}
