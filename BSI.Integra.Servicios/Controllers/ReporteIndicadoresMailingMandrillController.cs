using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteIndicadoresMailingMandrill
    /// <summary>
    /// Autor: Gian Miranda
    /// Fecha: 26/08/2021
    /// <summary>
    /// Gestión de Reporte de Indicadores de Mailing Mandrill
    /// </summary>
    /// [Route("api/ReporteIndicadoresCampaniasMailing")]
    public class ReporteIndicadoresMailingMandrillController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public ReporteIndicadoresMailingMandrillController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
        }
    }
}
