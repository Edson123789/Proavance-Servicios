using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CertificadoPartnerComplementoRepositorio : BaseRepository<TCertificadoPartnerComplemento, CertificadoPartnerComplementoBO>
    {
        #region Metodos Base
        public CertificadoPartnerComplementoRepositorio() : base()
        {
        }
        public CertificadoPartnerComplementoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoPartnerComplementoBO> GetBy(Expression<Func<TCertificadoPartnerComplemento, bool>> filter)
        {
            IEnumerable<TCertificadoPartnerComplemento> listado = base.GetBy(filter);
            List<CertificadoPartnerComplementoBO> listadoBO = new List<CertificadoPartnerComplementoBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoPartnerComplementoBO objetoBO = Mapper.Map<TCertificadoPartnerComplemento, CertificadoPartnerComplementoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoPartnerComplementoBO FirstById(int id)
        {
            try
            {
                TCertificadoPartnerComplemento entidad = base.FirstById(id);
                CertificadoPartnerComplementoBO objetoBO = new CertificadoPartnerComplementoBO();
                Mapper.Map<TCertificadoPartnerComplemento, CertificadoPartnerComplementoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoPartnerComplementoBO FirstBy(Expression<Func<TCertificadoPartnerComplemento, bool>> filter)
        {
            try
            {
                TCertificadoPartnerComplemento entidad = base.FirstBy(filter);
                CertificadoPartnerComplementoBO objetoBO = Mapper.Map<TCertificadoPartnerComplemento, CertificadoPartnerComplementoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoPartnerComplementoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoPartnerComplemento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoPartnerComplementoBO> listadoBO)
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

        public bool Update(CertificadoPartnerComplementoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoPartnerComplemento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoPartnerComplementoBO> listadoBO)
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
        private void AsignacionId(TCertificadoPartnerComplemento entidad, CertificadoPartnerComplementoBO objetoBO)
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

        private TCertificadoPartnerComplemento MapeoEntidad(CertificadoPartnerComplementoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoPartnerComplemento entidad = new TCertificadoPartnerComplemento();
                entidad = Mapper.Map<CertificadoPartnerComplementoBO, TCertificadoPartnerComplemento>(objetoBO,
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
        /// Obtiene los centro de costo que tienen asignado a un certificado partner complemento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CertificadoPartnerComplementoDTO> ObtenerTodo()
        {
            try
            {
                
                return GetBy(x => x.Estado, x => new CertificadoPartnerComplementoDTO{
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    MencionEnCertificado = x.MencionEnCertificado,
                    FrontalCentral = x.FrontalCentral,
                    FrontalInferiorIzquierda = x.FrontalInferiorIzquierda,
                    PosteriorCentral = x.PosteriorCentral,
                    PosteriorInferiorIzquierda = x.PosteriorInferiorIzquierda,
                    NombreUsuario = x.UsuarioModificacion }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene los centro de costo que tienen asignado a un certificado partner complemento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CentroCostoAsignadoCertificadoPartnerComplementoDTO> ObtenerCentroCostoAsociadoPorId(int id)
        {
            try
            {
                List<CentroCostoAsignadoCertificadoPartnerComplementoDTO> centrosCosto = new List<CentroCostoAsignadoCertificadoPartnerComplementoDTO>();
                var query = "SELECT IdCertificadoPartnerComplemento,IdCentroCosto, NombreCentroCosto FROM ope.V_ObtenerCentroCostoPorCertificadoPartnerComplemento WHERE IdCertificadoPartnerComplemento = @id;";
                var centrosCostoDB = _dapper.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(centrosCostoDB) && !centrosCostoDB.Contains("[]"))
                {
                    centrosCosto = JsonConvert.DeserializeObject<List<CentroCostoAsignadoCertificadoPartnerComplementoDTO>>(centrosCostoDB);
                }
                return centrosCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CertificadoPartnerComplementoDTO ObtenerPorCentroCosto(int IdCentroCosto)
        {
            try
            {
                CertificadoPartnerComplementoDTO centrosCosto = new CertificadoPartnerComplementoDTO();
                var query = "SELECT  Id,MencionEnCertificado,FrontalCentral,FrontalInferiorIzquierda,PosteriorCentral,PosteriorInferiorIzquierda " +
                            " FROM ope.V_ObtenerCertificadoPartnerComplementoPorCentroCosto WHERE IdCentroCosto = @IdCentroCosto;";
                var centrosCostoDB = _dapper.FirstOrDefault(query, new { IdCentroCosto });
                if (!string.IsNullOrEmpty(centrosCostoDB) && !centrosCostoDB.Contains("[]"))
                {
                    centrosCosto = JsonConvert.DeserializeObject<CertificadoPartnerComplementoDTO>(centrosCostoDB);
                }
                return centrosCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
