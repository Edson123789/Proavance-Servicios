using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ObtenerCategoriaFiltroDTO
    {
        public int? Id { get; set; }
        public int? IdActividad { get; set;}
    }
    public class ObtenerCategoriaFiltroV2DTO
    {
        public int IdActividadCabecera { get; set; }
        public int IdOcurrenciaPadre { get; set; }
    }
}
