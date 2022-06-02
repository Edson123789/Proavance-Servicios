using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class CronogramaPagoDetalleModRepositorio:BaseRepository<TCronogramaPagoDetalleMod, CronogramaPagoDetalleModBO>
    {
        #region Metodos Base
        public CronogramaPagoDetalleModRepositorio() : base()
        {
        }
        public CronogramaPagoDetalleModRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        #endregion
    }
}
