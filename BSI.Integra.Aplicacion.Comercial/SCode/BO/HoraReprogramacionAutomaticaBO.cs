using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class HoraReprogramacionAutomaticaBO : BaseBO
    {
        public int IdOportunidad { get; set; }
        public int IdOcurrencia { get; set; }
        public string CodigoFase { get; set; }
        public int IdActividadCabecera { get; set; }
        public int IdTipoDato { get; set; }
        public int IdPersonal { get; set; }
        public int IdCategoriaOrigen { get; set; }

        private ReprogramacionCabeceraRepositorio _repReprogramacionCabecera;
        private TiempoLibreRepositorio _repTiempoLibre;
        private HoraBloqueadaRepositorio _repHoraBloqueda;

        public List<List<TimeSpan?>> personalHorario;
        public HoraReprogramacionAutomaticaBO()
        {

        }
        public HoraReprogramacionAutomaticaBO(integraDBContext integraDBContext)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repReprogramacionCabecera = new ReprogramacionCabeceraRepositorio(integraDBContext);
            _repTiempoLibre = new TiempoLibreRepositorio(integraDBContext);
            _repHoraBloqueda = new HoraBloqueadaRepositorio(integraDBContext);
        }

        /// <summary>
        /// Obtiene la Fecha para Programacion Automatica de una Actividad
        /// </summary>
        /// <param name="IdOportunidad"></param>
        /// <param name="CodigoFase"></param>
        /// <param name="IdOcurrencia"></param>
        /// <returns></returns>
        public string ObtenerFechaHoraActividadReprogramacionAutomatica()
        {
           var listRpta = GetFechaHoraReprogramacion();

            if (listRpta.Day == 1 && listRpta.Month == 5 && listRpta.Year == 2018)
            {
                listRpta = listRpta.AddDays(1);
            }
            if (listRpta.Day == 28 && listRpta.Month == 7 && listRpta.Year == 2018)
            {
                listRpta = listRpta.AddDays(2);
            }
            if (listRpta.Day == 25 && listRpta.Month == 12)
            {
                listRpta = listRpta.AddDays(1);
            }
            if (listRpta.Day == 1 && listRpta.Month == 1)
            {
                listRpta = listRpta.AddDays(1);
            }
            if (listRpta.Day == 1 && listRpta.Month == 4 && listRpta.Year == 2021)
            {
                listRpta = listRpta.AddDays(2);
            }
            if (listRpta.Day == 2 && listRpta.Month == 4 && listRpta.Year == 2021)
            {
                listRpta = listRpta.AddDays(1);
            }

            var Listarpta = listRpta.Year + "/" + listRpta.Month + "/" + listRpta.Day + " " + listRpta.Hour + ":" + listRpta.Minute + ":" + listRpta.Second;

            return Listarpta;
        }
        public string ObtenerFechaHoraReprogramacionAutomaticaOperaciones(int taboperaciones)
        {
           var listRpta = ObtenerFechaReprogramacionOperaciones(taboperaciones);

            if (listRpta.Day == 1 && listRpta.Month == 5 && listRpta.Year == 2018)
            {
                listRpta = listRpta.AddDays(1);
            }
            if (listRpta.Day == 28 && listRpta.Month == 7 && listRpta.Year == 2018)
            {
                listRpta = listRpta.AddDays(2);
            }
            if (listRpta.Day == 25 && listRpta.Month == 12)
            {
                listRpta = listRpta.AddDays(2);
            }
            if (listRpta.Day == 1 && listRpta.Month == 1)
            {
                listRpta = listRpta.AddDays(2);
            }
            if (listRpta.Day == 1 && listRpta.Month == 4 && listRpta.Year == 2021)
            {
                listRpta = listRpta.AddDays(4);
            }
            if (listRpta.Day == 2 && listRpta.Month == 4 && listRpta.Year == 2021)
            {
                listRpta = listRpta.AddDays(3);
            }
            var Listarpta = listRpta.Year + "/" + listRpta.Month + "/" + listRpta.Day + " " + listRpta.Hour + ":" + listRpta.Minute + ":" + listRpta.Second;

            return Listarpta;
        }
        public string ObtenerFechaHoraReprogramacionManualOperaciones(DateTime fecha)
        {
            var listRpta = ObtenerFechaReprogramacionManualOperaciones(fecha);
            var Listarpta = listRpta.Year + "-" + listRpta.Month + "-" + listRpta.Day + " " + listRpta.Hour + ":" + listRpta.Minute + ":" + listRpta.Second;

            return Listarpta;
        }
        /// <summary>
        /// Valida las restricciones para obtner la fecha de Reprogramacion Automatica
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="idActividadCabecera"></param>
        /// <param name="idTipoDato"></param>
        /// <param name="idCategoria"></param>
        /// <param name="codigoFase"></param>
        /// <param name="idOcurrencia"></param>
        /// <returns></returns>
        public DateTime GetFechaHoraReprogramacion()
        {
            var reprogramacion = _repReprogramacionCabecera.ObtenerCantidadIntervaloYReprogramacionPorDiaPermitida(this.IdActividadCabecera, this.IdCategoriaOrigen);
            var reprogramacionesAsesor = _repReprogramacionCabecera.ObtenerCantidadReprogramacionDelDiaPorAsesor(this.IdActividadCabecera, this.IdCategoriaOrigen, this.IdPersonal);
         
            if (reprogramacion != null)
            {
                if (this.CodigoFase == "BNC" || this.CodigoFase == "IT")
                    reprogramacion.IntervaloSigProgramacionMin = 240;
                if (this.CodigoFase == "IP" || this.CodigoFase == "PF" || this.CodigoFase == "IC")
                    reprogramacion.IntervaloSigProgramacionMin = 150;
            }
            if (reprogramacionesAsesor == null)
            {
                if (reprogramacion == null)
                    throw new Exception("No existe Intervalo para reprogramacion para esta Actividad y Categoria de dato");

                if (this.IdOcurrencia == 234) // SI ES 222. Dato para eliminar
                {
                    reprogramacion.IntervaloSigProgramacionMin = 1440;//24 HORAS
                }
                //nuevo calculo si pasa la media noche
                var flujoNormal = 1;
                if (DateTime.Now.Date == DateTime.Now.AddMinutes(reprogramacion.IntervaloSigProgramacionMin).Date)
                {
                    //si con la suma de  tiempo aun sigue siendo el mismo dia//caso por defecto
                    flujoNormal = 1;
                }
                else if (DateTime.Now.Date < DateTime.Now.AddMinutes(reprogramacion.IntervaloSigProgramacionMin).Date)
                {
                    flujoNormal = 0;
                }
                //fin nuevo calculo si pasa la media noche
                DateTime fecha = CalcularProgramacionAutomaticaByAsesor(DateTime.Now.AddMinutes(reprogramacion.IntervaloSigProgramacionMin), flujoNormal, reprogramacion.IntervaloSigProgramacionMin);

                fecha = CalcularHorario(fecha);

                return fecha;
            }
            else
            {
                if (reprogramacion == null)
                {
                    throw new Exception("No existe Intervalo para reprogramacion para esta Actividad y Categoria de dato");
                }
                else
                {
                    if (reprogramacionesAsesor.ReproDia < reprogramacion.MaxReproPorDia)
                    {
                        //valido 222. Dato para eliminar
                        if (this.IdOcurrencia == 234) // SI ES 222. Dato para eliminar
                        {
                            reprogramacion.IntervaloSigProgramacionMin = 1440;//24 HORAS
                        }

                        //nuevo calculo si pasa la media noche
                        var flujoNormal = 1;
                        if (DateTime.Now.Date == DateTime.Now.AddMinutes(reprogramacion.IntervaloSigProgramacionMin).Date)
                        {
                            //si con la suma de  tiempo aun sigue siendo el mismo dia//caso por defecto
                            flujoNormal = 1;
                        }
                        else if (DateTime.Now.Date < DateTime.Now.AddMinutes(reprogramacion.IntervaloSigProgramacionMin).Date)
                        {
                            flujoNormal = 0;
                        }
                        //fin nuevo calculo si pasa la media noche


                        var fecha = CalcularProgramacionAutomaticaByAsesor(DateTime.Now.AddMinutes(reprogramacion.IntervaloSigProgramacionMin),flujoNormal, reprogramacion.IntervaloSigProgramacionMin);

                        //aqui valido si la hora esta bien sino le trae la mas cercana
                        fecha = CalcularHorario(fecha);
                        //fin la validacion
                        return fecha;
                    }
                    else
                    {
                        throw new Exception("Ya no se pueden re-programar mas actividades");
                    }
                }
            }


        }
        public DateTime ObtenerFechaReprogramacionOperaciones(int taboperaciones)
        {
            ReprogramacionCabeceraRADTO reprogramacion = new ReprogramacionCabeceraRADTO();

            //var reprogramacion = _repReprogramacionCabecera.ObtenerCantidadIntervaloYReprogramacionPorDiaPermitida(this.IdActividadCabecera, this.IdCategoriaOrigen);
            //var reprogramacionesAsesor = _repReprogramacionCabecera.ObtenerCantidadReprogramacionDelDiaPorAsesor(this.IdActividadCabecera, this.IdCategoriaOrigen, this.IdPersonal);

            //reprogramacion.IntervaloSigProgramacionMin = 480;

            int dias = 0;
            switch (taboperaciones)
            {
                case 1:
                    dias = 2;//2 dias//Pago al Dia
                    break;
                case 2:
                    dias = 1;//1 dia//Pago Atrasado
                    break;
                case 3:
                    dias = 3;//2 dias//Seg.Academico
                    break;
                case 4:
                    dias = 3;//3 dias//Culminados
                    break;
                case 5:
                    dias = 3;//3 dias//Culminado Deudor
                    break;
                case 6:
                    dias = 3;//3 dias//Certificado
                    break;
                case 7:
                    dias = 3;//3 dias//Reservado sin Deuda
                    break;
                case 8:
                    dias = 3;//3 dias//Reservado con Deuda
                    break;
                case 9:
                    dias = 3;//3 dias//Retirado
                    break;
                case 10:
                    dias = 3;//3 dias//Abandonado
                    break;
                case 11:
                    dias = 1;//1 dia//Asig./Reasig.
                    break;
                case 12:
                    dias = 1;//1 dia//Prog.Manual
                    break;
                case 13:
                    dias = 1;//1 dia//Mensajes Recibidos
                    break;
                default:
                    dias = 1;
                    break;
            }


            DateTime fecha = CalcularProgramacionAutomaticaByAsesorOperaciones(dias);

            fecha = CalcularHorarioOperaciones(fecha);

            return fecha;
           
        }
        public DateTime ObtenerFechaReprogramacionManualOperaciones(DateTime fecha)
        {
            DateTime fechafinal = CalcularHorarioOperaciones(fecha);

            return fechafinal;

        }
        /// <summary>
        /// Valida el Horario del Asesor vs su timepo Libre del Dia
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="horaParaProgramar"></param>
        /// <returns></returns>
        public DateTime CalcularProgramacionAutomaticaByAsesor(DateTime horaParaProgramar,int flujoNormal,int IntervaloSigProgramacionMin)
        {

            List<List<TimeSpan?>> horario = personalHorario;

            var libreEntrada = _repTiempoLibre.ObtenerTiempoLibreTipoUno();
            var libreEntradaAlmuerzo = _repTiempoLibre.ObtenerTiempoLibreTipoDos();


            foreach (var item in horario)
            {
                if (item[0] != null)
                {
                    if (libreEntrada != null)
                    {
                        TimeSpan tiempo = new TimeSpan(0, libreEntrada.TiempoMin, 0);
                        item[0] = item[0].Value.Add(tiempo);
                    }
                }
                if (item[2] != null)
                {
                    if (libreEntradaAlmuerzo != null)
                    {
                        TimeSpan tiempo2 = new TimeSpan(0, libreEntradaAlmuerzo.TiempoMin, 0);
                        item[2] = item[2].Value.Add(tiempo2);
                    }
                }
            }

            DateTime resp;
            if (flujoNormal == 0)//caso Pase de la media noche
            {
                resp = CalcularProgramacionAutomaticaMediaNoche(horario, horaParaProgramar, IntervaloSigProgramacionMin);
            }
            else//sigue flujo normal
            {
                resp = CalcularProgramacionAutomatica(horario, horaParaProgramar);
            }

            

            return resp;
        }
        /// <summary>
        /// Valida el Horario del Asesor vs su timepo Libre del Dia
        /// </summary>
        /// <param name="dias"></param>

        /// <returns></returns>
        public DateTime CalcularProgramacionAutomaticaByAsesorOperaciones(int dias)
        {

            List<List<TimeSpan?>> horario = personalHorario;

            DateTime resp;
            resp = CalcularProgramacionAutomaticaOperaciones(horario, dias);


            return resp;
        }
        /// <summary>
        /// Obtiene la fecha disponible para la reprogramacion segun el horario del Asesor
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="horaParaReprogramar"></param>
        /// <returns></returns>
        public DateTime CalcularProgramacionAutomatica(List<List<TimeSpan?>> horario, DateTime horaParaReprogramar)
        {
            if ((horario[(int)horaParaReprogramar.DayOfWeek][0] <= horaParaReprogramar.TimeOfDay &&
                 horario[(int)horaParaReprogramar.DayOfWeek][1] >= horaParaReprogramar.TimeOfDay) ||
                (horario[(int)horaParaReprogramar.DayOfWeek][2] <= horaParaReprogramar.TimeOfDay &&
                 horario[(int)horaParaReprogramar.DayOfWeek][3] >= horaParaReprogramar.TimeOfDay))

                return horaParaReprogramar;

            TimeSpan resto = new TimeSpan(0, 0, 0);

            int cont = (int)horaParaReprogramar.DayOfWeek;

            while (true)
            {
                if (horario[cont][0].HasValue && horario[cont][1].HasValue)
                {
                    if (horario[cont][0] > horaParaReprogramar.TimeOfDay)
                    {
                        TimeSpan nuevaHora = horario[cont][0].Value + resto;
                        horaParaReprogramar = CalcularProgramacionAutomatica(horario, new DateTime(horaParaReprogramar.Year,
                                                                                                   horaParaReprogramar.Month,
                                                                                                   horaParaReprogramar.Day,
                                                                                                   nuevaHora.Hours,
                                                                                                   nuevaHora.Minutes,
                                                                                                   nuevaHora.Seconds));
                        break;
                    }

                    if (horario[cont][1].Value >= horaParaReprogramar.TimeOfDay)
                        break;
                    else
                        resto = horaParaReprogramar.TimeOfDay - horario[cont][1].Value;
                }

                if (horario[cont][2].HasValue && horario[cont][3].HasValue)
                {
                    if (horario[cont][2] > horaParaReprogramar.TimeOfDay)
                    {
                        TimeSpan nuevaHora = horario[cont][2].Value + resto;
                        horaParaReprogramar = CalcularProgramacionAutomatica(horario, new DateTime(horaParaReprogramar.Year,
                                                                                                   horaParaReprogramar.Month,
                                                                                                   horaParaReprogramar.Day,
                                                                                                   nuevaHora.Hours,
                                                                                                   nuevaHora.Minutes,
                                                                                                   nuevaHora.Seconds));
                        break;
                    }
                    if (horario[cont][3].Value >= horaParaReprogramar.TimeOfDay)
                        break;
                    else
                        resto = horaParaReprogramar.TimeOfDay - horario[cont][3].Value;

                }

                cont = (cont + 1) % horario.Count;
                horaParaReprogramar = horaParaReprogramar.AddDays(1);
                horaParaReprogramar = new DateTime(horaParaReprogramar.Year,
                                                   horaParaReprogramar.Month,
                                                   horaParaReprogramar.Day, 0, 0, 0);

            }

            return horaParaReprogramar;
        }
        /// <summary>
        /// Obtiene la fecha disponible para la reprogramacion segun el horario del Asesor pasada la media noche
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="horaParaReprogramar"></param>
        /// <returns></returns>
        public DateTime CalcularProgramacionAutomaticaMediaNoche(List<List<TimeSpan?>> horario, DateTime horaParaReprogramar,int IntervaloSigProgramacionMin)
        {
            if ((horario[(int)horaParaReprogramar.DayOfWeek][0] <= horaParaReprogramar.TimeOfDay &&
                 horario[(int)horaParaReprogramar.DayOfWeek][1] >= horaParaReprogramar.TimeOfDay) ||
                (horario[(int)horaParaReprogramar.DayOfWeek][2] <= horaParaReprogramar.TimeOfDay &&
                 horario[(int)horaParaReprogramar.DayOfWeek][3] >= horaParaReprogramar.TimeOfDay))
                    return horaParaReprogramar;

            TimeSpan? restantedia = horario[(int)horaParaReprogramar.Date.AddDays(-1).DayOfWeek][3] != null ? horario[(int)horaParaReprogramar.AddDays(-1).DayOfWeek][3] - DateTime.Now.TimeOfDay : horario[(int)horaParaReprogramar.AddDays(-1).DayOfWeek][1] - DateTime.Now.TimeOfDay;
            if (restantedia < new TimeSpan(0,0,0))
            {
                restantedia= new TimeSpan(0, 0, 0);
            }
            TimeSpan resto = new TimeSpan(0, IntervaloSigProgramacionMin, 0)- restantedia.Value;

            int cont = (int)horaParaReprogramar.DayOfWeek;

            while (true)
            {
                if (horario[cont][0].HasValue && horario[cont][1].HasValue)
                {
                    if (horario[cont][0] > horaParaReprogramar.TimeOfDay)
                    {
                        TimeSpan nuevaHora = horario[cont][0].Value + resto;
                        horaParaReprogramar = CalcularProgramacionAutomatica(horario, new DateTime(horaParaReprogramar.Year,
                                                                                                   horaParaReprogramar.Month,
                                                                                                   horaParaReprogramar.Day,
                                                                                                   nuevaHora.Hours,
                                                                                                   nuevaHora.Minutes,
                                                                                                   nuevaHora.Seconds));
                        break;
                    }

                    if (horario[cont][1].Value >= horaParaReprogramar.TimeOfDay)
                        break;
                    else
                        resto = horaParaReprogramar.TimeOfDay - horario[cont][1].Value;
                }

                if (horario[cont][2].HasValue && horario[cont][3].HasValue)
                {
                    if (horario[cont][2] > horaParaReprogramar.TimeOfDay)
                    {
                        TimeSpan nuevaHora = horario[cont][2].Value + resto;
                        horaParaReprogramar = CalcularProgramacionAutomatica(horario, new DateTime(horaParaReprogramar.Year,
                                                                                                   horaParaReprogramar.Month,
                                                                                                   horaParaReprogramar.Day,
                                                                                                   nuevaHora.Hours,
                                                                                                   nuevaHora.Minutes,
                                                                                                   nuevaHora.Seconds));
                        break;
                    }
                    if (horario[cont][3].Value >= horaParaReprogramar.TimeOfDay)
                        break;
                    else
                        resto = horaParaReprogramar.TimeOfDay - horario[cont][3].Value;

                }

                cont = (cont + 1) % horario.Count;
                horaParaReprogramar = horaParaReprogramar.AddDays(1);
                horaParaReprogramar = new DateTime(horaParaReprogramar.Year,
                                                   horaParaReprogramar.Month,
                                                   horaParaReprogramar.Day, 0, 0, 0);

            }

            return horaParaReprogramar;
        }
        /// <summary>
        /// Obtiene la fecha disponible para la reprogramacion segun el horario del Asesor pasada la media noche
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="horaParaReprogramar"></param>
        /// <returns></returns>
        public DateTime CalcularProgramacionAutomaticaOperaciones(List<List<TimeSpan?>> horario, int dias)
        {
            var estado = true;
            DateTime horaParaReprogramarfinal = new DateTime();
            while (estado)
            {
                if (horario[(int)DateTime.Now.AddDays(dias).DayOfWeek][0] != null)
                {

                    DateTime horaParaReprogramar = DateTime.Now.AddDays(dias);
                    horaParaReprogramarfinal = new DateTime(horaParaReprogramar.Year,
                                                   horaParaReprogramar.Month,
                                                   horaParaReprogramar.Day, 0, 0, 0);
                    estado = false;
                }
                else
                {
                    dias = dias + 1;
                }
            }
            return horaParaReprogramarfinal.Add(horario[(int)DateTime.Now.AddDays(dias).DayOfWeek][0].Value);

        }
        /// <summary>
        /// Obtiene la fecha disponible para la Reprogramacion Automatica validando las horas bloqueadas para el Asesor
        /// durante el dia
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public DateTime CalcularHorario(DateTime fecha)
        {
            int intervalo = 2;

            var horario = personalHorario;

            var horas_bloqueadas = _repHoraBloqueda.ObtenerHorasBloquedasReprogramacionPorAsesor(this.IdPersonal, fecha);

            TimeSpan tiempo_pregun = new TimeSpan(fecha.Hour, fecha.Minute, fecha.Second);

            if (horario[(int)fecha.DayOfWeek][0] <= tiempo_pregun && horario[(int)fecha.DayOfWeek][1] >= tiempo_pregun)
            {
                TimeSpan? hora_inicio = horario[(int)fecha.DayOfWeek][0];

                while (hora_inicio < horario[(int)fecha.DayOfWeek][1])
                {
                    TimeSpan tiempo = new TimeSpan(0, intervalo, 0);
                    hora_inicio = hora_inicio.Value.Add(tiempo);

                    if (hora_inicio > fecha.TimeOfDay)
                    {
                        var hora_bloq = horas_bloqueadas.Where(w => w.Fecha.Date == fecha.Date && w.Hora.Hour == hora_inicio.Value.Hours && w.Hora.Minute == hora_inicio.Value.Minutes).FirstOrDefault();

                        if (hora_bloq == null)
                        {
                            DateTime hora_final = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora_inicio.Value.Hours, hora_inicio.Value.Minutes, hora_inicio.Value.Seconds);
                            return hora_final;
                        }
                    }
                }
            }
            else
            {
                TimeSpan? hora_inicio = horario[(int)fecha.DayOfWeek][2];

                while (hora_inicio < horario[(int)fecha.DayOfWeek][3])
                {
                    TimeSpan tiempo = new TimeSpan(0, intervalo, 0);
                    hora_inicio = hora_inicio.Value.Add(tiempo);

                    if (hora_inicio > fecha.TimeOfDay)
                    {
                        var hora_bloq = horas_bloqueadas.Where(w => w.Fecha == fecha && w.Hora == fecha).FirstOrDefault();
                        if (hora_bloq == null)
                        {
                            DateTime hora_final = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora_inicio.Value.Hours, hora_inicio.Value.Minutes, hora_inicio.Value.Seconds);
                            return hora_final;
                        }
                    }
                }
            }
            return fecha;
        }
        

        /// <summary>
        /// Obtiene la fecha disponible para la Reprogramacion Automatica validando las horas bloqueadas para el Coordinador Operaciones
        /// durante el dia
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public DateTime CalcularHorarioOperaciones(DateTime fecha)
        {
            bool validadiaconespacio = true;
            int intervalo = 3;

            var horario = personalHorario;

            var horas_bloqueadas = _repHoraBloqueda.ObtenerHorasBloquedasReprogramacionPorAsesor(this.IdPersonal, fecha);

            TimeSpan tiempo_pregun = new TimeSpan(fecha.Hour, fecha.Minute, fecha.Second);

            int countControlarBucle = 0;
            while(horario[(int)fecha.DayOfWeek][0] == null && countControlarBucle<8)
            {
                fecha = fecha.AddDays(1);
                countControlarBucle++;
                horas_bloqueadas = _repHoraBloqueda.ObtenerHorasBloquedasReprogramacionPorAsesor(this.IdPersonal, fecha);
            }

            if (horario[(int)fecha.DayOfWeek][0] <= tiempo_pregun && horario[(int)fecha.DayOfWeek][1] >= tiempo_pregun)
            {
                TimeSpan? hora_inicio = horario[(int)fecha.DayOfWeek][0];

                while (hora_inicio < horario[(int)fecha.DayOfWeek][1])
                {
                    TimeSpan tiempo = new TimeSpan(0, intervalo, 0);
                    hora_inicio = hora_inicio.Value.Add(tiempo);

                    if (hora_inicio > fecha.TimeOfDay)
                    {
                        var hora_bloq = horas_bloqueadas.Where(w => w.Fecha.Date == fecha.Date && w.Hora.Hour == hora_inicio.Value.Hours && w.Hora.Minute == hora_inicio.Value.Minutes).FirstOrDefault();

                        if (hora_bloq == null)
                        {
                            DateTime hora_final = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora_inicio.Value.Hours, hora_inicio.Value.Minutes, hora_inicio.Value.Seconds);
                            validadiaconespacio = false;
                            return hora_final;
                        }
                    }
                }
            }
           
            TimeSpan? hora_inicio2 = horario[(int)fecha.DayOfWeek][2];

            while (hora_inicio2 < horario[(int)fecha.DayOfWeek][3])
            {
                TimeSpan tiempo = new TimeSpan(0, intervalo, 0);
                hora_inicio2 = hora_inicio2.Value.Add(tiempo);

                if (hora_inicio2 > fecha.TimeOfDay)
                {
                    var hora_bloq = horas_bloqueadas.Where(w => w.Fecha.Date == fecha.Date && w.Hora.Hour == hora_inicio2.Value.Hours && w.Hora.Minute == hora_inicio2.Value.Minutes).FirstOrDefault();

                    if (hora_bloq == null)
                    {
                        DateTime hora_final = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora_inicio2.Value.Hours, hora_inicio2.Value.Minutes, hora_inicio2.Value.Seconds);
                        validadiaconespacio = false;
                        return hora_final;
                    }
                }
            }
            //añadido para vovler a llamr al dia sgte
            if (validadiaconespacio == true)
            {
                return CalcularHorarioOperaciones(fecha.AddDays(1));
            }
            return fecha;
        }
    }
}
