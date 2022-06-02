using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class RegistroArchivoStorageBO : BaseBO
    {
        private readonly RegistroArchivoStorageRepositorio _repRegistro;

        public int IdUrlSubContenedor { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }

        public RegistroArchivoStorageBO()
        {
        }

        public RegistroArchivoStorageBO(integraDBContext context)
        {
            _repRegistro = new RegistroArchivoStorageRepositorio(context);
        }

    }
}
