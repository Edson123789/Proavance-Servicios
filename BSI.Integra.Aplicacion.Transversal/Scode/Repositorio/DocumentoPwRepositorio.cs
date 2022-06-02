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
    public class DocumentoPwRepositorio : BaseRepository<TDocumentoPw, DocumentoPwBO>
    {
        #region Metodos Base
        public DocumentoPwRepositorio() : base()
        {
        }
        public DocumentoPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentoPwBO> GetBy(Expression<Func<TDocumentoPw, bool>> filter)
        {
            IEnumerable<TDocumentoPw> listado = base.GetBy(filter);
            List<DocumentoPwBO> listadoBO = new List<DocumentoPwBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentoPwBO objetoBO = Mapper.Map<TDocumentoPw, DocumentoPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentoPwBO FirstById(int id)
        {
            try
            {
                TDocumentoPw entidad = base.FirstById(id);
                DocumentoPwBO objetoBO = new DocumentoPwBO();
                Mapper.Map<TDocumentoPw, DocumentoPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoPwBO FirstBy(Expression<Func<TDocumentoPw, bool>> filter)
        {
            try
            {
                TDocumentoPw entidad = base.FirstBy(filter);
                DocumentoPwBO objetoBO = Mapper.Map<TDocumentoPw, DocumentoPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentoPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentoPwBO> listadoBO)
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

        public bool Update(DocumentoPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentoPwBO> listadoBO)
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
        private void AsignacionId(TDocumentoPw entidad, DocumentoPwBO objetoBO)
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

        private TDocumentoPw MapeoEntidad(DocumentoPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentoPw entidad = new TDocumentoPw();
                entidad = Mapper.Map<DocumentoPwBO, TDocumentoPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.DocumentoSeccion != null && objetoBO.DocumentoSeccion.Count > 0)
                {
                    foreach (var hijo in objetoBO.DocumentoSeccion)
                    {
                        TDocumentoSeccionPw entidadHijo = new TDocumentoSeccionPw();
                        entidadHijo = Mapper.Map<DocumentoSeccionPwBO, TDocumentoSeccionPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TDocumentoSeccionPw.Add(entidadHijo);
                    }
                }
                if (objetoBO.BandejaPendiente != null && objetoBO.BandejaPendiente.Count > 0)
                {
                    foreach (var hijo in objetoBO.BandejaPendiente)
                    {
                        TBandejaPendientePw entidadHijo = new TBandejaPendientePw();
                        entidadHijo = Mapper.Map<BandejaPendientePwBO, TBandejaPendientePw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TBandejaPendientePw.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        ///  Obtiene todos los registros sin los campos de auditoria
        /// </summary>
        /// <returns></returns>
        public List<DocumentoPwDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new DocumentoPwDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdPlantillaPw = y.IdPlantillaPw,
                    EstadoFlujo = y.EstadoFlujo,
                    Asignado = y.Asignado
,                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Obtiene todos los registros por IdPLantillaPw
        /// </summary>
        /// <returns></returns>
        public List<DocumentoPwDTO> ObtenerDocumentoPorIdPlantilla(int IdPlantilla)
        {
            try
            {
                var lista = GetBy(x => x.IdPlantillaPw == IdPlantilla && x.Estado == true , y => new DocumentoPwDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdPlantillaPw = y.IdPlantillaPw,
                    EstadoFlujo = y.EstadoFlujo,
                    Asignado = y.Asignado
,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
