using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaCertificadoPartnerComplementoRepositorio : BaseRepository<TRaCertificadoPartnerComplemento, RaCertificadoPartnerComplementoBO>
    {
        #region Metodos Base
        public RaCertificadoPartnerComplementoRepositorio() : base()
        {
        }
        public RaCertificadoPartnerComplementoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaCertificadoPartnerComplementoBO> GetBy(Expression<Func<TRaCertificadoPartnerComplemento, bool>> filter)
        {
            IEnumerable<TRaCertificadoPartnerComplemento> listado = base.GetBy(filter);
            List<RaCertificadoPartnerComplementoBO> listadoBO = new List<RaCertificadoPartnerComplementoBO>();
            foreach (var itemEntidad in listado)
            {
                RaCertificadoPartnerComplementoBO objetoBO = Mapper.Map<TRaCertificadoPartnerComplemento, RaCertificadoPartnerComplementoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaCertificadoPartnerComplementoBO FirstById(int id)
        {
            try
            {
                TRaCertificadoPartnerComplemento entidad = base.FirstById(id);
                RaCertificadoPartnerComplementoBO objetoBO = new RaCertificadoPartnerComplementoBO();
                Mapper.Map<TRaCertificadoPartnerComplemento, RaCertificadoPartnerComplementoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaCertificadoPartnerComplementoBO FirstBy(Expression<Func<TRaCertificadoPartnerComplemento, bool>> filter)
        {
            try
            {
                TRaCertificadoPartnerComplemento entidad = base.FirstBy(filter);
                RaCertificadoPartnerComplementoBO objetoBO = Mapper.Map<TRaCertificadoPartnerComplemento, RaCertificadoPartnerComplementoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaCertificadoPartnerComplementoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaCertificadoPartnerComplemento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaCertificadoPartnerComplementoBO> listadoBO)
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

        public bool Update(RaCertificadoPartnerComplementoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaCertificadoPartnerComplemento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaCertificadoPartnerComplementoBO> listadoBO)
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
        private void AsignacionId(TRaCertificadoPartnerComplemento entidad, RaCertificadoPartnerComplementoBO objetoBO)
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

        private TRaCertificadoPartnerComplemento MapeoEntidad(RaCertificadoPartnerComplementoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaCertificadoPartnerComplemento entidad = new TRaCertificadoPartnerComplemento();
                entidad = Mapper.Map<RaCertificadoPartnerComplementoBO, TRaCertificadoPartnerComplemento>(objetoBO,
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
        public List<CentroCostoAsignadoCertificadoPartnerComplementoDTO> ObtenerCentroCostoAsociadoPorId(int id)
        {
            try
            {
                List<CentroCostoAsignadoCertificadoPartnerComplementoDTO> centrosCosto = new List<CentroCostoAsignadoCertificadoPartnerComplementoDTO>();
                var query = "SELECT IdRaCentroCosto, NombreCentroCosto FROM ope.V_ObtenerCentroCostoPorCertificadoPartnerComplemento WHERE IdRaCertificadoPartnerComplemento = @id;";
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
    }
}
