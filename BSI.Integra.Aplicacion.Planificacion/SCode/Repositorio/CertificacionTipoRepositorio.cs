
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class CertificacionTipoRepositorio : BaseRepository<TCertificacionTipo, CertificacionTipoBO>
    {
        #region Metodos Base
        public CertificacionTipoRepositorio() : base()
        {
        }
        public CertificacionTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificacionTipoBO> GetBy(Expression<Func<TCertificacionTipo, bool>> filter)
        {
            IEnumerable<TCertificacionTipo> listado = base.GetBy(filter);
            List<CertificacionTipoBO> listadoBO = new List<CertificacionTipoBO>();
            foreach (var itemEntidad in listado)
            {
                CertificacionTipoBO objetoBO = Mapper.Map<TCertificacionTipo, CertificacionTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificacionTipoBO FirstById(int id)
        {
            try
            {
                TCertificacionTipo entidad = base.FirstById(id);
                CertificacionTipoBO objetoBO = new CertificacionTipoBO();
                Mapper.Map<TCertificacionTipo, CertificacionTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificacionTipoBO FirstBy(Expression<Func<TCertificacionTipo, bool>> filter)
        {
            try
            {
                TCertificacionTipo entidad = base.FirstBy(filter);
                CertificacionTipoBO objetoBO = Mapper.Map<TCertificacionTipo, CertificacionTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificacionTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificacionTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificacionTipoBO> listadoBO)
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

        public bool Update(CertificacionTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificacionTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificacionTipoBO> listadoBO)
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
        private void AsignacionId(TCertificacionTipo entidad, CertificacionTipoBO objetoBO)
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

        private TCertificacionTipo MapeoEntidad(CertificacionTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificacionTipo entidad = new TCertificacionTipo();
                entidad = Mapper.Map<CertificacionTipoBO, TCertificacionTipo>(objetoBO,
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
        /// Obtiene una lista de CertificacionTipos  para ser usados en un combobox
        /// </summary>
        /// <returns></returns>
        public List<CertificacionTipoDTO> ObtenerTodoCertificacionTipoFiltro()
        {
            try
            {
                List<CertificacionTipoDTO> CertificacionTipoes = new List<CertificacionTipoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.T_CertificacionTipo WHERE Estado = 1";
                var CertificacionTipoesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(CertificacionTipoesDB) && !CertificacionTipoesDB.Contains("[]"))
                {
                    CertificacionTipoes = JsonConvert.DeserializeObject<List<CertificacionTipoDTO>>(CertificacionTipoesDB);
                }
                return CertificacionTipoes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
