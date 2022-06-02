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
    public class CloudflareLlaveGeneradaRepositorio : BaseRepository<TCloudflareLlaveGenerada, CloudflareLlaveGeneradaBO>
    {
        #region Metodos Base
        public CloudflareLlaveGeneradaRepositorio() : base()
        {
        }
        public CloudflareLlaveGeneradaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CloudflareLlaveGeneradaBO> GetBy(Expression<Func<TCloudflareLlaveGenerada, bool>> filter)
        {
            IEnumerable<TCloudflareLlaveGenerada> listado = base.GetBy(filter);
            List<CloudflareLlaveGeneradaBO> listadoBO = new List<CloudflareLlaveGeneradaBO>();
            foreach (var itemEntidad in listado)
            {
                CloudflareLlaveGeneradaBO objetoBO = Mapper.Map<TCloudflareLlaveGenerada, CloudflareLlaveGeneradaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CloudflareLlaveGeneradaBO FirstById(int id)
        {
            try
            {
                TCloudflareLlaveGenerada entidad = base.FirstById(id);
                CloudflareLlaveGeneradaBO objetoBO = new CloudflareLlaveGeneradaBO();
                Mapper.Map<TCloudflareLlaveGenerada, CloudflareLlaveGeneradaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CloudflareLlaveGeneradaBO FirstBy(Expression<Func<TCloudflareLlaveGenerada, bool>> filter)
        {
            try
            {
                TCloudflareLlaveGenerada entidad = base.FirstBy(filter);
                CloudflareLlaveGeneradaBO objetoBO = Mapper.Map<TCloudflareLlaveGenerada, CloudflareLlaveGeneradaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CloudflareLlaveGeneradaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCloudflareLlaveGenerada entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CloudflareLlaveGeneradaBO> listadoBO)
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

        public bool Update(CloudflareLlaveGeneradaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCloudflareLlaveGenerada entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CloudflareLlaveGeneradaBO> listadoBO)
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
        private void AsignacionId(TCloudflareLlaveGenerada entidad, CloudflareLlaveGeneradaBO objetoBO)
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

        private TCloudflareLlaveGenerada MapeoEntidad(CloudflareLlaveGeneradaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCloudflareLlaveGenerada entidad = new TCloudflareLlaveGenerada();
                entidad = Mapper.Map<CloudflareLlaveGeneradaBO, TCloudflareLlaveGenerada>(objetoBO,
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

        public List<listaCloudflareLlaveGeneradaBO> ObtenerListaCloudflareLlaveGenerada()
        {
            try
            {
                List<listaCloudflareLlaveGeneradaBO> capitulosFiltro = new List<listaCloudflareLlaveGeneradaBO>();
                var _queryfiltrocapitulo = "Select Id, IdCloudflareUsuarioLlave, JsonRespuesta, KeyId, KeyPem, KeyJwk, Created, Success, Valido FROM pla.T_CloudflareLlaveGenerada WHERE Estado=1";
                var SubfiltroCapitulo = _dapper.QuerySPDapper(_queryfiltrocapitulo, new { });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<listaCloudflareLlaveGeneradaBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public registroCloudflareLlaveGeneradaBO ObtenerRegistroCloudflareLlaveGenerada()
        {
            try
            {
                registroCloudflareLlaveGeneradaBO capitulosFiltro = new registroCloudflareLlaveGeneradaBO();
                var _queryfiltrocapitulo = "Select Id, IdCloudflareUsuarioLlave, KeyId, KeyPem, Created, Valido FROM pla.T_CloudflareLlaveGenerada WHERE Estado=1";
                var SubfiltroCapitulo = _dapper.FirstOrDefault(_queryfiltrocapitulo, new { });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<registroCloudflareLlaveGeneradaBO>(SubfiltroCapitulo);
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
