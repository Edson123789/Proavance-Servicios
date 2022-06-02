using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class AreaCcRepositorio : BaseRepository<TAreaCc, AreaCcBO>
    {
        #region Metodos Base
        public AreaCcRepositorio() : base()
        {
        }
        public AreaCcRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AreaCcBO> GetBy(Expression<Func<TAreaCc, bool>> filter)
        {
            IEnumerable<TAreaCc> listado = base.GetBy(filter);
            List<AreaCcBO> listadoBO = new List<AreaCcBO>();
            foreach (var itemEntidad in listado)
            {
                AreaCcBO objetoBO = Mapper.Map<TAreaCc, AreaCcBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AreaCcBO FirstById(int id)
        {
            try
            {
                TAreaCc entidad = base.FirstById(id);
                AreaCcBO objetoBO = new AreaCcBO();
                Mapper.Map<TAreaCc, AreaCcBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AreaCcBO FirstBy(Expression<Func<TAreaCc, bool>> filter)
        {
            try
            {
                TAreaCc entidad = base.FirstBy(filter);
                AreaCcBO objetoBO = Mapper.Map<TAreaCc, AreaCcBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AreaCcBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAreaCc entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AreaCcBO> listadoBO)
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

        public bool Update(AreaCcBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAreaCc entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AreaCcBO> listadoBO)
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
        private void AsignacionId(TAreaCc entidad, AreaCcBO objetoBO)
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

        private TAreaCc MapeoEntidad(AreaCcBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAreaCc entidad = new TAreaCc();
                entidad = Mapper.Map<AreaCcBO, TAreaCc>(objetoBO,
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
        /// Obtiene lista de todas las areas de centro de costo
        /// </summary>
        /// <returns></returns>
        public List<AreaCentroCostoDTO> ObtenerAreaCentroCosto()
        {
            try
            {
                string _queryAreaCC = string.Empty;
                _queryAreaCC = "SELECT Id,Nombre,Concat(Id,'-',Codigo)as Codigo FROM pla.V_TAreaCC_ObtenerDatos WHERE Estado=1";
                var AreaCC = _dapper.QueryDapper(_queryAreaCC, null);
                return JsonConvert.DeserializeObject<List<AreaCentroCostoDTO>>(AreaCC);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<AreaCentroCostoListaDTO> ObtenerAreaCentroCostoLista(AreaFiltroCentroCostoDTO filtro)
        {
            try
            {
                string _queryAreaCentroCosto = "pla.SP_GetAllAreasBS";
                var AreaCentroCosto = _dapper.QuerySPDapper(_queryAreaCentroCosto, filtro);
                return JsonConvert.DeserializeObject<List<AreaCentroCostoListaDTO>>(AreaCentroCosto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
