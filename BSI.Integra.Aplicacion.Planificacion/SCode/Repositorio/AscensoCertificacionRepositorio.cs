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
    public class AscensoCertificacionRepositorio : BaseRepository<TAscensoCertificacion, AscensoCertificacionBO>
    {
        #region Metodos Base
        public AscensoCertificacionRepositorio() : base()
        {
        }
        public AscensoCertificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AscensoCertificacionBO> GetBy(Expression<Func<TAscensoCertificacion, bool>> filter)
        {
            IEnumerable<TAscensoCertificacion> listado = base.GetBy(filter);
            List<AscensoCertificacionBO> listadoBO = new List<AscensoCertificacionBO>();
            foreach (var itemEntidad in listado)
            {
                AscensoCertificacionBO objetoBO = Mapper.Map<TAscensoCertificacion, AscensoCertificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AscensoCertificacionBO FirstById(int id)
        {
            try
            {
                TAscensoCertificacion entidad = base.FirstById(id);
                AscensoCertificacionBO objetoBO = new AscensoCertificacionBO();
                Mapper.Map<TAscensoCertificacion, AscensoCertificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AscensoCertificacionBO FirstBy(Expression<Func<TAscensoCertificacion, bool>> filter)
        {
            try
            {
                TAscensoCertificacion entidad = base.FirstBy(filter);
                AscensoCertificacionBO objetoBO = Mapper.Map<TAscensoCertificacion, AscensoCertificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AscensoCertificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAscensoCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AscensoCertificacionBO> listadoBO)
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

        public bool Update(AscensoCertificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAscensoCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AscensoCertificacionBO> listadoBO)
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
        private void AsignacionId(TAscensoCertificacion entidad, AscensoCertificacionBO objetoBO)
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

        private TAscensoCertificacion MapeoEntidad(AscensoCertificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAscensoCertificacion entidad = new TAscensoCertificacion();
                entidad = Mapper.Map<AscensoCertificacionBO, TAscensoCertificacion>(objetoBO,
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
        /// Obtiene los AscensoCertificacion dado un IdAscenso
        /// </summary>
        /// <returns></returns>
        public List<AscensoCertificacionDTO> ObtenerTodoAscensoCertificacionPorIdAscenso(int IdAscenso)
        {
            try
            {
                List<AscensoCertificacionDTO> AscensoCertificacions = new List<AscensoCertificacionDTO>();
                var _query = string.Empty;
                _query = "Select Id,  IdAscenso, IdCertificacion FROM pla.T_AscensoCertificacion WHERE  Estado = 1 AND IdAscenso="+IdAscenso;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    AscensoCertificacions = JsonConvert.DeserializeObject<List<AscensoCertificacionDTO>>(_resultado);
                }
                return AscensoCertificacions;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		
    }


}
