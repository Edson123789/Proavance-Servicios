using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoSeguimientoAlumnoCategoriaRepositorio : BaseRepository<TTipoSeguimientoAlumnoCategoria, TipoSeguimientoAlumnoCategoriaBO>
    {
        #region Metodos Base
        public TipoSeguimientoAlumnoCategoriaRepositorio() : base()
        {
        }
        public TipoSeguimientoAlumnoCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoSeguimientoAlumnoCategoriaBO> GetBy(Expression<Func<TTipoSeguimientoAlumnoCategoria, bool>> filter)
        {
            IEnumerable<TTipoSeguimientoAlumnoCategoria> listado = base.GetBy(filter);
            List<TipoSeguimientoAlumnoCategoriaBO> listadoBO = new List<TipoSeguimientoAlumnoCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoSeguimientoAlumnoCategoriaBO objetoBO = Mapper.Map<TTipoSeguimientoAlumnoCategoria, TipoSeguimientoAlumnoCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoSeguimientoAlumnoCategoriaBO FirstById(int id)
        {
            try
            {
                TTipoSeguimientoAlumnoCategoria entidad = base.FirstById(id);
                TipoSeguimientoAlumnoCategoriaBO objetoBO = new TipoSeguimientoAlumnoCategoriaBO();
                Mapper.Map<TTipoSeguimientoAlumnoCategoria, TipoSeguimientoAlumnoCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoSeguimientoAlumnoCategoriaBO FirstBy(Expression<Func<TTipoSeguimientoAlumnoCategoria, bool>> filter)
        {
            try
            {
                TTipoSeguimientoAlumnoCategoria entidad = base.FirstBy(filter);
                TipoSeguimientoAlumnoCategoriaBO objetoBO = Mapper.Map<TTipoSeguimientoAlumnoCategoria, TipoSeguimientoAlumnoCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoSeguimientoAlumnoCategoriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoSeguimientoAlumnoCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoSeguimientoAlumnoCategoriaBO> listadoBO)
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

        public bool Update(TipoSeguimientoAlumnoCategoriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoSeguimientoAlumnoCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoSeguimientoAlumnoCategoriaBO> listadoBO)
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
        private void AsignacionId(TTipoSeguimientoAlumnoCategoria entidad, TipoSeguimientoAlumnoCategoriaBO objetoBO)
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

        private TTipoSeguimientoAlumnoCategoria MapeoEntidad(TipoSeguimientoAlumnoCategoriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoSeguimientoAlumnoCategoria entidad = new TTipoSeguimientoAlumnoCategoria();
                entidad = Mapper.Map<TipoSeguimientoAlumnoCategoriaBO, TTipoSeguimientoAlumnoCategoria>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TipoSeguimientoAlumnoCategoriaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTipoSeguimientoAlumnoCategoria, bool>>> filters, Expression<Func<TTipoSeguimientoAlumnoCategoria, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTipoSeguimientoAlumnoCategoria> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TipoSeguimientoAlumnoCategoriaBO> listadoBO = new List<TipoSeguimientoAlumnoCategoriaBO>();

            foreach (var itemEntidad in listado)
            {
                TipoSeguimientoAlumnoCategoriaBO objetoBO = Mapper.Map<TTipoSeguimientoAlumnoCategoria, TipoSeguimientoAlumnoCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los tipo de seguimiento alumno categoria
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
