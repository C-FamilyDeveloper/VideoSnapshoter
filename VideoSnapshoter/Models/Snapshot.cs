using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VideoSnapshoter.Core;

namespace VideoSnapshoter.Models
{
    public class Snapshot 
    {
        public string DateTimeString { get; set; }
        public BitmapSource Image { get; set; }
        public BitmapSource SnapshotOriginal { get; set; }
    }
}
