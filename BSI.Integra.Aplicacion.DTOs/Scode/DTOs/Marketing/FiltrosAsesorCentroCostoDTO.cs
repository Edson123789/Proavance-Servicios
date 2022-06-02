using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltrosAsesorCentroCostoDTO
    {
        // public List<AsesorCentroCostoOcurrenciaFiltroDTO> ListaAsesorCentroCostoOcurrencias { get; set; }
        public List<FiltroDTO> ListaAreasCapacitacion { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> ListaSubAreaCapacitacion { get; set; }
        public List<PGeneralSubAreaFiltroDTO> ListaProgramaGeneral { get; set; }
        public List<PaisFiltroDTO> ListaPais { get; set; }
        public List<ProbabilidadRegistroPwFiltroDTO> ListaProbabilidad { get; set; }
        public List<FaseOportunidadFiltroDTO> ListaFaseOportunidad { get; set; }
        public List<FiltroBasicoDTO> ListaGrupoFiltroProgramaCritico { get; set; }
        public List<NombreTipoCategoriaOrigenFiltroDTO> ListaCategoriaOrigen {get;set;}
        public List<FiltroDTO> ListaTipoCategoriaOrigen { get; set; }


    }
}
