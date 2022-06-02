using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroAgendaDTO
    {
        //public List<EstadoOportunidadFiltroDTO> listaEstadoOportunidad { get; set; }
        public List<EstadoOcurrenciaFiltroDTO> listaEstadoOcurrencia { get; set; }
        public List<AlumnoFiltroAutocompleteDTO> listaContacto { get; set; }
        public List<PersonalAutocompleteDTO> listaPersonal { get; set; }//asesor
        public List<FaseOportunidadFiltroDTO> listaFaseOportunidad { get; set; }
        public List<FiltroDTO> listaTipoDato { get; set; }
        public List<OrigenFiltroDTO> listaOrigen { get; set; }
        public List<ProbabilidadRegistroPwFiltroDTO> listaProbabilidadRegistro { get; set; }
        public List<CentroCostoFiltroAutocompleteDTO> listaCentroCosto { get; set; }
        public List<CategoriaOrigenFiltroDTO> listaCategoriaOrigen { get; set; }

    }
}
