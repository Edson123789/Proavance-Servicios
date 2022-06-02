using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosPendienteDTO
    {
        public List<ModalidadCursoFiltroDTO> ListaModalidades { get; set; }
        public List<FiltroDTO> ListaPeriodo { get; set; }
        public List<DatoPersonalCoordinadorDTO> ListaCoordinador { get; set; }

        public List<PersonalAsignadoDTO> AsistentesActivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesInactivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesTotales { get; set; }
    }
}
