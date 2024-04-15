using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileExplorer.ViewModel.Helpers
{
    internal class BindingHelper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
