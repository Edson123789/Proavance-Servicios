using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BeneficioPreRequisitoDTO
    {
        public List<CompuestoBeneficioModalidadAlternoDTO> Beneficios { get; set; }
        public List<CompuestoPreRequisitoModalidadDTO> PreRequisitos { get; set; }
        public List<CompuestoMotivacionModalidadAlternoDTO> Motivaciones { get; set; }
        public List<CompuestoCertificacionModalidadAlternoDTO> Certificaciones { get; set; }
        public List<CompuestoProblemaModalidadAlternoDTO> Problemas { get; set; }

    }
    public class BeneficioDetalleRequisitoFiltroDTO
    {
        public int IdProgramaGeneral { get; set; }
        public int IdBeneficio { get; set; }
    }
    public class BeneficioDetalleRequisitoDTO
    {
        public int IdProgramaGeneral { get; set; }
        public int IdBeneficio { get; set; }
        public int IdTipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class BeneficioDetalleRequisitoCodificadoDTO
    {
        public string DatosCodificados { get; set; }
    }
    public class ConfiguracionBeneficioDetalleRequisitoDTO
    {
        public int IdPGeneral { get; set; }
        public int IdBeneficio { get; set; }
        public string Requisito { get; set; }
        public string ProcesoSolicitud { get; set; }
        public string DetallesAdicionales { get; set; }
        public string Usuario { get; set; }
    }
}
