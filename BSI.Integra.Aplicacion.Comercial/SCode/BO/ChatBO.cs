using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class ChatBO
    {
        public ChatComboBoxDTO ChatComboBox;
        private AreaFormacionBO _areaFormacion;
        private CargoBO _cargo;
        private AreaTrabajoBO _areaTrabajo;
        private IndustriaBO _industria;

        public ChatBO() {
            ChatComboBox = new ChatComboBoxDTO();
            _cargo = new CargoBO();
            _areaFormacion = new AreaFormacionBO();
            _areaTrabajo = new AreaTrabajoBO();
            _industria = new IndustriaBO();
        }

        /// <summary>
        /// Obtiene todos los combo box para ser inicializados en el chat
        /// </summary>
        public void CargarComboBoxChat() {
            ChatComboBox.ListaCargo = _cargo.ObtenerTodoFiltro();
            ChatComboBox.ListaAreaFormacion = _areaFormacion.ObtenerTodoFiltro();
            ChatComboBox.ListaAreaTrabajo = _areaTrabajo.ObtenerTodoAreaTrabajoCB();
            ChatComboBox.ListaIndustria = _industria.ObtenerTodoFiltro();

        }

        public class ChatComboBoxDTO {
            public List<CargoFiltroDTO> ListaCargo { get; set; }
            public List<AreaFormacionFiltroDTO> ListaAreaFormacion { get; set; }
            public List<AreaTrabajoBO> ListaAreaTrabajo { get; set; }
            public List<IndustriaFiltroDTO> ListaIndustria { get; set; }
            //public List<EmpresaBO> ListaEmpresa { get; set; }
        }
    }

}
