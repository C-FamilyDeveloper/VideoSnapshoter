using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VideoSnapshoter.Models.Extensions
{
    public static class BitmapExtensions
    {
        public static BitmapSource Resize(this BitmapSource source, int newHeight, int newWidth)
        {
            var scaleTransform = new ScaleTransform(newWidth / (double)source.Width, newHeight / (double)source.Height);
            return new TransformedBitmap(source, scaleTransform);
        }
        public static void Save(this BitmapSource source, string path)
        {
            using var fileStream = new FileStream(path, FileMode.Create);
            PngBitmapEncoder encoder = new()
            {
                Interlace = PngInterlaceOption.On
            };
            encoder.Frames.Add(BitmapFrame.Create(source));
            encoder.Save(fileStream);
        }
    }
}
