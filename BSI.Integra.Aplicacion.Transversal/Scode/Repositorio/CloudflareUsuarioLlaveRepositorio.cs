using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CloudflareUsuarioLlaveRepositorio : BaseRepository<TCloudflareUsuarioLlave, CloudflareUsuarioLlaveBO>
    {
        #region Metodos Base
        public CloudflareUsuarioLlaveRepositorio() : base()
        {
        }
        public CloudflareUsuarioLlaveRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CloudflareUsuarioLlaveBO> GetBy(Expression<Func<TCloudflareUsuarioLlave, bool>> filter)
        {
            IEnumerable<TCloudflareUsuarioLlave> listado = base.GetBy(filter);
            List<CloudflareUsuarioLlaveBO> listadoBO = new List<CloudflareUsuarioLlaveBO>();
            foreach (var itemEntidad in listado)
            {
                CloudflareUsuarioLlaveBO objetoBO = Mapper.Map<TCloudflareUsuarioLlave, CloudflareUsuarioLlaveBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CloudflareUsuarioLlaveBO FirstById(int id)
        {
            try
            {
                TCloudflareUsuarioLlave entidad = base.FirstById(id);
                CloudflareUsuarioLlaveBO objetoBO = new CloudflareUsuarioLlaveBO();
                Mapper.Map<TCloudflareUsuarioLlave, CloudflareUsuarioLlaveBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CloudflareUsuarioLlaveBO FirstBy(Expression<Func<TCloudflareUsuarioLlave, bool>> filter)
        {
            try
            {
                TCloudflareUsuarioLlave entidad = base.FirstBy(filter);
                CloudflareUsuarioLlaveBO objetoBO = Mapper.Map<TCloudflareUsuarioLlave, CloudflareUsuarioLlaveBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CloudflareUsuarioLlaveBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCloudflareUsuarioLlave entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CloudflareUsuarioLlaveBO> listadoBO)
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

        public bool Update(CloudflareUsuarioLlaveBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCloudflareUsuarioLlave entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CloudflareUsuarioLlaveBO> listadoBO)
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
        private void AsignacionId(TCloudflareUsuarioLlave entidad, CloudflareUsuarioLlaveBO objetoBO)
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

        private TCloudflareUsuarioLlave MapeoEntidad(CloudflareUsuarioLlaveBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCloudflareUsuarioLlave entidad = new TCloudflareUsuarioLlave();
                entidad = Mapper.Map<CloudflareUsuarioLlaveBO, TCloudflareUsuarioLlave>(objetoBO,
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

        public List<listaCloudflareUsuarioLlaveBO> ObtenerListaCloudflareUsuarioLlave()
        {
            try
            {
                List<listaCloudflareUsuarioLlaveBO> capitulosFiltro = new List<listaCloudflareUsuarioLlaveBO>();
                var _queryfiltrocapitulo = "Select Id, AuthKey, AuthEmail, AccountId, IdPersonal, Activar FROM pla.T_CloudflareUsuarioLlave WHERE Estado=1";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<listaCloudflareUsuarioLlaveBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public registroCloudflareUsuarioLlaveBO ObtenerRegistroCloudflareUsuarioLlavePorIdPersonal(int IdPersonal)
        {
            try
            {
                registroCloudflareUsuarioLlaveBO capitulosFiltro = new registroCloudflareUsuarioLlaveBO();
                var _queryfiltrocapitulo = "Select Id, AuthKey, AuthEmail, AccountId, IdPersonal FROM pla.T_CloudflareUsuarioLlave WHERE Estado=1 AND IdPersonal=@IdPersonal";
                var SubfiltroCapitulo = _dapper.FirstOrDefault(_queryfiltrocapitulo, new { IdPersonal });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<registroCloudflareUsuarioLlaveBO>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public registroCloudflareUsuarioLlaveBO ObtenerRegistroCloudflareUsuarioLlavePorEmail(string Email)
        {
            try
            {
                registroCloudflareUsuarioLlaveBO capitulosFiltro = new registroCloudflareUsuarioLlaveBO();
                var _queryfiltrocapitulo = "Select Id, AuthKey, AuthEmail, AccountId, IdPersonal, Activar FROM pla.T_CloudflareUsuarioLlave WHERE Estado=1 AND AuthEmail=@Email";
                var SubfiltroCapitulo = _dapper.FirstOrDefault(_queryfiltrocapitulo, new { Email });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<registroCloudflareUsuarioLlaveBO>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public List<registroVideoCloudflareConfigurado> ObtenerRegistroVideoRegistradoCloudflareAula()
        {
            try
            {
                List<registroVideoCloudflareConfigurado> capitulosFiltro = new List<registroVideoCloudflareConfigurado>();
                var _query = "select VideoId,TotalMinutos AS TotalSegundos, IdPGeneral from pla.T_ConfigurarVideoPrograma where Estado=1";
                var ejecucuion = _dapper.FirstOrDefault(_query, null);
                if (!string.IsNullOrEmpty(ejecucuion) && !ejecucuion.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<registroVideoCloudflareConfigurado>>(ejecucuion);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

    }
}
