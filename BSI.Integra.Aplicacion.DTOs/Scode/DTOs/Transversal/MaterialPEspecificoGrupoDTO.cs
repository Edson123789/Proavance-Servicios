using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialPEspecificoGrupoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdGrupo { get; set; }
    }
}
