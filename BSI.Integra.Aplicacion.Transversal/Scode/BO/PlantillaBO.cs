using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: PlantillaBO
    /// Autor: Esthephany Tanco - Wilber Choque - Edgar Serruto.
    /// Fecha: 30/04/2021
    /// <summary>
    /// Columnas y funciones de la tabla T_Plantilla
    /// </summary>
    public class PlantillaBO : BaseBO
    {
        /// Propiedades		                Significado
        /// -------------	                -----------------------
        /// Nombre                          Nombre de plantilla       
        /// Descripcion                     Descripción de plantilla
        /// IdPlantillaBase                 Id de plantilla base
        /// EstadoAgenda                    Validacion estado en Agenda
        /// Documento                       Id de documento
        /// IdMigracion                     Id de migración
        /// IdPersonalAreaTrabajo           Id de area de trabajo de personal
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaBase { get; set; }
        public bool EstadoAgenda { get; set; }
        public int Documento { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public List<PlantillaClaveValorBO> PlantillaClaveValor { get; set; }
        public List<FaseByPlantillaBO> FaseByPlantilla { get; set; }
        public List<PlantillaAsociacionModuloSistemaBO> ListaPlantillaAsociacionModuloSistema { get; set; }

        public PlantillaBO() {
            PlantillaClaveValor = new List<PlantillaClaveValorBO>();
            FaseByPlantilla = new List<FaseByPlantillaBO>();
            ListaPlantillaAsociacionModuloSistema = new List<PlantillaAsociacionModuloSistemaBO>();
        }
    }
}
