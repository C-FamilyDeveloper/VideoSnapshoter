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
    internal class OpenFileDialogService : IFileDialogService
    {
        private FileDialog fileDialog;
        public OpenFileDialogService (string filtername, string  extensions) 
        {
            if (extensions.Split(" ").Any())
            {
                extensions = string.Join(";",extensions.Split(" "));
            }
            fileDialog = new OpenFileDialog
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
