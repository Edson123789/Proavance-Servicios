using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class SeccionTipoDetallePwDTO
	{
        public int Id { get; set; } 
        public string Nombre { get; set; }
        public int IdSeccionTipoContenido { get; set; }
        public string NombreSeccionTipoContenido { get; set; }

    }

    public class SeccionTipoDetalleGrillaPwDTO
    {
        public int IdSeccionTipoDetallePw { get; set; }
        public string NombreSubSeccion { get; set; }
        public int IdSubSeccionTipoContenido { get; set; }
        public string NombreSeccionTipoContenido { get; set; }

    }
}
