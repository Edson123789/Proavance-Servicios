using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class HorarioGrupoInsertarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<int> IdPersonal { get; set; }
        public string Usuario { get; set; }

    }
}
