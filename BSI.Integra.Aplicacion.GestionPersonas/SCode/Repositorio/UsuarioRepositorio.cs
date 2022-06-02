using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: UsuarioRepositorio
    /// Autor: Nelson Huaman - Ansoli Espinoza - Edgar Serruto.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Usuarios
    /// </summary>
    public class UsuarioRepositorio : BaseRepository<TUsuario, UsuarioBO>
    {

        #region Metodos Base
        public UsuarioRepositorio() : base()
        {
        }
        public UsuarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<UsuarioBO> GetBy(Expression<Func<TUsuario, bool>> filter)
        {
            IEnumerable<TUsuario> listado = base.GetBy(filter);
            List<UsuarioBO> listadoBO = new List<UsuarioBO>();
            foreach (var itemEntidad in listado)
            {
                UsuarioBO objetoBO = Mapper.Map<TUsuario, UsuarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }



      
        public UsuarioBO FirstById(int id)
        {
            try
            {
                TUsuario entidad = base.FirstById(id);
                UsuarioBO objetoBO = new UsuarioBO();
                Mapper.Map<TUsuario, UsuarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public UsuarioBO FirstBy(Expression<Func<TUsuario, bool>> filter)
        {
            try
            {
                TUsuario entidad = base.FirstBy(filter);
                UsuarioBO objetoBO = Mapper.Map<TUsuario, UsuarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(UsuarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<UsuarioBO> listadoBO)
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

        public bool Update(UsuarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<UsuarioBO> listadoBO)
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
        private void AsignacionId(TUsuario entidad, UsuarioBO objetoBO)
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


        //CORREGIR IDMIGRACION
        private TUsuario MapeoEntidad(UsuarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TUsuario entidad = new TUsuario();
                entidad = Mapper.Map<UsuarioBO, TUsuario>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<UsuarioBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TUsuario, bool>>> filters, Expression<Func<TUsuario, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TUsuario> listado = base.GetFiltered(filters, orderBy, ascending);
            List<UsuarioBO> listadoBO = new List<UsuarioBO>();

            foreach (var itemEntidad in listado)
            {
                UsuarioBO objetoBO = Mapper.Map<TUsuario, UsuarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
        ///Repositorio: UsuarioRepositorio
        ///Autor: Edgar Serruto.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Este método obtiene una lista de Roles de Usuario para Filtro
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerUsuarioRolCombo()
        {
            try
            {
                List<FiltroDTO> lista = new List<FiltroDTO>();
                var query = "SELECT Id, Nombre FROM [conf].[V_TUsuarioRol_ObtenerCombo] WHERE Estado = 1";
                var respuesta = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
