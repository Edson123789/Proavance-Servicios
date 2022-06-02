using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AscensoExperienciaCargoIndustriaRepositorio : BaseRepository<TAscensoExperienciaCargoIndustria, AscensoExperienciaCargoIndustriaBO>
    {
        #region Metodos Base
        public AscensoExperienciaCargoIndustriaRepositorio() : base()
        {
        }
        public AscensoExperienciaCargoIndustriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AscensoExperienciaCargoIndustriaBO> GetBy(Expression<Func<TAscensoExperienciaCargoIndustria, bool>> filter)
        {
            IEnumerable<TAscensoExperienciaCargoIndustria> listado = base.GetBy(filter);
            List<AscensoExperienciaCargoIndustriaBO> listadoBO = new List<AscensoExperienciaCargoIndustriaBO>();
            foreach (var itemEntidad in listado)
            {
                AscensoExperienciaCargoIndustriaBO objetoBO = Mapper.Map<TAscensoExperienciaCargoIndustria, AscensoExperienciaCargoIndustriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AscensoExperienciaCargoIndustriaBO FirstById(int id)
        {
            try
            {
                TAscensoExperienciaCargoIndustria entidad = base.FirstById(id);
                AscensoExperienciaCargoIndustriaBO objetoBO = new AscensoExperienciaCargoIndustriaBO();
                Mapper.Map<TAscensoExperienciaCargoIndustria, AscensoExperienciaCargoIndustriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AscensoExperienciaCargoIndustriaBO FirstBy(Expression<Func<TAscensoExperienciaCargoIndustria, bool>> filter)
        {
            try
            {
                TAscensoExperienciaCargoIndustria entidad = base.FirstBy(filter);
                AscensoExperienciaCargoIndustriaBO objetoBO = Mapper.Map<TAscensoExperienciaCargoIndustria, AscensoExperienciaCargoIndustriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AscensoExperienciaCargoIndustriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAscensoExperienciaCargoIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AscensoExperienciaCargoIndustriaBO> listadoBO)
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

        public bool Update(AscensoExperienciaCargoIndustriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAscensoExperienciaCargoIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AscensoExperienciaCargoIndustriaBO> listadoBO)
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
        private void AsignacionId(TAscensoExperienciaCargoIndustria entidad, AscensoExperienciaCargoIndustriaBO objetoBO)
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

        private TAscensoExperienciaCargoIndustria MapeoEntidad(AscensoExperienciaCargoIndustriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAscensoExperienciaCargoIndustria entidad = new TAscensoExperienciaCargoIndustria();
                entidad = Mapper.Map<AscensoExperienciaCargoIndustriaBO, TAscensoExperienciaCargoIndustria>(objetoBO,
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
        /// Obtiene los AscensoExperienciaCargoIndustrias dado un Id de Ascenso
        /// </summary>
        /// <returns></returns>
        public List<AscensoExperienciaCargoIndustriaDTO> ObtenerTodoAscensoExperienciaCargoIndustriasIndustriaPorIdAscenso(int IdAscenso)
        {
            try
            {
                List<AscensoExperienciaCargoIndustriaDTO> AscensoExperienciaCargoIndustrias = new List<AscensoExperienciaCargoIndustriaDTO>();
                var _query = string.Empty;
                _query = "Select Id, IdAscenso, AniosExperiencia, IdCargo, IdIndustria, IdAreaTrabajo, DescripcionPuestoAnterior FROM pla.T_AscensoExperienciaCargoIndustria WHERE  Estado = 1 AND IdAscenso="+IdAscenso;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    AscensoExperienciaCargoIndustrias = JsonConvert.DeserializeObject<List<AscensoExperienciaCargoIndustriaDTO>>(_resultado);
                }
                return AscensoExperienciaCargoIndustrias;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		
    }


}
