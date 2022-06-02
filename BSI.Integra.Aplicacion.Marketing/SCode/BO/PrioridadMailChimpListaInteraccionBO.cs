using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Persistencia.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class PrioridadMailChimpListaInteraccionBO : BaseEntity
    {
        public int IdPrioridadMailChimpLista { get; set; }
        public double ClickRate { get; set; }
        public int Clicks { get; set; }
        public double OpenRate { get; set; }
        public int SubscriberClicks { get; set; }
        public int Opens { get; set; }
        public int UniqueOpens { get; set; }
        public int? MemberCount { get; set; }
        public int CleanedCount { get; set; }
        public int? EmailSend { get; set; }
        public byte[] RowVersion { get; set; }

        integraDBContext Contexto;
        private DapperRepository DapperRepository;
        public PrioridadMailChimpListaInteraccionBO()
        {
            Contexto = new integraDBContext();
            DapperRepository = new DapperRepository(Contexto);
        }

        public List<TPrioridadMailChimpListaInteraccion> PrioridadesMailChimpListaInteracciones()
        {
            List<TPrioridadMailChimpListaInteraccion> lista = new List<TPrioridadMailChimpListaInteraccion>();
            DateTime fecha5dias_antes = DateTime.Now.Date.AddDays(-5);
            try
            {
                lista = (from x in Contexto.TPrioridadMailChimpListaInteraccion
                         join y in Contexto.TPrioridadMailChimpLista on x.IdPrioridadMailChimpLista equals y.Id
                         where
                         y.Enviado == true
                         && y.IdCampaniaMailchimp != null
                         && y.IdListaMailchimp != null
                         && y.FechaEnvio != null
                         && y.FechaEnvio > fecha5dias_antes
                         select x).ToList();
            }
            catch (Exception e)
            {

            }
            return lista;
        }

        public List<TPrioridadMailChimpListaInteraccion> PrioridadesMailChimpListaInteraccionesByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            List<TPrioridadMailChimpListaInteraccion> lista = new List<TPrioridadMailChimpListaInteraccion>();
            try
            {
                lista = (from x in Contexto.TPrioridadMailChimpListaInteraccion
                         join y in Contexto.TPrioridadMailChimpLista on x.IdPrioridadMailChimpLista equals y.Id
                            where
                            y.Enviado == true
                            && y.IdCampaniaMailchimp != null
                            && y.IdListaMailchimp != null
                            && y.FechaEnvio != null
                            && y.FechaEnvio > fechaInicio
                            && y.FechaEnvio <= fechaFin
                         select x).ToList();

            }
            catch (Exception e)
            {

            }
            return lista;
        }
    }
}
