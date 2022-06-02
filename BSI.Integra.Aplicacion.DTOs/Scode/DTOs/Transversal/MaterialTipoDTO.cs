using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialTipoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreMaterialTipoEntrega { get; set; }
        public string NombreUsuario { get; set; }

        public List<MaterialAsociacionAccionDTO> ListaMaterialAsociacionAccion { get; set; }
        public List<MaterialAsociacionCriterioVerificacionDTO> ListaMaterialAsociacionCriterioVerificacion { get; set; }
        public List<MaterialAsociacionVersionDTO> ListaMaterialAsociacionVersion { get; set; }
        public MaterialTipoDTO()
        {
            ListaMaterialAsociacionAccion = new List<MaterialAsociacionAccionDTO>();
            ListaMaterialAsociacionCriterioVerificacion = new List<MaterialAsociacionCriterioVerificacionDTO>();
            ListaMaterialAsociacionVersion = new List<MaterialAsociacionVersionDTO>();
        }
    }
    public class MaterialAsociacionAccionDTO
    { 
        public int Id { get; set; }
        public int IdMaterialTipo { get; set; }
        public int IdMaterialAccion { get; set; }
    }

    public class MaterialAsociacionCriterioVerificacionDTO
    {
        public int Id { get; set; }
        public int IdMaterialTipo { get; set; }
        public int IdMaterialCriterioVerificacion { get; set; }
    }

    public class MaterialAsociacionVersionDTO
    {
        public int Id { get; set; }
        public int IdMaterialTipo { get; set; }
        public int IdMaterialVersion { get; set; }
    }
}
