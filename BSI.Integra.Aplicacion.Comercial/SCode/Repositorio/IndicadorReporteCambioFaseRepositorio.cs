using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class IndicadorReporteCambioFaseRepositorio : BaseRepository<TIndicadorReporteCambioFase, IndicadorReporteCambioFaseBO>
    {
        #region Metodos Base
        public IndicadorReporteCambioFaseRepositorio() : base()
        {
        }
        public IndicadorReporteCambioFaseRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<IndicadorReporteCambioFaseBO> GetBy(Expression<Func<TIndicadorReporteCambioFase, bool>> filter)
        {
            IEnumerable<TIndicadorReporteCambioFase> listado = base.GetBy(filter);
            List<IndicadorReporteCambioFaseBO> listadoBO = new List<IndicadorReporteCambioFaseBO>();
            foreach (var itemEntidad in listado)
            {
                IndicadorReporteCambioFaseBO objetoBO = Mapper.Map<TIndicadorReporteCambioFase, IndicadorReporteCambioFaseBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IndicadorReporteCambioFaseBO FirstById(int id)
        {
            try
            {
                TIndicadorReporteCambioFase entidad = base.FirstById(id);
                IndicadorReporteCambioFaseBO objetoBO = new IndicadorReporteCambioFaseBO();
                Mapper.Map<TIndicadorReporteCambioFase, IndicadorReporteCambioFaseBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IndicadorReporteCambioFaseBO FirstBy(Expression<Func<TIndicadorReporteCambioFase, bool>> filter)
        {
            try
            {
                TIndicadorReporteCambioFase entidad = base.FirstBy(filter);
                IndicadorReporteCambioFaseBO objetoBO = Mapper.Map<TIndicadorReporteCambioFase, IndicadorReporteCambioFaseBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IndicadorReporteCambioFaseBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TIndicadorReporteCambioFase entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<IndicadorReporteCambioFaseBO> listadoBO)
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

        public bool Update(IndicadorReporteCambioFaseBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TIndicadorReporteCambioFase entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<IndicadorReporteCambioFaseBO> listadoBO)
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
        private void AsignacionId(TIndicadorReporteCambioFase entidad, IndicadorReporteCambioFaseBO objetoBO)
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

        private TIndicadorReporteCambioFase MapeoEntidad(IndicadorReporteCambioFaseBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TIndicadorReporteCambioFase entidad = new TIndicadorReporteCambioFase();
                entidad = Mapper.Map<IndicadorReporteCambioFaseBO, TIndicadorReporteCambioFase>(objetoBO,
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
        /// Obtiene la lista de indicadores para el Reporte de Control Operativo
        /// </summary>
        /// <returns></returns>
        public List<ReporteControlOperativoIndicadorDTO> ObtenerIndicadores()
        {
            try
            {
                var lista = GetBy(x => x.Estado == true, y => new ReporteControlOperativoIndicadorDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Orden = y.Orden
                }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
