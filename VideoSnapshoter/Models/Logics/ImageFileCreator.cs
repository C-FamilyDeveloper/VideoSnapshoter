using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using VideoSnapshoter.Models.Exceptions;
using VideoSnapshoter.Models.Services;

namespace VideoSnapshoter.Models.Logics
{
    public static class ImageFileCreator
    {
        public static void Save(Snapshot snapshot, string path)
        {
            if (snapshot == null)
            {
                throw new FileNotChooseException();
            }
            using var fileStream = new FileStream(path, FileMode.Create);
            PngBitmapEncoder encoder = new()
            {
                Interlace = PngInterlaceOption.On
            };
            encoder.Frames.Add(BitmapFrame.Create(snapshot.SnapshotOriginal));
            encoder.Save(fileStream);           
        }
    }
}
