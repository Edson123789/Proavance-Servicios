using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PlantillaPwDTO
	{
		public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaMaestroPw { get; set; }
        public int IdRevisionPw { get; set; }

    }
}
