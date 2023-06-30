using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoSnapshoter.Models.Exceptions
{
    public class FileNotChooseException : Exception
    {
        public override string Message => "Файл не выбран";
    }
}
