using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Maestros.Repositorio
{
    /// Repositorio: BloqueHorario
    /// Autor: Gian Miranda
    /// Fecha: 10/02/2021
    /// <summary>
    /// Gestion de las peticiones a la tabla mkt.T_BloqueHorario
    /// </summary>
    public class BloqueHorarioRepositorio : BaseRepository<TBloqueHorario, BloqueHorarioBO>
    {
        #region Metodos Base
        public BloqueHorarioRepositorio() : base()
        {
        }
        public BloqueHorarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<BloqueHorarioBO> GetBy(Expression<Func<TBloqueHorario, bool>> filter)
        {
            IEnumerable<TBloqueHorario> listado = base.GetBy(filter);
            List<BloqueHorarioBO> listadoBO = new List<BloqueHorarioBO>();
            foreach (var itemEntidad in listado)
            {
                BloqueHorarioBO objetoBO = Mapper.Map<TBloqueHorario, BloqueHorarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public BloqueHorarioBO FirstById(int id)
        {
            try
            {
                TBloqueHorario entidad = base.FirstById(id);
                BloqueHorarioBO objetoBO = new BloqueHorarioBO();
                Mapper.Map<TBloqueHorario, BloqueHorarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public BloqueHorarioBO FirstBy(Expression<Func<TBloqueHorario, bool>> filter)
        {
            try
            {
                TBloqueHorario entidad = base.FirstBy(filter);
                BloqueHorarioBO objetoBO = Mapper.Map<TBloqueHorario, BloqueHorarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(BloqueHorarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TBloqueHorario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<BloqueHorarioBO> listadoBO)
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

        public bool Update(BloqueHorarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TBloqueHorario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<BloqueHorarioBO> listadoBO)
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
        private void AsignacionId(TBloqueHorario entidad, BloqueHorarioBO objetoBO)
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

        private TBloqueHorario MapeoEntidad(BloqueHorarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TBloqueHorario entidad = new TBloqueHorario();
                entidad = Mapper.Map<BloqueHorarioBO, TBloqueHorario>(objetoBO,
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

        public List<BloqueHorarioProcesarBicDTO> ObtenerPorIdConfiguracionBic(int IdConfiguracionBic)
        {

            try
            {
                List<BloqueHorarioProcesarBicDTO> lista = new List<BloqueHorarioProcesarBicDTO>();
                string _query = "Select Nombre, HoraInicio, HoraFin From mkt.V_ObtenerBloqueHorario Where IdConfiguracionBIC=@IdConfiguracionBic";
                var queryRespuesta = _dapper.QueryDapper(_query, new { IdConfiguracionBic = IdConfiguracionBic });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<BloqueHorarioProcesarBicDTO>>(queryRespuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
