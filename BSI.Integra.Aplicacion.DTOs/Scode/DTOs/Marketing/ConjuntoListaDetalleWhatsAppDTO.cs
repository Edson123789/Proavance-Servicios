﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConjuntoListaDetalleWhatsAppDTO
    {
        public int? Id { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdConjuntoLista { get; set; }
        public List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaPrincipal { get; set; }
        public List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaSecundario { get; set; }
    }

    public class ConjuntoListaDetalleSmsDTO
    {
        public int? Id { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdConjuntoLista { get; set; }
        public int? IdPGeneral { get; set; }
    }
}
