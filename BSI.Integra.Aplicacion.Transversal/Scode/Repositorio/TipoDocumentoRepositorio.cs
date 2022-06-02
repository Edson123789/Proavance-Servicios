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
    public class TipoDocumentoRepositorio : BaseRepository<TTipoDocumento, TipoDocumentoBO>
    {
        #region Metodos Base
        public TipoDocumentoRepositorio() : base()
        {
        }
        public TipoDocumentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDocumentoBO> GetBy(Expression<Func<TTipoDocumento, bool>> filter)
        {
            IEnumerable<TTipoDocumento> listado = base.GetBy(filter);
            List<TipoDocumentoBO> listadoBO = new List<TipoDocumentoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDocumentoBO objetoBO = Mapper.Map<TTipoDocumento, TipoDocumentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDocumentoBO FirstById(int id)
        {
            try
            {
                TTipoDocumento entidad = base.FirstById(id);
                TipoDocumentoBO objetoBO = new TipoDocumentoBO();
                Mapper.Map<TTipoDocumento, TipoDocumentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDocumentoBO FirstBy(Expression<Func<TTipoDocumento, bool>> filter)
        {
            try
            {
                TTipoDocumento entidad = base.FirstBy(filter);
                TipoDocumentoBO objetoBO = Mapper.Map<TTipoDocumento, TipoDocumentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDocumentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDocumento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDocumentoBO> listadoBO)
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

        public bool Update(TipoDocumentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDocumento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDocumentoBO> listadoBO)
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
        private void AsignacionId(TTipoDocumento entidad, TipoDocumentoBO objetoBO)
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

        private TTipoDocumento MapeoEntidad(TipoDocumentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDocumento entidad = new TTipoDocumento();
                entidad = Mapper.Map<TipoDocumentoBO, TTipoDocumento>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TipoDocumentoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTipoDocumento, bool>>> filters, Expression<Func<TTipoDocumento, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTipoDocumento> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TipoDocumentoBO> listadoBO = new List<TipoDocumentoBO>();

            foreach (var itemEntidad in listado)
            {
                TipoDocumentoBO objetoBO = Mapper.Map<TTipoDocumento, TipoDocumentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion


        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<TipoDocumentoDTO> ObtenerTodoTipoDocumento()
        {
            try
            {
                var lista = GetBy(x => true, y => new TipoDocumentoDTO
                {
                    Id = y.Id,
                    Clave = y.Clave,
                    Descripcion = y.Descripcion
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
