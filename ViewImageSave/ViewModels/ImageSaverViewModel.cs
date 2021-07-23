using InpadPlugins.ViewImageSave.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InpadPlugins.ViewImageSave.ViewModels
{
    public class ImageSaverViewModel : INotifyPropertyChanged
    {
        private string path;

        public string Path
        {
            get => path;
            set 
            {
                path = value; 
                OnPropertyChanged();
            }
        }
        public ICommand OpenFileDialogCommand { get; }
        public ICommand CloseCommand { get; }

        public ImageSaverViewModel()
        {
            OpenFileDialogCommand = new OpenFileDialogCommand(this);
            CloseCommand = new CloseCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
