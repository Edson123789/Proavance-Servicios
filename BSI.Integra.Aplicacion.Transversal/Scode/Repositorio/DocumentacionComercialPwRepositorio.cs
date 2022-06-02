using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class DocumentacionComercialPwRepositorio : BaseRepository<TDocumentacionComercialPw, DocumentacionComercialPwBO>
    {
        #region Metodos Base
        public DocumentacionComercialPwRepositorio() : base()
        {
        }
        public DocumentacionComercialPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DocumentacionComercialPwBO> GetBy(Expression<Func<TDocumentacionComercialPw, bool>> filter)
        {
            IEnumerable<TDocumentacionComercialPw> listado = base.GetBy(filter);
            List<DocumentacionComercialPwBO> listadoBO = new List<DocumentacionComercialPwBO>();
            foreach (var itemEntidad in listado)
            {
                DocumentacionComercialPwBO objetoBO = Mapper.Map<TDocumentacionComercialPw, DocumentacionComercialPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DocumentacionComercialPwBO FirstById(int id)
        {
            try
            {
                TDocumentacionComercialPw entidad = base.FirstById(id);
                DocumentacionComercialPwBO objetoBO = new DocumentacionComercialPwBO();
                Mapper.Map<TDocumentacionComercialPw, DocumentacionComercialPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentacionComercialPwBO FirstBy(Expression<Func<TDocumentacionComercialPw, bool>> filter)
        {
            try
            {
                TDocumentacionComercialPw entidad = base.FirstBy(filter);
                DocumentacionComercialPwBO objetoBO = Mapper.Map<TDocumentacionComercialPw, DocumentacionComercialPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DocumentacionComercialPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDocumentacionComercialPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DocumentacionComercialPwBO> listadoBO)
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

        public bool Update(DocumentacionComercialPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDocumentacionComercialPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DocumentacionComercialPwBO> listadoBO)
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
        private void AsignacionId(TDocumentacionComercialPw entidad, DocumentacionComercialPwBO objetoBO)
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

        private TDocumentacionComercialPw MapeoEntidad(DocumentacionComercialPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDocumentacionComercialPw entidad = new TDocumentacionComercialPw();
                entidad = Mapper.Map<DocumentacionComercialPwBO, TDocumentacionComercialPw>(objetoBO,
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

        public DocumentoComercialContenidoDTO ObtnerDocumentoComercial_contenido(string tipoDocumento , string modalidad, int idPais)
        {
            try
            {
                string _queryContenidoDocumento = "select Contenido From pla.V_TDocumentacionComercial_pw_Contenido Where Tipo=@TipoDocumento and Modalidad=@Modalidad and IdPais=@IdPais and Estado=1";
                var contenidoDocumento = _dapper.FirstOrDefault(_queryContenidoDocumento, new { TipoDocumento = tipoDocumento, Modalidad = modalidad, IdPais = idPais});
                return JsonConvert.DeserializeObject<DocumentoComercialContenidoDTO>(contenidoDocumento);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DocumentoComercialContenidoDTO DocumentoComercial_contenido(string tipoDocumento , string modalidad, int idPais)
        {
            try
            {
                string _queryContenidoDocumento = "select Contenido From pla.V_TDocumentacionComercial_pw_Contenido Where Tipo=@TipoDocumento and Modalidad=@Modalidad and IdPais=@IdPais and Estado=1";
                var contenidoDocumento = _dapper.FirstOrDefault(_queryContenidoDocumento, new { TipoDocumento=tipoDocumento,Modalidad=modalidad,IdPais=idPais});
                return JsonConvert.DeserializeObject<DocumentoComercialContenidoDTO>(contenidoDocumento);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
