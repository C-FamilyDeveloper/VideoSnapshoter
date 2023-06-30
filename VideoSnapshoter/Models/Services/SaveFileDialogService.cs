using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoSnapshoter.Models.Exceptions;
using VideoSnapshoter.Models.Interfaces;

namespace VideoSnapshoter.Models.Services
{
    public class SaveFileDialogService : IFileDialogService
    {
        private FileDialog fileDialog;
        public SaveFileDialogService(string filtername, string extensions)
        {
            if (extensions.Split(" ").Any())
            {
                extensions = string.Join(";", extensions.Split(" "));
            }
            fileDialog = new SaveFileDialog
            {
                Filter = filtername + @"|" + extensions
            };
        }
        public string GetFileName()
        {
            fileDialog.ShowDialog();
            if (string.IsNullOrWhiteSpace(fileDialog.FileName))
            {
                throw new FileNotChooseException();
            }
            return fileDialog.FileName;
        }
    }
}
