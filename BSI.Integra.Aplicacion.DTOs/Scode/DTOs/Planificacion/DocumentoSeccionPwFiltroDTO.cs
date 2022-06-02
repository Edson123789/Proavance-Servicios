using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoSeccionPwFiltroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public int IdDocumentoPW { get; set; }
        public int IdSeccionPW { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public string NombreSeccionTipoContenido { get; set; }
        public int? IdSeccionTipoDetallePw { get; set; }
        public string NombreSubSeccion { get; set; }
        public int? IdSubSeccionTipoContenido { get; set; }
        public string ContenidoSubSeccion { get; set; }
        public int? NumeroFila { get; set; }
        public List<listaGridListaSeccionesDTO> listaGridListaSecciones { get;set;}
    }

    public class DocumentoSeccionPwFiltroAgrupadoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPW { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int IdDocumentoPW { get; set; }
        public int IdSeccionPW { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public List<SubSeccionTipoDetallePwDTO> ListaSubSeccionesPw { get; set; }
    }

    public class DocumentoSeccionPwFiltroPortalDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPW { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public int IdDocumentoPW { get; set; }
        public int IdSeccionPW { get; set; }
        public bool EstadoDocumentoSeccion { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public string NombreSeccionTipoContenido { get; set; }
        public int? IdSeccionTipoDetallePw { get; set; }
        public string NombreSubSeccion { get; set; }
        public int? IdSubSeccionTipoContenido { get; set; }
        public string ContenidoSubSeccion { get; set; }
        public int? NumeroFila { get; set; }
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdPlantillaMaestroPw { get; set; }
        public int Version { get; set; }
    }

    public class DocumentoSeccionPwFiltroAgrupadoPortalDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPW { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int IdDocumentoPW { get; set; }
        public int IdSeccionPW { get; set; }
        public bool EstadoDocumentoSeccion { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdPlantillaMaestroPw { get; set; }
        public int Version { get; set; }
        public List<SubSeccionTipoDetallePwDTO> ListaSubSeccionesPw { get; set; }
    }
}
