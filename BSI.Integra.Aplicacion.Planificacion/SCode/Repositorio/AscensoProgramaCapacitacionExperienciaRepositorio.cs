using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class AscensoProgramaCapacitacionExperienciaRepositorio : BaseRepository<TAscensoProgramaCapacitacionExperiencia, AscensoProgramaCapacitacionExperienciaBO>
    {
        #region Metodos Base
        public AscensoProgramaCapacitacionExperienciaRepositorio() : base()
        {
        }
        public AscensoProgramaCapacitacionExperienciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AscensoProgramaCapacitacionExperienciaBO> GetBy(Expression<Func<TAscensoProgramaCapacitacionExperiencia, bool>> filter)
        {
            IEnumerable<TAscensoProgramaCapacitacionExperiencia> listado = base.GetBy(filter);
            List<AscensoProgramaCapacitacionExperienciaBO> listadoBO = new List<AscensoProgramaCapacitacionExperienciaBO>();
            foreach (var itemEntidad in listado)
            {
                AscensoProgramaCapacitacionExperienciaBO objetoBO = Mapper.Map<TAscensoProgramaCapacitacionExperiencia, AscensoProgramaCapacitacionExperienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AscensoProgramaCapacitacionExperienciaBO FirstById(int id)
        {
            try
            {
                TAscensoProgramaCapacitacionExperiencia entidad = base.FirstById(id);
                AscensoProgramaCapacitacionExperienciaBO objetoBO = new AscensoProgramaCapacitacionExperienciaBO();
                Mapper.Map<TAscensoProgramaCapacitacionExperiencia, AscensoProgramaCapacitacionExperienciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AscensoProgramaCapacitacionExperienciaBO FirstBy(Expression<Func<TAscensoProgramaCapacitacionExperiencia, bool>> filter)
        {
            try
            {
                TAscensoProgramaCapacitacionExperiencia entidad = base.FirstBy(filter);
                AscensoProgramaCapacitacionExperienciaBO objetoBO = Mapper.Map<TAscensoProgramaCapacitacionExperiencia, AscensoProgramaCapacitacionExperienciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AscensoProgramaCapacitacionExperienciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAscensoProgramaCapacitacionExperiencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AscensoProgramaCapacitacionExperienciaBO> listadoBO)
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

        public bool Update(AscensoProgramaCapacitacionExperienciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAscensoProgramaCapacitacionExperiencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AscensoProgramaCapacitacionExperienciaBO> listadoBO)
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
        private void AsignacionId(TAscensoProgramaCapacitacionExperiencia entidad, AscensoProgramaCapacitacionExperienciaBO objetoBO)
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

        private TAscensoProgramaCapacitacionExperiencia MapeoEntidad(AscensoProgramaCapacitacionExperienciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAscensoProgramaCapacitacionExperiencia entidad = new TAscensoProgramaCapacitacionExperiencia();
                entidad = Mapper.Map<AscensoProgramaCapacitacionExperienciaBO, TAscensoProgramaCapacitacionExperiencia>(objetoBO,
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
        /// Obtiene una lista de los ProgramasCapacidacionExperiencia (IDs) dado un Id de Ascenso
        /// </summary>
        /// <param name="IdAscenso"></param>
        /// <returns></returns>
        public List<AscensoProgramaCapacitacionExperienciaDTO> ObtenerProgramasCapacitacionExperienciaPorAscenso(int IdAscenso)
        {
            try
            {
                string _query = "SELECT Id, IdAscenso, IdProgramaCapacitacion, Contenido  FROM pla.T_AscensoProgramaCapacitacionExperiencia WHERE Estado=1 AND IdAscenso=" + IdAscenso;
                var RegistrosBO = _dapper.QueryDapper(_query, null);
                List<AscensoProgramaCapacitacionExperienciaDTO> listaAscensoProgramaCapacitacionExperienciaes = JsonConvert.DeserializeObject<List<AscensoProgramaCapacitacionExperienciaDTO>>(RegistrosBO);
                return listaAscensoProgramaCapacitacionExperienciaes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


    }
}
