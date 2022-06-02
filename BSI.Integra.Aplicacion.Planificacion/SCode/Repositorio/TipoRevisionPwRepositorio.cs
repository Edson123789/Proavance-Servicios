using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class TipoRevisionPwRepositorio : BaseRepository<TTipoRevisionPw, TipoRevisionPwBO>
    {
        #region Metodos Base
        public TipoRevisionPwRepositorio() : base()
        {
        }
        public TipoRevisionPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoRevisionPwBO> GetBy(Expression<Func<TTipoRevisionPw, bool>> filter)
        {
            IEnumerable<TTipoRevisionPw> listado = base.GetBy(filter);
            List<TipoRevisionPwBO> listadoBO = new List<TipoRevisionPwBO>();
            foreach (var itemEntidad in listado)
            {
                TipoRevisionPwBO objetoBO = Mapper.Map<TTipoRevisionPw, TipoRevisionPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoRevisionPwBO FirstById(int id)
        {
            try
            {
                TTipoRevisionPw entidad = base.FirstById(id);
                TipoRevisionPwBO objetoBO = new TipoRevisionPwBO();
                Mapper.Map<TTipoRevisionPw, TipoRevisionPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoRevisionPwBO FirstBy(Expression<Func<TTipoRevisionPw, bool>> filter)
        {
            try
            {
                TTipoRevisionPw entidad = base.FirstBy(filter);
                TipoRevisionPwBO objetoBO = Mapper.Map<TTipoRevisionPw, TipoRevisionPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoRevisionPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoRevisionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoRevisionPwBO> listadoBO)
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

        public bool Update(TipoRevisionPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoRevisionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoRevisionPwBO> listadoBO)
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
        private void AsignacionId(TTipoRevisionPw entidad, TipoRevisionPwBO objetoBO)
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

        private TTipoRevisionPw MapeoEntidad(TipoRevisionPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoRevisionPw entidad = new TTipoRevisionPw();
                entidad = Mapper.Map<TipoRevisionPwBO, TTipoRevisionPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TipoRevisionPwBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTipoRevisionPw, bool>>> filters, Expression<Func<TTipoRevisionPw, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTipoRevisionPw> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TipoRevisionPwBO> listadoBO = new List<TipoRevisionPwBO>();

            foreach (var itemEntidad in listado)
            {
                TipoRevisionPwBO objetoBO = Mapper.Map<TTipoRevisionPw, TipoRevisionPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        ///  Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<TipoRevisionPwDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new TipoRevisionPwDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                    Codigo = y.Codigo
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Id, Nombre de los Tipos de Revisiones para ser listados
        /// </summary>
        /// <returns></returns>
        public List<ListaTipoRevisionDTO> ObtenerListaTipoRevision()
        {
            try
            {
                var lista = GetBy(x => true, y => new ListaTipoRevisionDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
