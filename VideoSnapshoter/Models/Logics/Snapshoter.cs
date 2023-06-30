using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VideoSnapshoter.Models.Logics
{
    public static class Snapshoter
    {
        public static BitmapSource MakeSnapshot(FrameworkElement element) 
        {
            System.Drawing.Size dpi = new (96, 96);
            RenderTargetBitmap bmp =
              new((int)element.Width, (int)element.Width,
                dpi.Width, dpi.Height, PixelFormats.Default);
            bmp.Render(element);
            return bmp;
        }       
    }
}
