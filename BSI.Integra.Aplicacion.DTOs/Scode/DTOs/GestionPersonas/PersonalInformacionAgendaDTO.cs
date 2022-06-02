using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class PersonalInformacionAgendaDTO
    {
        public PersonalDatosAgendaDTO DatosPersonal { get; set; }
        public List<PersonalAsignadoDTO> Asignados { get; set; }
    }
}
