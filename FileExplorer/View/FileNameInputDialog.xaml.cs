using FileExplorer.ViewModel;
using System.Windows;

namespace FileExplorer.View
{
    public partial class FileNameInputDialog : Window
    {
        public FileNameInputDialog(string path)
        {
            InitializeComponent();
            DataContext = new FileNameInputDialogViewModel(path, this);
        }
    }
}
