using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProveedorCalificacionDTO
    {
        public int IdProveedor { get; set; }
        public int[] ListaIdSubCriterioCalificacion { get; set; }
        public int IdPrestacionRegistro { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
