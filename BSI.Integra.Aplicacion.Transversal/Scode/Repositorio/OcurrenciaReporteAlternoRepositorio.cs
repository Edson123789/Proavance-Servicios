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
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class OcurrenciaReporteAlternoRepositorio : BaseRepository<TOcurrenciaReporteAlterno, OcurrenciaReporteAlternoBO>
    {
        private int idOcurrencia;

        #region Metodos Base
        public OcurrenciaReporteAlternoRepositorio() : base()
        {
        }
        public OcurrenciaReporteAlternoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OcurrenciaReporteAlternoBO> GetBy(Expression<Func<TOcurrenciaReporteAlterno, bool>> filter)
        {
            IEnumerable<TOcurrenciaReporteAlterno> listado = base.GetBy(filter);
            List<OcurrenciaReporteAlternoBO> listadoBO = new List<OcurrenciaReporteAlternoBO>();
            foreach (var itemEntidad in listado)
            {
                OcurrenciaReporteAlternoBO objetoBO = Mapper.Map<TOcurrenciaReporteAlterno, OcurrenciaReporteAlternoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OcurrenciaReporteAlternoBO FirstById(int id)
        {
            try
            {
                TOcurrenciaReporteAlterno entidad = base.FirstById(id);
                OcurrenciaReporteAlternoBO objetoBO = new OcurrenciaReporteAlternoBO();
                Mapper.Map<TOcurrenciaReporteAlterno, OcurrenciaReporteAlternoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OcurrenciaReporteAlternoBO FirstBy(Expression<Func<TOcurrenciaReporteAlterno, bool>> filter)
        {
            try
            {
                TOcurrenciaReporteAlterno entidad = base.FirstBy(filter);
                OcurrenciaReporteAlternoBO objetoBO = Mapper.Map<TOcurrenciaReporteAlterno, OcurrenciaReporteAlternoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OcurrenciaReporteAlternoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOcurrenciaReporteAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OcurrenciaReporteAlternoBO> listadoBO)
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

        public bool Update(OcurrenciaReporteAlternoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOcurrenciaReporteAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OcurrenciaReporteAlternoBO> listadoBO)
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
        private void AsignacionId(TOcurrenciaReporteAlterno entidad, OcurrenciaReporteAlternoBO objetoBO)
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

        private TOcurrenciaReporteAlterno MapeoEntidad(OcurrenciaReporteAlternoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOcurrenciaReporteAlterno entidad = new TOcurrenciaReporteAlterno();
                entidad = Mapper.Map<OcurrenciaReporteAlternoBO, TOcurrenciaReporteAlterno>(objetoBO,
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
    }
}
