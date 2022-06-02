using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoReclamoAlumnoRepositorio : BaseRepository<TTipoReclamoAlumno, TipoReclamoAlumnoBO>
    {
        #region Metodos Base
        public TipoReclamoAlumnoRepositorio() : base()
        {
        }
        public TipoReclamoAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoReclamoAlumnoBO> GetBy(Expression<Func<TTipoReclamoAlumno, bool>> filter)
        {
            IEnumerable<TTipoReclamoAlumno> listado = base.GetBy(filter);
            List<TipoReclamoAlumnoBO> listadoBO = new List<TipoReclamoAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoReclamoAlumnoBO objetoBO = Mapper.Map<TTipoReclamoAlumno, TipoReclamoAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoReclamoAlumnoBO FirstById(int id)
        {
            try
            {
                TTipoReclamoAlumno entidad = base.FirstById(id);
                TipoReclamoAlumnoBO objetoBO = new TipoReclamoAlumnoBO();
                Mapper.Map<TTipoReclamoAlumno, TipoReclamoAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoReclamoAlumnoBO FirstBy(Expression<Func<TTipoReclamoAlumno, bool>> filter)
        {
            try
            {
                TTipoReclamoAlumno entidad = base.FirstBy(filter);
                TipoReclamoAlumnoBO objetoBO = Mapper.Map<TTipoReclamoAlumno, TipoReclamoAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoReclamoAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoReclamoAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoReclamoAlumnoBO> listadoBO)
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

        public bool Update(TipoReclamoAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoReclamoAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoReclamoAlumnoBO> listadoBO)
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
        private void AsignacionId(TTipoReclamoAlumno entidad, TipoReclamoAlumnoBO objetoBO)
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

        private TTipoReclamoAlumno MapeoEntidad(TipoReclamoAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoReclamoAlumno entidad = new TTipoReclamoAlumno();
                entidad = Mapper.Map<TipoReclamoAlumnoBO, TTipoReclamoAlumno>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TipoReclamoAlumnoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTipoReclamoAlumno, bool>>> filters, Expression<Func<TTipoReclamoAlumno, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTipoReclamoAlumno> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TipoReclamoAlumnoBO> listadoBO = new List<TipoReclamoAlumnoBO>();

            foreach (var itemEntidad in listado)
            {
                TipoReclamoAlumnoBO objetoBO = Mapper.Map<TTipoReclamoAlumno, TipoReclamoAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        public List<registroTipoReclamoAlumnoDTO> ObtenerListaTipoReclamoAlumno()
        {
            try
            {
                var lista = GetBy(x => true, y => new registroTipoReclamoAlumnoDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
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
