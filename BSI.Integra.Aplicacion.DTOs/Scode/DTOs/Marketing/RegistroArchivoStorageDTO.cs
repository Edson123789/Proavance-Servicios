using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegistroArchivoStorageDTO
    {
        public int Id { get; set; }
        public string Contenedor { get; set; }
        public string NombreArchivo {get;set;}
        public string Ruta { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdUrlBlockStorage { get; set; }
    }

    public class RegistroArchivoMostrarFiltroDTO
    {
        public int IdPersonal { get; set; }
        public int IdUrlBlockStorage{ get; set; }
        public string NombreArchivo{ get; set; }
    }
}
