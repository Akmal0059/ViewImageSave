using InpadPlugins.ViewImageSave.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InpadPlugins.ViewImageSave.Commands
{
    public class CloseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ImageSaverViewModel viewModel;

        public CloseCommand(ImageSaverViewModel vm) => viewModel = vm;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (string.IsNullOrEmpty(viewModel.Path))
            {
                System.Windows.Forms.MessageBox.Show("Выберите путь сохранения!", "Ошибка");
                return;
            }
            Window win = parameter as Window;
            win.DialogResult = true;
            win.Close();
        }
    }
}
