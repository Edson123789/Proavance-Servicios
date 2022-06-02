using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.IO;
using BSI.Integra.Aplicacion.DTOs.Reportes;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ContenidoCertificadoIrcaRepositorio : BaseRepository<TContenidoCertificadoIrca, ContenidoCertificadoIrcaBO>
    {
        #region Metodos Base
        public ContenidoCertificadoIrcaRepositorio() : base()
        {
        }
        public ContenidoCertificadoIrcaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ContenidoCertificadoIrcaBO> GetBy(Expression<Func<TContenidoCertificadoIrca, bool>> filter)
        {
            IEnumerable<TContenidoCertificadoIrca> listado = base.GetBy(filter);
            List<ContenidoCertificadoIrcaBO> listadoBO = new List<ContenidoCertificadoIrcaBO>();
            foreach (var itemEntidad in listado)
            {
                ContenidoCertificadoIrcaBO objetoBO = Mapper.Map<TContenidoCertificadoIrca, ContenidoCertificadoIrcaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ContenidoCertificadoIrcaBO FirstById(int id)
        {
            try
            {
                TContenidoCertificadoIrca entidad = base.FirstById(id);
                ContenidoCertificadoIrcaBO objetoBO = new ContenidoCertificadoIrcaBO();
                Mapper.Map<TContenidoCertificadoIrca, ContenidoCertificadoIrcaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ContenidoCertificadoIrcaBO FirstBy(Expression<Func<TContenidoCertificadoIrca, bool>> filter)
        {
            try
            {
                TContenidoCertificadoIrca entidad = base.FirstBy(filter);
                ContenidoCertificadoIrcaBO objetoBO = Mapper.Map<TContenidoCertificadoIrca, ContenidoCertificadoIrcaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ContenidoCertificadoIrcaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TContenidoCertificadoIrca entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ContenidoCertificadoIrcaBO> listadoBO)
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

        public bool Update(ContenidoCertificadoIrcaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TContenidoCertificadoIrca entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ContenidoCertificadoIrcaBO> listadoBO)
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
        private void AsignacionId(TContenidoCertificadoIrca entidad, ContenidoCertificadoIrcaBO objetoBO)
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

        private TContenidoCertificadoIrca MapeoEntidad(ContenidoCertificadoIrcaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TContenidoCertificadoIrca entidad = new TContenidoCertificadoIrca();
                entidad = Mapper.Map<ContenidoCertificadoIrcaBO, TContenidoCertificadoIrca>(objetoBO,
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

        public List<DatosGenerarCertificadoDTO> ObtenerDatosParaCertificadosIrca(int IdMatriculaCabecera)
        {
            var rpta = new List<DatosGenerarCertificadoDTO>();
            string _query = "pla.SP_ObtenerDatosGenerarCertificadosIrca";
            string query = _dapper.QuerySPDapper(_query, new { IdMatriculaCabecera });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<DatosGenerarCertificadoDTO>>(query);
            }
            return rpta;
        }
        public string ObtenerDescripcionResultadoIrca(int IdContenidoCertificadoIrca)
        {
            var rpta = new ValorStringDTO();
            string _query = "Select Resultado AS Valor From ope.V_ObtenerDescripcionResultadoIrca where Id=@IdContenidoCertificadoIrca";
            string query = _dapper.FirstOrDefault(_query, new { IdContenidoCertificadoIrca });

            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                return rpta.Valor;
            }
            return null;
        }
        public VistaPreviaCertificadoIrcaDTO ObtenerValoresVistaPreviaIrca(int IdPgeneral)
        {
            var rpta = new VistaPreviaCertificadoIrcaDTO();
            string _query = "Select Id, IdPespecifico From ope.T_ObtenerContenidoIrcaporIdPgeneral where IdPgeneral=@IdPgeneral";
            string query = _dapper.FirstOrDefault(_query, new { IdPgeneral });

            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<VistaPreviaCertificadoIrcaDTO>(query);
                
            }
            return rpta;
        }
    }
}
