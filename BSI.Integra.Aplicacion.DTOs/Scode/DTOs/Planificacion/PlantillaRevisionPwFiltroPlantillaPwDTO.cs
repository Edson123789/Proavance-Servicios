using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PlantillaRevisionPwFiltroPlantillaPwDTO
	{
        public int Id { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdRevisionNivelPw { get; set; }
        public int IdPersonal { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public int IdTipoRevisionPw { get; set; }
    }
}
