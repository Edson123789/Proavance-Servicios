using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SeccionDocumentoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int? IdPGeneral { get; set; }
        public int? OrdenWeb { get; set; }
		public int Orden { get; set; }
    }

	public class RegistroListaSeccionesDocumentoDTO
	{
		public int IdPGeneral { get; set; }
		public string Titulo { get; set; }
		public string Contenido { get; set; }
		public int? IdSeccionTipoDetalle_PW { get; set; }
		public int? NumeroFila { get; set; }
		public string Cabecera { get; set; }
		public string PiePagina { get; set; }
		public int? OrdenWeb { get; set; }
		public string NombreCurso { get; set; }
	}

	public class ProgramaGeneralDoumentoDTO
	{
		public virtual List<RegistroListaSeccionesDocumentoDTO> ListaSeccionesContenidosDocumento { get; set; }
		public virtual List<RegistroListaSeccionesDocumentoDTO> ListaSeccionesContenidosDocumentoEstructura { get; set; }
		public bool EsProgramaPadre { get; set; }
	}

	public class ProgramaGeneralEstructuraAgrupadoDTO
	{
		public string Seccion { get; set; }
		public string Titulo { get; set; }
		public List<ProgramaGeneralEstructuraDetalleDTO> DetalleContenido { get; set; }
	}

	public class ProgramaGeneralEstructuraDetalleDTO
	{
		public string Contenido { get; set; }
		public string Cabecera { get; set; }
		public string PiePagina { get; set; }
	}

	public class ProgramaGeneralSeccionDocumentoDTO
	{
		public string Seccion { get; set; }
		public List<ProgramaGeneralSeccionDocumentoDetalleDTO> DetalleSeccion { get; set; }
	}

	public class ProgramaGeneralSeccionDocumentoDetalleDTO
	{
		public string Titulo { get; set; }
		public string Cabecera { get; set; }
		public string PiePagina { get; set; }
		public List<string> DetalleContenido { get; set; }
	}

    public class ProgramaGeneralSeccionAnexosHTMLDTO
	{
		public string Seccion { get; set; }
		public string Contenido { get; set; }
	}


	public class ProgramaExpositoresDTO
	{
		public int Id { get; set; }
		public string PrimerNombre { get; set; }
		public string SegundoNombre { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public string NombrePais { get; set; }
		public string HojaVidaResumidaPerfil { get; set; }
		public int? IdPGeneral { get; set; }
	}

    public class ProgramaSeccionIndividualDTO
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int? IdSeccionTipoDetalle_PW { get; set; }
        public int? NumeroFila { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public int? OrdenWeb { get; set; }
    }
}
