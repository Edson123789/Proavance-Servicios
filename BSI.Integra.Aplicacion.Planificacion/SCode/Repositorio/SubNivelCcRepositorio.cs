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
    public class SubNivelCcRepositorio : BaseRepository<TSubNivelCc, SubNivelCcBO>
    {
        #region Metodos Base
        public SubNivelCcRepositorio() : base()
        {
        }
        public SubNivelCcRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SubNivelCcBO> GetBy(Expression<Func<TSubNivelCc, bool>> filter)
        {
            IEnumerable<TSubNivelCc> listado = base.GetBy(filter);
            List<SubNivelCcBO> listadoBO = new List<SubNivelCcBO>();
            foreach (var itemEntidad in listado)
            {
                SubNivelCcBO objetoBO = Mapper.Map<TSubNivelCc, SubNivelCcBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SubNivelCcBO FirstById(int id)
        {
            try
            {
                TSubNivelCc entidad = base.FirstById(id);
                SubNivelCcBO objetoBO = new SubNivelCcBO();
                Mapper.Map<TSubNivelCc, SubNivelCcBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SubNivelCcBO FirstBy(Expression<Func<TSubNivelCc, bool>> filter)
        {
            try
            {
                TSubNivelCc entidad = base.FirstBy(filter);
                SubNivelCcBO objetoBO = Mapper.Map<TSubNivelCc, SubNivelCcBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SubNivelCcBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSubNivelCc entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SubNivelCcBO> listadoBO)
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

        public bool Update(SubNivelCcBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSubNivelCc entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SubNivelCcBO> listadoBO)
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
        private void AsignacionId(TSubNivelCc entidad, SubNivelCcBO objetoBO)
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

        private TSubNivelCc MapeoEntidad(SubNivelCcBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSubNivelCc entidad = new TSubNivelCc();
                entidad = Mapper.Map<SubNivelCcBO, TSubNivelCc>(objetoBO,
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
        /// Obtiene lista de subniveles de centro de costos por id area centro de costo
        /// </summary>
        /// <param name="idAreaCC"></param>
        /// <returns></returns>
        public List<SubNivelCentroCostoDTO> ObtenerSubNivelCCPorAreaCC(int idAreaCC)
        {
            try
            {
                string _querySubNivel = string.Empty;
                _querySubNivel = "Select Id,Nombre,Codigo from pla.V_TSubNivelCC_ObtenerDatos where Estado=1 and IdAreaCC=@IdAreaCC";
                var SubNivelCC = _dapper.QueryDapper(_querySubNivel, new { IdAreaCC = idAreaCC });
                return JsonConvert.DeserializeObject<List<SubNivelCentroCostoDTO>>(SubNivelCC);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<SubNivelCentroCostoListaDTO> ObtenerSubNivelCentroCostoLista(FiltroSubNivelCentroCosto filtro)
        {
            try
            {
                string _querySubNivelCentroCosto = "pla.SP_GetAllSubNivel";
                var SubAreaCentroCosto = _dapper.QuerySPDapper(_querySubNivelCentroCosto, filtro);
                return JsonConvert.DeserializeObject<List<SubNivelCentroCostoListaDTO>>(SubAreaCentroCosto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
