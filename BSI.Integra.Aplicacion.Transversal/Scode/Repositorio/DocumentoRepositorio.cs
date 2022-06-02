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
    public class DocumentoRepositorio : BaseRepository<TDocumento, DocumentoBO>
    {
        #region Metodos Base
        public DocumentoRepositorio() : base()
        {
        }
        public DocumentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoBO> GetBy(Expression<Func<TDocumento, bool>> filter)
        {
            IEnumerable<TDocumento> listado = base.GetBy(filter);
            List<DocumentoBO> listadoBO = new List<DocumentoBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoBO objetoBO = Mapper.Map<TDocumento, DocumentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoBO FirstById(int id)
        {
            try
            {
                TDocumento entidad = base.FirstById(id);
                DocumentoBO objetoBO = new DocumentoBO();
                Mapper.Map<TDocumento, DocumentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoBO FirstBy(Expression<Func<TDocumento, bool>> filter)
        {
            try
            {
                TDocumento entidad = base.FirstBy(filter);
                DocumentoBO objetoBO = Mapper.Map<TDocumento, DocumentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoBO> listadoBO)
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

        public bool Update(DocumentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoBO> listadoBO)
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
        private void AsignacionId(TDocumento entidad, DocumentoBO objetoBO)
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

        private TDocumento MapeoEntidad(DocumentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumento entidad = new TDocumento();
                entidad = Mapper.Map<DocumentoBO, TDocumento>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<DocumentoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TDocumento, bool>>> filters, Expression<Func<TDocumento, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TDocumento> listado = base.GetFiltered(filters, orderBy, ascending);
            List<DocumentoBO> listadoBO = new List<DocumentoBO>();

            foreach (var itemEntidad in listado)
            {
                DocumentoBO objetoBO = Mapper.Map<TDocumento, DocumentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<DocumentoDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new DocumentoDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene el Id Y Nombre de los documentos para mostrarse en un combo
        /// </summary>
        /// <returns></returns>
        public List<DocumentoFiltroDTO> ObtenerListaDocumento()
        {
            try
            {
                var listaDocumento = GetBy(x => true, y => new DocumentoFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                }).ToList();

                return listaDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
