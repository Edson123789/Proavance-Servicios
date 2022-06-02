using System;
using System.Collections.Generic;
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
    /// Repositorio: AsignacionOportunidadRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión y Asignación de Oportunidades
    /// </summary>
    public class AsignacionOportunidadRepositorio : BaseRepository<TAsignacionOportunidad, AsignacionOportunidadBO>
    {
        #region Metodos Base
        public AsignacionOportunidadRepositorio() : base()
        {
        }
        public AsignacionOportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsignacionOportunidadBO> GetBy(Expression<Func<TAsignacionOportunidad, bool>> filter)
        {
            IEnumerable<TAsignacionOportunidad> listado = base.GetBy(filter);
            List<AsignacionOportunidadBO> listadoBO = new List<AsignacionOportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                AsignacionOportunidadBO objetoBO = Mapper.Map<TAsignacionOportunidad, AsignacionOportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsignacionOportunidadBO FirstById(int id)
        {
            try
            {
                TAsignacionOportunidad entidad = base.FirstById(id);
                AsignacionOportunidadBO objetoBO = new AsignacionOportunidadBO();
                Mapper.Map<TAsignacionOportunidad, AsignacionOportunidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionOportunidadBO FirstBy(Expression<Func<TAsignacionOportunidad, bool>> filter)
        {
            try
            {
                TAsignacionOportunidad entidad = base.FirstBy(filter);
                AsignacionOportunidadBO objetoBO = Mapper.Map<TAsignacionOportunidad, AsignacionOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsignacionOportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsignacionOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsignacionOportunidadBO> listadoBO)
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

        public bool Update(AsignacionOportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsignacionOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsignacionOportunidadBO> listadoBO)
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
        private void AsignacionId(TAsignacionOportunidad entidad, AsignacionOportunidadBO objetoBO)
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

        private TAsignacionOportunidad MapeoEntidad(AsignacionOportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsignacionOportunidad entidad = new TAsignacionOportunidad();
                entidad = Mapper.Map<AsignacionOportunidadBO, TAsignacionOportunidad>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.AsignacionOportunidadLog != null)
                {
                    TAsignacionOportunidadLog entidadHijo = new TAsignacionOportunidadLog();
                    entidadHijo = Mapper.Map<AsignacionOportunidadLogBO, TAsignacionOportunidadLog>(objetoBO.AsignacionOportunidadLog,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TAsignacionOportunidadLog.Add(entidadHijo);
                }
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        ///Repositorio: AsignacionOportunidadRepositorio
        ///Autor: Edgar S.
        ///Fecha: 08/02/2021
        /// <summary>
        /// Obtiene una AsignacionOportunidaBO por idOportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> ObjetoBO: AsignacionOportunidadBO </returns>
        public AsignacionOportunidadBO ObtenerPorIdOportunidad(int idOportunidad) {
            try
            {
                AsignacionOportunidadBO asignacionOportunidad = new AsignacionOportunidadBO();
                var query = "SELECT TOP 1 * FROM mkt.T_AsignacionOportunidad WHERE  IdOportunidad = @idOportunidad";
                var registrosBD = _dapper.FirstOrDefault(query, new { idOportunidad });
                asignacionOportunidad = JsonConvert.DeserializeObject<AsignacionOportunidadBO>(registrosBD);
                return asignacionOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene la cantidad de oportunidades asignadas por una fecha determinada
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <param name="fechaAsignacion"></param>
        /// <returns></returns>
        public CantidadAsignacionOportunidadDTO ObtenerCantidadOportunidadesAsesor(int idAsesor, DateTime fechaAsignacion) {
            try
            {
                CantidadAsignacionOportunidadDTO cantidadAsignacion = new CantidadAsignacionOportunidadDTO();
                var _query = "SELECT Count(Id) AS Cantidad FROM mkt.V_TAsignacionOportunidad_ObtenerCantidadOportunidadesAsesor WHERE IdPersonal = @idAsesor AND CONVERT(DATE, FechaAsignacion) = Convert(date, @fechaAsignacion) AND Estado = 1 ";
                var registrosBD = _dapper.FirstOrDefault(_query, new { idAsesor, fechaAsignacion});
                cantidadAsignacion = JsonConvert.DeserializeObject<CantidadAsignacionOportunidadDTO>(registrosBD);
                return cantidadAsignacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public MaximaAsignacionAsesorCentroCostoDTO ObtenerMaximaAsignacionAsesor(int idAsesor)
        {
            try
            {
                MaximaAsignacionAsesorCentroCostoDTO maximaAsignacionAsesorCentroCosto = new MaximaAsignacionAsesorCentroCostoDTO();
                var _query = "SELECT AsignacionMax FROM mkt.V_TAsesorCentroCosto_ObtenerAsignacionMaxima WHERE IdPersonal = @idAsesor AND Estado = 1";
                var maximaAsignacionAsesorCentroCostoDB = _dapper.FirstOrDefault(_query, new { idAsesor});
                maximaAsignacionAsesorCentroCosto = JsonConvert.DeserializeObject<MaximaAsignacionAsesorCentroCostoDTO>(maximaAsignacionAsesorCentroCostoDB);
                return maximaAsignacionAsesorCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
