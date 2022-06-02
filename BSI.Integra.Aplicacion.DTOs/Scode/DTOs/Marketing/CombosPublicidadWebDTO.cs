using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosPublicidadWebDTO
    {
        public List<ChatZopimFiltroDTO> ChatZoopim { get; set; }
        public List<CategoriaOrigenFiltroDTO> CategoriaOrigen { get; set; }
        public List<FormularioSolicitudTextoBotonFiltroDTO> TextoBotones { get; set; }
        public List<CampoContactoFiltroDTO> CampoContactos { get; set; }
        public List<PGeneralFiltroDTO> ProgramasGenerales { get; set; }

        public List<TipoPublicidadWebFiltroDTO> TipoPublicidad { get; set; }
        public List<PEspecificoCentroCostoDTO> CentroCosto { get; set; }
    }
}
