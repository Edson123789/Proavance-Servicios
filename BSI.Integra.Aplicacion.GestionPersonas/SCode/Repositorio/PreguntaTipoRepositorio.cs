using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: Gestion de personas/PreguntaTipo
    /// Autor: Britsel Calluchi - Gian Miranda
    /// Fecha: 22/02/2021
    /// <summary>
    /// Repositorio para consultas de gp.T_PreguntaTipo
    /// </summary>
    public class PreguntaTipoRepositorio : BaseRepository<TPreguntaTipo, PreguntaTipoBO>
    {
        #region Metodos Base
        public PreguntaTipoRepositorio() : base()
        {
        }
        public PreguntaTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreguntaTipoBO> GetBy(Expression<Func<TPreguntaTipo, bool>> filter)
        {
            IEnumerable<TPreguntaTipo> listado = base.GetBy(filter);
            List<PreguntaTipoBO> listadoBO = new List<PreguntaTipoBO>();
            foreach (var itemEntidad in listado)
            {
                PreguntaTipoBO objetoBO = Mapper.Map<TPreguntaTipo, PreguntaTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreguntaTipoBO FirstById(int id)
        {
            try
            {
                TPreguntaTipo entidad = base.FirstById(id);
                PreguntaTipoBO objetoBO = new PreguntaTipoBO();
                Mapper.Map<TPreguntaTipo, PreguntaTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreguntaTipoBO FirstBy(Expression<Func<TPreguntaTipo, bool>> filter)
        {
            try
            {
                TPreguntaTipo entidad = base.FirstBy(filter);
                PreguntaTipoBO objetoBO = Mapper.Map<TPreguntaTipo, PreguntaTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreguntaTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreguntaTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreguntaTipoBO> listadoBO)
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

        public bool Update(PreguntaTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreguntaTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreguntaTipoBO> listadoBO)
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
        private void AsignacionId(TPreguntaTipo entidad, PreguntaTipoBO objetoBO)
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

        private TPreguntaTipo MapeoEntidad(PreguntaTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreguntaTipo entidad = new TPreguntaTipo();
                entidad = Mapper.Map<PreguntaTipoBO, TPreguntaTipo>(objetoBO,
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
        /// Repositorio: PreguntaTipoRepositorio
        /// Autor: Britsel Calluchi
        /// Fecha: 08/09/2021
        /// <summary>
        /// Obtiene tipos de preguntas habilitados
        /// </summary>
        /// <returns>List<PreguntaTipoDTO></returns>
        public List<PreguntaTipoDTO> ObtenerPreguntaTipo()
        {
            try
            {
                List<PreguntaTipoDTO> tipoPregunta = new List<PreguntaTipoDTO>();
                var query = "SELECT Id, Nombre, IdTipoRespuesta, TipoRespuesta FROM gp.V_ObtenerTipoPregunta";
                var dataDB = _dapper.QueryDapper(query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    tipoPregunta = JsonConvert.DeserializeObject<List<PreguntaTipoDTO>>(dataDB);
                }
                return tipoPregunta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
