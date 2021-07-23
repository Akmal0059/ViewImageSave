using InpadPlugins.ViewImageSave.ViewModels;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace InpadPlugins.ViewImageSave.Commands
{
    public class OpenFileDialogCommand : ICommand
    {
        private ImageSaverViewModel viewModel;
        public event EventHandler CanExecuteChanged;

        public OpenFileDialogCommand(ImageSaverViewModel vm) => viewModel = vm;


        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            using (OpenFileDialog folderBrowserDialog = new OpenFileDialog())
            {
                folderBrowserDialog.ValidateNames = false;
                folderBrowserDialog.CheckFileExists = false;
                folderBrowserDialog.CheckPathExists = true;
                folderBrowserDialog.FileName = "Выберите папку";

                if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    return;

                viewModel.Path = Path.GetDirectoryName(folderBrowserDialog.FileName);
            }
        }
    }
}
