﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TabAgendaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool VisualizarActividad { get; set; }
        public bool CargarInformacionInicial { get; set; }
        public string VistaBaseDatos { get; set; }
        public string CamposVista { get; set; }
        public string IdTipoCategoriaOrigen { get; set; }
        public string IdCategoriaOrigen { get; set; }
        public string IdTipoDato { get; set; }
        public string IdFaseOportunidad { get; set; }
        public string IdEstadoOportunidad { get; set; }
        public string Probabilidad { get; set; }
        public int Numeracion { get; set; }
        public bool ValidarFecha{ get; set; }
        public string CodigoAreaTrabajo { get; set; }
        public int? IdTabs { get; set; }
    }
}
