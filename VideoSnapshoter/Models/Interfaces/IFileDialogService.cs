using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoSnapshoter.Models.Interfaces
{
    public interface IFileDialogService
    {
        public string GetFileName();
    }
}
