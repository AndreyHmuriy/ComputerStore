using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.Model
{
    class ImageConverter
    {
        public byte[] ConvertImageToByte(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        public Image ConverByteToImage(byte[] image)
        {
            using (var ms = new MemoryStream(image))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
