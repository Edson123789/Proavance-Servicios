using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ConfiguracionBO
    {

        public List<FiltroDTO> ListaAreaCapacitacion;
        public List<SubAreaCapacitacionAutoselectDTO> ListaSubAreaCapacitacion;
        public List<PGeneralFiltroDTO> ListaProgramaGeneral;
        public List<AlumnoFiltroAutocompleteDTO> ListaAlumno;
        public List<CentroCostoFiltroAutocompleteDTO> ListaCentroCosto;
        private AreaCapacitacionBO _areaCapacitacionBO;
        private SubAreaCapacitacionBO _subAreaCapacitacionBO;
        private PgeneralBO _programaGeneralBO;
        private AlumnoBO _alumnoBO;
        private CentroCostoBO _centroCostoBO;

        public ConfiguracionBO()
        {
            //_dapperRepository = new DapperRepository();
            ListaAreaCapacitacion = new List<FiltroDTO>();
            ListaSubAreaCapacitacion = new List<SubAreaCapacitacionAutoselectDTO>();
            ListaProgramaGeneral = new List<PGeneralFiltroDTO>();
            ListaAlumno = new List<AlumnoFiltroAutocompleteDTO>();
            ListaCentroCosto = new List<CentroCostoFiltroAutocompleteDTO>();
            _areaCapacitacionBO = new AreaCapacitacionBO();
            _subAreaCapacitacionBO = new SubAreaCapacitacionBO();
            //_programaGeneralBO = new PgeneralBO(); ListaProgramaGeneral = new List<PGeneralFiltroDTO>();
            ListaAlumno = new List<AlumnoFiltroAutocompleteDTO>();
            ListaCentroCosto = new List<CentroCostoFiltroAutocompleteDTO>();
            _areaCapacitacionBO = new AreaCapacitacionBO();
            _subAreaCapacitacionBO = new SubAreaCapacitacionBO();
            //_programaGeneralBO = new PgeneralBO(); ListaProgramaGeneral = new List<PGeneralFiltroDTO>();
            ListaAlumno = new List<AlumnoFiltroAutocompleteDTO>();
            ListaCentroCosto = new List<CentroCostoFiltroAutocompleteDTO>();
            _areaCapacitacionBO = new AreaCapacitacionBO();
            _subAreaCapacitacionBO = new SubAreaCapacitacionBO();
            //_programaGeneralBO = new PgeneralBO();
            _alumnoBO = new AlumnoBO();
            _centroCostoBO = new CentroCostoBO();
            CargarSubAreaCapacitacion();
            CargarAreaCapacitacion();
            CargarProgramaGeneral();
        }

        /// <summary>
        /// Obtiene todas las sub areas de capacitación con estado activo
        /// </summary>
        private void CargarSubAreaCapacitacion()
        {
            ListaSubAreaCapacitacion = _subAreaCapacitacionBO.ObtenerTodoFiltroAutoselect();
        }
        /// <summary>
        /// Obtiene todas las areas de capacitación con estado activo
        /// </summary>
        private void CargarAreaCapacitacion()
        {
            ListaAreaCapacitacion = _areaCapacitacionBO.ObtenerTodoFiltro();
        }

        /// <summary>
        /// Obtiene todos los programadas generales con estado activo
        /// </summary>
        private void CargarProgramaGeneral()
        {
            //ListaProgramaGeneral = _programaGeneralBO.ObtenerTodoFiltro();
        }
        /// <summary>
        /// Obtiene un listado de alumnos que son filtrados por el nombre completo
        /// </summary>
        /// <param name="valor"></param>
        public void CargarAlumnoAutoComplete(string valor)
        {
            ListaAlumno = _alumnoBO.ObtenerTodoFiltroAutocomplete(valor);
        }

        /// <summary>
        /// Obtiene un listado de centros de costo que son filtrados por el nombre de centro de costo
        /// </summary>
        /// <param name="valor"></param>
        public void CargarCentroCostoAutoComplete(string valor)
        {
            ListaCentroCosto = _centroCostoBO.ObtenerTodoFiltroAutocomplete(valor);
        }

    }
}
