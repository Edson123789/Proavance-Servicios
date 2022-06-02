using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class AgendaTipoUsuarioRepositorio : BaseRepository<TAgendaTipoUsuario, AgendaTipoUsuarioBO>
    {
        #region Metodos Base
        public AgendaTipoUsuarioRepositorio() : base()
        {
        }
        public AgendaTipoUsuarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AgendaTipoUsuarioBO> GetBy(Expression<Func<TAgendaTipoUsuario, bool>> filter)
        {
            IEnumerable<TAgendaTipoUsuario> listado = base.GetBy(filter);
            List<AgendaTipoUsuarioBO> listadoBO = new List<AgendaTipoUsuarioBO>();
            foreach (var itemEntidad in listado)
            {
                AgendaTipoUsuarioBO objetoBO = Mapper.Map<TAgendaTipoUsuario, AgendaTipoUsuarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AgendaTipoUsuarioBO FirstById(int id)
        {
            try
            {
                TAgendaTipoUsuario entidad = base.FirstById(id);
                AgendaTipoUsuarioBO objetoBO = new AgendaTipoUsuarioBO();
                Mapper.Map<TAgendaTipoUsuario, AgendaTipoUsuarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AgendaTipoUsuarioBO FirstBy(Expression<Func<TAgendaTipoUsuario, bool>> filter)
        {
            try
            {
                TAgendaTipoUsuario entidad = base.FirstBy(filter);
                AgendaTipoUsuarioBO objetoBO = Mapper.Map<TAgendaTipoUsuario, AgendaTipoUsuarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AgendaTipoUsuarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAgendaTipoUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AgendaTipoUsuarioBO> listadoBO)
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

        public bool Update(AgendaTipoUsuarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAgendaTipoUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AgendaTipoUsuarioBO> listadoBO)
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
        private void AsignacionId(TAgendaTipoUsuario entidad, AgendaTipoUsuarioBO objetoBO)
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

        private TAgendaTipoUsuario MapeoEntidad(AgendaTipoUsuarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAgendaTipoUsuario entidad = new TAgendaTipoUsuario();
                entidad = Mapper.Map<AgendaTipoUsuarioBO, TAgendaTipoUsuario>(objetoBO,
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
        /// Obtiene los tipos de usuarios de la agenda Id y Nombre para Filtro
        /// </summary>
        /// <returns></returns>
        public List<AgendaTipoUsuarioDTO> ObtenerTipoUsuarioFiltro()
        {
            try
            {
                List<AgendaTipoUsuarioDTO> agendaUsuarios = new List<AgendaTipoUsuarioDTO>();
                string _query = "SELECT Id, Nombre FROM com.T_AgendaTipoUsuario WHERE Estado=1";
                var usuariosAgendaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(usuariosAgendaDB) && !usuariosAgendaDB.Contains("[]"))
                {
                    agendaUsuarios = JsonConvert.DeserializeObject<List<AgendaTipoUsuarioDTO>>(usuariosAgendaDB);
                }
                return agendaUsuarios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
