using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalCombosDTO
    {
        public List<CiudadDatosDTO> ListaCiudad { get; set; }
        public List<PaisFiltroComboDTO> ListaPais { get; set; }
        public List<FiltroIdNombreDTO> ListaEstadoCivil { get; set; }
        public List<FiltroIdNombreDTO> ListaSexo { get; set; }
        public List<FiltroIdNombreDTO> ListaSistemaPensionario { get; set; }
        public List<FiltroIdNombreDTO> ListaTipoDocumento { get; set; }
        public List<FiltroIdNombreDTO> ListaMotivoCese { get; set; }
        public List<EntidadSistemaPensionarioFiltroDTO> ListaEntidad { get; set; }
        public List<FiltroIdNombreDTO> ListaSedeTrabajo { get; set; }
        public List<FiltroIdNombreDTO> ListaPuestoTrabajo { get; set; }
        public List<FiltroIdNombreDTO> ListaAreaTrabajo { get; set; }
		public List<FiltroIdNombreDTO> ListaEntidadSeguroSalud { get; set; }
    }
}
