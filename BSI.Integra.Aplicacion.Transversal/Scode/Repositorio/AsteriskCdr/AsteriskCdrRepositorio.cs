using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.DTOs.AsteriskCdr;
using BSI.Integra.Persistencia.Models.Asterisk;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio.AsteriskCdr
{
    /// Repositorio: Transversal/AsteriskCdrRepositorio
    /// Autor: Ansoli Espinoza
    /// Fecha: 26-01-2021
    /// <summary>
    /// Repositorio del Asterisk
    /// </summary>
    public class AsteriskCdrRepositorio
    {
        private AsteriskCdrContext _db;

        public AsteriskCdrRepositorio()
        {
            _db = new AsteriskCdrContext();
        }

        /// Autor: Ansoli Espinoza
        /// Fecha: 26-01-2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Las llamadas que tengan el id mayor al enviado
        /// </summary>
        /// <param name="id">Cdr Id</param>
        /// <returns>Devuele el resumen de la informacion de llamada</returns>
        public List<CdrResumenDTO> ListadoLlamadasMayoresA(int id)
        {
            return _db.Cdr.Where(w => w.RecordingId > id).ToList().Select(s => new CdrResumenDTO()
            {
                FechaInicio = string.IsNullOrEmpty(s.Fechainicio)
                    ? s.Calldate.AddHours(-5) //se resta 5 ya que se guarda en utc la hora
                    : DateTime.ParseExact(s.Fechainicio, "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture),
                Src = s.Src, Dst = s.Dst, Duration = s.Duration, Billsec = s.Billsec,
                CallType = s.CallType, RecordingId = s.RecordingId, Recordingfile = s.Recordingfile,
                IdActividadDetalle = s.IdActividadDetalle, VariableRespaldo = s.VariableRespaldo
            }).ToList();
        }
    }
}
