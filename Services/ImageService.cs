using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ImageService
    {
        public static MemoryStream decodeImage(byte[] image64)
        {
            return new MemoryStream(image64);
        }
    }
}
