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
    public class AscensoProgramaCapacitacionRepositorio : BaseRepository<TAscensoProgramaCapacitacion, AscensoProgramaCapacitacionBO>
    {
        #region Metodos Base
        public AscensoProgramaCapacitacionRepositorio() : base()
        {
        }
        public AscensoProgramaCapacitacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AscensoProgramaCapacitacionBO> GetBy(Expression<Func<TAscensoProgramaCapacitacion, bool>> filter)
        {
            IEnumerable<TAscensoProgramaCapacitacion> listado = base.GetBy(filter);
            List<AscensoProgramaCapacitacionBO> listadoBO = new List<AscensoProgramaCapacitacionBO>();
            foreach (var itemEntidad in listado)
            {
                AscensoProgramaCapacitacionBO objetoBO = Mapper.Map<TAscensoProgramaCapacitacion, AscensoProgramaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AscensoProgramaCapacitacionBO FirstById(int id)
        {
            try
            {
                TAscensoProgramaCapacitacion entidad = base.FirstById(id);
                AscensoProgramaCapacitacionBO objetoBO = new AscensoProgramaCapacitacionBO();
                Mapper.Map<TAscensoProgramaCapacitacion, AscensoProgramaCapacitacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AscensoProgramaCapacitacionBO FirstBy(Expression<Func<TAscensoProgramaCapacitacion, bool>> filter)
        {
            try
            {
                TAscensoProgramaCapacitacion entidad = base.FirstBy(filter);
                AscensoProgramaCapacitacionBO objetoBO = Mapper.Map<TAscensoProgramaCapacitacion, AscensoProgramaCapacitacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AscensoProgramaCapacitacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAscensoProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AscensoProgramaCapacitacionBO> listadoBO)
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

        public bool Update(AscensoProgramaCapacitacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAscensoProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AscensoProgramaCapacitacionBO> listadoBO)
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
        private void AsignacionId(TAscensoProgramaCapacitacion entidad, AscensoProgramaCapacitacionBO objetoBO)
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

        private TAscensoProgramaCapacitacion MapeoEntidad(AscensoProgramaCapacitacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAscensoProgramaCapacitacion entidad = new TAscensoProgramaCapacitacion();
                entidad = Mapper.Map<AscensoProgramaCapacitacionBO, TAscensoProgramaCapacitacion>(objetoBO,
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
        /// Obtiene una lista de los ProgramasCapacidacion (IDs) dado un Id de Ascenso
        /// </summary>
        /// <param name="IdAscenso"></param>
        /// <returns></returns>
        public List<AscensoProgramaCapacitacionDTO> ObtenerProgramasCapacitacionPorAscenso(int IdAscenso)
        {
            try
            {
                string _query = "SELECT Id, IdAscenso, IdProgramaCapacitacion, Contenido  FROM pla.T_AscensoProgramaCapacitacion WHERE Estado=1 AND IdAscenso=" + IdAscenso;
                var RegistrosBO = _dapper.QueryDapper(_query, null);
                List<AscensoProgramaCapacitacionDTO> listaAscensoProgramaCapacitaciones = JsonConvert.DeserializeObject<List<AscensoProgramaCapacitacionDTO>>(RegistrosBO);
                return listaAscensoProgramaCapacitaciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
