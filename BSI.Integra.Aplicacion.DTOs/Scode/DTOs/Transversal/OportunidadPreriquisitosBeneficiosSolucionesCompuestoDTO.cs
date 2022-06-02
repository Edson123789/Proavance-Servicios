using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadPreriquisitosBeneficiosSolucionesCompuestoDTO
    {
        public OportunidadCompetidorDTO OportunidadCompetidor { get; set; }
        public List<OportunidadPrerequisitoGeneralDTO> ListaPrerequisitoGeneral { get; set; }
        public List<OportunidadPrerequisitoEspecificoDTO> ListaPrerequisitoEspecifico { get; set; }
        public List<OportunidadBeneficioDTO> ListaBeneficio { get; set; }
        public List<int> ListaCompetidor { get; set; }
        public List<SolucionClienteByActividadDTO> ListaSoluciones { get; set; }
        //Ultimo Agregado
        public List<OportunidadPrerequisitoGeneralAlternoDTO> ListaPrerequisitoGeneralAlterno { get; set; }
        public List<OportunidadBeneficioAlternoDTO> ListaBeneficioAlterno { get; set; }
    }
    public class OportunidadPreriquisitosBeneficiosSolucionesCompuestoAlternoDTO
    {
        public OportunidadCompetidorDTO OportunidadCompetidor { get; set; }
        //public List<OportunidadPrerequisitoGeneralDTO> ListaPrerequisitoGeneral { get; set; }
        //public List<OportunidadPrerequisitoEspecificoDTO> ListaPrerequisitoEspecifico { get; set; }
        //public List<OportunidadBeneficioDTO> ListaBeneficio { get; set; }
        public List<OportunidadMotivacionDTO> ListaMotivacion { get; set; }
        public List<OportunidadPublicoObjetivoDTO> ListaPublicoObjetivo { get; set; }
        public List<OportunidadCertificacionDTO> ListaCertificacion { get; set; }
        public List<OportunidadPrerequisitoGeneralAlternoDTO> ListaPrerequisitoGeneralAlterno { get; set; }
        public List<OportunidadBeneficioAlternoDTO> ListaBeneficioAlterno { get; set; }
        public List<int> ListaCompetidor { get; set; }
        //public List<SolucionClienteByActividadDTO> ListaSoluciones { get; set; }
        public List<SolucionClienteByActividadAlternoDTO> ListaSolucionesAlterno { get; set; }

    }
}
