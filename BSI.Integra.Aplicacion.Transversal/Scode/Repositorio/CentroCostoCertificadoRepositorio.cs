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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CentroCostoCertificadoRepositorio : BaseRepository<TCentroCostoCertificado, CentroCostoCertificadoBO>
    {
        #region Metodos Base
        public CentroCostoCertificadoRepositorio() : base()
        {
        }
        public CentroCostoCertificadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CentroCostoCertificadoBO> GetBy(Expression<Func<TCentroCostoCertificado, bool>> filter)
        {
            IEnumerable<TCentroCostoCertificado> listado = base.GetBy(filter);
            List<CentroCostoCertificadoBO> listadoBO = new List<CentroCostoCertificadoBO>();
            foreach (var itemEntidad in listado)
            {
                CentroCostoCertificadoBO objetoBO = Mapper.Map<TCentroCostoCertificado, CentroCostoCertificadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CentroCostoCertificadoBO FirstById(int id)
        {
            try
            {
                TCentroCostoCertificado entidad = base.FirstById(id);
                CentroCostoCertificadoBO objetoBO = new CentroCostoCertificadoBO();
                Mapper.Map<TCentroCostoCertificado, CentroCostoCertificadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CentroCostoCertificadoBO FirstBy(Expression<Func<TCentroCostoCertificado, bool>> filter)
        {
            try
            {
                TCentroCostoCertificado entidad = base.FirstBy(filter);
                CentroCostoCertificadoBO objetoBO = Mapper.Map<TCentroCostoCertificado, CentroCostoCertificadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CentroCostoCertificadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCentroCostoCertificado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CentroCostoCertificadoBO> listadoBO)
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

        public bool Update(CentroCostoCertificadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCentroCostoCertificado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CentroCostoCertificadoBO> listadoBO)
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
        private void AsignacionId(TCentroCostoCertificado entidad, CentroCostoCertificadoBO objetoBO)
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

        private TCentroCostoCertificado MapeoEntidad(CentroCostoCertificadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCentroCostoCertificado entidad = new TCentroCostoCertificado();
                entidad = Mapper.Map<CentroCostoCertificadoBO, TCentroCostoCertificado>(objetoBO,
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

        public List<FiltroDTO> ObtenerBrochurePorCentroCosto(int IdCentroCosto)
        {
            try
            {
                List<FiltroDTO> centrosCosto = new List<FiltroDTO>();
                var query = "SELECT IdCertificadoBrochure AS Id, Nombre FROM ope.V_ObtenerCertificadoBrochurePorCentroCosto WHERE IdCentroCosto = @IdCentroCosto and Estado=1 order by FechaCreacion desc";
                var centrosCostoDB = _dapper.QueryDapper(query, new { IdCentroCosto });
                if (!string.IsNullOrEmpty(centrosCostoDB) && !centrosCostoDB.Contains("[]"))
                {
                    centrosCosto = JsonConvert.DeserializeObject<List<FiltroDTO>>(centrosCostoDB);
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
