using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class TipoPublicidadWebRepositorio : BaseRepository<TTipoPublicidadWeb, TipoPublicidadWebBO>
    {
        #region Metodos Base
        public TipoPublicidadWebRepositorio() : base()
        {
        }
        public TipoPublicidadWebRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoPublicidadWebBO> GetBy(Expression<Func<TTipoPublicidadWeb, bool>> filter)
        {
            IEnumerable<TTipoPublicidadWeb> listado = base.GetBy(filter);
            List<TipoPublicidadWebBO> listadoBO = new List<TipoPublicidadWebBO>();
            foreach (var itemEntidad in listado)
            {
                TipoPublicidadWebBO objetoBO = Mapper.Map<TTipoPublicidadWeb, TipoPublicidadWebBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoPublicidadWebBO FirstById(int id)
        {
            try
            {
                TTipoPublicidadWeb entidad = base.FirstById(id);
                TipoPublicidadWebBO objetoBO = new TipoPublicidadWebBO();
                Mapper.Map<TTipoPublicidadWeb, TipoPublicidadWebBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoPublicidadWebBO FirstBy(Expression<Func<TTipoPublicidadWeb, bool>> filter)
        {
            try
            {
                TTipoPublicidadWeb entidad = base.FirstBy(filter);
                TipoPublicidadWebBO objetoBO = Mapper.Map<TTipoPublicidadWeb, TipoPublicidadWebBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoPublicidadWebBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoPublicidadWeb entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoPublicidadWebBO> listadoBO)
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

        public bool Update(TipoPublicidadWebBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoPublicidadWeb entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoPublicidadWebBO> listadoBO)
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
        private void AsignacionId(TTipoPublicidadWeb entidad, TipoPublicidadWebBO objetoBO)
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

        private TTipoPublicidadWeb MapeoEntidad(TipoPublicidadWebBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoPublicidadWeb entidad = new TTipoPublicidadWeb();
                entidad = Mapper.Map<TipoPublicidadWebBO, TTipoPublicidadWeb>(objetoBO,
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
        /// Obtiene el Id, Nombre de los Campo Contacto(activos) registrados en el sistema
        /// </summary>
        /// <returns></returns>
        public List<TipoPublicidadWebFiltroDTO> ObtenerCamposContactoFiltro()
        {
            try
            {
                var lista = GetBy(x => x.Estado == true, y => new TipoPublicidadWebFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }).ToList();

                return lista;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
