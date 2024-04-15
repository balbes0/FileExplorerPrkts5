using FileExplorer.ViewModel.Helpers;
using System.Windows;
using System.IO;    

namespace FileExplorer.ViewModel
{
    internal class CreateFileViewModel : BindingHelper
    {
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

        private readonly Window _window;
        string Path;
        public BindableCommand CreateFileCommand { get; set; }
        public BindableCommand CloseWindow { get; set; }
        public BindableCommand CreateFolderCommand { get; set; }
        public CreateFileViewModel(string path, Window window)
        {
            _window = window;
            Path = path;
            CreateFileCommand = new BindableCommand(_ => CreateNewFile());
            CloseWindow = new BindableCommand(_ => CloseWindowx());
            CreateFolderCommand = new BindableCommand(_ => CreateFolderx());
        }

        private void CreateFolderx()
        {
            try
            {
                Directory.CreateDirectory(Path + newfilename);
                _window.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateNewFile()
        {
            try
            {
                File.Create(Path+newfilename).Close();
                _window.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseWindowx()
        {
            _window.Close();
        }
    }
}
