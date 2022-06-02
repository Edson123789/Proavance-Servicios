using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CuentaGeneral { get; set; }
        public string CuentaGeneralCodigo { get; set; }
        public string CuentaEspecifica { get; set; }
        public string CuentaEspecificaCodigo { get; set; }
        public int IdProductoPresentacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
