using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class DocumentoIdentidadRepositorio : BaseRepository<TDocumentoIdentidad, DocumentoIdentidadBO>
    {
        #region Metodos Base
        public DocumentoIdentidadRepositorio() : base()
        {
        }
        public DocumentoIdentidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoIdentidadBO> GetBy(Expression<Func<TDocumentoIdentidad, bool>> filter)
        {
            IEnumerable<TDocumentoIdentidad> listado = base.GetBy(filter);
            List<DocumentoIdentidadBO> listadoBO = new List<DocumentoIdentidadBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoIdentidadBO objetoBO = Mapper.Map<TDocumentoIdentidad, DocumentoIdentidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoIdentidadBO FirstById(int id)
        {
            try
            {
                TDocumentoIdentidad entidad = base.FirstById(id);
                DocumentoIdentidadBO objetoBO = new DocumentoIdentidadBO();
                Mapper.Map<TDocumentoIdentidad, DocumentoIdentidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoIdentidadBO FirstBy(Expression<Func<TDocumentoIdentidad, bool>> filter)
        {
            try
            {
                TDocumentoIdentidad entidad = base.FirstBy(filter);
                DocumentoIdentidadBO objetoBO = Mapper.Map<TDocumentoIdentidad, DocumentoIdentidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoIdentidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoIdentidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoIdentidadBO> listadoBO)
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

        public bool Update(DocumentoIdentidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoIdentidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoIdentidadBO> listadoBO)
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
        private void AsignacionId(TDocumentoIdentidad entidad, DocumentoIdentidadBO objetoBO)
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

        private TDocumentoIdentidad MapeoEntidad(DocumentoIdentidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoIdentidad entidad = new TDocumentoIdentidad();
                entidad = Mapper.Map<DocumentoIdentidadBO, TDocumentoIdentidad>(objetoBO,
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
        public List<FiltroDTO> ObtenerDocumentoIdentidad()
        {
            try
            {
                List<FiltroDTO> listaDocumentoIdentidad = this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList();
                return listaDocumentoIdentidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
