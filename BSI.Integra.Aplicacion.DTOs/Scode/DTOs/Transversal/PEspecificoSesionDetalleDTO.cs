using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoSesionGrupoDTO
    {
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public string Curso { get; set; }
        public int? IdExpositor { get; set; }
        public int IdPEspecificoHijo { get; set; }
        public string Tipo { get; set; }
        public string Comentario { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public int IdGrupo { get; set; }
    }

    public class MaterialPEspecificoGrupoDetalleDTO
    {
        public int Id { get; set; }
        public int IdMaterialTipo { get; set; }
        public string NombreMaterialTipo { get; set; }
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }
        public int GrupoEdicionOrden { get; set; }
        public int GrupoEdicion { get; set; }
        public bool EsPrimerMaterial { get; set; }
        public bool TieneFurAsociado { get; set; }
        public bool EnviadoAProveedorImpresion { get; set; }
        public List<MaterialPEspecificoDetalleVersionDTO> ListaMaterialPEspecificoDetalle { get; set; }
        public MaterialPEspecificoGrupoDetalleDTO()
        {
            ListaMaterialPEspecificoDetalle = new List<MaterialPEspecificoDetalleVersionDTO>();
        }
    }

    public class ResultadoMaterialPEspecificoDetalleDTO
    {
        public int IdMaterialPEspecifico { get; set; }
        public int IdMaterialPEspecificoDetalle { get; set; }
        public int IdPEspecificoPadreIndividual { get; set; }
        public string NombrePEspecificoPadreIndividual { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public DateTime? FechaInicioCurso { get; set; }
        public int Grupo { get; set; }
        public int GrupoEdicion { get; set; }
        public int GrupoEdicionOrden { get; set; }
        public int IdMaterialTipo { get; set; }
        public string NombreMaterialTipo { get; set; }
        public int IdMaterialVersion { get; set; }
        public string NombreMaterialVersion { get; set; }
        public int IdMaterialEstado { get; set; }
        public string NombreMaterialEstado { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlArchivo { get; set; }
        public string UsuarioSubida { get; set; }
        public DateTime? FechaSubida { get; set; }
        public string ComentarioSubida { get; set; }
        public string UsuarioAprobacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public List<FiltroDTO> ListaMaterialAccion { get; set; }
        public bool EsPrimerMaterial { get; set; }
        public bool GrupoEdicionTieneFurAsociado { get; set; }
        public bool EnviadoAProveedorImpresion { get; set; }
        public bool DebeEnviarAProveedorImpresion { get; set; }
        public bool TodasVersionesMaterialGrupoEdicionAprobadas { get; set; }
        public bool TieneFurAsociado { get; set; }
        public bool EsMaterialEnviado { get; set; }
        public bool DebeEnviarAAlumnos { get; set; }

        public string UsuarioEnvio { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public ResultadoMaterialPEspecificoDetalleDTO()
        {
            ListaMaterialAccion = new List<FiltroDTO>();
        }
    }
}
