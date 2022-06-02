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
    /// BO: Planificacion/CourierDetalle
    /// Autor: Max Mantilla
    /// Fecha: 25/11/2021
    /// <summary>
    /// BO para la logica de detalle de los couriers
    /// </summary>
    public class CourierDetalleBO : BaseBO
    {
        /// Propiedades	                    Significado
        /// -----------	                    ------------
        /// IdCourier                       Id del courier
        /// IdPais                          Id del país del courier
        /// IdCiudad                        Id de la ciudad del courier
        /// Direccion		                Direccion del courier
        /// Telefono	                    Telefono del courier
        /// TiempoEnvio		                Tiempo de envio del courier


        public int IdCourier { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int TiempoEnvio { get; set; }


        private CourierDetalleRepositorio _repCargo;

        public CourierDetalleBO()
        {
            _repCargo = new CourierDetalleRepositorio();
        }
    }
}
