using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class RevisionNivelPwFiltroDTO
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public int IdTipoRevisionPw { get; set; }
        public int IdRevisionPw { get; set; }

    }
}
