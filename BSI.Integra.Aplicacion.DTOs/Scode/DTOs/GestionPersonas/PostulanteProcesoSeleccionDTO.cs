using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PostulanteProcesoSeleccionDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public int IdProcesoSeleccion { get; set; }
		public string Usuario { get; set; }
	}
	public class PostulanteProcesoSeleccionIdDTO
	{
		public int Id { get; set; }
		public string Usuario { get; set; }
	}

    public class PostulanteImportadoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }  
        public string Origen { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public List<int> ListaRespuestaDesaprobatoria { get; set; }
    }
    public class PostulanteProcesoSeleccionConsolidadoDTO
    {
        public List<PostulanteImportadoDTO> listaPostulante { get; set; }
        public string Usuario { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdEtapaProcesoSeleccion { get; set; }
        public int IdProveedor { get; set; }
        public int IdPersonal_Operador { get; set; }
        public int IdConvocatoria { get; set; }
    }

}
