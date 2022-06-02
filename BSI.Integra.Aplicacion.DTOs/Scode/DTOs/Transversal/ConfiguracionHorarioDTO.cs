using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
   public  class ConfiguracionHorarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public List<ConfiguracionHorarioMarcacionGrupoDTO> IdHorarioGrupoPersonal { get; set; }
    }
}
