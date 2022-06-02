using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class ReportePendienteHistoricoRepositorio : BaseRepository<TReportePendienteHistorico, ReportePendienteHistoricoBO>
    {
        #region Metodos Base
        public ReportePendienteHistoricoRepositorio() : base()
        {
        }
        public ReportePendienteHistoricoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ReportePendienteHistoricoBO> GetBy(Expression<Func<TReportePendienteHistorico, bool>> filter)
        {
            IEnumerable<TReportePendienteHistorico> listado = base.GetBy(filter);
            List<ReportePendienteHistoricoBO> listadoBO = new List<ReportePendienteHistoricoBO>();
            foreach (var itemEntidad in listado)
            {
                ReportePendienteHistoricoBO objetoBO = Mapper.Map<TReportePendienteHistorico, ReportePendienteHistoricoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ReportePendienteHistoricoBO FirstById(int id)
        {
            try
            {
                TReportePendienteHistorico entidad = base.FirstById(id);
                ReportePendienteHistoricoBO objetoBO = new ReportePendienteHistoricoBO();
                Mapper.Map<TReportePendienteHistorico, ReportePendienteHistoricoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ReportePendienteHistoricoBO FirstBy(Expression<Func<TReportePendienteHistorico, bool>> filter)
        {
            try
            {
                TReportePendienteHistorico entidad = base.FirstBy(filter);
                ReportePendienteHistoricoBO objetoBO = Mapper.Map<TReportePendienteHistorico, ReportePendienteHistoricoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ReportePendienteHistoricoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TReportePendienteHistorico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ReportePendienteHistoricoBO> listadoBO)
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

        public bool Update(ReportePendienteHistoricoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TReportePendienteHistorico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ReportePendienteHistoricoBO> listadoBO)
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
        private void AsignacionId(TReportePendienteHistorico entidad, ReportePendienteHistoricoBO objetoBO)
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

        private TReportePendienteHistorico MapeoEntidad(ReportePendienteHistoricoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TReportePendienteHistorico entidad = new TReportePendienteHistorico();
                entidad = Mapper.Map<ReportePendienteHistoricoBO, TReportePendienteHistorico>(objetoBO,
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
        ///  Obtiene el ultimo correlativo.
        /// </summary>
        /// <returns></returns>
        public int ObtenerCorrelativo()
        {
            try
            {                
                var correlativo = GetBy(x => x.Estado == true, x => new { x.Correlativo }).ToList();

                if (correlativo.Count() == 0 || correlativo == null)
                {
                    return 1;
                }
                else
                {
                    var correlativo2 = correlativo.Max(x => x.Correlativo);

                    if (correlativo2 > 0)
                    {
                        return correlativo2 + 1;
                    }
                    else
                    {
                        return 1;
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
