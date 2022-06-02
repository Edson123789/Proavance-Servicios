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
    public class AscensoAreaFormacionRepositorio : BaseRepository<TAscensoAreaFormacion, AscensoAreaFormacionBO>
    {
        #region Metodos Base
        public AscensoAreaFormacionRepositorio() : base()
        {
        }
        public AscensoAreaFormacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AscensoAreaFormacionBO> GetBy(Expression<Func<TAscensoAreaFormacion, bool>> filter)
        {
            IEnumerable<TAscensoAreaFormacion> listado = base.GetBy(filter);
            List<AscensoAreaFormacionBO> listadoBO = new List<AscensoAreaFormacionBO>();
            foreach (var itemEntidad in listado)
            {
                AscensoAreaFormacionBO objetoBO = Mapper.Map<TAscensoAreaFormacion, AscensoAreaFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AscensoAreaFormacionBO FirstById(int id)
        {
            try
            {
                TAscensoAreaFormacion entidad = base.FirstById(id);
                AscensoAreaFormacionBO objetoBO = new AscensoAreaFormacionBO();
                Mapper.Map<TAscensoAreaFormacion, AscensoAreaFormacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AscensoAreaFormacionBO FirstBy(Expression<Func<TAscensoAreaFormacion, bool>> filter)
        {
            try
            {
                TAscensoAreaFormacion entidad = base.FirstBy(filter);
                AscensoAreaFormacionBO objetoBO = Mapper.Map<TAscensoAreaFormacion, AscensoAreaFormacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AscensoAreaFormacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAscensoAreaFormacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AscensoAreaFormacionBO> listadoBO)
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

        public bool Update(AscensoAreaFormacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAscensoAreaFormacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AscensoAreaFormacionBO> listadoBO)
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
        private void AsignacionId(TAscensoAreaFormacion entidad, AscensoAreaFormacionBO objetoBO)
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

        private TAscensoAreaFormacion MapeoEntidad(AscensoAreaFormacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAscensoAreaFormacion entidad = new TAscensoAreaFormacion();
                entidad = Mapper.Map<AscensoAreaFormacionBO, TAscensoAreaFormacion>(objetoBO,
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
        /// Obtiene los AscensoAreaFormacion dado un IdAscenso
        /// </summary>
        /// <returns></returns>
        public List<AscensoAreaFormacionDTO> ObtenerTodoAscensoAreaFormacionPorIdAscenso(int IdAscenso)
        {
            try
            {
                List<AscensoAreaFormacionDTO> AscensoAreaFormacions = new List<AscensoAreaFormacionDTO>();
                var _query = string.Empty;
                _query = "Select Id,  IdAscenso, IdAreaFormacion FROM pla.T_AscensoAreaFormacion WHERE  Estado = 1 AND IdAscenso="+IdAscenso;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    AscensoAreaFormacions = JsonConvert.DeserializeObject<List<AscensoAreaFormacionDTO>>(_resultado);
                }
                return AscensoAreaFormacions;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		
    }


}
