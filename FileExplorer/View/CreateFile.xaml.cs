using System.Windows;
using FileExplorer.ViewModel;

namespace FileExplorer.View
{

    public partial class CreateFile : Window
    {
        public CreateFile(string path)
        {
            InitializeComponent();
            DataContext = new CreateFileViewModel(path, this);
        }
    }
}
