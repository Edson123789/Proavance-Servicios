using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class SentinelSueldoPorIndustriaDataDinamicoRepositorio : BaseRepository<TSentinelSueldoPorIndustriaDataDinamico, SentinelSueldoPorIndustriaDataDinamicoBO>
    {
        #region Metodos Base
        public SentinelSueldoPorIndustriaDataDinamicoRepositorio() : base()
        {
        }
        public SentinelSueldoPorIndustriaDataDinamicoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSueldoPorIndustriaDataDinamicoBO> GetBy(Expression<Func<TSentinelSueldoPorIndustriaDataDinamico, bool>> filter)
        {
            IEnumerable<TSentinelSueldoPorIndustriaDataDinamico> listado = base.GetBy(filter);
            List<SentinelSueldoPorIndustriaDataDinamicoBO> listadoBO = new List<SentinelSueldoPorIndustriaDataDinamicoBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSueldoPorIndustriaDataDinamicoBO objetoBO = Mapper.Map<TSentinelSueldoPorIndustriaDataDinamico, SentinelSueldoPorIndustriaDataDinamicoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSueldoPorIndustriaDataDinamicoBO FirstById(int id)
        {
            try
            {
                TSentinelSueldoPorIndustriaDataDinamico entidad = base.FirstById(id);
                SentinelSueldoPorIndustriaDataDinamicoBO objetoBO = new SentinelSueldoPorIndustriaDataDinamicoBO();
                Mapper.Map<TSentinelSueldoPorIndustriaDataDinamico, SentinelSueldoPorIndustriaDataDinamicoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSueldoPorIndustriaDataDinamicoBO FirstBy(Expression<Func<TSentinelSueldoPorIndustriaDataDinamico, bool>> filter)
        {
            try
            {
                TSentinelSueldoPorIndustriaDataDinamico entidad = base.FirstBy(filter);
                SentinelSueldoPorIndustriaDataDinamicoBO objetoBO = Mapper.Map<TSentinelSueldoPorIndustriaDataDinamico, SentinelSueldoPorIndustriaDataDinamicoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSueldoPorIndustriaDataDinamicoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSueldoPorIndustriaDataDinamico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSueldoPorIndustriaDataDinamicoBO> listadoBO)
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

        public bool Update(SentinelSueldoPorIndustriaDataDinamicoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSueldoPorIndustriaDataDinamico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSueldoPorIndustriaDataDinamicoBO> listadoBO)
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
        private void AsignacionId(TSentinelSueldoPorIndustriaDataDinamico entidad, SentinelSueldoPorIndustriaDataDinamicoBO objetoBO)
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

        private TSentinelSueldoPorIndustriaDataDinamico MapeoEntidad(SentinelSueldoPorIndustriaDataDinamicoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSueldoPorIndustriaDataDinamico entidad = new TSentinelSueldoPorIndustriaDataDinamico();
                entidad = Mapper.Map<SentinelSueldoPorIndustriaDataDinamicoBO, TSentinelSueldoPorIndustriaDataDinamico>(objetoBO,
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

        public SentinelSueldoPorIndustriaValorDTO ObtenerValorSueldoIndustria(int idCargo, int idIndustria, int idTamanio)
        {
            try
            {
                string _querysueldosporindustriadatadinamico = "Select Id,Valor from com.V_TSentinelSueldoPorIndustriaDataDinamico_Valor Where IdCargo=@IdCargo and IdIndustria=@IdIndustria and  IdTamanioEmpresa=@IdTamanio";
                var querysueldosporindustriadatadinamico = _dapper.FirstOrDefault(_querysueldosporindustriadatadinamico, new { IdCargo = idCargo, IdIndustria = idIndustria, IdTamanio = idTamanio });
                return JsonConvert.DeserializeObject<SentinelSueldoPorIndustriaValorDTO>(querysueldosporindustriadatadinamico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

    }
}
