using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VideoSnapshoter.Models.Services
{
    public static class BitmapResizerService
    {
        public static BitmapSource Resize(BitmapSource source, int newHeight, int newWidth)
        {
            var scaleTransform = new ScaleTransform(newWidth / (double)source.Width, newHeight / (double)source.Height);
            return new TransformedBitmap(source, scaleTransform);
        }
    }
}
