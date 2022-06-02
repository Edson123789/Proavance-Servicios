using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RaCertificadoBrochureRepositorio : BaseRepository<TRaCertificadoBrochure, RaCertificadoBrochureBO>
    {
        #region Metodos Base
        public RaCertificadoBrochureRepositorio() : base()
        {
        }
        public RaCertificadoBrochureRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaCertificadoBrochureBO> GetBy(Expression<Func<TRaCertificadoBrochure, bool>> filter)
        {
            IEnumerable<TRaCertificadoBrochure> listado = base.GetBy(filter);
            List<RaCertificadoBrochureBO> listadoBO = new List<RaCertificadoBrochureBO>();
            foreach (var itemEntidad in listado)
            {
                RaCertificadoBrochureBO objetoBO = Mapper.Map<TRaCertificadoBrochure, RaCertificadoBrochureBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaCertificadoBrochureBO FirstById(int id)
        {
            try
            {
                TRaCertificadoBrochure entidad = base.FirstById(id);
                RaCertificadoBrochureBO objetoBO = new RaCertificadoBrochureBO();
                Mapper.Map<TRaCertificadoBrochure, RaCertificadoBrochureBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaCertificadoBrochureBO FirstBy(Expression<Func<TRaCertificadoBrochure, bool>> filter)
        {
            try
            {
                TRaCertificadoBrochure entidad = base.FirstBy(filter);
                RaCertificadoBrochureBO objetoBO = Mapper.Map<TRaCertificadoBrochure, RaCertificadoBrochureBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaCertificadoBrochureBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaCertificadoBrochure entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaCertificadoBrochureBO> listadoBO)
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

        public bool Update(RaCertificadoBrochureBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaCertificadoBrochure entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaCertificadoBrochureBO> listadoBO)
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
        private void AsignacionId(TRaCertificadoBrochure entidad, RaCertificadoBrochureBO objetoBO)
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

        private TRaCertificadoBrochure MapeoEntidad(RaCertificadoBrochureBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaCertificadoBrochure entidad = new TRaCertificadoBrochure();
                entidad = Mapper.Map<RaCertificadoBrochureBO, TRaCertificadoBrochure>(objetoBO,
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

        public List<FiltroDTO> ObtenerFiltro() {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los centros de costo asociados a un certificado brochure
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CentroCostoAsignadoCertificadoBrochureDTO> ObtenerCentroCostoAsociadoPorId(int id)
        {
            try
            {
                List<CentroCostoAsignadoCertificadoBrochureDTO> centrosCosto = new List<CentroCostoAsignadoCertificadoBrochureDTO>();
                var query = "SELECT IdRaCentroCosto, NombreCentroCosto FROM ope.V_ObtenerCentroCostoPorCertificadoBrochure WHERE IdRaCertificadoBrochure = @id";
                var centrosCostoDB = _dapper.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(centrosCostoDB) && !centrosCostoDB.Contains("[]"))
                {
                    centrosCosto = JsonConvert.DeserializeObject<List<CentroCostoAsignadoCertificadoBrochureDTO>>(centrosCostoDB);
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
