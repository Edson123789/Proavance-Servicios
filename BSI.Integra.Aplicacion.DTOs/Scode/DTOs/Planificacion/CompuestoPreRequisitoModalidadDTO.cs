using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoPreRequisitoModalidadDTO
    {
        public int IdPreRequisito { get; set; }
        public int IdPGeneral { get; set; }
        public string NombrePreRequisito { get; set; }
        public int Orden { get; set; }
        public int Tipo { get; set; }
        public List<ModalidadCursoDTO> Modalidades { get; set; }
    }
}
