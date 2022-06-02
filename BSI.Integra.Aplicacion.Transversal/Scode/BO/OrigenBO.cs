using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{

    public class OrigenBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipodato { get; set; }
        public int Prioridad { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public byte[] RowVersion { get; set; }


        private OrigenRepositorio _repOrigen;

        public OrigenBO()
        {
            _repOrigen = new OrigenRepositorio();
			ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
			this.InicializarValidadoresGenerales(this.GetType().Name, this.GetType());
			
		}



        /// <summary>
        /// Obtiene los origenes para ser filtrados
        /// </summary>
        /// <returns></returns>
        public List<OrigenFiltroDTO> ObtenerTodoFiltro()
        {
            return _repOrigen.ObtenerTodoFiltro();
        }

		public void InicializarValidadoresGenerales(string nombreClass, Type entidad)
		{
			ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("Nombre").Name, "Nombre origen");
			ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("Descripcion").Name, "Descripcion origen");
			ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdTipodato").Name, "IdTipodato");
			ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("Prioridad").Name, "Prioridad origen");
			ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdCategoriaOrigen").Name, "IdCategoriaOrigen");
		}

	}

}
