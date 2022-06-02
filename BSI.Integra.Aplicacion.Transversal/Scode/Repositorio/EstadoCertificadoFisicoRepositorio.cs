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
    public class EstadoCertificadoFisicoRepositorio : BaseRepository<TEstadoCertificadoFisico, EstadoCertificadoFisicoBO>
    {
        #region Metodos Base
        public EstadoCertificadoFisicoRepositorio() : base()
        {
        }
        public EstadoCertificadoFisicoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoCertificadoFisicoBO> GetBy(Expression<Func<TEstadoCertificadoFisico, bool>> filter)
        {
            IEnumerable<TEstadoCertificadoFisico> listado = base.GetBy(filter);
            List<EstadoCertificadoFisicoBO> listadoBO = new List<EstadoCertificadoFisicoBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoCertificadoFisicoBO objetoBO = Mapper.Map<TEstadoCertificadoFisico, EstadoCertificadoFisicoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoCertificadoFisicoBO FirstById(int id)
        {
            try
            {
                TEstadoCertificadoFisico entidad = base.FirstById(id);
                EstadoCertificadoFisicoBO objetoBO = new EstadoCertificadoFisicoBO();
                Mapper.Map<TEstadoCertificadoFisico, EstadoCertificadoFisicoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoCertificadoFisicoBO FirstBy(Expression<Func<TEstadoCertificadoFisico, bool>> filter)
        {
            try
            {
                TEstadoCertificadoFisico entidad = base.FirstBy(filter);
                EstadoCertificadoFisicoBO objetoBO = Mapper.Map<TEstadoCertificadoFisico, EstadoCertificadoFisicoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoCertificadoFisicoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoCertificadoFisico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoCertificadoFisicoBO> listadoBO)
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

        public bool Update(EstadoCertificadoFisicoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoCertificadoFisico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoCertificadoFisicoBO> listadoBO)
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
        private void AsignacionId(TEstadoCertificadoFisico entidad, EstadoCertificadoFisicoBO objetoBO)
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

        private TEstadoCertificadoFisico MapeoEntidad(EstadoCertificadoFisicoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoCertificadoFisico entidad = new TEstadoCertificadoFisico();
                entidad = Mapper.Map<EstadoCertificadoFisicoBO, TEstadoCertificadoFisico>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<EstadoCertificadoFisicoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEstadoCertificadoFisico, bool>>> filters, Expression<Func<TEstadoCertificadoFisico, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TEstadoCertificadoFisico> listado = base.GetFiltered(filters, orderBy, ascending);
            List<EstadoCertificadoFisicoBO> listadoBO = new List<EstadoCertificadoFisicoBO>();

            foreach (var itemEntidad in listado)
            {
                EstadoCertificadoFisicoBO objetoBO = Mapper.Map<TEstadoCertificadoFisico, EstadoCertificadoFisicoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        public List<EstadoCertificadoFisicoDTO> ObtenerEstadParaFiltro()
        {
            try
            {
                List<EstadoCertificadoFisicoDTO> rpta = new List<EstadoCertificadoFisicoDTO>();
                string _query = "Select Id,Nombre From ope.V_ObtenerEstadoCertificadoFisico Where Estado=1";
                string query = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoCertificadoFisicoDTO>>(query);
                }
                return rpta;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 
    }
}
