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
    public class OcurrenciaReporteRepositorio : BaseRepository<TOcurrenciaReporte, OcurrenciaReporteBO>
    {
        private int idOcurrencia;

        #region Metodos Base
        public OcurrenciaReporteRepositorio() : base()
        {
        }
        public OcurrenciaReporteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OcurrenciaReporteBO> GetBy(Expression<Func<TOcurrenciaReporte, bool>> filter)
        {
            IEnumerable<TOcurrenciaReporte> listado = base.GetBy(filter);
            List<OcurrenciaReporteBO> listadoBO = new List<OcurrenciaReporteBO>();
            foreach (var itemEntidad in listado)
            {
                OcurrenciaReporteBO objetoBO = Mapper.Map<TOcurrenciaReporte, OcurrenciaReporteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OcurrenciaReporteBO FirstById(int id)
        {
            try
            {
                TOcurrenciaReporte entidad = base.FirstById(id);
                OcurrenciaReporteBO objetoBO = new OcurrenciaReporteBO();
                Mapper.Map<TOcurrenciaReporte, OcurrenciaReporteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OcurrenciaReporteBO FirstBy(Expression<Func<TOcurrenciaReporte, bool>> filter)
        {
            try
            {
                TOcurrenciaReporte entidad = base.FirstBy(filter);
                OcurrenciaReporteBO objetoBO = Mapper.Map<TOcurrenciaReporte, OcurrenciaReporteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OcurrenciaReporteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOcurrenciaReporte entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OcurrenciaReporteBO> listadoBO)
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

        public bool Update(OcurrenciaReporteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOcurrenciaReporte entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OcurrenciaReporteBO> listadoBO)
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
        private void AsignacionId(TOcurrenciaReporte entidad, OcurrenciaReporteBO objetoBO)
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

        private TOcurrenciaReporte MapeoEntidad(OcurrenciaReporteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOcurrenciaReporte entidad = new TOcurrenciaReporte();
                entidad = Mapper.Map<OcurrenciaReporteBO, TOcurrenciaReporte>(objetoBO,
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
