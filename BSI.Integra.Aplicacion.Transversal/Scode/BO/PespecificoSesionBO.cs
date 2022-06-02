using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PespecificoSesionBO: BaseBO
    {
        public int IdPespecifico { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int? IdExpositor { get; set; }
        public string Comentario { get; set; }
        public bool SesionAutoGenerada { get; set; }
        public int? IdAmbiente { get; set; }
        public bool? Predeterminado { get; set; }
		public int Version { get; set; }
		public int Grupo { get; set; }
		public bool EsSesionInicio { get; set; }
		public Guid? IdMigracion { get; set; }
		public int? GrupoSesion { get; set; }
        public int? IdProveedor { get; set; }
        public string UrlWebex { get; set; }
        public int? CuentaWebex { get; set; }
        public int? IdModalidadCurso { get; set; }
        public DateTime? FechaCancelacionWebinar { get; set; }
        public string ComentarioCancelacionWebinar { get; set; }
        public bool? EsWebinarConfirmado { get; set; }
		public bool? MostrarPortalWeb { get; set; }

		PespecificoSesionRepositorio _repPespecificoSesion;
        PespecificoRepositorio _repPespecifico;
        PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadrePespecificoHijo;
        PespecificoFrecuenciaRepositorio _repProgramaEspecificoFrecuencia;
        PespecificoFrecuenciaDetalleRepositorio _repPespecificoFrecuenciaDetalle;
        FrecuenciaRepositorio _repFrecuencia;
        FeriadoRepositorio _repFeriado;
        AmbienteRepositorio _repAmbiente;
        ExpositorRepositorio _repExpositor;
        //AmbienteBO _ambienteRepository;

        public PespecificoSesionBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
           

        }

        public PespecificoSesionBO(integraDBContext contexto)
        {
            _repPespecifico = new PespecificoRepositorio(contexto);
            _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio(contexto);
            _repProgramaEspecificoFrecuencia = new PespecificoFrecuenciaRepositorio(contexto);
            _repPespecificoFrecuenciaDetalle = new PespecificoFrecuenciaDetalleRepositorio(contexto);
            _repPespecificoSesion = new PespecificoSesionRepositorio(contexto);
            _repFrecuencia = new FrecuenciaRepositorio(contexto);
            _repFeriado = new FeriadoRepositorio(contexto);
            _repAmbiente = new AmbienteRepositorio(contexto);
            _repExpositor = new ExpositorRepositorio(contexto);
        }
        

        public bool ActualizarExpositorAmbienteDeSesionesByPEspecifico(Dictionary<string, string> dto)
        {
            int idPespecifico = Convert.ToInt32(dto["IdPespecifico"]);
            int idExpositor = Convert.ToInt32(dto["IdExpositor"]);
            int? idAmbiente;

            if (dto["IdAmbiente"] != null && dto["IdAmbiente"] != "")
            {
                idAmbiente = Convert.ToInt32(dto["IdAmbiente"]);
            }
            else
            {
                idAmbiente = null;
            }
           

            List<PespecificoSesionBO> listaPespecificoSesion = new List<PespecificoSesionBO>();                       

            var listaSesiones = _repPespecificoSesion.ListaPespecificoSesiones(idPespecifico);
            
            foreach (var sesion in listaSesiones)
            {
                PespecificoSesionBO sesionActualizar = _repPespecificoSesion.FirstById(sesion);

                sesionActualizar.IdExpositor = idExpositor;
                sesionActualizar.IdAmbiente = idAmbiente;
                _repPespecificoSesion.Update(sesionActualizar);
            }
           

            return true;
        }           

        public List<PespecificoSesionCompuestoDTO> ObtenerCronogramaSesionesPEspecifico(int idPespecifico, bool? cursoIndividual)
        {
            List<PespecificoSesionCompuestoDTO> ListaCronogramaSesiones = new List<PespecificoSesionCompuestoDTO>();
            DatosProgramaEspecificoDTO programaEspecifico = _repPespecifico.ObtenerDatosProgramaEspecificoPorId(idPespecifico);

            if (cursoIndividual.HasValue)
                if (cursoIndividual.Value)
                {//Si es un curso individual
                    return ListaCronogramaSesiones = _repPespecificoSesion.ObtenerCronogramaIndividualPorPEspecifico(programaEspecifico);
                }

             return GetCronogramaByPEspecifico(programaEspecifico);
        }

        public List<PespecificoSesionCompuestoDTO> GetCronogramaByPEspecifico(DatosProgramaEspecificoDTO programaEspecifico)
        {
            
            List<PespecificoSesionCompuestoDTO> sesionesPespecificoCompuesto = _repPespecificoSesion.ListaPespecificoSesioneshijos(programaEspecifico.Id);

            int c = 0;
            foreach (var item in sesionesPespecificoCompuesto)
            {
                sesionesPespecificoCompuesto[c].Cruce = SesionTieneCruce(item.Id);
                c++;
            }
            return sesionesPespecificoCompuesto;
        }

        private bool SesionTieneCruce(int idSesion)
        {
            RegistroSesionDTO sesion = _repPespecificoSesion.ObtnerSesionPorIdSesion(idSesion);

            DatosAmbienteDTO ambiente= new DatosAmbienteDTO();

            DateTime FechaInicio = sesion.FechaHoraInicio.Value, FechaFin = sesion.FechaHoraInicio.Value.AddHours(Convert.ToDouble(sesion.Duracion));            

            if (sesion.IdAmbiente != null)
                ambiente = _repAmbiente.ObtenerVirtualDeAmbiente(sesion.IdAmbiente);

            var listaCruces = _repPespecificoSesion.GetBy(s => s.Id != sesion.Id, y=> new { y.FechaHoraInicio,y.IdAmbiente,y.IdExpositor,y.Duracion})
                                    .Where(s => (s.FechaHoraInicio <= FechaInicio && FechaInicio <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion)).AddSeconds(-1)) ||
                                           (s.FechaHoraInicio.AddSeconds(1) <= FechaFin && FechaFin <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion)))
                                    )          
                                    .Where(s =>
                                    {
                                        if (sesion.IdExpositor == null && sesion.IdAmbiente.HasValue)
                                        {
                                            return false;
                                        }
                                        if (sesion.IdExpositor == null && sesion.IdAmbiente.HasValue)
                                        {
                                            if (ambiente.Virtual.HasValue)
                                            {
                                                return false;
                                            }
                                            else
                                            {
                                                return sesion.IdAmbiente == s.IdAmbiente;
                                            }
                                        }
                                        if (sesion.IdAmbiente == null && sesion.IdExpositor.HasValue)
                                        {
                                            return sesion.IdExpositor == s.IdExpositor;
                                        }
                                        
                                        return (sesion.IdAmbiente == s.IdAmbiente && !ambiente.Virtual.HasValue) || sesion.IdExpositor == s.IdExpositor;
                                        

                                    }).ToList();

            return listaCruces.Count == 0;
        }
               
        public DateTime ObtenerFechaAsignar(ListainformacionProgramaEspecificoHijoDTO curso, DateTime fechaAsignar, byte dia, byte[] diasFrecuencia, List<DatosFeriadoDTO> listaFeriados)
        {
            int cont = Array.IndexOf(diasFrecuencia, dia);
            DateTime fechaTemp;

            while (true)
            {
                fechaTemp = fechaAsignar;
                fechaAsignar = ObtenerProximoDiaSemana(fechaAsignar, (DayOfWeek)diasFrecuencia[cont]);

                if (fechaAsignar.DayOfYear == fechaTemp.DayOfYear && cont == (cont + 1) % diasFrecuencia.Length)
                {
                    fechaAsignar = fechaAsignar.AddDays(1);
                    continue;
                }

                //si es feriado continuar con la siguiente sesion
                if (FechaEsFeriadoPorCiudad(fechaAsignar, listaFeriados, curso.IdCiudad.Value))
                {
                    cont = (cont + 1) % diasFrecuencia.Length;
                    fechaAsignar = fechaAsignar.AddDays(1);
                }
                else
                    break;
            }

            return fechaAsignar;
        }

        private bool FechaEsFeriadoPorCiudad(DateTime fecha, List<DatosFeriadoDTO> feriados, int idCiudad)
        {
            var listaFeriados = feriados.Where(s => s.IdCiudad == idCiudad).ToList(); //buscar todos los feriados de la ciudad
            foreach (DatosFeriadoDTO feriado in listaFeriados)
            {
                if (feriado.Frecuencia == 1 && //si feriado es unico
                    feriado.Dia.Year == fecha.Year &&
                    feriado.Dia.Month == fecha.Month &&
                    feriado.Dia.Day == fecha.Day)
                    return true;
                if (feriado.Frecuencia == 0 && //si feriado es anual
                    feriado.Dia.Month == fecha.Month &&
                    feriado.Dia.Day == fecha.Day)
                    return true;
            }
            return false;
        }

        private DateTime ObtenerProximoDiaSemana(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public object VerificarFecha(int idSesion , DateTime fecha)
        {
            var sesion = _repPespecificoSesion.FirstById(idSesion);
            fecha.AddHours(-5);

            DateTime FechaInicio = fecha, FechaFin = fecha.AddHours(Convert.ToDouble(sesion.Duracion));

            var expositor = _repExpositor.FirstBy(s => s.Id == sesion.IdExpositor);

            AmbienteBO ambiente = new AmbienteBO();

            if (sesion.IdAmbiente.HasValue)
                ambiente = _repAmbiente.FirstById(sesion.IdAmbiente.Value);
            else
                ambiente = null;

            var PEspecifico = _repPespecifico.FirstById(sesion.IdPespecifico);
            var ListaFeriados = _repFeriado.GetBy(s => (s.Frecuencia == 0 && s.Dia.Day == fecha.Day && s.Dia.Month == fecha.Month && s.IdTroncalCiudad == PEspecifico.IdCiudad) ||
                                                (s.Frecuencia == 1 && s.Dia.Day == fecha.Day && s.Dia.Month == fecha.Month && s.Dia.Year == fecha.Year && s.IdTroncalCiudad == PEspecifico.IdCiudad))
                                    .ToList();

            if (ListaFeriados.Count != 0)
                return new { Feriados = ListaFeriados };

            var ListaCrucesExpositor = _repPespecificoSesion.GetBy(s => s.Id != idSesion &&
                                                    ( (s.FechaHoraInicio <= FechaInicio && FechaInicio <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion)).AddSeconds(-1)) ||
                                                                (s.FechaHoraInicio.AddSeconds(1) <= FechaFin && FechaFin <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion)))))
                                                    .Where(s => expositor == null ? false : expositor.Id == s.IdExpositor
                                                    //{
                                                    //    if (expositor == null)
                                                    //        return false;

                                                    //    return expositor.Id == s.IdExpositor;

                                                    //}
                                                    );

            var ListaCrucesAmbiente = _repPespecificoSesion.GetBy(s => s.Id != idSesion
                                    &&( (s.FechaHoraInicio <= FechaInicio && FechaInicio <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion)).AddSeconds(-1)) ||
                                                (s.FechaHoraInicio.AddSeconds(1) <= FechaFin && FechaFin <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion)))))
                                    .Where(s => ambiente == null || ambiente.Virtual ? false : ambiente.Id == s.IdAmbiente
                                    //{
                                    //    if (ambiente == null || ambiente.Virtual)
                                    //        return false;

                                    //    return ambiente.Id == s.IdAmbiente;
                                    //}
            );

            var CrucesDatosCompletosExpositor = (from cruces in ListaCrucesExpositor
                                                 join pespecificos in _repPespecifico.GetAll() on cruces.IdPespecifico equals pespecificos.Id
                                                 join ambientes in _repAmbiente.GetAll() on cruces.IdAmbiente equals ambientes.Id into AmbientesB
                                                 join expositores in _repExpositor.GetAll() on cruces.IdExpositor equals expositores.Id into ExpositoresB
                                                 from Expositordata in ExpositoresB.DefaultIfEmpty()
                                                 from Ambientesdata in AmbientesB.DefaultIfEmpty()
                                                 select new
                                                 {
                                                     sesion_id = cruces.Id,
                                                     sesion_fecha_inicio = cruces.FechaHoraInicio.ToString(),
                                                     sesion_fecha_fin = cruces.FechaHoraInicio.AddHours(Convert.ToDouble(cruces.Duracion)).ToString(),
                                                     sesion_comentario = cruces.Comentario,
                                                     pespecifico_id = pespecificos.Id,
                                                     pespecifico_nombre = pespecificos.Nombre,
                                                     ambiente_id = Ambientesdata == null ? "_" : Ambientesdata.Id.ToString(),
                                                     ambiente_nombre = Ambientesdata == null ? "(No tiene Ambiente)" : Ambientesdata.Nombre,
                                                     expositor_Id = Expositordata == null ? 0 : Expositordata.Id,
                                                     expositor_nombre = Expositordata == null ? "(No tiene Expositor)" : Expositordata.PrimerNombre
                                                 }).ToList();

            var CrucesDatosCompletosAmbiente = (from cruces in ListaCrucesAmbiente
                                                join pespecificos in _repPespecifico.GetAll() on cruces.IdPespecifico equals pespecificos.Id
                                                join ambientes in _repAmbiente.GetAll() on cruces.IdAmbiente equals ambientes.Id into AmbientesB
                                                join expositores in _repExpositor.GetAll() on cruces.IdExpositor equals expositores.Id into ExpositoresB
                                                from Expositordata in ExpositoresB.DefaultIfEmpty()
                                                from Ambientesdata in AmbientesB.DefaultIfEmpty()
                                                select new
                                                {
                                                    sesion_id = cruces.Id,
                                                    sesion_fecha_inicio = cruces.FechaHoraInicio.ToString(),
                                                    sesion_fecha_fin = cruces.FechaHoraInicio.AddHours(Convert.ToDouble(cruces.Duracion)).ToString(),
                                                    sesion_comentario = cruces.Comentario,
                                                    pespecifico_id = pespecificos.Id,
                                                    pespecifico_nombre = pespecificos.Nombre,
                                                    ambiente_id = Ambientesdata == null ? "_" : Ambientesdata.Id.ToString(),
                                                    ambiente_nombre = Ambientesdata == null ? "(No tiene Ambiente)" : Ambientesdata.Nombre,
                                                    expositor_Id = Expositordata == null ? 0 : Expositordata.Id,
                                                    expositor_nombre = Expositordata == null ? "(No tiene Expositor)" : Expositordata.PrimerNombre
                                                }).ToList();

            return new
            {
                CrucesExpositor = CrucesDatosCompletosExpositor,
                CrucesAmbiente = CrucesDatosCompletosAmbiente
            };
        }

        public bool ActualizarFechaParaSesionRecorrerFechas( string usuario)
        {
            var ListaFeriados = _repFeriado.GetBy(s => s.Tipo == 0);

            List<TPespecificoSesion> listaSesiones ;

            var PEspecificoPadre = (from sesion in _repPespecificoSesion.GetAll()
                                                 join PEspeficoHijo in _repPespecificoPadrePespecificoHijo.GetAll() on sesion.IdPespecifico equals PEspeficoHijo.PespecificoHijoId
                                                 join PEspecifico in _repPespecifico.GetAll() on PEspeficoHijo.PespecificoPadreId equals PEspecifico.Id
                                                 where sesion.Id == this.Id
                                                 select PEspecifico).FirstOrDefault();

            if (PEspecificoPadre == null) //Tiene que ser Curso Individual
            {
                PEspecificoPadre = (from sesiones in _repPespecificoSesion.GetAll()
                                    join PEspecifico in _repPespecifico.GetAll() on sesiones.IdPespecifico equals PEspecifico.Id
                                    where sesiones.Id == this.Id
                                    select PEspecifico).FirstOrDefault();

                if (!PEspecificoPadre.CursoIndividual.Value)
                    throw new Exception("Algo anda mal! Este Curso no es individual");
            }

            var frecuenciaList = (from tablaFrecuencia in _repProgramaEspecificoFrecuencia.GetAll()
                                  where tablaFrecuencia.IdPespecifico == PEspecificoPadre.Id
                                  select tablaFrecuencia).ToList();

            if (frecuenciaList.Count != 1)
                throw new Exception("El Programa Especifico no tiene frecuencia o tiene mas de una");

            var frecuencia = frecuenciaList.FirstOrDefault();
            var FrecuenciaGeneral = _repFrecuencia.FirstBy(s => s.Id == frecuencia.IdFrecuencia);
            var detelleFrecuencia = (from frecueciaDetalle in _repPespecificoFrecuenciaDetalle.GetAll()
                                     where frecueciaDetalle.IdPespecificoFrecuencia == frecuencia.Id
                                     select frecueciaDetalle).ToList();
            var diasFrecuencia = detelleFrecuencia.Select(s => s.DiaSemana).ToArray();

            if (!PEspecificoPadre.CursoIndividual.Value)
                listaSesiones = (from cursos in _repPespecificoPadrePespecificoHijo.GetAll()
                                 join sesiones in _repPespecificoSesion.GetAll() on cursos.PespecificoHijoId equals sesiones.IdPespecifico
                                 where cursos.PespecificoPadreId == PEspecificoPadre.Id && sesiones.SesionAutoGenerada
                                 select sesiones).ToList();
            else
                listaSesiones = (from sesiones in _repPespecificoSesion.GetAll()
                                 where sesiones.IdPespecifico == PEspecificoPadre.Id && sesiones.SesionAutoGenerada
                                 select sesiones).ToList();

            var sesionesNoCambian = listaSesiones.Where(s => s.FechaHoraInicio <= this.FechaHoraInicio).OrderBy(s => s.FechaHoraInicio).ToList();
            var sesionesCambiar = listaSesiones.Where(s => s.FechaHoraInicio > this.FechaHoraInicio).OrderBy(s => s.FechaHoraInicio).ToList();



            //Obtenemos lista de sesiones con sus cursos
            List<EsquemaSesionDTO> estructuraSesiones = new List<EsquemaSesionDTO>();
            estructuraSesiones = (from sesiones in sesionesCambiar
                                  join TablaPEspecifico in _repPespecifico.GetAll() on sesiones.IdPespecifico equals TablaPEspecifico.Id
                                  select new EsquemaSesionDTO
                                  {
                                      Curso = TablaPEspecifico,
                                      Dia = (byte)sesiones.FechaHoraInicio.DayOfWeek,
                                      Duracion = sesiones.Duracion,
                                      FechaAsignar = sesiones.FechaHoraInicio,
                                      SesionId = sesiones.Id
                                  }).ToList();

            int DetalleFrecuenciaAModificar = (sesionesNoCambian.Count) % frecuencia.NroSesiones;
            var fechaAsignar = this.FechaHoraInicio;

            int diaAsigar = (int)fechaAsignar.DayOfWeek;

            for (int i = 0; i < diasFrecuencia.Length; i++)
            {
                if (diasFrecuencia[i] >= diaAsigar)
                {
                    DetalleFrecuenciaAModificar = i;
                    break;
                }
            }

            if (detelleFrecuencia[DetalleFrecuenciaAModificar].DiaSemana == (byte)this.FechaHoraInicio.DayOfWeek)
            {
                DateTime fechaNueva = this.FechaHoraInicio.AddHours(Convert.ToDouble(this.Duracion));
                DateTime fechasesion =
                    new DateTime(this.FechaHoraInicio.Year, this.FechaHoraInicio.Month, this.FechaHoraInicio.Day, detelleFrecuencia[DetalleFrecuenciaAModificar].HoraDia.Hours, detelleFrecuencia[DetalleFrecuenciaAModificar].HoraDia.Minutes, 0);
                if (fechaNueva.AddSeconds(1) > fechasesion)
                {
                    DetalleFrecuenciaAModificar = (DetalleFrecuenciaAModificar + 1) % diasFrecuencia.Length;
                }
            }


            DateTime fechaTemp = fechaAsignar;

            using (TransactionScope scope = new TransactionScope())
            {
                //Cambiar fecha la sesion que se seleccion

                for (int i = 0; i < estructuraSesiones.Count; i++)
                {
                    if ((DetalleFrecuenciaAModificar) % diasFrecuencia.Length == 0 && i != 0)
                    {
                        fechaAsignar = fechaTemp.AddDays(FrecuenciaGeneral.NumDias);
                        fechaTemp = fechaAsignar;
                    }
                    fechaAsignar = getFechaAsignar(estructuraSesiones[i].Curso, fechaAsignar, diasFrecuencia[DetalleFrecuenciaAModificar], diasFrecuencia, ListaFeriados);
                    
                    fechaAsignar = fechaAsignar.Date + detelleFrecuencia[(DetalleFrecuenciaAModificar + i) % diasFrecuencia.Length].HoraDia;
                    estructuraSesiones[i].FechaAsignar = fechaAsignar;

                    //Actualiza la sesion
                    var sesion = _repPespecificoSesion.FirstBy(s => s.Id == estructuraSesiones[i].SesionId);
                    sesion.FechaHoraInicio = estructuraSesiones[i].FechaAsignar.Value;
                    sesion.FechaModificacion = DateTime.Now;
                    sesion.UsuarioModificacion = usuario;

                }

                scope.Complete();
            }
            return true;
        }

        public DateTime getFechaAsignar(TPespecifico curso, DateTime fechaAsignar, int dia, byte[] diasFrecuencia, IEnumerable<FeriadoBO> ListaFeriados)
        {
            int cont = Array.IndexOf(diasFrecuencia, dia);
            DateTime fechaTemp;

            while (true)
            {
                fechaTemp = fechaAsignar;
                fechaAsignar = GetNextWeekday(fechaAsignar, (DayOfWeek)diasFrecuencia[cont]);

                if (fechaAsignar.DayOfYear == fechaTemp.DayOfYear && cont == (cont + 1) % diasFrecuencia.Length)
                {
                    fechaAsignar = fechaAsignar.AddDays(1);
                    continue;
                }

                //si es feriado continuar con la siguiente sesion
                if (FechaEsFeriadoByCiudad(fechaAsignar, ListaFeriados, curso.IdCiudad.Value))
                {
                    cont = (cont + 1) % diasFrecuencia.Length;
                    fechaAsignar = fechaAsignar.AddDays(1);
                }
                else
                    break;
            }

            return fechaAsignar;
        }

        private DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        private bool FechaEsFeriadoByCiudad(DateTime fecha, IEnumerable<FeriadoBO> feriados, int IdCiudad)
        {
            var listaFeriados = feriados.Where(s => s.IdTroncalCiudad == IdCiudad).ToList(); //buscar todos los feriados de la ciudad
            foreach (FeriadoBO feriado in listaFeriados)
            {
                if (feriado.Frecuencia == 1 && //si feriado es unico
                    feriado.Dia.Year == fecha.Year &&
                    feriado.Dia.Month == fecha.Month &&
                    feriado.Dia.Day == fecha.Day)
                    return true;
                if (feriado.Frecuencia == 0 && //si feriado es anual
                    feriado.Dia.Month == fecha.Month &&
                    feriado.Dia.Day == fecha.Day)
                    return true;
            }
            return false;
        }
    }

}
