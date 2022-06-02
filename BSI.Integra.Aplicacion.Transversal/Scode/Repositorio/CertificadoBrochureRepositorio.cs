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
    public class CertificadoBrochureRepositorio : BaseRepository<TCertificadoBrochure, CertificadoBrochureBO>
    {
        #region Metodos Base
        public CertificadoBrochureRepositorio() : base()
        {
        }
        public CertificadoBrochureRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoBrochureBO> GetBy(Expression<Func<TCertificadoBrochure, bool>> filter)
        {
            IEnumerable<TCertificadoBrochure> listado = base.GetBy(filter);
            List<CertificadoBrochureBO> listadoBO = new List<CertificadoBrochureBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoBrochureBO objetoBO = Mapper.Map<TCertificadoBrochure, CertificadoBrochureBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoBrochureBO FirstById(int id)
        {
            try
            {
                TCertificadoBrochure entidad = base.FirstById(id);
                CertificadoBrochureBO objetoBO = new CertificadoBrochureBO();
                Mapper.Map<TCertificadoBrochure, CertificadoBrochureBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoBrochureBO FirstBy(Expression<Func<TCertificadoBrochure, bool>> filter)
        {
            try
            {
                TCertificadoBrochure entidad = base.FirstBy(filter);
                CertificadoBrochureBO objetoBO = Mapper.Map<TCertificadoBrochure, CertificadoBrochureBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoBrochureBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoBrochure entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoBrochureBO> listadoBO)
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

        public bool Update(CertificadoBrochureBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoBrochure entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoBrochureBO> listadoBO)
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
        private void AsignacionId(TCertificadoBrochure entidad, CertificadoBrochureBO objetoBO)
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

        private TCertificadoBrochure MapeoEntidad(CertificadoBrochureBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoBrochure entidad = new TCertificadoBrochure();
                entidad = Mapper.Map<CertificadoBrochureBO, TCertificadoBrochure>(objetoBO,
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
        /// Obtiene los centros de costo asociados a un certificado brochure
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CertificadoBrochureDTO> ObtenerTodo()
        {
            try
            {
                return GetBy(x => x.Estado, x => new CertificadoBrochureDTO { Id = x.Id, Nombre=x.Nombre, NombreEnCertificado=x.NombreEnCertificado, Contenido= x.Contenido, TotalHoras=x.TotalHoras, NombreUsuario= x.UsuarioModificacion }).ToList();
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
                var query = "SELECT IdCertificadoBrochure,IdCentroCosto, NombreCentroCosto FROM ope.V_ObtenerCentroCostoPorCertificadoBrochure WHERE IdCertificadoBrochure = @id";
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
