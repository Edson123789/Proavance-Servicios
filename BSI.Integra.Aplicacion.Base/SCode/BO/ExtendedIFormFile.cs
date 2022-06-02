using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BSI.Integra.Aplicacion.Base.BO
{
    public static class ExtendedIFormFile
    {

        public static byte[] ConvertToByte(this IFormFile file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.OpenReadStream());
            imageByte = rdr.ReadBytes((int)file.Length);
            return imageByte;
        }
    }
}
