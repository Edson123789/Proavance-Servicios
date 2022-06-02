using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    /// BO: Planificacion/Courier
    /// Autor: Max Mantilla
    /// Fecha: 25/11/2021
    /// <summary>
    /// BO para la logica de los couriers
    /// </summary>
    public class CourierBO : BaseBO
    {
        /// Propiedades	                    Significado
        /// -----------	                    ------------
        /// Nombre		                    Nombre del Courier
        /// IdPais                          Id del país del courier
        /// IdCiudad                        Id de la ciudad del courier
        /// Url		                        Url de la pagina Web del courier


        public string Nombre { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Url { get; set; }
        

        private CourierRepositorio _repCourier;

        public CourierBO()
        {
            _repCourier = new CourierRepositorio();
        }
    }
}
