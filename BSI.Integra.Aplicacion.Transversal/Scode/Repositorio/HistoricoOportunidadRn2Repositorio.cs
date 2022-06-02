
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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class HistoricoOportunidadRn2Repositorio : BaseRepository<THistoricoOportunidadRn2, HistoricoOportunidadRn2BO>
    {
        #region Metodos Base
        public HistoricoOportunidadRn2Repositorio() : base()
        {
        }
        public HistoricoOportunidadRn2Repositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<HistoricoOportunidadRn2BO> GetBy(Expression<Func<THistoricoOportunidadRn2, bool>> filter)
        {
            IEnumerable<THistoricoOportunidadRn2> listado = base.GetBy(filter);
            List<HistoricoOportunidadRn2BO> listadoBO = new List<HistoricoOportunidadRn2BO>();
            foreach (var itemEntidad in listado)
            {
                HistoricoOportunidadRn2BO objetoBO = Mapper.Map<THistoricoOportunidadRn2, HistoricoOportunidadRn2BO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public HistoricoOportunidadRn2BO FirstById(int id)
        {
            try
            {
                THistoricoOportunidadRn2 entidad = base.FirstById(id);
                HistoricoOportunidadRn2BO objetoBO = new HistoricoOportunidadRn2BO();
                Mapper.Map<THistoricoOportunidadRn2, HistoricoOportunidadRn2BO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public HistoricoOportunidadRn2BO FirstBy(Expression<Func<THistoricoOportunidadRn2, bool>> filter)
        {
            try
            {
                THistoricoOportunidadRn2 entidad = base.FirstBy(filter);
                HistoricoOportunidadRn2BO objetoBO = Mapper.Map<THistoricoOportunidadRn2, HistoricoOportunidadRn2BO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(HistoricoOportunidadRn2BO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                THistoricoOportunidadRn2 entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<HistoricoOportunidadRn2BO> listadoBO)
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

        public bool Update(HistoricoOportunidadRn2BO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                THistoricoOportunidadRn2 entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<HistoricoOportunidadRn2BO> listadoBO)
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
        private void AsignacionId(THistoricoOportunidadRn2 entidad, HistoricoOportunidadRn2BO objetoBO)
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

        private THistoricoOportunidadRn2 MapeoEntidad(HistoricoOportunidadRn2BO objetoBO)
        {
            try
            {
                //crea la entidad padre
                THistoricoOportunidadRn2 entidad = new THistoricoOportunidadRn2();
                entidad = Mapper.Map<HistoricoOportunidadRn2BO, THistoricoOportunidadRn2>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.HistoricoDetalleOportunidadRn2Inicio != null)
                {
                    THistoricoDetalleOportunidadRn2 entidadHijo = new THistoricoDetalleOportunidadRn2();
                    entidadHijo = Mapper.Map<HistoricoDetalleOportunidadRn2BO, THistoricoDetalleOportunidadRn2>(objetoBO.HistoricoDetalleOportunidadRn2Inicio,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.THistoricoDetalleOportunidadRn2.Add(entidadHijo);
                }
                if (objetoBO.HistoricoDetalleOportunidadRn2 != null)
                {
                    THistoricoDetalleOportunidadRn2 entidadHijo = new THistoricoDetalleOportunidadRn2();
                    entidadHijo = Mapper.Map<HistoricoDetalleOportunidadRn2BO, THistoricoDetalleOportunidadRn2>(objetoBO.HistoricoDetalleOportunidadRn2,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.THistoricoDetalleOportunidadRn2.Add(entidadHijo);

                }
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Inserta todos los nuevos rn2 que no existen en la tabla
        /// </summary>
        /// <returns></returns>
        public int InsertaNuevosRN2()
        {
            try
            {
                var resultado = 0;
                var nuevos = _dapper.QuerySPFirstOrDefault("com.SP_InsertarNuevosRN2", null);
                if (!string.IsNullOrEmpty(nuevos) && !nuevos.Contains("[]"))
                {
                    var respuesta = JsonConvert.DeserializeObject<Dictionary<string, int>>(nuevos);
                    resultado = respuesta.Select(x => x.Value).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// Procesa todos los rn2 segun el rango de fecha para su cierre o programacion de aqui a 30 dias
        /// </summary>
        /// <returns></returns>
        public TotalRN2OportunidadDTO ProcesarProgramadosRN2(FiltroFechaOportunidadRN2DTO fechas)
        {
            try
            {
                TotalRN2OportunidadDTO resultado = new TotalRN2OportunidadDTO();
                var nuevos = _dapper.QuerySPFirstOrDefault("com.SP_CalcularRn2", new {
                    fechaInicio = fechas.FechaInicio,
                    fechaFin = fechas.FechaFin
                });
                if (!string.IsNullOrEmpty(nuevos) && !nuevos.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<TotalRN2OportunidadDTO>(nuevos);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// Obtiene todos los RN2 segun una fecha Programada
        /// </summary>
        /// <returns></returns>
        public List<DataOportunidadRN2DTO> ObtenerRN2PorFechaProgramada(FiltroFechaOportunidadRN2DTO fechas)
        {
            try
            {
                List<DataOportunidadRN2DTO> respuesta = new List<DataOportunidadRN2DTO>();
                var nuevos = _dapper.QuerySPDapper("com.SP_ObtenerOportunidadRN2", new
                {
                    fechaInicio = fechas.FechaInicio,
                    fechaFin = fechas.FechaFin
                });
                if (!string.IsNullOrEmpty(nuevos) && !nuevos.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<DataOportunidadRN2DTO>>(nuevos);
                   
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
