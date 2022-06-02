using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class SeccionPwDTO
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public int? IdSeccionTipoContenido { get; set; }

        public List<SeccionTipoDetallePwDTO> listaGridSeccionTipoContenido { get; set; }

    }
}
