using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class LlamadaActividadRepositorio : BaseRepository<TLlamadaActividad, LlamadaActividadBO>
    {
        #region Metodos Base
        public LlamadaActividadRepositorio() : base()
        {
        }
        public LlamadaActividadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LlamadaActividadBO> GetBy(Expression<Func<TLlamadaActividad, bool>> filter)
        {
            IEnumerable<TLlamadaActividad> listado = base.GetBy(filter).ToList();
            List<LlamadaActividadBO> listadoBO = new List<LlamadaActividadBO>();
            foreach (var itemEntidad in listado)
            {
                LlamadaActividadBO objetoBO = Mapper.Map<TLlamadaActividad, LlamadaActividadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LlamadaActividadBO FirstById(int id)
        {
            try
            {
                TLlamadaActividad entidad = base.FirstById(id);
                LlamadaActividadBO objetoBO = Mapper.Map<TLlamadaActividad, LlamadaActividadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LlamadaActividadBO FirstBy(Expression<Func<TLlamadaActividad, bool>> filter)
        {
            try
            {
                TLlamadaActividad entidad = base.FirstBy(filter);
                LlamadaActividadBO objetoBO = Mapper.Map<TLlamadaActividad, LlamadaActividadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LlamadaActividadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLlamadaActividad entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<LlamadaActividadBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(LlamadaActividadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLlamadaActividad entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<LlamadaActividadBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TLlamadaActividad entidad, LlamadaActividadBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TLlamadaActividad MapeoEntidad(LlamadaActividadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLlamadaActividad entidad = new TLlamadaActividad();
                entidad = Mapper.Map<LlamadaActividadBO, TLlamadaActividad>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene las llamdas realizadas por el asesor para actividad luego de programar la actividad 
        /// </summary>
        /// <param name="idActividad"></param>
        /// <param name="idAsignado"></param>
        /// <param name="fechaFinLLamada"></param>
        /// <returns></returns>
        public List<LlamadaTrescxDTO> ObtenerLLamadasPorActividad(int idActividad, int idAsignado, DateTime fechaFinLLamada)
        {
            try
            {
                string _querygetLLamadas = "select cx.Id, cx.FechaCreacion, case when tiempoContesto_trescx = 0 then tiempoTimbrado_trescx else tiempoContesto_trescx end TiempoReal  from com.T_LlamadaActividad act inner join gp.T_Personal per on act.IdAsesor = per.Id " +
                                     "inner join com.V_ObtenerTresCX_Registrollamadas cx on cx.FromDn = per.Anexo3CX and cx.TimeEnd >= act.FechaInicioLlamada and cx.TimeEnd <= @FechaFinLlamada " +
                                     "WHERE act.IdActividadDetalle = @IdActividadDetalle AND act.IdAsesor =IdAsesor";

                var queryGetLLamadas = _dapper.QueryDapper(_querygetLLamadas, new { IdActividadDetalle =idActividad, IdAsesor =idAsignado, FechaFinLlamada =fechaFinLLamada});
                return JsonConvert.DeserializeObject<List<LlamadaTrescxDTO>>(queryGetLLamadas);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene toda la Llamada Actividad, para generar el constructor por IdActividad
        /// </summary>
        /// <param name="idActividad"></param>
        /// <returns></returns>
        public LlamadaActividadBO ObtenerLlamadaActividadPorActividad(int idActividad)
        {
            try
            {
                string _querygetLLamadas = "select * from com.T_LlamadaActividad where IdActividadDetalle = @IdActividad";
                var queryGetLLamadas = _dapper.FirstOrDefault(_querygetLLamadas,new { IdActividad=idActividad });
                return JsonConvert.DeserializeObject<LlamadaActividadBO>(queryGetLLamadas);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }
        /// <summary>
        /// Valida si exite en la tabla un registro con el mismo Id Actividad Detalle, para no volver insertar con el mismo registro
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <param name="idActividadDetalle"></param>
        /// <returns></returns>
        public bool ValidarExisteLlamadaPorActividad(int idAsesor, int idActividadDetalle)
        {
            try
            {
                string _query = "SELECT Id FROM com.V_TLlamadaActividad_ValidarPrimeraLlamada " +
                " WHERE IdAsesor = @IdAsesor and IdActividadDetalle = @IdActividadDetalle and EstadoProgramado = 0";
                var _llamada = _dapper.FirstOrDefault(_query , new { IdAsesor=idAsesor, IdActividadDetalle =idActividadDetalle });
                if (_llamada.Equals("null"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
