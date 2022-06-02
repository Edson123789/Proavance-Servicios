using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialPespecificoSesionDTO 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public int IdMaterialTipo { get; set; }
        public int? IdFur { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class MaterialPespecificoDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }
        public int IdMaterialTipo { get; set; }
        public int GrupoEdicion { get; set; }
        public int GrupoEdicionOrden { get; set; }
        public int? IdFur { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class MaterialPEspecificoDetalleDTO
    {
        public int Id { get; set; }
        public string ComentarioSubida { get; set; }
        public string NombreUsuario { get; set; }
    }
    public class DocumentosOportunidadDTO
    {
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public int IdClasificacionPersona { get; set; }
        public string ComentarioSubida { get; set; }
        public string NombreUsuario { get; set; }
    }
    public class DocumentosMarketingDTO
    {
        public string NombreUsuario { get; set; }
        public string IPCliente { get; set; }

    }

    public class MaterialPEspecificoDetalleVersionDTO
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public int IdMaterialVersion { get; set; }
    }
    public class ProyectoAplicacionEntregaVersionPwDTO
    {
        public int Id { get; set; }
        public string EnlaceProyecto { get; set; }
        public int Version { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Usuario { get; set; }
    }
    public class ProyectoAplicacionEntregaVersionPwDTOV2
    {
        public int Id { get; set; }
        public string ArchivoProyecto { get; set; }
        public DateTime FechaEnvioProyecto { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool EsEntregable { get; set; }
        public string Usuario { get; set; }
    }
}
