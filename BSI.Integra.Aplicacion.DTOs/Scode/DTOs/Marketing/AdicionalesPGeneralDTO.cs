using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AdicionalesPGeneralDTO
    {
        public PGeneralNombreDescripcionDTO PGeneralNombreDescripcion { get; set; }
        public List<DatoAdicionalPaginaDTO> datosAdicionales { get; set; }
    }
}
