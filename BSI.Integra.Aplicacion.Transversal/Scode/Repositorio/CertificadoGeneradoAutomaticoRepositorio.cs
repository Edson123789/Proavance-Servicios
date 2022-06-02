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
    /// Repositorio: CertificadoGeneradoAutomaticoRepositorio
    /// Autor: Fischer Valdez - Priscila Pacsi 
    /// Fecha: 01/10/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_CertificadoGeneradoAutomatico
    /// </summary>
    public class CertificadoGeneradoAutomaticoRepositorio : BaseRepository<TCertificadoGeneradoAutomatico, CertificadoGeneradoAutomaticoBO>
    {
        #region Metodos Base
        public CertificadoGeneradoAutomaticoRepositorio() : base()
        {
        }
        public CertificadoGeneradoAutomaticoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoGeneradoAutomaticoBO> GetBy(Expression<Func<TCertificadoGeneradoAutomatico, bool>> filter)
        {
            IEnumerable<TCertificadoGeneradoAutomatico> listado = base.GetBy(filter);
            List<CertificadoGeneradoAutomaticoBO> listadoBO = new List<CertificadoGeneradoAutomaticoBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoGeneradoAutomaticoBO objetoBO = Mapper.Map<TCertificadoGeneradoAutomatico, CertificadoGeneradoAutomaticoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoGeneradoAutomaticoBO FirstById(int id)
        {
            try
            {
                TCertificadoGeneradoAutomatico entidad = base.FirstById(id);
                CertificadoGeneradoAutomaticoBO objetoBO = new CertificadoGeneradoAutomaticoBO();
                Mapper.Map<TCertificadoGeneradoAutomatico, CertificadoGeneradoAutomaticoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoGeneradoAutomaticoBO FirstBy(Expression<Func<TCertificadoGeneradoAutomatico, bool>> filter)
        {
            try
            {
                TCertificadoGeneradoAutomatico entidad = base.FirstBy(filter);
                CertificadoGeneradoAutomaticoBO objetoBO = Mapper.Map<TCertificadoGeneradoAutomatico, CertificadoGeneradoAutomaticoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoGeneradoAutomaticoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoGeneradoAutomatico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoGeneradoAutomaticoBO> listadoBO)
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

        public bool Update(CertificadoGeneradoAutomaticoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoGeneradoAutomatico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoGeneradoAutomaticoBO> listadoBO)
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
        private void AsignacionId(TCertificadoGeneradoAutomatico entidad, CertificadoGeneradoAutomaticoBO objetoBO)
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

        private TCertificadoGeneradoAutomatico MapeoEntidad(CertificadoGeneradoAutomaticoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoGeneradoAutomatico entidad = new TCertificadoGeneradoAutomatico();
                entidad = Mapper.Map<CertificadoGeneradoAutomaticoBO, TCertificadoGeneradoAutomatico>(objetoBO,
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
        public bool ActualizarCertificadosGenerados(int IdPgeneralConfiguracionPlantilla, int IdPgeneral)
        {
            string _query = "SP_ActualizarCertificadosEmitidos";
            string query = _dapper.QuerySPFirstOrDefault(_query, new { IdPgeneralConfiguracionPlantilla, IdPgeneral });
            if (query.Contains("true"))
            {
                return true;
            }
            return false;
        }
        public int ObtenerCorrelativoCertificado ()
        {
            ValorIntDTO valor = new ValorIntDTO();
            string _query = "Select Id as Valor from pla.T_CertificadoGeneradoAutomatico Order by Id desc";
            string query = _dapper.FirstOrDefault(_query, null);
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                valor = JsonConvert.DeserializeObject<ValorIntDTO>(query);
                return valor.Valor+1;
            }
            else
            {
                throw new Exception("No se pudo Calcular Consecutivo");
            }
             
        }
        public List<ObtenerCertificadoGeneradoDTO> ObtenerCertificadosporMatricula (int IdMatriculaCabecera)
        {
            List<ObtenerCertificadoGeneradoDTO> valor = new List<ObtenerCertificadoGeneradoDTO>();
            string _query = "Select Id,NombrePlantilla,FechaCreacion,UrlDocumento from pla.V_ObtenerCertificadoPorMatricula Where IdMatriculaCabecera = @IdMatriculaCabecera";
            string query = _dapper.QueryDapper(_query, new { IdMatriculaCabecera});
            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                valor = JsonConvert.DeserializeObject<List<ObtenerCertificadoGeneradoDTO>>(query);
                return valor;
            }
            return valor;
             
        }
        ///Autor: Priscila Pacsi Gamboa
        ///Fecha: 01/10/2021
        /// <summary>
        /// Obtiene los certificados generados por IdMatriculaCabecera
        /// </summary>
        /// <returns>List<NombreCertificadoDTO></returns>
        /// <param name="IdMatriculaCabecera"> Id de la matricula</param>
        public List<NombreCertificadoDTO> ObtenerCertificados(int IdMatriculaCabecera)
        {
            List<NombreCertificadoDTO> valor = new List<NombreCertificadoDTO>();
            string _query = "Select NombreArchivo from pla.T_CertificadoGeneradoAutomatico Where IdMatriculaCabecera = @IdMatriculaCabecera";
            string query = _dapper.QueryDapper(_query, new { IdMatriculaCabecera });
            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                valor = JsonConvert.DeserializeObject<List<NombreCertificadoDTO>>(query);
            }
            
            return valor;

        }
        public int ObtenerRegistrosProgramaPorMatricula(int IdMatriculaCabecera)
        {
            ValorIntDTO valor = new ValorIntDTO();
            string _query = "select top 1 Id AS Valor from pla.T_CertificadoGeneradoAutomatico where IdMatriculaCabecera = @IdMatriculaCabecera AND NombreArchivo like '%p%' AND Estado=1";
            string query = _dapper.FirstOrDefault(_query, new { IdMatriculaCabecera });
            if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query!="null")
            {
                valor = JsonConvert.DeserializeObject<ValorIntDTO>(query);
            }
            else {
                valor.Valor = -1;
            }

            return valor.Valor;

        }
        public int EliminarRegistrosPorId(int idCertificadoGeneradoAutomatico)
        {

            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio();
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio();
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();

            var CertificadoAutomatico = _repCertificadoGeneradoAutomatico.FirstById(idCertificadoGeneradoAutomatico);
            var matriculaCabecera = _repMatriculaCabecera.FirstById(CertificadoAutomatico.IdMatriculaCabecera);
            if (matriculaCabecera.IdEstadoMatriculaCertificado != null &&
                matriculaCabecera.IdEstadoMatriculaCertificado != 0 &&
                matriculaCabecera.IdSubEstadoMatriculaCertificado != null &&
                matriculaCabecera.IdSubEstadoMatriculaCertificado != 0)
            {
                matriculaCabecera.IdEstadoMatricula =(int)matriculaCabecera.IdEstadoMatriculaCertificado;
                matriculaCabecera.IdSubEstadoMatricula = (int)matriculaCabecera.IdSubEstadoMatriculaCertificado;
                matriculaCabecera.UsuarioModificacion = "SYSTEM";
                matriculaCabecera.FechaModificacion = DateTime.Now;
                _repMatriculaCabecera.Update(matriculaCabecera);
            }

            _repCertificadoGeneradoAutomatico.Delete(idCertificadoGeneradoAutomatico,"SYSTEM");

            var detalles = _repCertificadoGeneradoAutomaticoContenido.GetBy(x=>x.IdCertificadoGeneradoAutomatico == idCertificadoGeneradoAutomatico && x.Estado==true);

            List<int> IdCertificadoGeneradoAutomaticoContenido = new List<int>();
            IdCertificadoGeneradoAutomaticoContenido.AddRange(detalles.Select(s => s.Id));
            _repCertificadoGeneradoAutomaticoContenido.Delete(IdCertificadoGeneradoAutomaticoContenido, "SYSTEM");

            return idCertificadoGeneradoAutomatico;

        }
    }
}
